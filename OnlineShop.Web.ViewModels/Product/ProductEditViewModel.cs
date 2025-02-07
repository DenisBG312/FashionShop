using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Web.ViewModels.Product
{
    public class ProductEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountedPrice => IsOnSale && DiscountPercentage.HasValue
            ? Price - (Price * DiscountPercentage.Value / 100)
            : Price;
        [DisplayName("Stock Quantity")]
        public int StockQuantity { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsOnSale { get; set; }
        [Range(0, 100, ErrorMessage = "You have to write an integer number between 0 and 100.")]
        public int? DiscountPercentage { get; set; }
        [DisplayName("Gender")]
        
        public int GenderId { get; set; }
        [DisplayName("Clothing Type")]
        public int ClothingTypeId { get; set; }
        
        public List<SelectListItem> Genders { get; set; }
        public List<SelectListItem> ClothingTypes { get; set; }
    }
}
