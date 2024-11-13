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
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasData(GenerateReviews());
        }

        private IEnumerable<Review> GenerateReviews()
        {
            return new List<Review>()
            {
                new Review()
                {
                    Id = 1,
                    ProductId = 1,
                    UserId = "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                    Rating = 4,
                    Comment = "I really liked wearing these shoes. They are very comfortable"
                }
            };
        }
    }
}
