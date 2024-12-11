using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;
using OnlineShop.Data.Repository.Interfaces;
using OnlineShop.Services.Data.Interfaces;
using OnlineShop.Web.ViewModels.Wishlist;

namespace OnlineShop.Services.Data
{
    public class ProductWishlistService : IProductWishlistService
    {
        private readonly IRepository<ProductWishlist, int> _wishlistRepository;
        private readonly IRepository<Product, int> _productRepository;
        public ProductWishlistService(IRepository<ProductWishlist, int> wishlistRepository, IRepository<Product, int> productRepository)
        {
            _wishlistRepository = wishlistRepository;
            _productRepository = productRepository;
        }

        public async Task<bool> AddToWishlistAsync(string userId, int productId)
        {
            var existingWishlistItem = _wishlistRepository
                .GetAllAttached()
                .Include(p => p.Product)
                .FirstOrDefault(w => w.UserId == userId && w.ProductId == productId);

            if (existingWishlistItem != null)
            {
                return false;
            }

            var product = await _productRepository.GetByIdAsync(productId);

            var wishlistItem = new ProductWishlist
            {
                UserId = userId,
                ProductId = productId,
                IsOnSale = product.IsOnSale
            };

            await _wishlistRepository.AddAsync(wishlistItem);
            await _wishlistRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFromWishlistAsync(string userId, int wishlistProductId)
        {
            var wishlistItem = _wishlistRepository.GetAllAttached()
                .FirstOrDefault(w => w.UserId == userId && w.Id == wishlistProductId);

            if (wishlistItem == null)
            {
                return false;
            }

            await _wishlistRepository.DeleteAsync(wishlistProductId);
            await _wishlistRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<GetAllWishlistProductsViewModel>> GetUserWishlistAsync(string userId)
        {
            var result = _wishlistRepository.GetAllAttached()
                .Include(w => w.Product)
                .Where(u => u.UserId == userId)
                .ToList();

            var viewModels = new List<GetAllWishlistProductsViewModel>();

            foreach (var wishlistProduct in result)
            {
                var viewModel = new GetAllWishlistProductsViewModel()
                {
                    Id = wishlistProduct.Id,
                    ProductId = wishlistProduct.ProductId,
                    IsOnSale = wishlistProduct.Product.IsOnSale,
                    DiscountPercentage = wishlistProduct.Product.DiscountPercentage,
                    ProductImgUrl = wishlistProduct.Product.ImageUrl,
                    ProductName = wishlistProduct.Product.Name
                };

                viewModels.Add(viewModel);
            }

            return viewModels;
        }
    }
}
