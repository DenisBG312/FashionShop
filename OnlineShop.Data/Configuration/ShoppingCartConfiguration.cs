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
                    UserId = "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                    Amount = 1575.00m,
                },
            };
        }
    }
}
