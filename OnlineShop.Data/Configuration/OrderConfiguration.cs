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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasData(GenerateOrders());
        }

        private IEnumerable<Order> GenerateOrders()
        {
            return new List<Order>
            {
                new Order
                {
                    Id = 1,
                    UserId = "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                    OrderDate = DateTime.Now,
                    TotalAmount = 150.00m,
                    IsCompleted = false
                },
                new Order
                {
                    Id = 2,
                    UserId = "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                    OrderDate = DateTime.Now.AddDays(-1),
                    TotalAmount = 75.50m,
                    IsCompleted = true
                }
            };
        }
    }
}
