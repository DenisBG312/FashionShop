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
            builder.HasData(GenerateAdminUser());
        }

        private ApplicationUser GenerateAdminUser()
        {
            var admin = new ApplicationUser
            {
                Id = "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                FirstName = "Admin",
                LastName = "User",
                ProfileImgUrl = "https://a0.anyrgb.com/pngimg/1850/1546/admin-administrator-icon-admin%D0%B8%D1%81%D1%82%D1%80%D0%B0%D1%82%D0%BE%D1%80-system-administrator-administrator-nuvola-user-profile-hearing-login-internet-forum.png",
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
