using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Web.ViewModels.Product
{
    public class CreateProductViewModel
    {
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Image URL is required.")]
        [Url(ErrorMessage = "Please enter a valid URL.")]
        [DisplayName("Image URL")]
        public string ImageUrl { get; set; } = null!;

        [Required(ErrorMessage = "Stock quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity cannot be negative.")]
        [DisplayName("Stock Quantity")]
        public int StockQuantity { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [DisplayName("Gender")]
        public int GenderId { get; set; }

        [Required(ErrorMessage = "Clothing Type is required.")]
        [DisplayName("Clothing Type")]
        public int ClothingTypeId { get; set; }

        public List<SelectListItem> Genders { get; set; }
        public List<SelectListItem> ClothingTypes { get; set; }
    }
}