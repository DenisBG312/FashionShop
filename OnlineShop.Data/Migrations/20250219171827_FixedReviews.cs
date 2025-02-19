using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2);

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
                columns: new[] { "OrderDate", "TotalAmount", "UserId" },
                values: new object[] { new DateTime(2025, 2, 19, 19, 18, 26, 880, DateTimeKind.Local).AddTicks(5736), 144.98m, "9bd65753-4ac3-437f-a1ba-e9320baf1097" });

            migrationBuilder.UpdateData(
                table: "OrdersProducts",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 1, 1 },
                column: "UnitPrice",
                value: 39.99m);

            migrationBuilder.UpdateData(
                table: "OrdersProducts",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 1, 2 },
                column: "UnitPrice",
                value: 65.00m);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Amount", "Status" },
                values: new object[] { 144.98m, 0 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Comment", "Rating", "ReviewDate", "UserId" },
                values: new object[] { "I bought these jeans for my wife. She is more than happy, as am I.", 5, new DateTime(2025, 2, 19, 19, 18, 26, 881, DateTimeKind.Local).AddTicks(6610), "9bd65753-4ac3-437f-a1ba-e9320baf1097" });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Comment", "ProductId", "Rating", "ReviewDate", "UserId" },
                values: new object[,]
                {
                    { 2, "Well the shoes are good but they are not good for running!", 2, 3, new DateTime(2025, 2, 19, 19, 18, 26, 881, DateTimeKind.Local).AddTicks(6628), "9bd65753-4ac3-437f-a1ba-e9320baf1097" },
                    { 3, "Amazing jacket but the sleeves are a little too short", 3, 4, new DateTime(2025, 2, 19, 19, 18, 26, 881, DateTimeKind.Local).AddTicks(6630), "9bd65753-4ac3-437f-a1ba-e9320baf1097" }
                });

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Amount", "PaymentDate", "UserId" },
                values: new object[] { 104.99m, new DateTime(2025, 2, 19, 19, 18, 26, 881, DateTimeKind.Local).AddTicks(7561), "9bd65753-4ac3-437f-a1ba-e9320baf1097" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "145a6cd8-b1bf-4410-a4af-0b48d792b2e9", "AQAAAAIAAYagAAAAEEA3u2gVFDABq73alU5835qszgv7jb6aj9mPbS6IMwaw4I11jgL5SSnZvOjY+NXYNw==", "ca674063-436f-415d-83be-b9c6f23b9ab4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9bd65753-4ac3-437f-a1ba-e9320baf1097",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "628cef2f-529c-400d-b686-8752053b25f9", "AQAAAAIAAYagAAAAEMN0mMi6CG+cJa3cCMmnLULMtI+oWPZhpH6GnyxsDuDtEHZuRxFqmq0tEphYNqDDKA==", "a89df985-f3dc-4687-8f4a-d18d5e9014dc" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "OrderDate", "TotalAmount", "UserId" },
                values: new object[] { new DateTime(2025, 2, 19, 19, 6, 34, 104, DateTimeKind.Local).AddTicks(9531), 150.00m, "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f" });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "IsCancelled", "IsCompleted", "OrderDate", "TotalAmount", "UserId" },
                values: new object[] { 2, false, true, new DateTime(2025, 2, 18, 19, 6, 34, 104, DateTimeKind.Local).AddTicks(9539), 75.50m, "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f" });

            migrationBuilder.UpdateData(
                table: "OrdersProducts",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 1, 1 },
                column: "UnitPrice",
                value: 19.99m);

            migrationBuilder.UpdateData(
                table: "OrdersProducts",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 1, 2 },
                column: "UnitPrice",
                value: 29.99m);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Amount", "Status" },
                values: new object[] { 100.20m, 1 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Comment", "Rating", "ReviewDate", "UserId" },
                values: new object[] { "I really liked wearing these shoes. They are very comfortable", 4, new DateTime(2025, 2, 19, 19, 6, 34, 105, DateTimeKind.Local).AddTicks(6145), "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f" });

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Amount", "PaymentDate", "UserId" },
                values: new object[] { 1575.00m, new DateTime(2025, 2, 19, 19, 6, 34, 105, DateTimeKind.Local).AddTicks(7071), "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f" });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "OrderId", "PaymentDate", "Status" },
                values: new object[] { 2, 300.10m, 2, new DateTime(2024, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });
        }
    }
}
