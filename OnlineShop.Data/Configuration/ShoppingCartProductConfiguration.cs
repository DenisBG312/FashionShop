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
    public class ShoppingCartProductConfiguration : IEntityTypeConfiguration<ShoppingCartProduct>
    {
        public void Configure(EntityTypeBuilder<ShoppingCartProduct> builder)
        {
            builder.HasKey(sp => new { sp.ShoppingCartId, sp.ProductId });

            builder.HasData(GenerateShoppingCartProducts());
        }

        private IEnumerable<ShoppingCartProduct> GenerateShoppingCartProducts()
        {
            return new List<ShoppingCartProduct>()
            {
                new ShoppingCartProduct()
                {
                    ShoppingCartId = 1,
                    ProductId = 1,
                    Quantity = 1
                },
                new ShoppingCartProduct()
                {
                    ShoppingCartId = 1,
                    ProductId = 2,
                    Quantity = 1
                }
            };
        }
    }
}
