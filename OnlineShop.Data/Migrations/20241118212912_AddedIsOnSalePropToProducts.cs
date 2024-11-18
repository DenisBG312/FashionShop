using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsOnSalePropToProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOnSale",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsOnSale",
                value: false);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsOnSale",
                value: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOnSale",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b64245d2-86ae-4c9d-815c-9877952cf418", "AQAAAAIAAYagAAAAEAnCyF4MAwr8InwOYoI3iplwnpXSfC7FEnr0rRYQOKx/wMNoF6f09lFN8ICb/mVzxA==", "8445e030-d4aa-4b76-ac65-563682742919" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 11, 16, 16, 24, 15, 852, DateTimeKind.Local).AddTicks(2230));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2024, 11, 15, 16, 24, 15, 852, DateTimeKind.Local).AddTicks(2239));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDate",
                value: new DateTime(2024, 11, 16, 16, 24, 15, 852, DateTimeKind.Local).AddTicks(7088));

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2024, 11, 16, 16, 24, 15, 852, DateTimeKind.Local).AddTicks(7568));
        }
    }
}
