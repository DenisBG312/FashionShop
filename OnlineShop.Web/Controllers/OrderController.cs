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

namespace OnlineShop.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {

        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userOrders = await _context.Orders
                .Where(o => o.UserId == userId)
                .OrderBy(o => o.Id)
                .Include(o => o.OrderProducts)
                .Include(o => o.Payments)
                .ToListAsync();

            var orders = userOrders.Select((order, index) => new OrderIndexViewModel
            {
                OrderId = order.Id,
                CustomOrderNumber = index + 1, // Custom order number logic based on the index
                OrderDate = order.OrderDate,
                TotalAmount = order.OrderProducts.Sum(op => op.UnitPrice * op.Quantity), // Calculate total amount
                IsCompleted = order.IsCompleted,
                IsCancelled = order.IsCancelled,
                Transactions = order.Payments.Select(p => new TransactionViewModel
                {
                    PaymentMethod = p.PaymentMethod.ToString(),
                    Amount = p.Amount,
                    PaymentDate = p.PaymentDate,
                    Status = p.Status.ToString()
                })// Ensure this property exists in your Order model
            }).ToList();

            return View(orders);
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
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            foreach (var orderProduct in order.OrderProducts)
            {
                var product = await _context.Products.FindAsync(orderProduct.ProductId);
                if (product == null || product.StockQuantity < orderProduct.Quantity)
                {
                    TempData["ErrorMessage"] = "Insufficient stock for one or more products in the order.";
                    return RedirectToAction("Details", new { id });
                }
            }

            foreach (var orderProduct in order.OrderProducts)
            {
                var product = await _context.Products.FindAsync(orderProduct.ProductId);
                if (product != null)
                {
                    product.StockQuantity -= orderProduct.Quantity;
                }
            }


            // Set IsCompleted to true or add your logic here
            order.IsCompleted = true;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Thank you for your order! Your order has been finalized. \u2705";

            return RedirectToAction("Details", new { id }); // Redirect to the order details page
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
                .Include(o => o.User) // Make sure to include the User navigation property
                .FirstOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                return NotFound();
            }

            using (var stream = new MemoryStream())
            {
                var document = new Document(PageSize.A4, 50, 50, 25, 25);
                PdfWriter.GetInstance(document, stream);
                document.Open();

                // Add title with styling
                var titleFont = FontFactory.GetFont("Arial", 22, Font.BOLD, BaseColor.BLUE);
                var title = new Paragraph($"Transaction History for Order Number: {order.Id}", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20 // Space after title
                };
                document.Add(title);

                // Add order details
                var detailsFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);
                document.Add(new Paragraph($"Buyer: {order.User.UserName}", detailsFont));
                document.Add(new Paragraph($"Order Date: {order.OrderDate:dd MMMM yyyy}", detailsFont));
                document.Add(new Paragraph($"Total Amount: {order.TotalAmount:C}", detailsFont));
                document.Add(new Paragraph("\n", detailsFont)); // Space

                // Add divider
                document.Add(new Chunk(new LineSeparator(1f, 100f, BaseColor.BLACK, Element.ALIGN_CENTER, 1))); // Horizontal line

                // Create a table for transactions
                var table = new PdfPTable(4)
                {
                    WidthPercentage = 100,
                    SpacingBefore = 20,
                    SpacingAfter = 20,
                    HorizontalAlignment = Element.ALIGN_LEFT
                };

                // Set table headers with style
                var headerFont = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.WHITE);
                var cellFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);

                table.AddCell(CreateCell("Payment Method", headerFont, BaseColor.DARK_GRAY));
                table.AddCell(CreateCell("Amount", headerFont, BaseColor.DARK_GRAY));
                table.AddCell(CreateCell("Payment Date", headerFont, BaseColor.DARK_GRAY));
                table.AddCell(CreateCell("Status", headerFont, BaseColor.DARK_GRAY));

                // Populate table rows
                foreach (var payment in order.Payments)
                {
                    table.AddCell(CreateCell(payment.PaymentMethod.ToString(), cellFont, BaseColor.WHITE));
                    table.AddCell(CreateCell(payment.Amount.ToString("C"), cellFont, BaseColor.WHITE));
                    table.AddCell(CreateCell(payment.PaymentDate.ToString("dd MMMM yyyy"), cellFont, BaseColor.WHITE));
                    table.AddCell(CreateCell(payment.Status.ToString(), cellFont, BaseColor.WHITE));
                }

                document.Add(table);

                // Add divider
                document.Add(new Chunk(new LineSeparator(1f, 100f, BaseColor.BLACK, Element.ALIGN_CENTER, 1))); // Horizontal line

                // Add logo at the bottom center
                string logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "your_logo.png"); // Update with your logo path
                if (System.IO.File.Exists(logoPath))
                {
                    var logo = iTextSharp.text.Image.GetInstance(logoPath);
                    logo.ScaleToFit(140f, 120f);
                    logo.Alignment = Element.ALIGN_CENTER;
                    document.Add(new Paragraph("\n")); // Space before the logo
                    document.Add(logo);
                }

                // Add signature row
                var signatureFont = FontFactory.GetFont("Arial", 14, Font.BOLD, BaseColor.BLACK);
                var signatureRow = new Paragraph("Owner of Website / SEO Signature:", signatureFont)
                {
                    Alignment = Element.ALIGN_LEFT,
                    SpacingAfter = 5 // Space after signature row
                };
                document.Add(signatureRow);

                // Add signature image
                string signaturePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "signature.png"); // Update with your signature path
                if (System.IO.File.Exists(signaturePath))
                {
                    var signature = iTextSharp.text.Image.GetInstance(signaturePath);
                    signature.ScaleToFit(200f, 100f);
                    signature.Alignment = Element.ALIGN_LEFT; // Align the signature to the left
                    document.Add(new Paragraph("\n")); // Add space before the signature
                    document.Add(signature);
                }

                // Close document
                document.Close();

                var fileName = $"TransactionHistory_{order.Id}.pdf";
                return File(stream.ToArray(), "application/pdf", fileName);
            }
        }

        // Helper method to create styled cells with borders
        private PdfPCell CreateCell(string text, Font font, BaseColor backgroundColor)
        {
            var cell = new PdfPCell(new Phrase(text, font))
            {
                BackgroundColor = backgroundColor,
                Padding = 10,
                HorizontalAlignment = Element.ALIGN_CENTER,
                Border = PdfPCell.BOX, // Add borders to cells
                BorderColor = BaseColor.GRAY // Color of borders
            };
            return cell;
        }


    }
}
