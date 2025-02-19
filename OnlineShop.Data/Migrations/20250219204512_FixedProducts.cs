using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f307d331-b995-4601-8139-348b45aa0953", "AQAAAAIAAYagAAAAED/4p9S6TYuSMS6vRBcv/LMK0Id5xZtfMLFLydKwArYjHFAu/b9jejNUzEnDM5BE1Q==", "f42b6388-2b11-4dfc-9301-14eedab11788" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9bd65753-4ac3-437f-a1ba-e9320baf1097",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e3702937-dcbb-4521-b3ff-14f2c9d8e3e5", "AQAAAAIAAYagAAAAEGLXQRheR1FiaJR7mmxiILxbECefl8eBzsj6sX0nsqhnEFGmE0HCQOAEYkPrOhBoMQ==", "27438714-38b8-475d-a753-1c00858d84ba" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 2, 19, 22, 45, 9, 852, DateTimeKind.Local).AddTicks(5042));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ClothingTypeId", "Description", "GenderId", "ImageUrl", "Name" },
                values: new object[] { 2, "Very comfortable jacket for men.", 1, "https://images-cdn.ubuy.co.in/653b4be936138146b54c2af8-junge-denim-jacket-men-fleece-jacket.jpg", "Men Jacket" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "GenderId", "ImageUrl", "Name" },
                values: new object[] { "Sneakers that are extremely comfortable.", 2, "https://dimg.dillards.com/is/image/DillardsZoom/mainProduct/kurt-geiger-london-kensington-denim-fabric-sneakers/00000001_zi_4062c067-b0b4-494b-a79a-6e2b6957ae45.jpg", "Patchwork Sneakers" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDate",
                value: new DateTime(2025, 2, 19, 22, 45, 9, 853, DateTimeKind.Local).AddTicks(1773));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReviewDate",
                value: new DateTime(2025, 2, 19, 22, 45, 9, 853, DateTimeKind.Local).AddTicks(1788));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "ReviewDate",
                value: new DateTime(2025, 2, 19, 22, 45, 9, 853, DateTimeKind.Local).AddTicks(1791));

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 2, 19, 22, 45, 9, 853, DateTimeKind.Local).AddTicks(2607));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e92632ca-307e-48ee-b3ee-2bbf03a07d59", "AQAAAAIAAYagAAAAEJHFgYAGChotpik6ayzE7A34kamW4SejXzi7Oup7v6NXTlgGOy8l2l4ozTDdF4nMtQ==", "56f4d9cf-6ef9-4e30-98f8-0bdb7715aabb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9bd65753-4ac3-437f-a1ba-e9320baf1097",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3b69ca28-6e9e-4a6e-8e60-f14a8253716e", "AQAAAAIAAYagAAAAEFT2XJZjbmPkD66BcyI7fXYYEe5pAl5kC/YNVIQvv/sY/N1T4VfW+hNVkbrplBlOqg==", "87874c9d-6e4e-40d7-8d1d-268e0e079631" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 2, 19, 19, 18, 26, 880, DateTimeKind.Local).AddTicks(5736));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ClothingTypeId", "Description", "GenderId", "ImageUrl", "Name" },
                values: new object[] { 3, "Very comfortable boots for women.", 2, "https://images.unsplash.com/photo-1605732440685-d0654d81aa30?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Black Leather Brogan Black Boots" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "GenderId", "ImageUrl", "Name" },
                values: new object[] { "Leather shoes that are extremely comfortable for men.", 1, "https://images.unsplash.com/photo-1638609348722-aa2a3a67db26?q=80&w=1945&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Leather Shoes" });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDate",
                value: new DateTime(2025, 2, 19, 19, 18, 26, 881, DateTimeKind.Local).AddTicks(6610));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReviewDate",
                value: new DateTime(2025, 2, 19, 19, 18, 26, 881, DateTimeKind.Local).AddTicks(6628));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "ReviewDate",
                value: new DateTime(2025, 2, 19, 19, 18, 26, 881, DateTimeKind.Local).AddTicks(6630));

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 2, 19, 19, 18, 26, 881, DateTimeKind.Local).AddTicks(7561));
        }
    }
}
