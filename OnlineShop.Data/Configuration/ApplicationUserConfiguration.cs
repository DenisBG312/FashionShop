using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Data.Configuration
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData(GenerateUser());
        }

        private ApplicationUser GenerateUser()
        {
            var user = new ApplicationUser()
            {
                Id = "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                FirstName = "Denis",
                LastName = "Tsranski",
                Email = "bgdenibg@gmail.com",
                NormalizedEmail = "bgdenibg@gmail.com".ToUpper(),
                UserName = "bgdenibg@gmail.com",
                NormalizedUserName = "bgdenibg@gmail.com".ToUpper(),
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            var password = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = password.HashPassword(user, "Denkata123!");

            return user;
        }
    }
}
