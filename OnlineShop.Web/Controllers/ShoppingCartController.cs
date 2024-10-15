using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Data.Models.Enums.Payment;
using static OnlineShop.Common.EntityValidationConstants;
using OrderProduct = OnlineShop.Data.Models.OrderProduct;
using Payment = OnlineShop.Data.Models.Payment;

namespace OnlineShop.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext _context;

        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await _context.ShoppingCarts
                .Include(c => c.ShoppingCartProducts)
                .ThenInclude(cp => cp.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || !cart.ShoppingCartProducts.Any())
            {
                ViewBag.Message = "Your cart is empty.";
                return View(new ShoppingCart());
            }

            return View(cart);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Retrieve the user's shopping cart
            var shoppingCart = _context.ShoppingCarts
                .Include(sc => sc.ShoppingCartProducts)
                .FirstOrDefault(sc => sc.UserId == userId);

            if (shoppingCart == null)
            {
                // Create a new shopping cart for the user if none exists
                shoppingCart = new ShoppingCart
                {
                    UserId = userId,
                    Amount = 0 // Initial amount
                };
                _context.ShoppingCarts.Add(shoppingCart);
                _context.SaveChanges();
            }

            // Check if the product is already in the cart
            var cartProduct = shoppingCart.ShoppingCartProducts
                .FirstOrDefault(scp => scp.ProductId == productId);

            if (cartProduct != null)
            {
                // Update quantity if product already exists in cart
                cartProduct.Quantity += quantity;
            }
            else
            {
                // Add new product to the cart
                cartProduct = new ShoppingCartProduct
                {
                    ShoppingCartId = shoppingCart.Id,
                    ProductId = productId,
                    Quantity = quantity
                };
                _context.ShoppingCartsProducts.Add(cartProduct);
            }

            // Update the total amount in the cart
            var product = _context.Products.Find(productId);
            shoppingCart.Amount += product.Price * quantity;

            _context.SaveChanges();

            return RedirectToAction("Index"); // Redirect back to the product list or cart page
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int shoppingCartId, int productId, int quantity)
        {
            var shoppingCartProduct = _context.ShoppingCartsProducts
                .Include(scp => scp.Product) // Include product to access price
                .FirstOrDefault(scp => scp.ShoppingCartId == shoppingCartId && scp.ProductId == productId);

            if (shoppingCartProduct != null)
            {
                var shoppingCart = _context.ShoppingCarts
                    .Include(shoppingCart => shoppingCart.ShoppingCartProducts)
                    .FirstOrDefault(sc => sc.Id == shoppingCartId);

                if (shoppingCart != null)
                {
                    // Subtract the current product's total price from the cart amount
                    shoppingCart.Amount -= shoppingCartProduct.Product.Price * shoppingCartProduct.Quantity;

                    // Update the quantity of the product in the cart
                    shoppingCartProduct.Quantity = quantity;

                    // Add the updated product's total price back to the cart amount
                    shoppingCart.Amount += shoppingCartProduct.Product.Price * shoppingCartProduct.Quantity;

                    // Check if quantity is zero after updating
                    if (shoppingCartProduct.Quantity == 0)
                    {
                        // Remove product if quantity is zero
                        _context.ShoppingCartsProducts.Remove(shoppingCartProduct);
                    }

                    // Check if there are any products left in the cart
                    if (!shoppingCart.ShoppingCartProducts.Any())
                    {
                        shoppingCart.Amount = 0; // Set amount to zero if cart is empty
                    }

                    _context.SaveChanges();
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int shoppingCartId, int productId)
        {
            var shoppingCartProduct = _context.ShoppingCartsProducts
                .Include(scp => scp.Product) // Include product to access price
                .FirstOrDefault(scp => scp.ShoppingCartId == shoppingCartId && scp.ProductId == productId);

            if (shoppingCartProduct != null)
            {
                var shoppingCart = _context.ShoppingCarts
                    .Include(sc => sc.ShoppingCartProducts) // Include products to check emptiness
                    .FirstOrDefault(sc => sc.Id == shoppingCartId);

                if (shoppingCart != null)
                {
                    // Subtract the product's price * quantity from the cart's total amount
                    shoppingCart.Amount -= shoppingCartProduct.Product.Price * shoppingCartProduct.Quantity;

                    // Remove the product from the cart
                    _context.ShoppingCartsProducts.Remove(shoppingCartProduct);
                    _context.SaveChanges();

                    // Check if there are any products left in the cart
                    if (!shoppingCart.ShoppingCartProducts.Any())
                    {
                        _context.ShoppingCarts.Remove(shoppingCart); // Delete the shopping cart
                    }

                    _context.SaveChanges();
                }
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
