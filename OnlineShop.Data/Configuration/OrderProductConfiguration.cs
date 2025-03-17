using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Data.Models;

namespace OnlineShop.Data.Configuration
{
    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasKey(op => new { op.OrderId, op.ProductId, op.SizeId });

            builder.HasData(GenerateOrderProducts());
        }

        private IEnumerable<OrderProduct> GenerateOrderProducts()
        {
            return new List<OrderProduct>()
            {
                new OrderProduct
                {
                    OrderId = 1,
                    ProductId = 1,
                    SizeId = 2,
                    Quantity = 2,
                    UnitPrice = 39.99m
                },
                new OrderProduct
                {
                    OrderId = 1,
                    ProductId = 2,
                    SizeId = 1,
                    Quantity = 1,
                    UnitPrice = 65.00m
                }
            };
        }
    }
}
