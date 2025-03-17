using OnlineShop.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;
using OnlineShop.Data.Models.Enums.Payment;
using OnlineShop.Data.Repository;
using OnlineShop.Services.Data.Interfaces;
using OnlineShop.Web.ViewModels.Order;
using OnlineShop.Web.ViewModels.Transaction;
using System.Drawing.Printing;
using System.Globalization;
using System.Security.Claims;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;
using OnlineShop.Data.Repository.Interfaces;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OnlineShop.Web.ViewModels.Payment;
using OnlineShop.Web.ViewModels.Product;

namespace OnlineShop.Services.Data
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order, int> _orderRepository;
        public OrderService(IRepository<Order, int> orderRepository)
        {
            _orderRepository = orderRepository;
        }


        public async Task<List<OrderIndexViewModel>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllAttached()
                .Include(o => o.OrderProducts)
                    .ThenInclude(o => o.Product)
                    .ThenInclude(s => s.ProductSizes)
                .Include(p => p.Payments)
                .Include(s => s.User)
                .ToListAsync();

            List<OrderIndexViewModel> orderIndexViewModels = new List<OrderIndexViewModel>();

            foreach (var order in orders)
            {
                orderIndexViewModels.Add(new OrderIndexViewModel
                {
                    OrderId = order.Id,
                    CustomOrderNumber = order.Id /* TODO: Make orders GUID */,
                    OrderDate = order.OrderDate,
                    TotalAmount = order.Payments.Sum(p => p.Amount),
                    IsCompleted = order.IsCompleted,
                    IsCancelled = order.IsCancelled,
                    Transactions = order.Payments.Select(p => new TransactionViewModel
                    {
                        Amount = p.Amount,
                        PaymentDate = p.PaymentDate,
                        Status = p.Status.ToString()
                    }),
                    UserName = order.User.UserName
                });
            }

            return orderIndexViewModels;
        }

        public async Task<IEnumerable<OrderIndexViewModel>> GetAllOrdersForUser(string userId)
        {
            var userOrders = await _orderRepository.GetAllAttached()
                .Where(o => o.UserId == userId && !o.IsCancelled)
                .Include(o => o.OrderProducts)
                    .ThenInclude(p => p.Product)
                    .ThenInclude(s => s.ProductSizes)
                .Include(o => o.Payments)
                .ToListAsync();

            List<OrderIndexViewModel> orderIndexViewModels = new List<OrderIndexViewModel>();

            foreach (var order in userOrders)
            {
                var totalAmount = order.OrderProducts.Sum(op =>
                {
                    var discount = op.Product.DiscountPercentage ?? 0;
                    var discountedPrice = op.UnitPrice * (1 - discount / 100);
                    return discountedPrice * op.Quantity;
                });

                orderIndexViewModels.Add(new OrderIndexViewModel
                {
                    OrderId = order.Id,
                    CustomOrderNumber = order.Id /* TODO: Make Order.Id GUID */,
                    OrderDate = order.OrderDate,
                    TotalAmount = totalAmount,
                    IsCompleted = order.IsCompleted,
                    IsCancelled = order.IsCancelled,
                    Transactions = order.Payments.Select(p => new TransactionViewModel
                    {
                        Amount = p.Amount,
                        PaymentDate = p.PaymentDate,
                        Status = p.Status.ToString()
                    }).ToList()
                });
            }

            return orderIndexViewModels;
        }

        public async Task<OrderDetailsViewModel?> GetOrderDetails(int orderId)
        {
            var order = await _orderRepository.GetAllAttached()
                .Include(o => o.Payments)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Size)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return null;
            }

            var userId = order.UserId;

            var userOrders = await _orderRepository.GetAllAttached()
                .Where(o => o.UserId == userId)
                .OrderBy(o => o.Id)
                .Include(o => o.OrderProducts)
                .ToListAsync();

            var viewModel = new OrderDetailsViewModel
            {
                OrderId = order.Id,
                CustomOrderNumber = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.OrderProducts.Sum(op => op.UnitPrice * op.Quantity),
                IsCompleted = order.IsCompleted,
                IsCancelled = order.IsCancelled,
                OrderProducts = order.OrderProducts.Select(op => new OrderProductViewModel
                {
                    ProductName = op.Product.Name,
                    ImgUrl = op.Product.ImageUrl,
                    Quantity = op.Quantity,
                    UnitPrice = op.UnitPrice,
                    SizeName = op.Size?.Name ?? "N/A"
                }).ToList()
            };

            return viewModel;
        }

        public async Task<OrderDetailsViewAdminModel> GetOrderAdminDetails(int id)
        {
            var order = await _orderRepository.GetAllAttached()
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .Include(o => o.Payments)
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return null;

            return new OrderDetailsViewAdminModel()
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                IsCompleted = order.IsCompleted,
                IsCancelled = order.IsCancelled,
                UserName = order.User.UserName,
                Products = order.OrderProducts.Select(op => new ProductDetailsAdminViewModel()
                {
                    Name = op.Product.Name,
                    ImageUrl = op.Product.ImageUrl,
                    Quantity = op.Quantity,
                    Price = op.UnitPrice
                }).ToList(),
                Payments = order.Payments.Select(p => new PaymentDetailsAdminViewModel()
                {
                    Amount = p.Amount,
                    PaymentDate = p.PaymentDate,
                    Status = p.Status.ToString()
                }).ToList()
            };
        }

        public async Task<bool> ReactivateOrder(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null)
            {
                return false;
            }

            if (order.IsCancelled)
            {
                order.IsCancelled = false;
                order.IsCompleted = false;

                await _orderRepository.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> CancelOrder(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null)
            {
                return false;
            }

            if (!order.IsCompleted && !order.IsCancelled)
            {
                order.IsCancelled = true;
                await _orderRepository.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> FinalizeOrder(int orderId)
        {
            var order = await _orderRepository.GetAllAttached()
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .ThenInclude(p => p.ProductSizes)
                .Include(o => o.Payments)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return false;
            }

            order.IsCompleted = true;
            foreach (var payment in order.Payments)
            {
                payment.Status = Status.Completed;
            }

            foreach (var orderProduct in order.OrderProducts)
            {
                var product = orderProduct.Product;

                var productSize = product.ProductSizes.FirstOrDefault(ps => ps.SizeId == orderProduct.SizeId);
                if (productSize != null)
                {
                    if (productSize.StockQuantity > 0)
                    {
                        productSize.StockQuantity -= 1;
                    }
                }

                product.SalesCount += 1;
            }

            await _orderRepository.UpdateAsync(order);
            await _orderRepository.SaveChangesAsync();
            return true;
        }

        public async Task<OrderTransactionHistoryViewModel> GetTransactionHistoryAsync(int orderId, string userId)
        {
            var order = await _orderRepository.GetAllAttached()
                .Where(o => o.UserId == userId && o.Id == orderId)
                .Include(o => o.Payments)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                return null;
            }

            var userOrders = await _orderRepository.GetAllAttached()
                .Where(o => o.UserId == userId)
                .OrderBy(o => o.Id)
                .ToListAsync();

            var viewModel = new OrderTransactionHistoryViewModel
            {
                OrderId = order.Id,
                CustomOrderNumber = order.Id /* MAKE Order.Id GUID */,
                Payments = order.Payments.ToList()
            };

            return viewModel;
        }

        [ExcludeFromCodeCoverage]
        public async Task<byte[]> GenerateOrderTransactionPdfAsync(int orderId)
        {
            var order = await _orderRepository.GetAllAttached()
                .Include(o => o.User)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                    .ThenInclude(p => p.User)
                .Include(o => o.Payments)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                return null;

            using (var stream = new MemoryStream())
            {
                var document = new Document(PageSize.A4, 40, 40, 50, 40);
                PdfWriter writer = PdfWriter.GetInstance(document, stream);
                document.Open();

                string logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "logo.png");
                if (File.Exists(logoPath))
                {
                    var logo = Image.GetInstance(logoPath);
                    logo.ScaleToFit(100f, 50f);
                    logo.Alignment = Element.ALIGN_LEFT;
                    document.Add(logo);
                }

                var titleFont = FontFactory.GetFont("Arial", 26, Font.BOLD, new BaseColor(50, 50, 50));
                var title = new Paragraph($"Transaction Details", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20
                };
                document.Add(title);

                var infoFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);
                var orderInfoTable = new PdfPTable(2)
                {
                    WidthPercentage = 100,
                    SpacingAfter = 15
                };
                orderInfoTable.SetWidths(new float[] { 1.5f, 2.5f });

                AddKeyValueCell(orderInfoTable, "Order Date:", order.OrderDate.ToString("dd MMMM yyyy"), infoFont);
                AddKeyValueCell(orderInfoTable, "Total Amount:", order.TotalAmount.ToString("C", new CultureInfo("en-US")), infoFont);
                AddKeyValueCell(orderInfoTable, "Order Status:", order.IsCompleted ? "Completed" : order.IsCancelled ? "Cancelled" : "Pending", infoFont);
                AddKeyValueCell(orderInfoTable, "Seller Email:", order.OrderProducts.FirstOrDefault()?.Product.User?.Email ?? "N/A", infoFont);
                AddKeyValueCell(orderInfoTable, "Customer Email:", order.User?.Email ?? "N/A", infoFont);

                document.Add(orderInfoTable);

                var productTable = new PdfPTable(4)
                {
                    WidthPercentage = 100,
                    SpacingBefore = 20,
                    SpacingAfter = 20
                };
                productTable.SetWidths(new float[] { 3f, 1.5f, 2f, 2f });
                AddTableHeader(productTable, new[] { "Product", "Quantity", "Unit Price", "Subtotal" });

                bool alternate = false;
                foreach (var orderProduct in order.OrderProducts)
                {
                    AddStyledCell(productTable, orderProduct.Product.Name, infoFont, alternate);
                    AddStyledCell(productTable, orderProduct.Quantity.ToString(), infoFont, alternate);
                    AddStyledCell(productTable, orderProduct.UnitPrice.ToString("C", new CultureInfo("en-US")), infoFont, alternate);
                    AddStyledCell(productTable, (orderProduct.Quantity * orderProduct.UnitPrice).ToString("C", new CultureInfo("en-US")), infoFont, alternate);
                    alternate = !alternate;
                }
                var totalCell = new PdfPCell(new Phrase("Total", FontFactory.GetFont("Arial", 12, Font.BOLD)))
                {
                    Colspan = 3,
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    BackgroundColor = new BaseColor(220, 220, 220),
                    Padding = 8
                };
                productTable.AddCell(totalCell);
                productTable.AddCell(new PdfPCell(new Phrase(order.TotalAmount.ToString("C", new CultureInfo("en-US")), FontFactory.GetFont("Arial", 12, Font.BOLD)))
                {
                    HorizontalAlignment = Element.ALIGN_RIGHT,
                    BackgroundColor = new BaseColor(220, 220, 220),
                    Padding = 8
                });
                document.Add(productTable);

                var paymentTable = new PdfPTable(3)
                {
                    WidthPercentage = 100,
                    SpacingBefore = 20
                };
                paymentTable.SetWidths(new float[] { 2f, 2f, 2f });
                AddTableHeader(paymentTable, new[] { "Amount", "Payment Date", "Status" });

                alternate = false;
                foreach (var payment in order.Payments)
                {
                    AddStyledCell(paymentTable, payment.Amount.ToString("C", new CultureInfo("en-US")), infoFont, alternate);
                    AddStyledCell(paymentTable, payment.PaymentDate.ToString("dd MMMM yyyy"), infoFont, alternate);
                    AddStyledCell(paymentTable, payment.Status.ToString(), infoFont, alternate);
                    alternate = !alternate;
                }
                document.Add(paymentTable);

                var signatureFont = FontFactory.GetFont("Arial", 12, Font.ITALIC, BaseColor.BLACK);
                var signatureParagraph = new Paragraph("Owner of Website / SEO Signature:", signatureFont)
                {
                    SpacingBefore = 20,
                    Alignment = Element.ALIGN_LEFT
                };
                document.Add(signatureParagraph);

                string signaturePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "signature.png");
                if (File.Exists(signaturePath))
                {
                    var signature = Image.GetInstance(signaturePath);
                    signature.ScaleToFit(150f, 50f);
                    document.Add(signature);
                }

                document.Close();
                return stream.ToArray();
            }
        }

        private void AddKeyValueCell(PdfPTable table, string key, string value, Font font)
        {
            table.AddCell(new PdfPCell(new Phrase(key, FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.DARK_GRAY)))
            {
                BackgroundColor = new BaseColor(240, 240, 240),
                Padding = 8,
                Border = PdfPCell.NO_BORDER
            });
            table.AddCell(new PdfPCell(new Phrase(value, font))
            {
                BackgroundColor = new BaseColor(250, 250, 250),
                Padding = 8,
                Border = PdfPCell.NO_BORDER
            });
        }

        private void AddStyledCell(PdfPTable table, string text, Font font, bool alternate = false)
        {
            var cell = new PdfPCell(new Phrase(text, font))
            {
                Padding = 8,
                BackgroundColor = alternate ? new BaseColor(245, 245, 245) : BaseColor.WHITE
            };
            table.AddCell(cell);
        }

        [ExcludeFromCodeCoverage]
        private void AddTableHeader(PdfPTable table, string[] headers)
        {
            var headerFont = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.WHITE);
            var headerBackground = new BaseColor(70, 130, 180);
            foreach (var header in headers)
            {
                var cell = new PdfPCell(new Phrase(header, headerFont))
                {
                    BackgroundColor = headerBackground,
                    Padding = 10,
                    HorizontalAlignment = Element.ALIGN_CENTER
                };
                table.AddCell(cell);
            }
        }



    }
}

