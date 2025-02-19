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
    public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.HasData(GenerateShoppingCarts());
        }

        private IEnumerable<ShoppingCart> GenerateShoppingCarts()
        {
            return new List<ShoppingCart>()
            {
                new ShoppingCart()
                {
                    Id = 1,
                    UserId = "9bd65753-4ac3-437f-a1ba-e9320baf1097",
                    Amount = 104.99m
                },
            };
        }
    }
}
