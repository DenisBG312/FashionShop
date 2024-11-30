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
            builder.HasData(GenerateRegularUser(), GenerateAdminUser());
        }

        private ApplicationUser GenerateRegularUser()
        {
            var user = new ApplicationUser
            {
                Id = "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                FirstName = "Denis",
                LastName = "Tsranski",
                Email = "bgdenibg@gmail.com",
                NormalizedEmail = "BGDENIBG@GMAIL.COM",
                UserName = "bgdenibg@gmail.com",
                NormalizedUserName = "BGDENIBG@GMAIL.COM",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            var password = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = password.HashPassword(user, "Denkata123!");

            return user;
        }

        private ApplicationUser GenerateAdminUser()
        {
            var admin = new ApplicationUser
            {
                Id = "8a914c36-ea3f-49f0-9ad3-3d32134b2f8c",
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@onlineshop.com",
                NormalizedEmail = "ADMIN@ONLINESHOP.COM",
                UserName = "admin@onlineshop.com",
                NormalizedUserName = "ADMIN@ONLINESHOP.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            var password = new PasswordHasher<ApplicationUser>();
            admin.PasswordHash = password.HashPassword(admin, "Admin1234");

            return admin;
        }
    }
}
