using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MockQueryable;
using Moq;
using OnlineShop.Data.Models;
using OnlineShop.Data.Repository.Interfaces;
using OnlineShop.Services.Data;

namespace OnlineShop.Services.Tests
{
    [TestFixture]
    public class ShoppingCartServiceTests
    {
        private Mock<IRepository<ShoppingCart, int>> _mockShoppingCartRepository;
        private Mock<IRepository<Product, int>> _mockProductRepository;
        private Mock<IRepository<Order, int>> _mockOrderRepository;
        private Mock<IRepository<Payment, int>> _mockPaymentRepository;
        private Mock<IRepository<OrderProduct, int>> _mockOrderProductRepository;

        private ShoppingCartService _shoppingCartService;
        [SetUp]
        public void Setup()
        {
            _mockShoppingCartRepository = new Mock<IRepository<ShoppingCart, int>>();
            _mockProductRepository = new Mock<IRepository<Product, int>>();
            _mockOrderRepository = new Mock<IRepository<Order, int>>();
            _mockPaymentRepository = new Mock<IRepository<Payment, int>>();
            _mockOrderProductRepository = new Mock<IRepository<OrderProduct, int>>();

            _shoppingCartService = new ShoppingCartService(
                _mockShoppingCartRepository.Object,
                _mockProductRepository.Object,
                _mockOrderRepository.Object,
                _mockPaymentRepository.Object,
                _mockOrderProductRepository.Object
            );
        }

        [Test]
        public async Task GetCartAsync_ShouldReturnCorrectShoppingCart_WhenUserIdExists()
        {
            var userId = "user123";
            var mockShoppingCart = new ShoppingCart
            {
                Id = 1,
                UserId = userId,
                ShoppingCartProducts = new List<ShoppingCartProduct>
                {
                    new ShoppingCartProduct
                    {
                        Product = new Product { Id = 1, Name = "Test Product", Price = 100 },
                        Quantity = 2
                    }
                }
            };

            _mockShoppingCartRepository
                .Setup(repo => repo.GetAllAttached())
                .Returns(new List<ShoppingCart> { mockShoppingCart }.AsQueryable().BuildMock());

            var result = await _shoppingCartService.GetCartAsync(userId);

            Assert.IsNotNull(result);
            Assert.AreEqual(userId, result.UserId);
            Assert.AreEqual(1, result.ShoppingCartProducts.Count);
            Assert.AreEqual("Test Product", result.ShoppingCartProducts.First().Product.Name);
            Assert.AreEqual(2, result.ShoppingCartProducts.First().Quantity);
        }
    }
}
