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
                    Name = "Women Jeans",
                    Description = "Very beautiful and comfortable jeans for women.",
                    Price = 39.99m,
                    ImageUrl = "https://images.unsplash.com/photo-1591195853828-11db59a44f6b?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    StockQuantity = 27,
                    GenderId = 2,
                    ClothingTypeId = 4,
                    UserId = "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                },
                new Product
                {
                    Id = 2,
                    Name = "Black Leather Brogan Black Boots",
                    Description = "Very comfortable boots for women.",
                    Price = 65.00m,
                    ImageUrl = "https://images.unsplash.com/photo-1605732440685-d0654d81aa30?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    StockQuantity = 40,
                    GenderId = 2,
                    ClothingTypeId = 3,
                    UserId = "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f"
                },
                new Product
                {
                    Id = 3,
                    Name = "Brown Jacket",
                    Description = "Very good-looking jacket for men.",
                    Price = 65.00m,
                    ImageUrl = "https://images.unsplash.com/photo-1591047139829-d91aecb6caea?q=80&w=1936&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    StockQuantity = 40,
                    GenderId = 1,
                    ClothingTypeId = 2,
                    UserId = "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                    IsOnSale = true,
                    DiscountPercentage = 25
                },
                new Product
                {
                    Id = 4,
                    Name = "Leather Shoes",
                    Description = "Leather shoes that are extremely comfortable for men.",
                    Price = 55.00m,
                    ImageUrl = "https://images.unsplash.com/photo-1638609348722-aa2a3a67db26?q=80&w=1945&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    StockQuantity = 17,
                    GenderId = 1,
                    ClothingTypeId = 3,
                    UserId = "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f"
                },
                new Product
                {
                    Id = 5,
                    Name = "Black T-Shirt",
                    Description = "Stylish t-shirt for women.",
                    Price = 24.99m,
                    ImageUrl = "https://images.unsplash.com/photo-1583743814966-8936f5b7be1a?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    StockQuantity = 23,
                    GenderId = 2,
                    ClothingTypeId = 1,
                    UserId = "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f"
                },
                new Product
                {
                    Id = 6,
                    Name = "Clothing Set",
                    Description = "A clothing set for men specially gathered.",
                    Price = 24.99m,
                    ImageUrl = "https://images.unsplash.com/photo-1467043237213-65f2da53396f?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D",
                    StockQuantity = 23,
                    GenderId = 2,
                    ClothingTypeId = 1,
                    UserId = "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                    IsOnSale = true,
                    DiscountPercentage = 15
                },
            };
        }
    }
}
