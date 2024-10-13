using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Web.ViewModels.Product;

namespace OnlineShop.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index(int? genderId, int? clothingTypeId, string searchTerm)
        {
            var productsQuery = _context.Products.AsQueryable();

            if (genderId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.GenderId == genderId.Value);
            }

            if (clothingTypeId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.ClothingTypeId == clothingTypeId.Value);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                productsQuery = productsQuery.Where(p => p.Name.Contains(searchTerm));
            }

            var products = await productsQuery.ToListAsync();

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var genders = await _context.Genders.ToListAsync();
            var clothingTypes = await _context.ClothingTypes.ToListAsync();

            var viewModel = new CreateProductViewModel
            {
                Genders = genders.Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.Name
                }).ToList(),

                ClothingTypes = clothingTypes.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel product)
        {
            ModelState.Remove("Genders");
            ModelState.Remove("ClothingTypes");

            if (!ModelState.IsValid)
            {
                // Repopulate Genders and ClothingTypes if the model state is invalid
                product.Genders = await _context.Genders
                    .Select(g => new SelectListItem
                    {
                        Value = g.Id.ToString(),
                        Text = g.Name
                    }).ToListAsync();

                product.ClothingTypes = await _context.ClothingTypes
                    .Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToListAsync();

                return View(product);
            }

            // Create a new Product entity with the submitted data
            Product productEntity = new Product()
            {
                Name = product.Name,
                StockQuantity = product.StockQuantity,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                GenderId = product.GenderId,              // Add the GenderId
                ClothingTypeId = product.ClothingTypeId   // Add the ClothingTypeId
            };

            await _context.Products.AddAsync(productEntity);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _context
                .Products
                .Include(product => product.Gender)
                .Include(product => product.ClothingType)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var productDetailsViewModel = new ProductDetailsViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                StockQuantity = product.StockQuantity,
                Gender = product.Gender.Name,
                ClothingType = product.ClothingType.Name
            };

            return View(productDetailsViewModel);
        }
    }
}
