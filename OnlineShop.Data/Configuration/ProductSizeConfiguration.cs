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
    public class ProductSizeConfiguration : IEntityTypeConfiguration<ProductSize>
    {
        public void Configure(EntityTypeBuilder<ProductSize> builder)
        {
            builder.HasKey(ps => new { ps.ProductId, ps.SizeId });

            builder.HasData(Seed());
        }

        private List<ProductSize> Seed()
        {
            return new List<ProductSize>()
            {
                new ProductSize() { SizeId = 1, ProductId = 1, StockQuantity = 10 },
                new ProductSize() {SizeId = 2, ProductId = 2, StockQuantity = 5},
                new ProductSize() {SizeId = 3, ProductId = 3, StockQuantity = 19},
                new ProductSize() { SizeId = 1, ProductId = 4, StockQuantity = 10 },
                new ProductSize() {SizeId = 4, ProductId = 5, StockQuantity = 5},
                new ProductSize() {SizeId = 1, ProductId = 6, StockQuantity = 19}
            };
        }
    }
}
