using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Data.Models;
using OnlineShop.Data.Repository;
using OnlineShop.Data.Repository.Interfaces;
using OnlineShop.Services.Data.Interfaces;

namespace OnlineShop.Services.Data
{
    public class ClothingTypeService : IClothingTypeService
    {
        private readonly IRepository<ClothingType, int> _clothingTypeRepository;
        public ClothingTypeService(IRepository<ClothingType, int> clothingTypeRepository)
        {
            _clothingTypeRepository = clothingTypeRepository;
        }

        public async Task<IEnumerable<ClothingType>> GetAllClothingTypesAsync()
        {
            return await _clothingTypeRepository.GetAllAsync();
        }
    }
}
