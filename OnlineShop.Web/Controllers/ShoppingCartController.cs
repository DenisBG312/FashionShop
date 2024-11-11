using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Data.Models.Enums.Payment;
using OnlineShop.Services.Data.Interfaces;
using static OnlineShop.Common.EntityValidationConstants;
using OrderProduct = OnlineShop.Data.Models.OrderProduct;
using Payment = OnlineShop.Data.Models.Payment;

namespace OnlineShop.Web.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(ApplicationDbContext context, IShoppingCartService shoppingCartService)
        {
            _context = context;
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var cart = await _shoppingCartService.GetCartAsync(userId);

            if (cart == null || !cart.ShoppingCartProducts.Any())
            {
                ViewBag.Message = "Your cart is empty.";
                return View(new ShoppingCart());
            }

            return View(cart);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _shoppingCartService.AddToCartAsync(userId, productId, quantity);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.ErrorMessage);
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity(int shoppingCartId, int productId, int quantity)
        {
            var result = await _shoppingCartService.UpdateQuantityAsync(shoppingCartId, productId, quantity);

            if (!result)
            {
                TempData["ErrorMessage"] = "Unable to update quantity. Please try again.";
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Quantity updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int shoppingCartId, int productId)
        {
            var result = await _shoppingCartService.RemoveFromCartAsync(shoppingCartId, productId);

            if (!result)
            {
                TempData["ErrorMessage"] = "Failed to remove the item from the cart. The item might not exist in the cart.";
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult PlaceOrder(int shoppingCartId, PaymentMethod paymentMethod)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // Retrieve the shopping cart
            var shoppingCart = _context.ShoppingCarts
                .Include(sc => sc.ShoppingCartProducts)
                    .ThenInclude(scp => scp.Product)
                .FirstOrDefault(sc => sc.Id == shoppingCartId);

            if (shoppingCart != null && shoppingCart.ShoppingCartProducts.Any())
            {
                var insufficientStockMessages = new List<string>(); // List to collect error messages

                // Check stock quantities before proceeding
                foreach (var cartProduct in shoppingCart.ShoppingCartProducts)
                {
                    var product = _context.Products.Find(cartProduct.ProductId);
                    if (product == null || product.StockQuantity < cartProduct.Quantity)
                    {
                        // Collect error messages for each product with insufficient stock
                        insufficientStockMessages.Add($"Insufficient stock for {cartProduct.Product.Name}. Available: {product?.StockQuantity ?? 0}.");
                    }
                }

                // If there are any stock issues, return the messages to the view
                if (insufficientStockMessages.Any())
                {
                    TempData["ErrorMessages"] = string.Join("<br />", insufficientStockMessages); // Join messages with line breaks
                    return RedirectToAction("Index", "ShoppingCart", new { shoppingCartId }); // Keep user on the shopping cart page
                }

                // Calculate total amount from the shopping cart
                decimal totalAmount = shoppingCart.ShoppingCartProducts.Sum(scp => scp.Quantity * scp.Product.Price);

                // Create a new order
                var order = new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.Now,
                    TotalAmount = totalAmount // Set the total amount
                };

                // Add order to the database
                _context.Orders.Add(order);
                _context.SaveChanges(); // Save to generate order ID

                // Create a new payment record
                var payment = new Payment
                {
                    PaymentMethod = paymentMethod, // Use the enum directly
                    Amount = totalAmount,
                    PaymentDate = DateTime.Now,
                    Status = Status.Pending, // Set initial status as Pending
                    OrderId = order.Id // Associate payment with order
                };

                // Add payment to the database
                _context.Payments.Add(payment);
                _context.SaveChanges(); // Save to link payment with order

                // Create order products from the shopping cart
                foreach (var cartProduct in shoppingCart.ShoppingCartProducts)
                {
                    var orderProduct = new OrderProduct
                    {
                        OrderId = order.Id,
                        ProductId = cartProduct.ProductId,
                        Quantity = cartProduct.Quantity,
                        UnitPrice = cartProduct.Product.Price // Store the price at the time of order
                    };

                    _context.OrdersProducts.Add(orderProduct);
                }

                _context.SaveChanges(); // Save order products and stock changes

                // Clear the shopping cart
                _context.ShoppingCartsProducts.RemoveRange(shoppingCart.ShoppingCartProducts);
                _context.ShoppingCarts.Remove(shoppingCart);

                _context.SaveChanges(); // Save to clear the cart

                // Redirect to the orders index or details view
                return RedirectToAction("Index", "Order");
            }

            // Handle cases where the shopping cart is empty or doesn't exist
            TempData["ErrorMessage"] = "Your shopping cart is empty.";
            return RedirectToAction("Index", "ShoppingCart", new { shoppingCartId });
        }

    }
}
