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
                    UserId = "89c2eda5-ff48-4f89-ace6-8d47a02c0af1",
                    Rating = 4,
                    Comment = "I really liked wearing these shoes. They are very comfortable"
                }
            };
        }
    }
}
