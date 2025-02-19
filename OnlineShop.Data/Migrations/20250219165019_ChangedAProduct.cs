using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedAProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "130464cb-22b5-463d-ae9d-e33bb2218fb3", "AQAAAAIAAYagAAAAEAt5PZEDM41zpKQZs28WJ1VnIb69LZo7QpeprsVTRDPEyOb/1hgXuyyuekbO5wargA==", "d6206e31-44b4-4667-b9cd-9d635313391e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9bd65753-4ac3-437f-a1ba-e9320baf1097",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "05929b58-09cf-40e2-bf4b-1f71619d7cd5", "AQAAAAIAAYagAAAAEFtCbnKXQ13pezKGPvVzvNdhJzOucC1LWUZfT/Bp0Y4xWABMbNqPo8CmtdG4OuLnxQ==", "7d157a6e-872f-46e0-919e-0cef08fb6237" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 2, 19, 18, 50, 15, 433, DateTimeKind.Local).AddTicks(4781));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2025, 2, 18, 18, 50, 15, 433, DateTimeKind.Local).AddTicks(4791));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ClothingTypeId", "Description", "GenderId", "ImageUrl", "Name", "Price", "StockQuantity" },
                values: new object[] { 4, "Very beautiful and comfortable jeans for women.", 2, "https://images.unsplash.com/photo-1591195853828-11db59a44f6b?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Women Jeans", 39.99m, 27 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDate",
                value: new DateTime(2025, 2, 19, 18, 50, 15, 435, DateTimeKind.Local).AddTicks(3670));

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 2, 19, 18, 50, 15, 435, DateTimeKind.Local).AddTicks(4570));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c43c4b53-8364-4f9e-82af-eeb51802520f", "AQAAAAIAAYagAAAAEPXGZn3c2wlX4ByJNBIRmwXGz3Z3wU/x5oMl9N9bFf1LfwH//fpoliOtd8Yk65zcvg==", "bd3182f1-8cba-4064-ba04-0cb8a3907357" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9bd65753-4ac3-437f-a1ba-e9320baf1097",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1090b786-ca2d-4d55-9c0f-79907c78bbd4", "AQAAAAIAAYagAAAAEPxt9oEEWSEmCPWWZ2h4+mbW2evlxwTgRe6DYj5W2b5UbtQbEazJJ8h9s5zM0PqiVA==", "c287fca2-23df-4f43-9ec5-1a25436959ad" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 2, 19, 17, 45, 29, 265, DateTimeKind.Local).AddTicks(7222));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2025, 2, 18, 17, 45, 29, 265, DateTimeKind.Local).AddTicks(7229));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ClothingTypeId", "Description", "GenderId", "ImageUrl", "Name", "Price", "StockQuantity" },
                values: new object[] { 3, "One of the best nike models ever created", 1, "https://static.nike.com/a/images/t_PDP_936_v1/f_auto,q_auto:eco/66d8f65e-6ecd-414c-bd03-e50a996f7de0/NIKE+AIR+MAX+PLUS.png", "Nike Air Max Plus", 1500.00m, 100 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDate",
                value: new DateTime(2025, 2, 19, 17, 45, 29, 266, DateTimeKind.Local).AddTicks(4649));

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 2, 19, 17, 45, 29, 266, DateTimeKind.Local).AddTicks(5595));
        }
    }
}
