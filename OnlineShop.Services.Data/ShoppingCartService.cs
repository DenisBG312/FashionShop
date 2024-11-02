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
using OnlineShop.Web.ViewModels.Order;

namespace OnlineShop.Services.Data
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart, int> _shoppingCartRepository;

        public ShoppingCartService(BaseRepository<ShoppingCart, int> shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
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

        public Task AddToCartAsync(string userId, int productId, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateQuantityAsync(int shoppingCartId, int productId, int quantity)
        {
            throw new NotImplementedException();
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
