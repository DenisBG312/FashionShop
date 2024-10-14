using OnlineShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Web.ViewModels.Product
{
    public class ProductDetailsViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public int StockQuantity { get; set; }
        public string Gender { get; set; } = null!;
        public string ClothingType { get; set; } = null!;
        public string? PostedBy { get; set; }
        public string UserId { get; set; } = null!;
        public List<Review> Reviews { get; set; }
    }
}
