using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Data.Models;
using OnlineShop.Data.Repository;
using OnlineShop.Data.Repository.Interfaces;
using OnlineShop.Web.ViewModels.Payment;
using OnlineShop.Data.Models.Enums.Payment;
using OnlineShop.Services.Data.Interfaces;

namespace OnlineShop.Services.Data
{
    public class PaymentService : IPaymentService
    {
        private readonly IRepository<Payment, int> _paymentRepository;
        private readonly IRepository<Order, int> _orderRepository;
        private readonly IRepository<Product, int> _productRepository;

        public PaymentService(BaseRepository<Payment, int> paymentRepository, BaseRepository<Order, int> orderRepository, BaseRepository<Product, int> productRepository)
        {
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<PaymentCreationResult> PreparePaymentAsync(int orderId)
        {
            var order = await _orderRepository
                .GetAllAttached()
                .Include(o => o.Payments)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return new PaymentCreationResult
                {
                    Success = false,
                    ErrorMessage = "Order not found."
                };
            }

            var remainingAmount = order.TotalAmount - order.Payments.Sum(p => p.Amount);

            if (remainingAmount <= 0)
            {
                return new PaymentCreationResult
                {
                    Success = false,
                    ErrorMessage = "This order is already fully paid."
                };
            }

            return new PaymentCreationResult
            {
                Success = true,
                RemainingAmount = remainingAmount
            };
        }

        public async Task<bool> ProcessPaymentAsync(CreatePaymentViewModel model)
        {
            var order = await _orderRepository
                .GetAllAttached()
                .Include(o => o.OrderProducts)
                .FirstOrDefaultAsync(o => o.Id == model.OrderId);

            if (order == null) return false;

            var payment = new Payment
            {
                OrderId = model.OrderId,
                Amount = model.Amount,
                PaymentMethod = model.PaymentMethod,
                PaymentDate = DateTime.UtcNow,
                Status = Status.Completed
            };

            await _paymentRepository.AddAsync(payment);

            foreach (var orderProduct in order.OrderProducts)
            {
                var product = await _productRepository.GetByIdAsync(orderProduct.ProductId);
                if (product != null)
                {
                    product.StockQuantity -= orderProduct.Quantity;
                }
            }

            await _orderRepository.SaveChangesAsync();
            return true;
        }
    }
}
