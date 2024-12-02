using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Services.Data;
using OnlineShop.Services.Data.Interfaces;
using OnlineShop.Web.ViewModels.Product;
using System.Security.Claims;

namespace OnlineShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;
        public ProductController(ApplicationDbContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            
            return View(products);
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



        public string? GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
