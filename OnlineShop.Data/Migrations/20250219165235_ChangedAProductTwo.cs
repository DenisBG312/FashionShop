using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedAProductTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ba147e8f-04c4-4ed3-ba5b-824194830fe1", "AQAAAAIAAYagAAAAELedma92SsQ4w8SSiWni/TqewkMs9V5ptCH8l4gGu13MnqeXy0SQcf+dp+uP3kRJ5w==", "71db3017-5621-4a38-9841-b1658e4725c1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9bd65753-4ac3-437f-a1ba-e9320baf1097",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1ee23462-eee6-473b-881b-d7f969652e7e", "AQAAAAIAAYagAAAAEFY9Q7EMd3hMPtP9tJQd/7v2BmUDpt8SZL1Nf2j1wmv/DgjFw2lj02gUtsI32CiIgg==", "d50d3a9e-5792-4ed4-b59e-73fc8a9a61ca" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 2, 19, 18, 52, 34, 416, DateTimeKind.Local).AddTicks(179));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2025, 2, 18, 18, 52, 34, 416, DateTimeKind.Local).AddTicks(186));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ClothingTypeId", "Description", "ImageUrl", "Name", "Price", "StockQuantity" },
                values: new object[] { 3, "Very comfortable boots for women.", "https://images.unsplash.com/photo-1605732440685-d0654d81aa30?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "Black Leather Brogan Black Boots", 65.00m, 40 });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDate",
                value: new DateTime(2025, 2, 19, 18, 52, 34, 416, DateTimeKind.Local).AddTicks(6843));

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 2, 19, 18, 52, 34, 416, DateTimeKind.Local).AddTicks(7763));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                keyValue: 2,
                columns: new[] { "ClothingTypeId", "Description", "ImageUrl", "Name", "Price", "StockQuantity" },
                values: new object[] { 2, "One of the greatest puffers ever created", "https://images.stockx.com/images/Trapstar-Shooters-Hooded-Puffer-Black-Reflective.jpg?fit=fill&bg=FFFFFF&w=700&h=500&fm=webp&auto=compress&q=90&dpr=2&trim=color&updated_at=1673460322", "Trapstar Shooters Hooded Puffer Black", 75.00m, 50 });

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
    }
}
