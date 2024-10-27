using OnlineShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;
using OnlineShop.Data.Repository;
using OnlineShop.Services.Data.Interfaces;
using OnlineShop.Web.ViewModels.Order;
using OnlineShop.Web.ViewModels.Transaction;

namespace OnlineShop.Services.Data
{
    public class OrderService : IOrderService
    {
        private readonly BaseRepository<Order, int> _orderRepository;
        public OrderService(BaseRepository<Order, int> orderRepository)
        {
            _orderRepository = orderRepository;
        }


        public async Task<IEnumerable<OrderIndexViewModel>> GetAllOrders(string userId)
        {
            var userOrders = await _orderRepository.GetAllAttached()
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderProducts)
                .Include(o => o.Payments)
                .ToListAsync();

            return userOrders.Select((order, index) => new OrderIndexViewModel()
            {
                OrderId = order.Id,
                CustomOrderNumber = index + 1,
                OrderDate = order.OrderDate,
                TotalAmount = order.OrderProducts.Sum(op => op.UnitPrice * op.Quantity),
                IsCompleted = order.IsCompleted,
                IsCancelled = order.IsCancelled,
                Transactions = order.Payments.Select(p => new TransactionViewModel()
                {
                    PaymentMethod = p.PaymentMethod.ToString(),
                    Amount = p.Amount,
                    PaymentDate = p.PaymentDate,
                    Status = p.Status.ToString()
                })
            }).ToList();
        }
    }
}
