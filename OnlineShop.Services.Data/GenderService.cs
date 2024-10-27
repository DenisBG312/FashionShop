using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Data.Models;
using OnlineShop.Data.Repository;
using OnlineShop.Services.Data.Interfaces;

namespace OnlineShop.Services.Data
{
    public class GenderService : IGenderService
    {
        private readonly BaseRepository<Gender, int> _genderRepository;

        public GenderService(BaseRepository<Gender, int> genderRepository)
        {
            _genderRepository = genderRepository;
        }
        public async Task<IEnumerable<Gender>> GetAllGendersAsync()
        {
            return await _genderRepository.GetAllAsync();
        }
    }
}
