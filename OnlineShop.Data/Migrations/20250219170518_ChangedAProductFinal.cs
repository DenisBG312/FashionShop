using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedAProductFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bbd267d7-30ed-4c08-b6ed-aa75cf05ca23", "AQAAAAIAAYagAAAAEAL1eRB4CmaMR2IkmLIaCkt9xCzfjG92dPjvtrT1yQ05CNM/zVG4QVVW51MINqnh5g==", "969f119b-bcdd-4b7f-a907-e275e267aebc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9bd65753-4ac3-437f-a1ba-e9320baf1097",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bddee79b-b0a5-4a03-a66d-c907b5ee8528", "AQAAAAIAAYagAAAAEHsfoc5latntgWFWFaVtR1XgcU6yRbDnaq5lRwaCVpjrzJuU64s5E24BJvLWMaNXkA==", "ec84d795-cfe9-495f-be94-2cad62e08d1a" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 2, 19, 19, 5, 18, 75, DateTimeKind.Local).AddTicks(4147));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2025, 2, 18, 19, 5, 18, 75, DateTimeKind.Local).AddTicks(4155));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ClothingTypeId", "Description", "DiscountPercentage", "GenderId", "ImageUrl", "IsOnSale", "Name", "Price", "StockQuantity", "UserId" },
                values: new object[,]
                {
                    { 3, 2, "Very good-looking jacket for men.", 25, 1, "https://images.unsplash.com/photo-1605732440685-d0654d81aa30?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", true, "Brown Jacket", 65.00m, 40, "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f" },
                    { 4, 3, "Leather shoes that are extremely comfortable for men.", null, 1, "https://images.unsplash.com/photo-1638609348722-aa2a3a67db26?q=80&w=1945&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", false, "Leather Shoes", 55.00m, 17, "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f" },
                    { 5, 1, "Stylish t-shirt for women.", null, 2, "https://images.unsplash.com/photo-1583743814966-8936f5b7be1a?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", false, "Black T-Shirt", 24.99m, 23, "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f" },
                    { 6, 1, "A clothing set for men specially gathered.", 15, 2, "https://images.unsplash.com/photo-1467043237213-65f2da53396f?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", true, "Clothing Set", 24.99m, 23, "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f" }
                });

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDate",
                value: new DateTime(2025, 2, 19, 19, 5, 18, 76, DateTimeKind.Local).AddTicks(822));

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 2, 19, 19, 5, 18, 76, DateTimeKind.Local).AddTicks(1644));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

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
    }
}
