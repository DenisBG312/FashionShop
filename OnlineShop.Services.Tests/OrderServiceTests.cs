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
using Microsoft.AspNetCore.Mvc;

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

            _orderService = new OrderService(_mockOrderRepository.Object);
        }

        [Test]
        public async Task GetAllOrders_ShouldReturnCorrectOrdersForUser()
        {
            IQueryable<Order> orderMockQueryable = mockOrders.BuildMock();

            _mockOrderRepository
                .Setup(r => r.GetAllAttached())
                .Returns(orderMockQueryable);

            IEnumerable<OrderIndexViewModel> allOrdersActual = await _orderService.GetAllOrdersForUser(userId);

            Assert.AreEqual(1, allOrdersActual.Count());
            var order = allOrdersActual.First();
            Assert.AreEqual(100m, order.TotalAmount);
            Assert.AreEqual(false, order.IsCompleted);
            Assert.AreEqual(false, order.IsCancelled);
        }

        [Test]
        public async Task GetOrderDetails_ShouldReturnCorrectViewModel_WhenOrderExists()
        {
            var orderId = 1;

            _mockOrderRepository
                .Setup(r => r.GetAllAttached())
                .Returns(mockOrders.BuildMock());

            var expectedViewModel = new OrderDetailsViewModel
            {
                OrderId = 1,
                CustomOrderNumber = 1,
                OrderDate = mockOrders.First().OrderDate,
                TotalAmount = mockOrders.First().OrderProducts.Sum(op => op.UnitPrice * op.Quantity),
                IsCompleted = false,
                IsCancelled = false,
                OrderProducts = mockOrders.First().OrderProducts.Select(op => new OrderProductViewModel
                {
                    ProductName = op.Product.Name,
                    Quantity = op.Quantity,
                    UnitPrice = op.UnitPrice
                }).ToList()
            };

            var result = await _orderService.GetOrderDetails(orderId);

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedViewModel.OrderId, result.OrderId);
            Assert.AreEqual(expectedViewModel.CustomOrderNumber, result.CustomOrderNumber);
            Assert.AreEqual(expectedViewModel.OrderDate, result.OrderDate);
            Assert.AreEqual(expectedViewModel.TotalAmount, result.TotalAmount);
            Assert.AreEqual(expectedViewModel.IsCompleted, result.IsCompleted);
            Assert.AreEqual(expectedViewModel.IsCancelled, result.IsCancelled);

            Assert.AreEqual(expectedViewModel.OrderProducts.Count(), result.OrderProducts.Count());
            Assert.AreEqual(expectedViewModel.OrderProducts.First().ProductName, result.OrderProducts.First().ProductName);
            Assert.AreEqual(expectedViewModel.OrderProducts.First().Quantity, result.OrderProducts.First().Quantity);
            Assert.AreEqual(expectedViewModel.OrderProducts.First().UnitPrice, result.OrderProducts.First().UnitPrice);
        }

        [Test]
        public async Task GetOrderDetails_ShouldReturnNull_WhenOrderDoesNotExist()
        {
            var nonExistentOrderId = 999;

            _mockOrderRepository
                .Setup(r => r.GetAllAttached())
                .Returns(mockOrders.BuildMock());

            var result = await _orderService.GetOrderDetails(nonExistentOrderId);

            Assert.IsNull(result, "The result should be null when the order does not exist.");
        }

        [Test]
        public async Task ReactivateOrder_ShouldReturnTrue_AndUpdateOrder_WhenOrderIsCancelled()
        {
            var mockOrders = new List<Order>()
            {
                new Order()
                {
                    Id = 2,
                    UserId = userId,
                    IsCancelled = true,
                    IsCompleted = false,
                    OrderDate = DateTime.Now,
                    TotalAmount = 50m,
                    OrderProducts = new List<OrderProduct>()
                    {
                        new OrderProduct()
                        {
                            Product = new Product() { Id = 2, Name = "Laptop" },
                            Quantity = 1,
                            UnitPrice = 100
                        }
                    },
                    Payments = new List<Payment>()
                    {
                        new Payment { PaymentMethod = PaymentMethod.DebitCard, Amount = 100, Status = Status.Completed }
                    }
                }
            };

            var orderId = 2;

            _mockOrderRepository
                .Setup(r => r.GetByIdAsync(orderId))
                .ReturnsAsync(mockOrders.First(o => o.Id == orderId));

            var result = await _orderService.ReactivateOrder(orderId);

            Assert.IsTrue(result);

            var orderTest = mockOrders.First(o => o.Id == orderId);

            Assert.IsFalse(orderTest.IsCancelled);
            Assert.IsFalse(orderTest.IsCompleted);
        }

        [Test]
        public async Task ReactivateOrder_ShouldReturnFalse_WhenOrderIsNotCancelled()
        {
            var orderId = 1;

            var order = mockOrders.First(o => o.Id == orderId);

            _mockOrderRepository
                .Setup(r => r.GetByIdAsync(orderId))
                .ReturnsAsync(order);

            var result = await _orderService.ReactivateOrder(orderId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task ReactivateOrder_ShouldReturnFalse_WhenOrderIsNotFound()
        {
            var orderId = 999;

            _mockOrderRepository
                .Setup(r => r.GetAllAttached())
                .Returns(mockOrders.BuildMock());

            var result = await _orderService.ReactivateOrder(orderId);

            Assert.IsFalse(result, "The result should be null when the order does not exist.");
        }

        [Test]
        public async Task CancelOrder_ShouldReturnTrue_WhenOrderCanBeCancelled()
        {
            var mockOrders = new List<Order>()
            {
                new Order()
                {
                    Id = 3,
                    UserId = userId,
                    IsCancelled = false,
                    IsCompleted = false,
                    OrderDate = DateTime.Now,
                    TotalAmount = 50m,
                    OrderProducts = new List<OrderProduct>()
                    {
                        new OrderProduct()
                        {
                            Product = new Product() { Id = 2, Name = "Laptop" },
                            Quantity = 1,
                            UnitPrice = 100
                        }
                    },
                    Payments = new List<Payment>()
                    {
                        new Payment { PaymentMethod = PaymentMethod.DebitCard, Amount = 100, Status = Status.Completed }
                    }
                }
            };

            var orderId = 3;
            var order = mockOrders.First(o => o.Id == orderId);

            _mockOrderRepository
                .Setup(r => r.GetByIdAsync(orderId))
                .ReturnsAsync(order);

            var result = await _orderService.CancelOrder(orderId);

            Assert.IsTrue(result);
            Assert.IsTrue(order.IsCancelled, "The order's IsCancelled property should be true.");
        }

        [Test]
        public async Task FinalizeOrder_ShouldReturnTrue_WhenOrderCanBeFinalized()
        {
            var mockProducts = new List<Product>
            {
                new Product { Id = 1, Name = "Laptop", StockQuantity = 10 },
                new Product { Id = 2, Name = "Mouse", StockQuantity = 5 }
            };

            var mockOrder = new Order
            {
                Id = 3,
                IsCompleted = false,
                OrderProducts = new List<OrderProduct>
                {
                    new OrderProduct { Product = mockProducts[0], Quantity = 2, UnitPrice = 100 },
                    new OrderProduct { Product = mockProducts[1], Quantity = 3, UnitPrice = 20 }
                },
                Payments = new List<Payment>
                {
                    new Payment { PaymentMethod = PaymentMethod.DebitCard, Amount = 260, Status = Status.Pending }
                }
            };

            IQueryable<Order> orderMockQueryable = new List<Order> { mockOrder }.BuildMock();

            _mockOrderRepository
                .Setup(r => r.GetAllAttached())
                .Returns(orderMockQueryable);

            _mockOrderRepository
                .Setup(r => r.UpdateAsync(It.IsAny<Order>()))
                .ReturnsAsync(true)
            .Verifiable();

            var result = await _orderService.FinalizeOrder(mockOrder.Id);

            Assert.IsTrue(result, "FinalizeOrder should return true for valid orders.");
            Assert.IsTrue(mockOrder.IsCompleted, "The order's IsCompleted property should be true.");
            Assert.AreEqual(8, mockProducts[0].StockQuantity, "StockQuantity of Product 1 should be reduced.");
            Assert.AreEqual(2, mockProducts[1].StockQuantity, "StockQuantity of Product 2 should be reduced.");
            Assert.IsTrue(mockOrder.Payments.All(p => p.Status == Status.Completed), "All payment statuses should be set to Completed.");
        }

        [Test]
        public async Task GetTransactionHistoryAsync_ShouldReturnCorrectTransactionHistory()
        {
            var userId = "user1";
            var orderId = 2;

            var mockOrders = new List<Order>
            {
                new Order
                {
                    Id = 1,
                    UserId = userId,
                    Payments = new List<Payment>
                    {
                        new Payment { PaymentMethod = PaymentMethod.DebitCard, Amount = 100, Status = Status.Completed }
                    }
                },
                new Order
                {
                    Id = 2,
                    UserId = userId,
                    Payments = new List<Payment>
                    {
                        new Payment { PaymentMethod = PaymentMethod.BankTransfer, Amount = 200, Status = Status.Pending }
                    }
                },
                new Order
                {
                    Id = 3,
                    UserId = userId,
                    Payments = new List<Payment>
                    {
                        new Payment { PaymentMethod = PaymentMethod.Cash, Amount = 150, Status = Status.Completed }
                    }
                }
            };

            IQueryable<Order> orderMockQueryable = mockOrders.BuildMock();

            var mockOrderRepository = new Mock<IRepository<Order, int>>();

            mockOrderRepository
                .Setup(repo => repo.GetAllAttached())
                .Returns(orderMockQueryable);

            var orderService = new OrderService(mockOrderRepository.Object);

            var result = await orderService.GetTransactionHistoryAsync(orderId, userId);

            Assert.IsNotNull(result);
            Assert.AreEqual(orderId, result.OrderId);
            Assert.AreEqual(2, result.CustomOrderNumber);
            Assert.AreEqual(1, result.Payments.Count);
            Assert.AreEqual(200, result.Payments.First().Amount);
            Assert.AreEqual(Status.Pending, result.Payments.First().Status);
        }


    }
}
