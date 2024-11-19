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
    public class ClothingTypeConfiguration : IEntityTypeConfiguration<ClothingType>
    {
        public void Configure(EntityTypeBuilder<ClothingType> builder)
        {
            builder.HasData(GenerateClothingTypes());
        }

        private IEnumerable<ClothingType> GenerateClothingTypes()
        {
            return new List<ClothingType>
            {
                new ClothingType { Id = 1, Name = "T-Shirt" },
                new ClothingType { Id = 2, Name = "Jacket" },
                new ClothingType { Id = 3, Name = "Shoes" },
                new ClothingType { Id = 4, Name = "Jeans" },
                new ClothingType { Id = 5, Name = "Shirts" },
                new ClothingType { Id = 6, Name = "Shorts" }
            };
        }
    }
}
