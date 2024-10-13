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
                }).ToList()
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

            // Check stock quantities
            foreach (var orderProduct in order.OrderProducts)
            {
                var product = await _context.Products.FindAsync(orderProduct.ProductId);
                if (product == null || product.StockQuantity < orderProduct.Quantity)
                {
                    TempData["ErrorMessage"] = "Insufficient stock for one or more products in the order.";
                    return RedirectToAction("Details", new { id });
                }
            }

            // Deduct stock quantities
            foreach (var orderProduct in order.OrderProducts)
            {
                var product = await _context.Products.FindAsync(orderProduct.ProductId);
                if (product != null)
                {
                    product.StockQuantity -= orderProduct.Quantity; // Deduct the quantity
                }
            }

            // Set IsCompleted to true
            order.IsCompleted = true;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Thank you for your order! Your order has been finalized. \u2705";

            return RedirectToAction("Details", new { id }); // Redirect to the order details page
        }

        public IActionResult ExportToPdf(int orderId)
        {
            var order = _context.Orders
                .Include(o => o.Payments)
                .FirstOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                return NotFound();
            }

            using (var stream = new MemoryStream())
            {
                var document = new Document();
                PdfWriter.GetInstance(document, stream);
                document.Open();

                // Add content to the PDF
                document.Add(new Paragraph($"Transaction History for Order Number: {order.Id}"));
                document.Add(new Paragraph($"Order Date: {order.OrderDate}"));
                document.Add(new Paragraph($"Total Amount: {order.TotalAmount:C}"));
                document.Add(new Paragraph("\n"));

                // Create a table for transactions
                var table = new PdfPTable(4);
                table.AddCell("Payment Method");
                table.AddCell("Amount");
                table.AddCell("Payment Date");
                table.AddCell("Status");

                foreach (var payment in order.Payments)
                {
                    table.AddCell(payment.PaymentMethod.ToString());
                    table.AddCell(payment.Amount.ToString("C"));
                    table.AddCell(payment.PaymentDate.ToString());
                    table.AddCell(payment.Status.ToString());
                }

                document.Add(table);
                document.Close();

                var fileName = $"TransactionHistory_{order.Id}.pdf";
                return File(stream.ToArray(), "application/pdf", fileName);
            }
        }
    }
}
