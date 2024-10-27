using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Data.Models;
using OnlineShop.Services.Data.Interfaces;
using OnlineShop.Web.ViewModels.Product;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using OnlineShop.Data.Repository;

namespace OnlineShop.Services.Data
{
    public class ProductService : IProductService
    {
        private readonly BaseRepository<Product, int> _productRepository;

        public ProductService(BaseRepository<Product, int> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IEnumerable<Product>> GetProductsAsync(int? genderId, int? clothingTypeId, string searchTerm)
        {
            var productsQuery = await _productRepository.GetAllAsync();

            if (genderId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.GenderId == genderId.Value);
            }

            if (clothingTypeId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.ClothingTypeId == clothingTypeId.Value);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                productsQuery = productsQuery.Where(p => p.Name.Contains(searchTerm));
            }

            return productsQuery.ToList();
        }

        public Task CreateProductAsync(CreateProductViewModel product, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ProductEditViewModel?> GetEditProductViewModelAsync(int productId, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProductAsync(ProductEditViewModel product, string userId)
        {
            throw new NotImplementedException();
        }

        //public async Task CreateProductAsync(CreateProductViewModel product, string userId)
        //{
        //    var newProduct = new Product
        //    {
        //        Name = product.Name,
        //        Description = product.Description,
        //        Price = product.Price,
        //        StockQuantity = product.StockQuantity,
        //        ImageUrl = product.ImageUrl,
        //        GenderId = product.GenderId,
        //        ClothingTypeId = product.ClothingTypeId,
        //        UserId = userId
        //    };

        //    await _context.Products.AddAsync(newProduct);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task<ProductEditViewModel?> GetEditProductViewModelAsync(int productId, string userId)
        //{
        //    var product = await _context.Products
        //        .Include(p => p.Gender)
        //        .Include(p => p.ClothingType)
        //        .Where(p => p.Id == productId && p.UserId == userId)
        //        .FirstOrDefaultAsync(p => p.Id == productId);

        //    if (product == null) return null;

        //    var productEditViewModel = new ProductEditViewModel
        //    {
        //        Id = product.Id,
        //        Name = product.Name,
        //        Description = product.Description,
        //        Price = product.Price,
        //        StockQuantity = product.StockQuantity,
        //        ImageUrl = product.ImageUrl,
        //        GenderId = product.GenderId,
        //        ClothingTypeId = product.ClothingTypeId,
        //        Genders = await GetGendersAsync(),
        //        ClothingTypes = await GetClothingTypesAsync()
        //    };

        //    return productEditViewModel;
        //}

        //public async Task<bool> UpdateProductAsync(ProductEditViewModel product, string userId)
        //{
        //    var productEntity = await _context.Products
        //        .Where(p => p.Id == product.Id && p.UserId == userId)
        //        .FirstOrDefaultAsync();

        //    if (productEntity == null)
        //    {
        //        return false;
        //    }

        //    productEntity.Name = product.Name;
        //    productEntity.Description = product.Description;
        //    productEntity.Price = product.Price;
        //    productEntity.StockQuantity = product.StockQuantity;
        //    productEntity.GenderId = product.GenderId;
        //    productEntity.ClothingTypeId = product.ClothingTypeId;
        //    productEntity.ImageUrl = product.ImageUrl;

        //    await _context.SaveChangesAsync();

        //    return true;
        //}


        //public async Task<List<SelectListItem>> GetGendersAsync()
        //{
        //    return await _context.Genders
        //        .Select(g => new SelectListItem
        //        {
        //            Value = g.Id.ToString(),
        //            Text = g.Name
        //        }).ToListAsync();
        //}

        //public async Task<List<SelectListItem>> GetClothingTypesAsync()
        //{
        //    return await _context.ClothingTypes
        //        .Select(c => new SelectListItem
        //        {
        //            Value = c.Id.ToString(),
        //            Text = c.Name
        //        }).ToListAsync();
        //}
    }
}
