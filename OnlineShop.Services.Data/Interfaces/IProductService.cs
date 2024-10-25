using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Data.Models;

namespace OnlineShop.Services.Data.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync(int? genderId, int? clothingTypeId, string searchTerm);
    }
}
