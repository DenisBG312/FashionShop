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

namespace OnlineShop.Web.Controllers
{
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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Get the list of products from the database
            var products = await _context.Products.ToListAsync();

            var productSelectList = products.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            }).ToList();

            var viewModel = new CreateOrderViewModel
            {
                Products = productSelectList
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderViewModel model)
        {
            ModelState.Remove("Quantity");

            if (!ModelState.IsValid)
            {
                // Return the view with the model if invalid
                return View(model);
            }

            var order = new Order
            {
                OrderDate = DateTime.Now,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                OrderProducts = new List<OrderProduct>()
            };

            // Variable to calculate total amount
            decimal totalAmount = 0;

            foreach (var orderProduct in model.OrderProducts)
            {
                if (orderProduct.Quantity > 0)
                {
                    // Retrieve the product from the database
                    var product = await _context.Products.FindAsync(orderProduct.ProductId);
                    if (product == null || product.StockQuantity < orderProduct.Quantity)
                    {
                        // Handle insufficient stock case
                        ModelState.AddModelError("", $"Not enough stock for {product?.Name ?? "product"}.");
                        return View(model);
                    }

                    // Calculate total price for the current product
                    decimal productTotalPrice = product.Price * orderProduct.Quantity;

                    // Add to the total amount
                    totalAmount += productTotalPrice;

                    // Add the product to the order
                    order.OrderProducts.Add(new OrderProduct
                    {
                        ProductId = orderProduct.ProductId,
                        Quantity = orderProduct.Quantity,
                        UnitPrice = product.Price // Set unit price for each order product
                    });

                    // Decrease the product stock quantity
                    product.StockQuantity -= orderProduct.Quantity;
                }
            }

            // Set the total amount for the order
            order.TotalAmount = totalAmount;

            // Add the order to the context
            _context.Orders.Add(order);
            await _context.SaveChangesAsync(); // Save changes to update the database

            return RedirectToAction("Index"); // Redirect to orders index or another view
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
                .Include(order => order.OrderProducts)
                .ToListAsync();

            int userOrderNumber = userOrders.FindIndex(o => o.Id == order.Id) + 1;

            var viewModel = new OrderDetailsViewModel
            {
                OrderId = order.Id,
                CustomOrderNumber = userOrderNumber,
                OrderDate = order.OrderDate,
                TotalAmount = order.OrderProducts.Sum(op => op.UnitPrice * op.Quantity),
                IsCompleted = order.IsCompleted,
                OrderProducts = order.OrderProducts
            };

            return View(viewModel);
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
