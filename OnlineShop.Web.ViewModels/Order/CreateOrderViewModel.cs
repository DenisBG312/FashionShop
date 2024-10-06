using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Web.ViewModels.Order
{
    public class CreateOrderViewModel
    {
        // Property to hold the selected product ID
        [Required(ErrorMessage = "Please select a product.")]
        public int ProductId { get; set; }

        // Property for quantity
        [Required(ErrorMessage = "Please enter a quantity.")]
        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100.")]
        public int Quantity { get; set; }
        public List<SelectListItem> Products { get; set; } = new List<SelectListItem>();
        public List<OrderProductInputModel> OrderProducts { get; set; } = new List<OrderProductInputModel>();
    }
}