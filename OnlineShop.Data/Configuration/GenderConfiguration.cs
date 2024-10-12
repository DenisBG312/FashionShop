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
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.HasData(GenerateGenders());
        }

        private IEnumerable<Gender> GenerateGenders()
        {
            return new List<Gender>
            {
                new Gender { Id = 1, Name = "Men" },
                new Gender { Id = 2, Name = "Women" },
                new Gender { Id = 3, Name = "Kids" }
            };
        }
    }
}
