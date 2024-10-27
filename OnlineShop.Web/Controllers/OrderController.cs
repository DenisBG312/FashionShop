using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Web.ViewModels.Order;
using System.Security.Claims;
using OnlineShop.Web.ViewModels.Transaction;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Microsoft.AspNetCore.Authorization;
using iTextSharp.text.pdf.draw;
using OnlineShop.Data.Models.Enums.Payment;
using OnlineShop.Services.Data.Interfaces;

namespace OnlineShop.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IOrderService _orderService;

        public OrderController(ApplicationDbContext context, IOrderService orderService)
        {
            _context = context;
            _orderService = orderService;
        }
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderService.GetAllOrders(userId);

            return View(orders.ToList());
        }


        public async Task<IActionResult> Details(int id)
        {
            var order = await _context.Orders
                .Include(o => o.Payments)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            var userId = order.UserId;

            var userOrders = await _context.Orders
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

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Reactivate(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            // Reactivate the order
            if (order.IsCancelled)
            {
                order.IsCancelled = false;
                order.IsCompleted = false;
                await _context.SaveChangesAsync();
            }

            TempData["SuccessMessage"] = "Order reactivated successfully.";
            return RedirectToAction("Details", new { id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            // Ensure the order is not completed before cancelling
            if (!order.IsCompleted && !order.IsCancelled)
            {
                order.IsCancelled = true; // Set the order as cancelled
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> FinalizeOrder(int id)
        {
            var order = await _context.Orders
                .Include(op => op.OrderProducts)
                .Include(o => o.Payments) // Include payments
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            // Check if there is enough stock for each product
            foreach (var orderProduct in order.OrderProducts)
            {
                var product = await _context.Products.FindAsync(orderProduct.ProductId);
                if (product == null || product.StockQuantity < orderProduct.Quantity)
                {
                    TempData["ErrorMessage"] = "Insufficient stock for one or more products in the order.";
                    return RedirectToAction("Details", new { id });
                }
            }

            // Reduce stock for each product in the order
            foreach (var orderProduct in order.OrderProducts)
            {
                var product = await _context.Products.FindAsync(orderProduct.ProductId);
                if (product != null)
                {
                    product.StockQuantity -= orderProduct.Quantity;
                }
            }

            // Mark order as completed
            order.IsCompleted = true;

            // Automatically update the status of all associated payments to "Completed"
            foreach (var payment in order.Payments)
            {
                payment.Status = Status.Completed;
            }

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Thank you for your order! Your order has been finalized. \u2705";

            return RedirectToAction("Details", new { id });
        }


        public async Task<IActionResult> TransactionHistory(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userOrders = await _context.Orders
                .Where(o => o.UserId == userId)
                .OrderBy(o => o.Id)
                .Include(o => o.Payments)
                .ToListAsync();

            var order = userOrders.FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            int customOrderNumber = userOrders.FindIndex(o => o.Id == order.Id) + 1;

            var viewModel = new OrderTransactionHistoryViewModel
            {
                OrderId = order.Id,
                CustomOrderNumber = customOrderNumber,
                Payments = order.Payments.ToList() // Directly assign the payments
            };

            return View(viewModel);
        }

        public IActionResult ExportToPdf(int orderId)
        {
            var order = _context.Orders
                .Include(o => o.Payments)
                .Include(o => o.User)
                .FirstOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                return NotFound();
            }

            using (var stream = new MemoryStream())
            {
                var document = new Document(PageSize.A4, 40, 40, 40, 40); // Adjusted margins for more spacing
                PdfWriter.GetInstance(document, stream);
                document.Open();

                // Header and Title Section
                var headerFont = FontFactory.GetFont("Arial", 24, Font.BOLD, BaseColor.DARK_GRAY);
                var header = new Paragraph($"Order #{order.Id} Transaction History", headerFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 15 // Spacing after header
                };
                document.Add(header);

                // Line Separator (thinner, subtle color)
                var separator = new LineSeparator(1f, 100f, new BaseColor(220, 220, 220), Element.ALIGN_CENTER, -2);
                document.Add(new Chunk(separator));

                // Order Information
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

                // Transaction Table Setup
                var table = new PdfPTable(4)
                {
                    WidthPercentage = 100,
                    SpacingBefore = 20,
                    SpacingAfter = 20,
                    HorizontalAlignment = Element.ALIGN_LEFT
                };
                table.SetWidths(new float[] { 3f, 2f, 2f, 2f }); // Adjusted column widths

                // Table Header Styling
                var headerCellFont = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.WHITE);
                var headerCellColor = new BaseColor(100, 149, 237); // Light blue

                table.AddCell(CreateStyledCell("Payment Method", headerCellFont, headerCellColor));
                table.AddCell(CreateStyledCell("Amount", headerCellFont, headerCellColor));
                table.AddCell(CreateStyledCell("Payment Date", headerCellFont, headerCellColor));
                table.AddCell(CreateStyledCell("Status", headerCellFont, headerCellColor));

                // Populate Table Rows with Styling
                var rowCellFont = FontFactory.GetFont("Arial", 11, Font.NORMAL, BaseColor.BLACK);
                foreach (var payment in order.Payments)
                {
                    table.AddCell(CreateStyledCell(SplitCamelCase(payment.PaymentMethod.ToString()), rowCellFont, BaseColor.WHITE));
                    table.AddCell(CreateStyledCell(payment.Amount.ToString("C"), rowCellFont, BaseColor.WHITE));
                    table.AddCell(CreateStyledCell(payment.PaymentDate.ToString("dd MMMM yyyy"), rowCellFont, BaseColor.WHITE));
                    table.AddCell(CreateStyledCell(SplitCamelCase(payment.Status.ToString()), rowCellFont, BaseColor.WHITE));
                }

                document.Add(table);

                // Add a horizontal line for visual separation
                document.Add(new Chunk(new LineSeparator(1f, 100f, new BaseColor(200, 200, 200), Element.ALIGN_CENTER, -1)));

                // Add signature and logo (centered)
                AddSignatureAndLogo(document);

                // Close document and return the PDF file
                document.Close();
                var fileName = $"TransactionHistory_{order.Id}.pdf";
                return File(stream.ToArray(), "application/pdf", fileName);
            }
        }

        // Method to Create Styled Cells
        private PdfPCell CreateStyledCell(string text, Font font, BaseColor backgroundColor)
        {
            var cell = new PdfPCell(new Phrase(text, font))
            {
                BackgroundColor = backgroundColor,
                Padding = 10,
                HorizontalAlignment = Element.ALIGN_CENTER,
                BorderColor = new BaseColor(220, 220, 220), // Light gray borders
                BorderWidth = 0.5f
            };
            return cell;
        }

        // Helper method for signature and logo
        private void AddSignatureAndLogo(Document document)
        {
            // Add signature line
            var signatureFont = FontFactory.GetFont("Arial", 14, Font.BOLD, BaseColor.BLACK);
            var signatureParagraph = new Paragraph("Owner of Website / SEO Signature:", signatureFont)
            {
                SpacingBefore = 20,
                Alignment = Element.ALIGN_LEFT
            };
            document.Add(signatureParagraph);

            // Signature image
            string signaturePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "signature.png");
            if (System.IO.File.Exists(signaturePath))
            {
                var signature = iTextSharp.text.Image.GetInstance(signaturePath);
                signature.ScaleToFit(150f, 75f); // Resized for a sleeker look
                signature.Alignment = Element.ALIGN_LEFT;
                document.Add(signature);
            }

            // Logo (centered)
            string logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "your_logo.png");
            if (System.IO.File.Exists(logoPath))
            {
                var logo = iTextSharp.text.Image.GetInstance(logoPath);
                logo.ScaleToFit(100f, 50f);
                logo.Alignment = Element.ALIGN_CENTER;
                document.Add(logo);
            }
        }

        private string SplitCamelCase(string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, "(\\B[A-Z])", " $1");
        }


    }
}
