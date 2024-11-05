using OnlineShop.Data;
using System;
using System.Collections.Generic;
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
using System.Xml.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using OnlineShop.Data.Repository.Interfaces;
using Document = System.Reflection.Metadata.Document;

namespace OnlineShop.Services.Data
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order, int> _orderRepository;
        public OrderService(BaseRepository<Order, int> orderRepository)
        {
            _orderRepository = orderRepository;
        }


        public async Task<IEnumerable<OrderIndexViewModel>> GetAllOrders(string userId)
        {
            var userOrders = await _orderRepository.GetAllAttached()
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderProducts)
                .Include(o => o.Payments)
                .ToListAsync();

            return userOrders.Select((order, index) => new OrderIndexViewModel()
            {
                OrderId = order.Id,
                CustomOrderNumber = index + 1,
                OrderDate = order.OrderDate,
                TotalAmount = order.OrderProducts.Sum(op => op.UnitPrice * op.Quantity),
                IsCompleted = order.IsCompleted,
                IsCancelled = order.IsCancelled,
                Transactions = order.Payments.Select(p => new TransactionViewModel()
                {
                    PaymentMethod = p.PaymentMethod.ToString(),
                    Amount = p.Amount,
                    PaymentDate = p.PaymentDate,
                    Status = p.Status.ToString()
                })
            }).ToList();
        }

        public async Task<OrderDetailsViewModel?> GetOrderDetails(int orderId)
        {
            var order = await _orderRepository.GetAllAttached()
                .Include(o => o.Payments)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
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

            int userOrderNumber = userOrders.FindIndex(o => o.Id == order.Id) + 1;

            var viewModel = new OrderDetailsViewModel
            {
                OrderId = order.Id,
                CustomOrderNumber = userOrderNumber,
                OrderDate = order.OrderDate,
                TotalAmount = order.OrderProducts.Sum(op => op.UnitPrice * op.Quantity),
                IsCompleted = order.IsCompleted,
                IsCancelled = order.IsCancelled,
                OrderProducts = order.OrderProducts.Select(op => new OrderProductViewModel
                {
                    ProductName = op.Product.Name,
                    Quantity = op.Quantity,
                    UnitPrice = op.UnitPrice
                }).ToList()
            };

            return viewModel;
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
                .Include(o => o.Payments)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return false;
            }

            foreach (var orderProduct in order.OrderProducts)
            {
                if (orderProduct.Product == null || orderProduct.Product.StockQuantity < orderProduct.Quantity)
                {
                    return false;
                }
            }

            foreach (var orderProduct in order.OrderProducts)
            {
                orderProduct.Product.StockQuantity -= orderProduct.Quantity;
            }

            order.IsCompleted = true;
            foreach (var payment in order.Payments)
            {
                payment.Status = Status.Completed;
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

            int customOrderNumber = userOrders.FindIndex(o => o.Id == order.Id) + 1;

            var viewModel = new OrderTransactionHistoryViewModel
            {
                OrderId = order.Id,
                CustomOrderNumber = customOrderNumber,
                Payments = order.Payments.ToList()
            };

            return viewModel;
        }

        public async Task<byte[]> GenerateOrderTransactionPdfAsync(int orderId)
        {
            var order = await _orderRepository.GetAllAttached()
                .Include(o => o.User)
                .Include(o => o.OrderProducts)
                    .ThenInclude(op => op.Product)
                .Include(o => o.Payments)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return null;
            }

            using (var stream = new MemoryStream())
            {
                var document = new iTextSharp.text.Document(PageSize.A4, 40, 40, 40, 40);
                PdfWriter writer = PdfWriter.GetInstance(document, stream);
                document.Open();

                // Header Section
                var titleFont = FontFactory.GetFont("Arial", 24, Font.BOLD, BaseColor.BLACK);
                var title = new Paragraph($"Order #{order.Id} - Transaction Details", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20
                };
                document.Add(title);

                // Order Information
                var infoFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.DARK_GRAY);
                var orderInfo = new Paragraph($@"
Order Date: {order.OrderDate:dd MMMM yyyy}
Total Amount: {order.TotalAmount:C}
Order Status: {(order.IsCompleted ? "Completed" : order.IsCancelled ? "Cancelled" : "Pending")}
", infoFont)
                {
                    SpacingAfter = 15
                };
                document.Add(orderInfo);

                // User Details Section
                var userDetails = new Paragraph($@"
Customer Name: {order.User.UserName}
Customer Email: {order.User.Email}
", infoFont);
                document.Add(userDetails);

                // Product Table Section
                var productTable = new PdfPTable(4) { WidthPercentage = 100, SpacingBefore = 20, SpacingAfter = 20 };
                productTable.SetWidths(new float[] { 3f, 2f, 2f, 2f });

                AddTableHeader(productTable, new[] { "Product", "Quantity", "Unit Price", "Subtotal" });

                foreach (var orderProduct in order.OrderProducts)
                {
                    productTable.AddCell(new PdfPCell(new Phrase(orderProduct.Product.Name, infoFont)));
                    productTable.AddCell(new PdfPCell(new Phrase(orderProduct.Quantity.ToString(), infoFont)));
                    productTable.AddCell(new PdfPCell(new Phrase(orderProduct.UnitPrice.ToString("C"), infoFont)));
                    productTable.AddCell(new PdfPCell(new Phrase((orderProduct.Quantity * orderProduct.UnitPrice).ToString("C"), infoFont)));
                }

                // Total Row
                var totalCell = new PdfPCell(new Phrase("Total", FontFactory.GetFont("Arial", 12, Font.BOLD))) { Colspan = 3, HorizontalAlignment = Element.ALIGN_RIGHT };
                productTable.AddCell(totalCell);
                productTable.AddCell(new PdfPCell(new Phrase(order.TotalAmount.ToString("C"), FontFactory.GetFont("Arial", 12, Font.BOLD))) { HorizontalAlignment = Element.ALIGN_RIGHT });
                document.Add(productTable);

                // Payment Information Table
                var paymentTable = new PdfPTable(4) { WidthPercentage = 100, SpacingBefore = 20 };
                paymentTable.SetWidths(new float[] { 3f, 2f, 2f, 2f });

                AddTableHeader(paymentTable, new[] { "Payment Method", "Amount", "Payment Date", "Status" });

                foreach (var payment in order.Payments)
                {
                    paymentTable.AddCell(new PdfPCell(new Phrase(payment.PaymentMethod.ToString(), infoFont)));
                    paymentTable.AddCell(new PdfPCell(new Phrase(payment.Amount.ToString("C"), infoFont)));
                    paymentTable.AddCell(new PdfPCell(new Phrase(payment.PaymentDate.ToString("dd MMMM yyyy"), infoFont)));
                    paymentTable.AddCell(new PdfPCell(new Phrase(payment.Status.ToString(), infoFont)));
                }
                document.Add(paymentTable);

                // Footer Section
                AddSignatureAndLogo(document);
                document.Close();

                return stream.ToArray();
            }
        }
        private void AddTableHeader(PdfPTable table, string[] headers)
        {
            var headerFont = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.WHITE);
            var headerBackground = new BaseColor(100, 149, 237);

            foreach (var header in headers)
            {
                var cell = new PdfPCell(new Phrase(header, headerFont))
                {
                    BackgroundColor = headerBackground,
                    Padding = 8,
                    HorizontalAlignment = Element.ALIGN_CENTER
                };
                table.AddCell(cell);
            }
        }

        private void AddSignatureAndLogo(iTextSharp.text.Document document)
        {
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

            string logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "your_logo.png");
            if (File.Exists(logoPath))
            {
                var logo = Image.GetInstance(logoPath);
                logo.ScaleToFit(100f, 50f);
                logo.Alignment = Element.ALIGN_RIGHT;
                document.Add(logo);
            }
        }
    }
}
