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
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
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
        public async Task<IActionResult> PlaceOrder(int shoppingCartId, PaymentMethod paymentMethod)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _shoppingCartService.PlaceOrderAsync(shoppingCartId, userId, paymentMethod);

            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = "Your shopping cart is empty.";
                return RedirectToAction("Index", "ShoppingCart", new { shoppingCartId });
            }

            return RedirectToAction("Index", "Order");
        }

    }
}
