using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Data.Models;
using OnlineShop.Web.ViewModels.Product;

namespace OnlineShop.Services.Data.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync(int? genderId, int? clothingTypeId, string searchTerm);
        Task CreateProductAsync(CreateProductViewModel product, string userId);
        Task<ProductEditViewModel?> GetEditProductViewModelAsync(int productId, string userId);
    }
}
