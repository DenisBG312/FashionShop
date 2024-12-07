using Microsoft.AspNetCore.Identity;
using Moq;
using OnlineShop.Data.Models;
using OnlineShop.Data.Repository.Interfaces;
using OnlineShop.Services.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MockQueryable;
using OnlineShop.Data.Models.Enums.Payment;
using OnlineShop.Services.Data.Interfaces;
using OnlineShop.Web.ViewModels.Order;

namespace OnlineShop.Services.Tests
{
    [TestFixture]
    public class OrderServiceTests
    {
        private static string userId = "user123";

        private List<Order> mockOrders = new List<Order>
        {
            new Order
            {
                Id = 1,
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                IsCompleted = false,
                IsCancelled = false,
                OrderProducts = new List<OrderProduct>
                {
                    new OrderProduct
                    {
                        Product = new Product { Name = "Product1", DiscountPercentage = 10 },
                        Quantity = 2,
                        UnitPrice = 50
                    }
                },
                Payments = new List<Payment>
                {
                    new Payment { PaymentMethod = PaymentMethod.DebitCard, Amount = 100, Status = Status.Completed }
                }
            }
        };

        private Mock<IRepository<Order, int>> _mockOrderRepository;
        private OrderService _orderService;

        [SetUp]
        public void Setup()
        {
            _mockOrderRepository = new Mock<IRepository<Order, int>>();
        }

        [Test]
        public async Task GetAllOrders_ShouldReturnCorrectOrdersForUser()
        {
            IQueryable<Order> orderMockQueryable = mockOrders.BuildMock();

            _mockOrderRepository
                .Setup(r => r.GetAllAttached())
                .Returns(orderMockQueryable);

            IOrderService orderService = new OrderService(_mockOrderRepository.Object);

            IEnumerable<OrderIndexViewModel> allOrdersActual = await orderService.GetAllOrders(userId);

            Assert.AreEqual(1, allOrdersActual.Count());
            var order = allOrdersActual.First();
            Assert.AreEqual(100m, order.TotalAmount);
            Assert.AreEqual(false, order.IsCompleted);
            Assert.AreEqual(false, order.IsCancelled);
        }
    }
}
