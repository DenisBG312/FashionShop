﻿using OnlineShop.Data.Models;
using OnlineShop.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using OnlineShop.Services.Data;
using Microsoft.AspNetCore.Identity;
using MockQueryable;
using OnlineShop.Data.Repository;
using OnlineShop.Web.ViewModels.Product;

namespace OnlineShop.Services.Tests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private static string userId = "user123";

        private Mock<IRepository<Product, int>> _productRepository;
        private Mock<IRepository<Review, int>> _reviewRepository;
        private Mock<IRepository<ClothingType, int>> _clothingTypeRepository;
        private Mock<IRepository<Gender, int>> _genderRepository;
        private Mock<UserManager<ApplicationUser>> _userManager;
        private Mock<ProductSizeRepository> _productSizeRepository;
        private Mock<IRepository<Size, int>> _sizeRepository;

        private ProductService _productService;

        [SetUp]
        public void Startup()
        {
            _productRepository = new Mock<IRepository<Product, int>>();
            _reviewRepository = new Mock<IRepository<Review, int>>();
            _clothingTypeRepository = new Mock<IRepository<ClothingType, int>>();
            _genderRepository = new Mock<IRepository<Gender, int>>();
            _productSizeRepository = new Mock<ProductSizeRepository>();
            _sizeRepository = new Mock<IRepository<Size, int>>();

            _userManager = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(),
                null, null, null, null, null, null, null, null);

            _productService = new ProductService(
                _productRepository.Object,
                _reviewRepository.Object,
                _userManager.Object,
                _genderRepository.Object,
                _clothingTypeRepository.Object,
                _productSizeRepository.Object,
                _sizeRepository.Object
            );
        }

        [Test]
        public async Task GetAllProducts_ShouldReturnAllProducts()
        {
            var mockProducts = new List<Product>()
            {
                new Product()
                {
                    Id = 1, ClothingTypeId = 1, GenderId = 1, IsOnSale = false, Name = "Laptop", Price = 600,
                    UserId = userId
                }
            };

            IQueryable<Product> orderMockQueryable = mockProducts.BuildMock();

            _productRepository
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(orderMockQueryable.ToList());

            var result = await _productService.GetAllProductsAsync();

            Assert.That(result.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task GetProductsAsync_ShouldReturnFilteredProducts()
        {
            var mockProducts = new List<Product>()
            {
                new Product() { Id = 1, Name = "Shirt", GenderId = 1, ClothingTypeId = 1, Price = 100 },
                new Product() { Id = 2, Name = "Pants", GenderId = 2, ClothingTypeId = 2, Price = 200 }
            };

            _productRepository
                .Setup(r => r.GetAllAttached())
                .Returns(mockProducts.AsQueryable());

            var result = await _productService.GetProductsAsync(1, 1, "Shirt", null, null, null, null, "popular");

            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Name, Is.EqualTo("Shirt"));
        }

        [Test]
        public async Task CreateProductAsync_ShouldCreateNewProduct()
        {
            var newProduct = new CreateProductViewModel
            {
                Name = "Jacket",
                Description = "Warm jacket",
                Price = 150,
                ImageUrl = "url_to_image",
                GenderId = 1,
                ClothingTypeId = 1
            };

            _productRepository.Setup(r => r.AddAsync(It.IsAny<Product>()));
            _productRepository.Setup(r => r.SaveChangesAsync());

            _productRepository.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
        }

        [Test]
        public async Task UpdateProductAsync_ShouldReturnFalse_WhenProductDoesNotExist()
        {
            var productEditViewModel = new ProductEditViewModel { Id = 1 };

            _productRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Product)null);

            var result = await _productService.UpdateProductAsync(productEditViewModel, userId);

            Assert.IsFalse(result);
        }

        [Test]
        public async Task UpdateProductAsync_ShouldReturnFalse_WhenUserDoesNotExist()
        {
            var productEditViewModel = new ProductEditViewModel { Id = 1 };

            _productRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Product { Id = 1, UserId = userId });

            var result = await _productService.UpdateProductAsync(productEditViewModel, userId);

            Assert.IsFalse(result);
        }

    }
}
