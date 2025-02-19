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
                    Name = "Men Jacket",
                    Description = "Very comfortable jacket for men.",
                    Price = 65.00m,
                    ImageUrl = "https://images-cdn.ubuy.co.in/653b4be936138146b54c2af8-junge-denim-jacket-men-fleece-jacket.jpg",
                    StockQuantity = 40,
                    GenderId = 1,
                    ClothingTypeId = 2,
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
                    Name = "Patchwork Sneakers",
                    Description = "Sneakers that are extremely comfortable.",
                    Price = 55.00m,
                    ImageUrl = "https://dimg.dillards.com/is/image/DillardsZoom/mainProduct/kurt-geiger-london-kensington-denim-fabric-sneakers/00000001_zi_4062c067-b0b4-494b-a79a-6e2b6957ae45.jpg",
                    StockQuantity = 17,
                    GenderId = 2,
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
