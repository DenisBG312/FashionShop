﻿using System.Security.Claims;
using iTextSharp.text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Services.Data.Interfaces;
using OnlineShop.Web.ViewModels.Product;
using X.PagedList.Extensions;


namespace OnlineShop.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProductService _productService;
        private readonly IProductWishlistService _productWishlistService;
        private readonly ApplicationDbContext _context;
        private const int pageSize = 6;

        public ProductController(
            UserManager<ApplicationUser> userManager,
            IProductService productService,
            IProductWishlistService productWishlistService,
            ApplicationDbContext context
            )
        {
            _userManager = userManager;
            _productService = productService;
            _productWishlistService = productWishlistService;
            _context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(
            int? genderId,
            int? clothingTypeId,
            int? minPrice,
            int? maxPrice,
            List<int>? sizeIds,
            string searchTerm,
            bool? isOnSale,
            string sortOrder,
            int page = 1)
        {
            int? sanitizedMinPrice = minPrice;
            int? sanitizedMaxPrice = maxPrice;

            if (sanitizedMinPrice < 0) sanitizedMinPrice = null;
            if (sanitizedMaxPrice < 0) sanitizedMaxPrice = null;

            if (sanitizedMinPrice.HasValue && sanitizedMaxPrice.HasValue && sanitizedMinPrice > sanitizedMaxPrice)
            {
                (sanitizedMaxPrice, sanitizedMinPrice) = (sanitizedMinPrice, sanitizedMaxPrice);
            }


            var products = await _productService.GetProductsAsync(
                genderId,
                clothingTypeId,
                searchTerm,
                sanitizedMinPrice,
                sanitizedMaxPrice,
                sizeIds,
                isOnSale,
                sortOrder);

            var userId = GetUserId();
            var wishlist = await _productWishlistService.GetUserWishlistAsync(userId!);
            var wishlistProductIds = wishlist.Select(p => p.ProductId).ToList();

            var pagedProducts = products.ToPagedList(page, pageSize);

            ViewBag.GenderCounts = await _productService.GetGenderCountsAsync(
                clothingTypeId,
                minPrice,
                maxPrice,
                searchTerm
            );

            ViewBag.ClothingTypeCounts = await _productService.GetClothingTypeCountsAsync(
                genderId,
                minPrice,
                maxPrice,
                searchTerm
            );

            var sizes = await _context.Sizes.ToListAsync();

            ViewBag.WishlistProductIds = wishlistProductIds;
            ViewBag.GenderId = genderId;
            ViewBag.ClothingTypeId = clothingTypeId;
            ViewBag.MinPrice = sanitizedMinPrice;
            ViewBag.MaxPrice = sanitizedMaxPrice;
            ViewBag.Sizes = sizes;
            ViewBag.IsOnSale = isOnSale ?? false;
            ViewBag.SortOrder = sortOrder;

            return View(pagedProducts);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = _userManager.GetUserId(User);
            var product = await _productService.GetEditProductViewModelAsync(id, userId!);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductEditViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            ModelState.Remove(nameof(model.Genders));
            ModelState.Remove(nameof(model.ClothingTypes));

            if (!ModelState.IsValid)
            {
                model.Genders = await _productService.GetGendersAsync();
                model.ClothingTypes = await _productService.GetClothingTypesAsync();
                return View(model);
            }

            var userId = _userManager.GetUserId(User);

            var success = await _productService.UpdateProductAsync(model, userId);

            if (!success)
            {
                return Forbid();
            }

            return RedirectToAction("Details", new { id = model.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitReview(int productId, int rating, string comment)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _productService.SubmitReview(productId, userId, rating, comment);

            return RedirectToAction("Details", new { id = productId });
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var productDetails = await _productService.ViewDetailsAboutProductAsync(id, userId);

            if (productDetails == null)
            {
                return NotFound();
            }

            return View(productDetails);
        }

        public string? GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
