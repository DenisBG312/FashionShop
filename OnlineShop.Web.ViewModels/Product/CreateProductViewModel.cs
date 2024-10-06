using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Common;

namespace OnlineShop.Web.ViewModels.Product
{
    using static EntityValidationConstants.Product;
    public class CreateProductViewModel
    {
        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Required]
        [Range(AmountMinValue, AmountMaxValue)]
        public decimal Price { get; set; }
        [Url]
        public string? ImageUrl { get; set; }
        [Required]
        [Range(StockQuantityMinValue, StockQuantityMaxValue)]
        public int StockQuantity { get; set; }
    }
}
