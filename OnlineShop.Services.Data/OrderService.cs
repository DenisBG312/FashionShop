using OnlineShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Models;
using OnlineShop.Data.Models.Enums.Payment;
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

        public async Task<OrderDetailsViewModel?> GetOrderDetails(int orderId)
        {
            var order = await _orderRepository.GetAllAttached()
                .Include(o => o.Payments)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return null;
            }

            var userId = order.UserId;

            var userOrders = await _orderRepository.GetAllAttached()
                .Where(o => o.UserId == userId)
                .OrderBy(o => o.Id)
                .Include(o => o.OrderProducts)
                .ToListAsync();

            int userOrderNumber = userOrders.FindIndex(o => o.Id == order.Id) + 1;

            var viewModel = new OrderDetailsViewModel
            {
                OrderId = order.Id,
                CustomOrderNumber = userOrderNumber,
                OrderDate = order.OrderDate,
                TotalAmount = order.OrderProducts.Sum(op => op.UnitPrice * op.Quantity),
                IsCompleted = order.IsCompleted,
                IsCancelled = order.IsCancelled,
                OrderProducts = order.OrderProducts.Select(op => new OrderProductViewModel
                {
                    ProductName = op.Product.Name,
                    Quantity = op.Quantity,
                    UnitPrice = op.UnitPrice
                }).ToList()
            };

            return viewModel;
        }

        public async Task<bool> ReactivateOrder(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null)
            {
                return false;
            }

            if (order.IsCancelled)
            {
                order.IsCancelled = false;
                order.IsCompleted = false;

                await _orderRepository.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> CancelOrder(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);

            if (order == null)
            {
                return false;
            }

            if (!order.IsCompleted && !order.IsCancelled)
            {
                order.IsCancelled = true;
                await _orderRepository.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> FinalizeOrder(int orderId)
        {
            var order = await _orderRepository.GetAllAttached()
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .Include(o => o.Payments)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return false;
            }

            foreach (var orderProduct in order.OrderProducts)
            {
                if (orderProduct.Product == null || orderProduct.Product.StockQuantity < orderProduct.Quantity)
                {
                    return false;
                }
            }

            foreach (var orderProduct in order.OrderProducts)
            {
                orderProduct.Product.StockQuantity -= orderProduct.Quantity;
            }

            order.IsCompleted = true;
            foreach (var payment in order.Payments)
            {
                payment.Status = Status.Completed;
            }

            await _orderRepository.UpdateAsync(order);
            await _orderRepository.SaveChangesAsync();
            return true;
        }

        public async Task<OrderTransactionHistoryViewModel> GetTransactionHistoryAsync(int orderId, string userId)
        {
            var order = await _orderRepository.GetAllAttached()
                .Where(o => o.UserId == userId && o.Id == orderId)
                .Include(o => o.Payments)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                return null;
            }

            var userOrders = await _orderRepository.GetAllAttached()
                .Where(o => o.UserId == userId)
                .OrderBy(o => o.Id)
                .ToListAsync();

            int customOrderNumber = userOrders.FindIndex(o => o.Id == order.Id) + 1;

            var viewModel = new OrderTransactionHistoryViewModel
            {
                OrderId = order.Id,
                CustomOrderNumber = customOrderNumber,
                Payments = order.Payments.ToList()
            };

            return viewModel;
        }
    }
}
