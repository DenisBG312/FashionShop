using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineShop.Web.ViewModels.Product
{
    public class CreateProductViewModel
    {
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Product name must be between {2} and {1} characters.")]
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

        [Required(ErrorMessage = "Gender is required.")]
        [DisplayName("Gender")]
        public int GenderId { get; set; }

        [Required(ErrorMessage = "Clothing Type is required.")]
        [DisplayName("Clothing Type")]
        public int ClothingTypeId { get; set; }

        public List<int> SelectedSizes { get; set; } = new List<int>();
        public Dictionary<int, int> StockQuantities { get; set; } = new Dictionary<int, int>();

        public List<SelectListItem> Sizes { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Genders { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ClothingTypes { get; set; } = new List<SelectListItem>();
    }
}