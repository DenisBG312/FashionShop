using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
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
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string ImageUrl { get; set; }
        
        public int GenderId { get; set; }
        public int ClothingTypeId { get; set; }
        
        public List<SelectListItem> Genders { get; set; }
        public List<SelectListItem> ClothingTypes { get; set; }
    }
}
