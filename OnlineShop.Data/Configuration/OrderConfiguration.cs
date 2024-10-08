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
                    UserId = "2a2d1de5-de58-4b33-a40e-71770a2b9479",
                    OrderDate = DateTime.Now,
                    TotalAmount = 150.00m,
                    IsCompleted = false
                },
                new Order
                {
                    Id = 2,
                    UserId = "2a2d1de5-de58-4b33-a40e-71770a2b9479",
                    OrderDate = DateTime.Now.AddDays(-1),
                    TotalAmount = 75.50m,
                    IsCompleted = true
                }
            };
        }
    }
}
