using Moq;
using OnlineShop.Data.Models;
using OnlineShop.Data.Repository.Interfaces;
using OnlineShop.Services.Data;
using OnlineShop.Services.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MockQueryable;

namespace OnlineShop.Services.Tests
{
    [TestFixture]
    public class ProductWishlistServiceTests
    {
        private static string userId = "user123";

        private Mock<IRepository<ProductWishlist, int>> _mockWishlistRepository;
        private Mock<IRepository<Product, int>> _mockProductRepository;
        private ProductWishlistService _productWishlistService;

        [SetUp]
        public void Setup()
        {
            _mockWishlistRepository = new Mock<IRepository<ProductWishlist, int>>();
            _mockProductRepository = new Mock<IRepository<Product, int>>();

            _productWishlistService =
                new ProductWishlistService(_mockWishlistRepository.Object, _mockProductRepository.Object);
        }


        [Test]
        public async Task GetAllWishlistProducts_ShouldReturnCorrectProductsForUser()
        {
            var mockWishlist = new List<ProductWishlist>
            {
                new ProductWishlist { UserId = userId, Product = new Product { Id = 101, Name = "Product1" } },
                new ProductWishlist { UserId = userId, Product = new Product { Id = 102, Name = "Product2" } },
                new ProductWishlist { UserId = userId, Product = new Product { Id = 103, Name = "Product3" } },
                new ProductWishlist { UserId = "anotherUser", Product = new Product { Id = 104, Name = "Product4" } }
            };

            IQueryable<ProductWishlist> productWishlistMockQueryable = mockWishlist.BuildMock();

            _mockWishlistRepository
                .Setup(r => r.GetAllAttached())
                .Returns(productWishlistMockQueryable);

            var result = await _productWishlistService.GetUserWishlistAsync(userId);

            Assert.AreEqual(3, result.Count());
        }

        [Test]
        public async Task AddWishlistProduct_AddProperlyProduct()
        {

            var newProductToAdd = new ProductWishlist()
            {
                Id = 1,
                ProductId = 100,
                AddedDate = DateTime.Now,
                IsOnSale = false,
                UserId = userId
            };

            var mockProduct = new Product
            {
                Id = 100,
                Name = "Product100",
                IsOnSale = false
            };

            _mockWishlistRepository
                .Setup(r => r.AddAsync(It.Is<ProductWishlist>(pw => pw.ProductId == newProductToAdd.ProductId && pw.UserId == newProductToAdd.UserId)))
                .Returns(Task.CompletedTask);

            _mockProductRepository
                .Setup(r => r.GetByIdAsync(100))
                .ReturnsAsync(mockProduct);

            var result = await _productWishlistService.AddToWishlistAsync(userId, 100);

            Assert.That(result, Is.True);

        }
    }
}
