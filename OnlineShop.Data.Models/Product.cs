﻿using System;
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
        [NotMapped]
        public int TotalStockQuantity => ProductSizes.Sum(ps => ps.StockQuantity);
        [Required]
        public bool IsOnSale { get; set; }
        public int? DiscountPercentage { get; set; }
        public decimal DiscountedPrice => IsOnSale && DiscountPercentage.HasValue
            ? Price - (Price * DiscountPercentage.Value / 100)
            : Price;
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required]
        public int SalesCount { get; set; }
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
        public ApplicationUser? User { get; set; }

        public ICollection<OrderProduct> OrderProducts { get; set; } = new HashSet<OrderProduct>();
        public ICollection<ShoppingCartProduct> ShoppingCartProducts { get; set; } = new HashSet<ShoppingCartProduct>();
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public ICollection<ProductSize> ProductSizes { get; set; } = new HashSet<ProductSize>();
    }
}
