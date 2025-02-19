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
                    UserId = "9bd65753-4ac3-437f-a1ba-e9320baf1097",
                    OrderDate = DateTime.Now,
                    TotalAmount = 144.98m,
                    IsCompleted = false
                }
            };
        }
    }
}
