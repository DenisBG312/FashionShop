using System.Security.Claims;
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
        private const int pageSize = 6;

        public ProductController(UserManager<ApplicationUser> userManager, IProductService productService)
        {
            _userManager = userManager;
            _productService = productService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(int? genderId, int? clothingTypeId, string searchTerm, int page = 1)
        {
            var products = await _productService.GetProductsAsync(genderId, clothingTypeId, searchTerm);

            var pagedProducts = products.ToPagedList(page, pageSize); // Ensure to use ToPagedList

            ViewBag.GenderId = genderId; // Pass genderId to the view
            ViewBag.ClothingTypeId = clothingTypeId; // Pass clothingTypeId to the view

            return View(pagedProducts);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {

            var viewModel = new CreateProductViewModel
            {
                Genders = await _productService.GetGendersAsync(),
                ClothingTypes = await _productService.GetClothingTypesAsync()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel product)
        {
            ModelState.Remove(nameof(product.Genders));
            ModelState.Remove(nameof(product.ClothingTypes));

            if (!ModelState.IsValid)
            {
                product.Genders = await _productService.GetGendersAsync();
                product.ClothingTypes = await _productService.GetClothingTypesAsync();

                return View(product);
            }

            await _productService.CreateProductAsync(product, GetUserId()!);

            return RedirectToAction(nameof(Index));
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
