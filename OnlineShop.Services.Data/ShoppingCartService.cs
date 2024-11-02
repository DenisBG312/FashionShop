using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;
using OnlineShop.Data.Models.Enums.Payment;
using OnlineShop.Data.Repository;
using OnlineShop.Data.Repository.Interfaces;
using OnlineShop.Services.Data.Interfaces;
using OnlineShop.Web.ViewModels.Cart;
using OnlineShop.Web.ViewModels.Order;

namespace OnlineShop.Services.Data
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart, int> _shoppingCartRepository;
        private readonly IRepository<Product, int> _productRepository;

        public ShoppingCartService(BaseRepository<ShoppingCart, int> shoppingCartRepository, BaseRepository<Product, int> productRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _productRepository = productRepository;
        }
        public async Task<ShoppingCart> GetCartAsync(string userId)
        {
            var cart = await _shoppingCartRepository
                .GetAllAttached()
                .Include(c => c.ShoppingCartProducts)
                .ThenInclude(cp => cp.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            return cart;
        }

        public async Task<AddToCartResult> AddToCartAsync(string userId, int productId, int quantity)
        {
            var shoppingCart = await GetCartAsync(userId);

            if (shoppingCart == null)
            {
                shoppingCart = new ShoppingCart
                {
                    UserId = userId,
                    Amount = 0
                };
                await _shoppingCartRepository.AddAsync(shoppingCart);
                await _shoppingCartRepository.SaveChangesAsync();
            }

            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                return new AddToCartResult { IsSuccess = false, ErrorMessage = "Product not found." };
            }

            var cartProduct = shoppingCart.ShoppingCartProducts
                .FirstOrDefault(scp => scp.ProductId == productId);

            if (cartProduct != null)
            {
                cartProduct.Quantity += quantity;
            }
            else
            {
                cartProduct = new ShoppingCartProduct
                {
                    ShoppingCartId = shoppingCart.Id,
                    ProductId = productId,
                    Quantity = quantity
                };
                shoppingCart.ShoppingCartProducts.Add(cartProduct);
            }

            shoppingCart.Amount += product.Price * quantity;

            await _shoppingCartRepository.SaveChangesAsync();

            return new AddToCartResult { IsSuccess = true };
        }

        public async Task<bool> UpdateQuantityAsync(int shoppingCartId, int productId, int quantity)
        {
            var shoppingCart = await _shoppingCartRepository
                .GetAllAttached()
                .Include(sc => sc.ShoppingCartProducts)
                .ThenInclude(scp => scp.Product)
                .FirstOrDefaultAsync(sc => sc.Id == shoppingCartId);

            if (shoppingCart == null)
            {
                return false;
            }

            var shoppingCartProduct = shoppingCart.ShoppingCartProducts
                .FirstOrDefault(scp => scp.ProductId == productId);

            if (shoppingCartProduct == null)
            {
                return false;
            }

            shoppingCart.Amount -= shoppingCartProduct.Product.Price * shoppingCartProduct.Quantity;
            
            shoppingCartProduct.Quantity = quantity;

            shoppingCart.Amount += shoppingCartProduct.Product.Price * shoppingCartProduct.Quantity;

            if (shoppingCartProduct.Quantity <= 0)
            {
                await _shoppingCartRepository.DeleteAsync(shoppingCartProduct.ProductId);
            }

            if (!shoppingCart.ShoppingCartProducts.Any())
            {
                shoppingCart.Amount = 0;
            }

            await _shoppingCartRepository.SaveChangesAsync();

            return true;
        }

        public Task RemoveFromCartAsync(int shoppingCartId, int productId)
        {
            throw new NotImplementedException();
        }

        public Task<PlaceOrderResult> PlaceOrderAsync(int shoppingCartId, string userId, PaymentMethod paymentMethod)
        {
            throw new NotImplementedException();
        }
    }
}
