using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Services.Data.Interfaces;
using OnlineShop.Web.ViewModels.Product;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using OnlineShop.Data.Repository;
using OnlineShop.Data.Repository.Interfaces;

namespace OnlineShop.Services.Data
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product, int> _productRepository;
        private readonly IRepository<Review, int> _reviewRepository;
        private readonly IRepository<ClothingType, int> _clothingTypeRepository;
        private readonly IRepository<Gender, int> _genderRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductService(IRepository<Product, int> productRepository, IRepository<Review, int> reviewRepository, UserManager<ApplicationUser> userManager, IRepository<Gender, int> genderRepository, IRepository<ClothingType, int> clothingTypeRepository)
        {
            _productRepository = productRepository;
            _reviewRepository = reviewRepository;
            _genderRepository = genderRepository;
            _clothingTypeRepository = clothingTypeRepository;
            _userManager = userManager;
        }
        public async Task<IEnumerable<Product>> GetProductsAsync(int? genderId, int? clothingTypeId, string searchTerm)
        {
            IQueryable<Product> productsQuery = _productRepository.GetAllAttached();

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

            return productsQuery.ToList();
        }

        public async Task CreateProductAsync(CreateProductViewModel product, string userId)
        {
            var newProduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                ImageUrl = product.ImageUrl,
                GenderId = product.GenderId,
                ClothingTypeId = product.ClothingTypeId,
                UserId = userId
            };

            await _productRepository.AddAsync(newProduct);
            await _productRepository.SaveChangesAsync();
        }

        public async Task<ProductEditViewModel?> GetEditProductViewModelAsync(int productId, string userId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null) return null;

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return null;

            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            if (!isAdmin && product.UserId != userId) return null;

            var productEditViewModel = new ProductEditViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                ImageUrl = product.ImageUrl,
                GenderId = product.GenderId,
                ClothingTypeId = product.ClothingTypeId,
                IsOnSale = product.IsOnSale,
                DiscountPercentage = product.IsOnSale ? (int?)(product.DiscountPercentage) : null,
                Genders = await GetGendersAsync(),
                ClothingTypes = await GetClothingTypesAsync()
            };

            return productEditViewModel;
        }

        public async Task<bool> UpdateProductAsync(ProductEditViewModel product, string userId)
        {
            var productEntity = await _productRepository.GetByIdAsync(product.Id);
            if (productEntity == null) return false;

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return false;

            var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
            if (!isAdmin && productEntity.UserId != userId) return false;

            productEntity.Name = product.Name;
            productEntity.Description = product.Description;
            productEntity.Price = product.Price;
            productEntity.StockQuantity = product.StockQuantity;
            productEntity.GenderId = product.GenderId;
            productEntity.ClothingTypeId = product.ClothingTypeId;
            productEntity.ImageUrl = product.ImageUrl;
            productEntity.IsOnSale = product.IsOnSale;
            productEntity.DiscountPercentage = product.DiscountPercentage;


            await _productRepository.UpdateAsync(productEntity);
            await _productRepository.SaveChangesAsync();

            return true;
        }

        public async Task<List<SelectListItem>> GetGendersAsync()
        {
            var genders = await _genderRepository.GetAllAsync();
            return genders.Select(g => new SelectListItem
            {
                Value = g.Id.ToString(),
                Text = g.Name
            }).ToList();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<List<SelectListItem>> GetClothingTypesAsync()
        {
            var clothingTypes = await _clothingTypeRepository.GetAllAsync();
            return clothingTypes.Select(g => new SelectListItem()
                {
                    Value = g.Id.ToString(),
                    Text = g.Name
                }).ToList();
        }

        public async Task SubmitReview(int productId, string userId, int rating, string comment)
        {
            var review = new Review
            {
                ProductId = productId,
                UserId = userId,
                Rating = rating,
                Comment = comment,
                ReviewDate = DateTime.Now
            };

            await _reviewRepository.AddAsync(review);
            await _reviewRepository.SaveChangesAsync();
        }

        public async Task<ProductDetailsViewModel?> ViewDetailsAboutProductAsync(int id, string userId)
        {
            var product = await _productRepository.GetAllAttached()
                .Include(p => p.Gender)
                .Include(p => p.ClothingType)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return null;

            var reviews = await _reviewRepository.GetAllAttached()
                .Include(p => p.User)
                .ToListAsync();

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
                PostedBy = $"{product.User?.FirstName} {product.User?.LastName}",
                Reviews = reviews.Where(r => r.ProductId == id).ToList(),
                UserId = product.UserId,
                DiscountPercentage = product.DiscountPercentage,
                IsOnSale = product.IsOnSale
            };

            return productDetailsViewModel;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return false;
            }

            await _productRepository.DeleteAsync(product.Id);
            await _productRepository.SaveChangesAsync();
            return true;
        }
    }
}
