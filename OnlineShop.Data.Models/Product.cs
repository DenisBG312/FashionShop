using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using OnlineShop.Common;

namespace OnlineShop.Data.Models
{
    using static EntityValidationConstants.Product;
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [Required]
        [Range(AmountMinValue, AmountMaxValue)]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        public int StockQuantity { get; set; }
        [Required]
        public int GenderId { get; set; }
        [ForeignKey(nameof(GenderId))]
        public Gender Gender { get; set; } = null!;
        [Required]
        public int ClothingTypeId { get; set; }
        [ForeignKey(nameof(ClothingTypeId))]
        public ClothingType ClothingType { get; set; } = null!;
        public string? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; } = new HashSet<OrderProduct>();
        public ICollection<ShoppingCartProduct> ShoppingCartProducts { get; set; } = new HashSet<ShoppingCartProduct>();
    }
}
