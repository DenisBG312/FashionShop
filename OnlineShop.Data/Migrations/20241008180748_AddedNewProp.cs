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
                values: new object[] { false, new DateTime(2024, 10, 8, 21, 7, 48, 399, DateTimeKind.Local).AddTicks(2371), "2a2d1de5-de58-4b33-a40e-71770a2b9479" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsCompleted", "OrderDate", "UserId" },
                values: new object[] { true, new DateTime(2024, 10, 7, 21, 7, 48, 399, DateTimeKind.Local).AddTicks(2379), "2a2d1de5-de58-4b33-a40e-71770a2b9479" });

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PaymentDate", "UserId" },
                values: new object[] { new DateTime(2024, 10, 8, 21, 7, 48, 399, DateTimeKind.Local).AddTicks(8355), "2a2d1de5-de58-4b33-a40e-71770a2b9479" });
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
                values: new object[] { new DateTime(2024, 10, 8, 19, 29, 29, 752, DateTimeKind.Local).AddTicks(7641), "1bcd7ed2-50a3-44b6-b5ec-9ebcc7494376" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "OrderDate", "UserId" },
                values: new object[] { new DateTime(2024, 10, 7, 19, 29, 29, 752, DateTimeKind.Local).AddTicks(7649), "1bcd7ed2-50a3-44b6-b5ec-9ebcc7494376" });

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PaymentDate", "UserId" },
                values: new object[] { new DateTime(2024, 10, 8, 19, 29, 29, 753, DateTimeKind.Local).AddTicks(3580), "1bcd7ed2-50a3-44b6-b5ec-9ebcc7494376" });
        }
    }
}
