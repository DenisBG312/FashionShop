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
                    UserId = "2a2d1de5-de58-4b33-a40e-71770a2b9479",
                    Amount = 1575.00m,
                },
            };
        }
    }
}
