using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsCancelledPropToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCancelled",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsCancelled", "OrderDate" },
                values: new object[] { false, new DateTime(2024, 10, 13, 11, 28, 50, 547, DateTimeKind.Local).AddTicks(4624) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsCancelled", "OrderDate" },
                values: new object[] { false, new DateTime(2024, 10, 12, 11, 28, 50, 547, DateTimeKind.Local).AddTicks(4633) });

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2024, 10, 13, 11, 28, 50, 548, DateTimeKind.Local).AddTicks(301));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCancelled",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 10, 12, 23, 30, 53, 371, DateTimeKind.Local).AddTicks(443));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2024, 10, 11, 23, 30, 53, 371, DateTimeKind.Local).AddTicks(453));

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2024, 10, 12, 23, 30, 53, 371, DateTimeKind.Local).AddTicks(7291));
        }
    }
}
