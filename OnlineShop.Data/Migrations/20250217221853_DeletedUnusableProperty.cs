using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeletedUnusableProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Payments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfileImgUrl", "SecurityStamp" },
                values: new object[] { "b06b7b66-b408-4915-94c1-90ee0f61d08d", "AQAAAAIAAYagAAAAED+yuvr0JvH1wJ273zjSvXEBGjUToLI0hTXYGiLIFMocmjpHVcJ1pwyBjON6FX4qmw==", "https://a0.anyrgb.com/pngimg/1850/1546/admin-administrator-icon-admin%D0%B8%D1%81%D1%82%D1%80%D0%B0%D1%82%D0%BE%D1%80-system-administrator-administrator-nuvola-user-profile-hearing-login-internet-forum.png", "37282019-b350-4ce8-9b4a-5e64c927e55f" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 2, 18, 0, 18, 50, 55, DateTimeKind.Local).AddTicks(3913));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2025, 2, 17, 0, 18, 50, 55, DateTimeKind.Local).AddTicks(3919));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDate",
                value: new DateTime(2025, 2, 18, 0, 18, 50, 56, DateTimeKind.Local).AddTicks(715));

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 2, 18, 0, 18, 50, 56, DateTimeKind.Local).AddTicks(1575));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentMethod",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "ProfileImgUrl", "SecurityStamp" },
                values: new object[] { "b2bb98e0-14f9-459e-9229-73da1e022ac7", "AQAAAAIAAYagAAAAEEDvorTrUg1CrIcgpckKP6ApytNN7vi7aYEGFuBO69e22qi1siBueoqd1c90aS9cEg==", null, "a1506cc6-f267-47a8-ac4c-93b678e3c933" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 12, 11, 23, 35, 37, 220, DateTimeKind.Local).AddTicks(6350));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2024, 12, 10, 23, 35, 37, 220, DateTimeKind.Local).AddTicks(6360));

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentMethod",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Payments",
                keyColumn: "Id",
                keyValue: 2,
                column: "PaymentMethod",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDate",
                value: new DateTime(2024, 12, 11, 23, 35, 37, 221, DateTimeKind.Local).AddTicks(4381));

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2024, 12, 11, 23, 35, 37, 221, DateTimeKind.Local).AddTicks(5261));
        }
    }
}
