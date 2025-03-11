using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Common;

namespace OnlineShop.Data.Models
{
    using static EntityValidationConstants.OrderProduct;
    public class OrderProduct
    {
        [Required]
        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; } = null!;
        [Required]
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;
        [Range(QuantityMinValue, QuantityMaxValue)]
        public int Quantity { get; set; }
        [Range(AmountMinValue, AmountMaxValue)]
        public decimal UnitPrice { get; set; }
        [Required]
        public int SizeId { get; set; }
        [ForeignKey(nameof(SizeId))]
        public Size Size { get; set; }
        [NotMapped]
        public decimal TotalPrice => Quantity * UnitPrice;
    }
}
