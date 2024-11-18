using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedDiscountPercentageProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiscountPercentage",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "35f6ad7b-fc5a-4d26-b484-23e95e22a08d", "AQAAAAIAAYagAAAAEOVOyLxMVuowV/DfswaWlnxS7fC/LAnXRx8PP7dceUFiWLm2AgrvpnbGA4QcFjtfDg==", "ee3e3214-8558-4e81-a970-ac47034e4215" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 11, 18, 23, 42, 0, 936, DateTimeKind.Local).AddTicks(2682));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2024, 11, 17, 23, 42, 0, 936, DateTimeKind.Local).AddTicks(2691));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DiscountPercentage",
                value: null);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "DiscountPercentage",
                value: null);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDate",
                value: new DateTime(2024, 11, 18, 23, 42, 0, 936, DateTimeKind.Local).AddTicks(9260));

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2024, 11, 18, 23, 42, 0, 937, DateTimeKind.Local).AddTicks(102));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bccbb753-bd83-4fe5-81cd-43cae9767fc6", "AQAAAAIAAYagAAAAEH0Fd/p1415qnmlHwMr7y/9vJ33uv/HqJj5FiLxEzyYhjeUSj3DYXjJwPL3EuY5rLA==", "0367b31b-0c78-4b7a-8a40-bca86f704dad" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 11, 18, 23, 29, 11, 200, DateTimeKind.Local).AddTicks(8364));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2024, 11, 17, 23, 29, 11, 200, DateTimeKind.Local).AddTicks(8373));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDate",
                value: new DateTime(2024, 11, 18, 23, 29, 11, 201, DateTimeKind.Local).AddTicks(6578));

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2024, 11, 18, 23, 29, 11, 201, DateTimeKind.Local).AddTicks(7623));
        }
    }
}
