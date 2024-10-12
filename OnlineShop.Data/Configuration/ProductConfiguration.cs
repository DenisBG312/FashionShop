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
                    Name = "Nike Air Max Plus",
                    Description = "One of the best nike models ever created",
                    Price = 1500.00m,
                    ImageUrl = "https://static.nike.com/a/images/t_PDP_936_v1/f_auto,q_auto:eco/66d8f65e-6ecd-414c-bd03-e50a996f7de0/NIKE+AIR+MAX+PLUS.png",
                    StockQuantity = 100,
                    GenderId = 1,
                    ClothingTypeId = 3
                },
                new Product
                {
                    Id = 2,
                    Name = "Trapstar Shooters Hooded Puffer Black",
                    Description = "One of the greatest puffers ever created",
                    Price = 75.00m,
                    ImageUrl = "https://images.stockx.com/images/Trapstar-Shooters-Hooded-Puffer-Black-Reflective.jpg?fit=fill&bg=FFFFFF&w=700&h=500&fm=webp&auto=compress&q=90&dpr=2&trim=color&updated_at=1673460322",
                    StockQuantity = 50,
                    GenderId = 2,
                    ClothingTypeId = 2
                }
            };
        }
    }
}
