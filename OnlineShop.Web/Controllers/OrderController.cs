using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Web.ViewModels.Order;
using System.Security.Claims;

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
            var orders = await _context.Orders
                .Include(o => o.Payments)
                .Where(o => o.UserId == userId)
                .ToListAsync();

            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Get the list of products from the database
            var products = await _context.Products.ToListAsync();
            var viewModel = new CreateOrderViewModel
            {
                Products = new SelectList(products, "Id", "Name") // Assuming Product has Id and Name properties
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Create a new order
                var order = new Order
                {
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier), // Replace with the actual logged-in user ID
                    OrderDate = DateTime.Now,
                    TotalAmount = model.Quantity * (await _context.Products.FindAsync(model.ProductId)).Price // Assuming Price is a property of Product
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();

                // Add OrderProduct relationship here
                var orderProduct = new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = model.ProductId,
                    Quantity = model.Quantity,
                    UnitPrice = (await _context.Products.FindAsync(model.ProductId)).Price
                };

                _context.OrdersProducts.Add(orderProduct);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index"); // Redirect to the appropriate action after creating the order
            }

            var products = await _context.Products.ToListAsync();
            model.Products = new SelectList(products, "Id", "Name");

            return View(model);
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

            return View(order);
        }
    }
}
