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
                    UserId = "9bd65753-4ac3-437f-a1ba-e9320baf1097",
                    Rating = 5,
                    Comment = "I bought these jeans for my wife. She is more than happy, as am I."
                },
                new Review()
                {
                    Id = 2,
                    ProductId = 2,
                    UserId = "9bd65753-4ac3-437f-a1ba-e9320baf1097",
                    Rating = 3,
                    Comment = "Well the shoes are good but they are not good for running!"
                },
                new Review()
                {
                    Id = 3,
                    ProductId = 3,
                    UserId = "9bd65753-4ac3-437f-a1ba-e9320baf1097",
                    Rating = 4,
                    Comment = "Amazing jacket but the sleeves are a little too short"
                }
            };
        }
    }
}
