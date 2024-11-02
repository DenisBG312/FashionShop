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
                .Include(o => o.Payments)
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return null;
            }

            using (var stream = new MemoryStream())
            {
                var document = new iTextSharp.text.Document(PageSize.A4, 40, 40, 40, 40);
                PdfWriter.GetInstance(document, stream);
                document.Open();

                var headerFont = FontFactory.GetFont("Arial", 24, Font.BOLD, BaseColor.DARK_GRAY);
                var header = new Paragraph($"Order #{order.Id} Transaction History", headerFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 15
                };
                document.Add(header);

                var separator = new LineSeparator(1f, 100f, new BaseColor(220, 220, 220), Element.ALIGN_CENTER, -2);
                document.Add(new Chunk(separator));

                var infoFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.DARK_GRAY);
                var infoParagraph = new Paragraph($@"
    Buyer: {order.User.UserName}
    Order Date: {order.OrderDate:dd MMMM yyyy}
    Total Amount: {order.TotalAmount:C}
", infoFont)
                {
                    SpacingAfter = 15
                };
                document.Add(infoParagraph);

                var table = new PdfPTable(4)
                {
                    WidthPercentage = 100,
                    SpacingBefore = 20,
                    SpacingAfter = 20,
                    HorizontalAlignment = Element.ALIGN_LEFT
                };
                table.SetWidths(new float[] { 3f, 2f, 2f, 2f });

                var headerCellFont = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.WHITE);
                var headerCellColor = new BaseColor(100, 149, 237);

                table.AddCell(CreateStyledCell("Payment Method", headerCellFont, headerCellColor));
                table.AddCell(CreateStyledCell("Amount", headerCellFont, headerCellColor));
                table.AddCell(CreateStyledCell("Payment Date", headerCellFont, headerCellColor));
                table.AddCell(CreateStyledCell("Status", headerCellFont, headerCellColor));

                var rowCellFont = FontFactory.GetFont("Arial", 11, Font.NORMAL, BaseColor.BLACK);
                foreach (var payment in order.Payments)
                {
                    table.AddCell(CreateStyledCell(SplitCamelCase(payment.PaymentMethod.ToString()), rowCellFont,
                        BaseColor.WHITE));
                    table.AddCell(CreateStyledCell(payment.Amount.ToString("C"), rowCellFont, BaseColor.WHITE));
                    table.AddCell(CreateStyledCell(payment.PaymentDate.ToString("dd MMMM yyyy"), rowCellFont,
                        BaseColor.WHITE));
                    table.AddCell(CreateStyledCell(SplitCamelCase(payment.Status.ToString()), rowCellFont,
                        BaseColor.WHITE));
                }

                document.Add(table);

                AddSignatureAndLogo(document);

                document.Close();
                return stream.ToArray();
            }
        }

        private void AddSignatureAndLogo(iTextSharp.text.Document document)
        {
            var signatureFont = FontFactory.GetFont("Arial", 14, Font.BOLD, BaseColor.BLACK);
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
                signature.ScaleToFit(150f, 75f);
                signature.Alignment = Element.ALIGN_LEFT;
                document.Add(signature);
            }

            string logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "your_logo.png");
            if (File.Exists(logoPath))
            {
                var logo = Image.GetInstance(logoPath);
                logo.ScaleToFit(100f, 50f);
                logo.Alignment = Element.ALIGN_CENTER;
                document.Add(logo);
            }
        }

        private PdfPCell CreateStyledCell(string text, Font font, BaseColor backgroundColor)
        {
            var cell = new PdfPCell(new Phrase(text, font))
            {
                BackgroundColor = backgroundColor,
                Padding = 10,
                HorizontalAlignment = Element.ALIGN_CENTER,
                BorderColor = new BaseColor(220, 220, 220),
                BorderWidth = 0.5f
            };
            return cell;
        }

        private string SplitCamelCase(string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, "(\\B[A-Z])", " $1");
        }
    }
}
