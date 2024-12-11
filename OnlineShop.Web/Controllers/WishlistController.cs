using Microsoft.AspNetCore.Mvc;
using OnlineShop.Services.Data.Interfaces;
using System.Security.Claims;

namespace OnlineShop.Web.Controllers
{
    public class WishlistController : Controller
    {
        private readonly IProductWishlistService _wishlistService;
        public WishlistController(IProductWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }
        public async Task<IActionResult> Index()
        {
            var userId = GetUserId();
            var wishlistItems = await _wishlistService.GetUserWishlistAsync(userId);

            return View(wishlistItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddToWishlist(int productId)
        {
            var userId = GetUserId();
            var success = await _wishlistService.AddToWishlistAsync(userId, productId);

            if (success)
            {
                TempData["SuccessMessage"] = "Product added to wishlist!";
            }
            else
            {
                TempData["ErrorMessage"] = "Product is already in your wishlist!";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromWishlist(int wishlistProductId)
        {
            var userId = GetUserId();
            var result = await _wishlistService.RemoveFromWishlistAsync(userId, wishlistProductId);

            if (result)
            {
                TempData["SuccessMessage"] = "Product removed from wishlist!";
            }
            else
            {
                TempData["ErrorMessage"] = "Product not found in your wishlist!";
            }

            return RedirectToAction("Index");
        }

        private string? GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
