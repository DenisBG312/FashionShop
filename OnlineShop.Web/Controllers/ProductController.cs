using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;

        public ProductController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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

            // Get the logged-in user's ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Create a new Product entity with the submitted data
            Product productEntity = new Product()
            {
                Name = product.Name,
                StockQuantity = product.StockQuantity,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                GenderId = product.GenderId,
                ClothingTypeId = product.ClothingTypeId,
                UserId = userId // Set the user who posted the product
            };

            await _context.Products.AddAsync(productEntity);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            // Check if the current user is the owner
            if (product.UserId != _userManager.GetUserId(User))
            {
                return Forbid();
            }

            // Populate the dropdowns
            var genders = await _context.Genders.ToListAsync();
            var clothingTypes = await _context.ClothingTypes.ToListAsync();

            var model = new ProductEditViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                GenderId = product.GenderId,
                ClothingTypeId = product.ClothingTypeId,
                ImageUrl = product.ImageUrl,
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

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductEditViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            // Check if the current user is the owner
            if (product.UserId != _userManager.GetUserId(User))
            {
                return Forbid();
            }

            ModelState.Remove("Genders");
            ModelState.Remove("ClothingTypes");

            if (ModelState.IsValid)
            {
                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.StockQuantity = model.StockQuantity;
                product.GenderId = model.GenderId;
                product.ClothingTypeId = model.ClothingTypeId;
                product.ImageUrl = model.ImageUrl;

                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", new { id = product.Id });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // Repopulate Genders and ClothingTypes if the model state is invalid
            model.Genders = await _context.Genders
                .Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.Name
                }).ToListAsync();

            model.ClothingTypes = await _context.ClothingTypes
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToListAsync();

            return View(model);
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _context
                .Products
                .Include(p => p.Gender)
                .Include(p => p.ClothingType)
                .Include(product => product.User)
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
                ClothingType = product.ClothingType.Name,
                PostedBy = product.User?.UserName,
                UserId = product.UserId
            };

            return View(productDetailsViewModel);
        }
    }
}
