
using Moq;
using OnlineShop.Data.Models;
using OnlineShop.Data.Repository.Interfaces;
using OnlineShop.Services.Data;

namespace OnlineShop.Services.Tests
{
    public class Tests
    {
        private Mock<IRepository<ClothingType, int>> _mockClothingTypeRepository;
        private ClothingTypeService _clothingTypeService;
        [SetUp]
        public void Setup()
        {
            _mockClothingTypeRepository = new Mock<IRepository<ClothingType, int>>();

            _clothingTypeService = new ClothingTypeService(_mockClothingTypeRepository.Object);
        }

        [Test]
        public async Task GetAllClothingTypesAsync_ShouldReturnAllClothingTypes()
        {
            var clothingTypes = new List<ClothingType>
            {
                new ClothingType { Id = 1, Name = "T-Shirts" },
                new ClothingType { Id = 2, Name = "Jeans" }
            };

            _mockClothingTypeRepository
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(clothingTypes);

            var result = await _clothingTypeService.GetAllClothingTypesAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("T-Shirts", result.First().Name);

            _mockClothingTypeRepository.Verify(repo => repo.GetAllAsync(), Times.Once);
        }
    }
}