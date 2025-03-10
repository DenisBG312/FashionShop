using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeededSizes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5f9f8286-a4b3-40d5-93db-2ff24f9d55e6", "AQAAAAIAAYagAAAAEJuYuNEJMrjrh735ZoGEvGOp4XoksFjwFAcBDWXOaUnzsXudFr1wUs0uS4SueXKyJA==", "a0887597-1c97-459c-b5ef-2b8bbed1c569" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9bd65753-4ac3-437f-a1ba-e9320baf1097",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3466f451-c1c7-46e5-834b-a0e5d2ddb8c9", "AQAAAAIAAYagAAAAEBlVCPDYpWm8nB4mYvnw6MAosDMa2IXEKQ75kkZxmYR9cAMw3ICmCzMNHRY0LVTLgQ==", "066669fe-b44a-4001-b0a7-916f2ab38b0a" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 3, 10, 21, 39, 34, 531, DateTimeKind.Local).AddTicks(3733));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDate",
                value: new DateTime(2025, 3, 10, 21, 39, 34, 532, DateTimeKind.Local).AddTicks(7107));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReviewDate",
                value: new DateTime(2025, 3, 10, 21, 39, 34, 532, DateTimeKind.Local).AddTicks(7127));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "ReviewDate",
                value: new DateTime(2025, 3, 10, 21, 39, 34, 532, DateTimeKind.Local).AddTicks(7130));

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 3, 10, 21, 39, 34, 532, DateTimeKind.Local).AddTicks(8347));

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "XS" },
                    { 2, "S" },
                    { 3, "M" },
                    { 4, "L" },
                    { 5, "XL" },
                    { 6, "XXL" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "26533d9b-bae4-40e1-aa6b-108e7e190577", "AQAAAAIAAYagAAAAENE1qrRm9ozsoezIiCHDMYao36OjadnD3bBSeRBGthycmXus1KCkhm1XWL9TfHveYQ==", "e070f07c-2522-4b0c-ac7d-cbf4de716927" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9bd65753-4ac3-437f-a1ba-e9320baf1097",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "23c97e5c-e957-4a1e-a00f-26eb3c9767ec", "AQAAAAIAAYagAAAAEPMjoV9l20CWPJimTInsf5N+dqU+rvss+G4ZcWdCncYO37b7oGTT5VIOGhX1EGzQNg==", "1e88fed7-a4bf-4116-b241-1793805906bc" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 3, 10, 21, 31, 12, 370, DateTimeKind.Local).AddTicks(5971));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDate",
                value: new DateTime(2025, 3, 10, 21, 31, 12, 372, DateTimeKind.Local).AddTicks(1004));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReviewDate",
                value: new DateTime(2025, 3, 10, 21, 31, 12, 372, DateTimeKind.Local).AddTicks(1020));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "ReviewDate",
                value: new DateTime(2025, 3, 10, 21, 31, 12, 372, DateTimeKind.Local).AddTicks(1022));

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 3, 10, 21, 31, 12, 372, DateTimeKind.Local).AddTicks(2058));
        }
    }
}
