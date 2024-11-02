﻿using OnlineShop.Data.Models.Enums.Payment;
using OnlineShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Web.ViewModels.Order;

namespace OnlineShop.Services.Data.Interfaces
{
    public interface IShoppingCartService
    {
        Task<ShoppingCart> GetCartAsync(string userId);

        Task AddToCartAsync(string userId, int productId, int quantity);

        Task UpdateQuantityAsync(int shoppingCartId, int productId, int quantity);

        Task RemoveFromCartAsync(int shoppingCartId, int productId);

        Task<PlaceOrderResult> PlaceOrderAsync(int shoppingCartId, string userId, PaymentMethod paymentMethod);
    }
}
