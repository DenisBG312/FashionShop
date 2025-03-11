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
using OnlineShop.Web.ViewModels.Size;

namespace OnlineShop.Services.Data
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product, int> _productRepository;
        private readonly IRepository<Review, int> _reviewRepository;
        private readonly IRepository<ClothingType, int> _clothingTypeRepository;
        private readonly IRepository<Gender, int> _genderRepository;
        private readonly IRepository<Size, int> _sizeRepository;
        private readonly ProductSizeRepository _productSizeRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProductService(IRepository<Product, int> productRepository,
            IRepository<Review, int> reviewRepository,
            UserManager<ApplicationUser> userManager,
            IRepository<Gender, int> genderRepository,
            IRepository<ClothingType, int> clothingTypeRepository,
            ProductSizeRepository productSizeRepository,
            IRepository<Size, int> sizeRepository)
        {
            _productRepository = productRepository;
            _reviewRepository = reviewRepository;
            _genderRepository = genderRepository;
            _clothingTypeRepository = clothingTypeRepository;
            _userManager = userManager;
            _productSizeRepository = productSizeRepository;
            _sizeRepository = sizeRepository;
        }
        public async Task<IEnumerable<Product>> GetProductsAsync(
            int? genderId,
            int? clothingTypeId,
            string searchTerm,
            int? minPrice,
            int? maxPrice,
            List<int> sizeIds,
            bool? isOnSale,
            string sortOrder)
        {
            IQueryable<Product> productsQuery = _productRepository.GetAllAttached()
                .Include(p => p.ProductSizes);

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

            if (sizeIds != null && sizeIds.Any())
            {
                productsQuery = productsQuery.Where(p => p.ProductSizes.Any(ps => sizeIds.Contains(ps.SizeId)));
            }

            if (isOnSale.HasValue && isOnSale.Value)
            {
                productsQuery = productsQuery.Where(p => p.IsOnSale);
            }

            var products = await productsQuery.ToListAsync();

            if (minPrice.HasValue || maxPrice.HasValue)
            {
                products = products.Where(p =>
                {
                    decimal effectivePrice = p.IsOnSale ? p.DiscountedPrice : p.Price;

                    bool aboveMinPrice = !minPrice.HasValue || effectivePrice >= minPrice.Value;

                    bool belowMaxPrice = !maxPrice.HasValue || effectivePrice <= maxPrice.Value;

                    return aboveMinPrice && belowMaxPrice;
                }).ToList();
            }

            switch (sortOrder?.ToLower())
            {
                case "price-asc":
                    products = products.OrderBy(p => p.IsOnSale ? p.DiscountedPrice : p.Price).ToList();
                    break;
                case "price-desc":
                    products = products.OrderByDescending(p => p.IsOnSale ? p.DiscountedPrice : p.Price).ToList();
                    break;
                case "newest":
                    products = products.OrderByDescending(p => p.CreatedDate).ToList();
                    break;
                case "popular":
                    products = products.OrderByDescending(p => p.SalesCount).ToList();
                    break;
                default:
                    products = products.OrderBy(p => p.Id).ToList();
                    break;
            }

            return products;
        }

        public async Task<List<GenderCountViewModel>> GetGenderCountsAsync(
            int? clothingTypeId,
            int? minPrice,
            int? maxPrice,
            string searchTerm)
        {
            var genders = await _genderRepository.GetAllAsync();
            IQueryable<Product> query = _productRepository.GetAllAttached();

            if (clothingTypeId.HasValue)
            {
                query = query.Where(p => p.ClothingTypeId == clothingTypeId.Value);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm));
            }

            var products = await query.ToListAsync();

            var counts = new List<GenderCountViewModel>();

            foreach (var gender in genders)
            {
                var genderProducts = products.Where(p => p.GenderId == gender.Id);

                var filtered = genderProducts.Where(p =>
                {
                    decimal price = p.IsOnSale ? p.DiscountedPrice : p.Price;
                    return (!minPrice.HasValue || price >= minPrice) &&
                           (!maxPrice.HasValue || price <= maxPrice);
                });

                counts.Add(new GenderCountViewModel
                {
                    Id = gender.Id,
                    Name = gender.Name,
                    Count = filtered.Count()
                });
            }

            return counts;
        }

        public async Task<List<ClothingTypeCountViewModel>> GetClothingTypeCountsAsync(
            int? genderId,
            int? minPrice,
            int? maxPrice,
            string searchTerm)
        {
            var clothingTypes = await _clothingTypeRepository.GetAllAsync();
            IQueryable<Product> query = _productRepository.GetAllAttached();

            if (genderId.HasValue)
            {
                query = query.Where(p => p.GenderId == genderId.Value);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm));
            }

            var products = await query.ToListAsync();

            var counts = new List<ClothingTypeCountViewModel>();

            foreach (var type in clothingTypes)
            {
                var typeProducts = products.Where(p => p.ClothingTypeId == type.Id);

                var filtered = typeProducts.Where(p =>
                {
                    decimal price = p.IsOnSale ? p.DiscountedPrice : p.Price;
                    return (!minPrice.HasValue || price >= minPrice) &&
                           (!maxPrice.HasValue || price <= maxPrice);
                });

                counts.Add(new ClothingTypeCountViewModel
                {
                    Id = type.Id,
                    Name = type.Name,
                    Count = filtered.Count()
                });
            }

            return counts;
        }

        public async Task CreateProductAsync(CreateProductViewModel model, string userId)
        {
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                ImageUrl = model.ImageUrl,
                GenderId = model.GenderId,
                ClothingTypeId = model.ClothingTypeId,
                UserId = userId,
                ProductSizes = model.SelectedSizes
                    .Select(sizeId => new ProductSize
                    {
                        SizeId = sizeId,
                        StockQuantity = model.StockQuantities[sizeId]
                    }).ToList()
            };

            await _productRepository.AddAsync(product);
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

        public async Task<List<SelectListItem>> GetSizesAsync()
        {
            var sizes = await _sizeRepository.GetAllAsync();
            return sizes.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name 
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
                .Include(p => p.ProductSizes)
                    .ThenInclude(ps => ps.Size)
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
                Gender = product.Gender.Name,
                ClothingType = product.ClothingType.Name,
                PostedBy = $"{product.User?.FirstName} {product.User?.LastName}",
                Reviews = reviews.Where(r => r.ProductId == id).ToList(),
                UserId = product.UserId,
                DiscountPercentage = product.DiscountPercentage,
                IsOnSale = product.IsOnSale,
                AvailableSizes = product.ProductSizes
                    .Select(ps => new SizeViewModel()
                    {
                        Id = ps.Size.Id,
                        Name = ps.Size.Name
                    })
                    .ToList()
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
