using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsCompleted", "OrderDate", "UserId" },
                values: new object[] { false, new DateTime(2024, 10, 8, 21, 7, 48, 399, DateTimeKind.Local).AddTicks(2371), "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsCompleted", "OrderDate", "UserId" },
                values: new object[] { true, new DateTime(2024, 10, 7, 21, 7, 48, 399, DateTimeKind.Local).AddTicks(2379), "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f" });

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PaymentDate", "UserId" },
                values: new object[] { new DateTime(2024, 10, 8, 21, 7, 48, 399, DateTimeKind.Local).AddTicks(8355), "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "OrderDate", "UserId" },
                values: new object[] { new DateTime(2024, 10, 8, 19, 29, 29, 752, DateTimeKind.Local).AddTicks(7641), "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "OrderDate", "UserId" },
                values: new object[] { new DateTime(2024, 10, 7, 19, 29, 29, 752, DateTimeKind.Local).AddTicks(7649), "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f" });

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PaymentDate", "UserId" },
                values: new object[] { new DateTime(2024, 10, 8, 19, 29, 29, 753, DateTimeKind.Local).AddTicks(3580), "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f" });
        }
    }
}
