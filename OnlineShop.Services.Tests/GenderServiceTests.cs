using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using OnlineShop.Data.Models;
using OnlineShop.Data.Repository.Interfaces;
using OnlineShop.Services.Data;

namespace OnlineShop.Services.Tests
{
    [TestFixture]
    public class GenderServiceTests
    {
        private Mock<IRepository<Gender, int>> _mockGenderRepository;
        private GenderService _genderService;

        [SetUp]
        public void Setup()
        {
            _mockGenderRepository = new Mock<IRepository<Gender, int>>();

            _genderService = new GenderService(_mockGenderRepository.Object);
        }

        [Test]
        public async Task GetAllGendersAsync_ShouldReturnAllGenders()
        {
            var genders = new List<Gender>()
            {
                new Gender() { Id = 1, Name = "Men" },
                new Gender() { Id = 2, Name = "Women" }
            };

            _mockGenderRepository
                .Setup(r => r.GetAllAsync())
                .ReturnsAsync(genders);

            var result = await _genderService.GetAllGendersAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("Men", result.First().Name);
        }
    }
}
