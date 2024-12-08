using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MockQueryable;
using Moq;
using OnlineShop.Data.Models;
using OnlineShop.Data.Models.Enums.Payment;
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

        [Test]
        public async Task AddToCartAsync_ShouldCreateNewCart_WhenCartDoesNotExist()
        {
            var userId = "user123";
            var productId = 1;
            var quantity = 2;

            var product = new Product
            {
                Id = productId,
                Price = 50m,
                IsOnSale = false    
            };

            _mockShoppingCartRepository
                .Setup(repo => repo.GetAllAttached())
                .Returns(new List<ShoppingCart>().AsQueryable().BuildMock());

            _mockProductRepository
                .Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync(product);

            var result = await _shoppingCartService.AddToCartAsync(userId, productId, quantity);

            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public async Task AddToCartAsync_ShouldAddProductToExistingCart()
        {
            var userId = "user123";
            var productId = 1;
            var quantity = 2;

            var existingCart = new ShoppingCart
            {
                Id = 1,
                UserId = userId,
                Amount = 0,
                ShoppingCartProducts = new List<ShoppingCartProduct>()
            };

            var product = new Product
            {
                Id = productId,
                Price = 50,
                IsOnSale = false
            };

            _mockShoppingCartRepository
                .Setup(repo => repo.GetAllAttached())
                .Returns(new List<ShoppingCart>(){existingCart}.AsQueryable().BuildMock());

            _mockProductRepository
                .Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync(product);

            var result = await _shoppingCartService.AddToCartAsync(userId, productId, quantity);

            Assert.IsTrue(result.IsSuccess);
        }

        [Test]
        public async Task AddToCartAsync_WhenProductDoesNotExist_ShouldReturnFalse()
        {
            var userId = "user123";
            var productId = 5;
            var quantity = 2;

            var mockShoppingCart = new ShoppingCart
            {
                Id = 1,
                UserId = userId,
                Amount = 0,
                ShoppingCartProducts = new List<ShoppingCartProduct>()
            };

            IQueryable<ShoppingCart> shoppingCartMockQueryable = new List<ShoppingCart> { mockShoppingCart }.BuildMock();


            _mockShoppingCartRepository
                .Setup(r => r.GetAllAttached())
                .Returns(shoppingCartMockQueryable);

            _mockProductRepository
                .Setup(repo => repo.GetByIdAsync(productId))
                .ReturnsAsync((Product)null!);

            var result = await _shoppingCartService.AddToCartAsync(userId, productId, quantity);

            Assert.IsFalse(result.IsSuccess);
            Assert.That(result.ErrorMessage, Is.EqualTo("Product not found."));
        }

        [Test]
        public async Task PlaceOrderAsync_WhenShoppingCartIsEmpty_ShouldGiveErrorMessage()
        {
            var shoppingCartId = 1;
            var userId = "user123";
            var paymentMethod = PaymentMethod.Cash;

            var shoppingCart = new ShoppingCart
            {
                Id = shoppingCartId,
                UserId = userId,
                ShoppingCartProducts = new List<ShoppingCartProduct>()
            };

            IQueryable<ShoppingCart> shoppingCartMockQueryable = new List<ShoppingCart> { shoppingCart }.BuildMock();

            _mockShoppingCartRepository
                .Setup(repo => repo.GetAllAttached())
                .Returns(shoppingCartMockQueryable);

            var result = await _shoppingCartService.PlaceOrderAsync(shoppingCartId, userId, paymentMethod);

            Assert.That(result.IsSuccess, Is.False);
            Assert.That(result.ErrorMessage, Is.EqualTo("Your shopping cart is empty."));
        }

        [Test]
        public async Task PlaceOrderAsync_ShouldCalculateTotalAmountCorrectly()
        {
            // Arrange
            var shoppingCartId = 1;
            var userId = "user123";
            var paymentMethod = PaymentMethod.Cash;

            var shoppingCartProducts = new List<ShoppingCartProduct>
            {
                new ShoppingCartProduct
                {
                    Product = new Product { Price = 100, IsOnSale = false },
                    Quantity = 2
                },
                new ShoppingCartProduct
                {
                    Product = new Product { Price = 50, IsOnSale = true, DiscountPercentage = 20},
                    Quantity = 3
                }
            };

            var shoppingCart = new ShoppingCart
            {
                Id = shoppingCartId,
                UserId = userId,
                ShoppingCartProducts = shoppingCartProducts
            };

            IQueryable<ShoppingCart> shoppingCartMockQueryable = new List<ShoppingCart> { shoppingCart }.BuildMock();
            _mockShoppingCartRepository
                .Setup(repo => repo.GetAllAttached())
                .Returns(shoppingCartMockQueryable);

            _mockOrderRepository.Setup(repo => repo.AddAsync(It.IsAny<Order>())).Returns(Task.CompletedTask);

            var result = await _shoppingCartService.PlaceOrderAsync(shoppingCartId, userId, paymentMethod);

            Assert.That(result.IsSuccess, Is.True);
        }

        [Test]
        public async Task UpdateQuantityAsync_ShouldUpdateQuantityAndRecalculateAmount()
        {
            var shoppingCartId = 1;
            var productId = 101;
            var newQuantity = 5;

            var shoppingCartProduct = new ShoppingCartProduct
            {
                ProductId = productId,
                Quantity = 2,
                Product = new Product { Price = 100m, IsOnSale = false }
            };

            var shoppingCart = new ShoppingCart
            {
                Id = shoppingCartId,
                Amount = 200m,
                ShoppingCartProducts = new List<ShoppingCartProduct> { shoppingCartProduct }
            };

            IQueryable<ShoppingCart> shoppingCartMockQueryable = new List<ShoppingCart> { shoppingCart }.BuildMock();
            _mockShoppingCartRepository
                .Setup(repo => repo.GetAllAttached())
                .Returns(shoppingCartMockQueryable);

            var result = await _shoppingCartService.UpdateQuantityAsync(shoppingCartId, productId, newQuantity);

            Assert.IsTrue(result);
            Assert.That(shoppingCart.Amount, Is.EqualTo(500));
            Assert.That(shoppingCart.ShoppingCartProducts.First().Quantity, Is.EqualTo(newQuantity));
        }

        [Test]
        public async Task RemoveFromCartAsync_ShouldRemoveProductAndRecalculateAmount()
        {
            var shoppingCartId = 1;
            var productId = 101;

            var shoppingCartProduct = new ShoppingCartProduct
            {
                ProductId = productId,
                Quantity = 2,
                Product = new Product { Price = 100, IsOnSale = false }
            };

            var shoppingCart = new ShoppingCart
            {
                Id = shoppingCartId,
                Amount = 200,
                ShoppingCartProducts = new List<ShoppingCartProduct> { shoppingCartProduct }
            };

            IQueryable<ShoppingCart> shoppingCartMockQueryable = new List<ShoppingCart> { shoppingCart }.BuildMock();

            _mockShoppingCartRepository
                .Setup(repo => repo.GetAllAttached())
                .Returns(shoppingCartMockQueryable);

            var result = await _shoppingCartService.RemoveFromCartAsync(shoppingCartId, productId);

            Assert.IsTrue(result);
            Assert.IsEmpty(shoppingCart.ShoppingCartProducts);
            Assert.That(shoppingCart.Amount, Is.EqualTo(0));
        }
    }
}
