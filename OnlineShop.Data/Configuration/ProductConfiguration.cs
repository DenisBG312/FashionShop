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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(GenerateProducts());
        }

        private IEnumerable<Product> GenerateProducts()
        {
            return new List<Product>()
            {
                new Product
                {
                    Id = 1,
                    Name = "iPhone 15 Pro Max",
                    Description = "One of the fastest phones ever created",
                    Price = 1500.00m,
                    ImageUrl = "https://api.technopolis.bg/medias/Product-details-main-502118.jpg?context=bWFzdGVyfHJvb3R8MjE5ODA1fGltYWdlL2pwZWd8YUROaUwyZzVZUzh6TkRFME1UUTVORGt3TWpneE5DOVFjbTlrZFdOMExXUmxkR0ZwYkhNdGJXRnBibDgxTURJeE1UZ3VhbkJufGRkNDUwZDJhOWEyZDg0MGMxMDJiZjdjYjMzZWM0ODhkMjBlNmNkYzUzOWJhNWRhMDI1MWYwMTJmMTU1NGM5NWY",
                    StockQuantity = 100
                },
                new Product
                {
                    Id = 2,
                    Name = "Versace Eros",
                    Description = "One of the greatest parfumes ever created",
                    Price = 75.00m,
                    ImageUrl = "https://parfium.bg/3095-large_default/versace-eros-toaletna-voda-za-myje.jpg",
                    StockQuantity = 50
                }
            };
        }
    }
}
