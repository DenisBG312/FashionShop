using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IRepository<Order, int> _orderRepository;
        private readonly IRepository<Payment, int> _paymentRepository;
        private readonly IRepository<OrderProduct, int> _orderProductRepository;

        public ShoppingCartService(IRepository<ShoppingCart, int> shoppingCartRepository, IRepository<Product, int> productRepository, IRepository<Order, int> orderRepository, IRepository<Payment, int> paymentRepository, IRepository<OrderProduct, int> orderProductRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
            _orderProductRepository = orderProductRepository;
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

            decimal productPrice = product.Price;

            if (product.IsOnSale && product.DiscountPercentage.HasValue)
            {
                productPrice = product.DiscountedPrice;
            }

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

            shoppingCart.Amount += productPrice * quantity;

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

            decimal productPrice = shoppingCartProduct.Product.Price;

            if (shoppingCartProduct.Product.IsOnSale && shoppingCartProduct.Product.DiscountPercentage.HasValue)
            {
                productPrice = shoppingCartProduct.Product.DiscountedPrice;
            }

            shoppingCart.Amount -= productPrice * shoppingCartProduct.Quantity;

            shoppingCartProduct.Quantity = quantity;

            shoppingCart.Amount += productPrice * shoppingCartProduct.Quantity;

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

        public async Task<bool> RemoveFromCartAsync(int shoppingCartId, int productId)
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

            var product = shoppingCartProduct.Product;
            decimal productPrice = product.Price;

            if (product.IsOnSale && product.DiscountPercentage.HasValue)
            {
                productPrice = product.DiscountedPrice;
            }

            shoppingCart.Amount -= productPrice * shoppingCartProduct.Quantity;

            shoppingCart.ShoppingCartProducts.Remove(shoppingCartProduct);

            if (!shoppingCart.ShoppingCartProducts.Any())
            {
                shoppingCart.Amount = 0;
            }

            await _shoppingCartRepository.SaveChangesAsync();

            return true;

        }

        public async Task<PlaceOrderResult> PlaceOrderAsync(int shoppingCartId, string userId, PaymentMethod paymentMethod)
        {
            var result = new PlaceOrderResult();

            var shoppingCart = await _shoppingCartRepository
                .GetAllAttached()
                .Include(sc => sc.ShoppingCartProducts)
                .ThenInclude(scp => scp.Product)
                .FirstOrDefaultAsync(sc => sc.Id == shoppingCartId);

            if (shoppingCart == null || !shoppingCart.ShoppingCartProducts.Any())
            {
                result.IsSuccess = false;
                result.ErrorMessage = "Your shopping cart is empty.";
                return result;
            }

            decimal totalAmount = 0;
            foreach (var cartProduct in shoppingCart.ShoppingCartProducts)
            {
                decimal productPrice = cartProduct.Product.Price;
                if (cartProduct.Product.IsOnSale && cartProduct.Product.DiscountPercentage.HasValue)
                {
                    productPrice = cartProduct.Product.DiscountedPrice;
                }

                totalAmount += cartProduct.Quantity * productPrice;
            }


            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                TotalAmount = totalAmount
            };

            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();

            var payment = new Payment
            {
                PaymentMethod = paymentMethod,
                Amount = totalAmount,
                PaymentDate = DateTime.Now,
                Status = Status.Pending,
                OrderId = order.Id
            };

            await _paymentRepository.AddAsync(payment);
            await _paymentRepository.SaveChangesAsync();

            foreach (var cartProduct in shoppingCart.ShoppingCartProducts)
            {
                var orderProduct = new OrderProduct
                {
                    OrderId = order.Id,
                    ProductId = cartProduct.ProductId,
                    Quantity = cartProduct.Quantity,
                    UnitPrice = cartProduct.Product.Price
                };

                if (cartProduct.Product.IsOnSale && cartProduct.Product.DiscountPercentage.HasValue)
                {
                    orderProduct.UnitPrice = cartProduct.Product.DiscountedPrice;
                }

                await _orderProductRepository.AddAsync(orderProduct);

                var product = await _productRepository.GetByIdAsync(cartProduct.ProductId);
            }

            await _orderProductRepository.SaveChangesAsync();

            foreach (var cartProduct in shoppingCart.ShoppingCartProducts)
            {
                await _shoppingCartRepository.DeleteAsync(cartProduct.ShoppingCartId);
            }

            await _shoppingCartRepository.DeleteAsync(shoppingCartId);
            await _shoppingCartRepository.SaveChangesAsync();

            result.IsSuccess = true;
            return result;
        }
    }
}
