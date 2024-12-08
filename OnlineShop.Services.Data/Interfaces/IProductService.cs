using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.Data.Models;
using OnlineShop.Web.ViewModels.Product;

namespace OnlineShop.Services.Data.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync(int? genderId, int? clothingTypeId, string searchTerm);
        Task CreateProductAsync(CreateProductViewModel product, string userId);
        Task<ProductEditViewModel?> GetEditProductViewModelAsync(int productId, string userId);
        Task<bool> UpdateProductAsync(ProductEditViewModel product, string userId);
        Task<List<SelectListItem>> GetGendersAsync();
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<List<SelectListItem>> GetClothingTypesAsync();
        Task SubmitReview(int productId, string userId, int rating, string comment);
        Task<ProductDetailsViewModel?> ViewDetailsAboutProductAsync(int id, string userId);
        Task<bool> DeleteProductAsync(int id);
    }
}
