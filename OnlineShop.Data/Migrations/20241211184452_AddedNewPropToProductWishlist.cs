using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewPropToProductWishlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "ProductWishlists");

            migrationBuilder.AddColumn<bool>(
                name: "IsOnSale",
                table: "ProductWishlists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4950ec01-1a44-41c1-a211-f65496f20d59", "AQAAAAIAAYagAAAAEHjS+Y8uET3coUjz2ZtIub/OP5dtIIPMP5muLlO01IMMye0XJACYtvKnh4xyd8P75A==", "4199afe8-bd2a-4f2b-a0cd-e36eda1f4385" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 12, 11, 20, 44, 51, 284, DateTimeKind.Local).AddTicks(9692));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2024, 12, 10, 20, 44, 51, 284, DateTimeKind.Local).AddTicks(9700));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDate",
                value: new DateTime(2024, 12, 11, 20, 44, 51, 285, DateTimeKind.Local).AddTicks(7266));

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2024, 12, 11, 20, 44, 51, 285, DateTimeKind.Local).AddTicks(8139));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOnSale",
                table: "ProductWishlists");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "ProductWishlists",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f81b5b4f-dea3-43a1-bf44-6f2d272a8cd9", "AQAAAAIAAYagAAAAED21XbZcUuc6QFc5V5FmtLO1cp2p/GekRnnozfGGB4//O9Vn0kiottdp5us17+823Q==", "8ae7552f-465d-4a79-a31f-415d9fd65772" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 12, 11, 20, 7, 22, 557, DateTimeKind.Local).AddTicks(52));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2024, 12, 10, 20, 7, 22, 557, DateTimeKind.Local).AddTicks(61));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDate",
                value: new DateTime(2024, 12, 11, 20, 7, 22, 557, DateTimeKind.Local).AddTicks(7110));

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2024, 12, 11, 20, 7, 22, 557, DateTimeKind.Local).AddTicks(7951));
        }
    }
}
