using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedOneMoreUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c43c4b53-8364-4f9e-82af-eeb51802520f", "AQAAAAIAAYagAAAAEPXGZn3c2wlX4ByJNBIRmwXGz3Z3wU/x5oMl9N9bFf1LfwH//fpoliOtd8Yk65zcvg==", "bd3182f1-8cba-4064-ba04-0cb8a3907357" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfileImgUrl", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9bd65753-4ac3-437f-a1ba-e9320baf1097", 0, "1090b786-ca2d-4d55-9c0f-79907c78bbd4", "john@email.com", true, "John", "Doe", false, null, "JOHN@EMAIL.COM", "JOHN@EMAIL.COM", "AQAAAAIAAYagAAAAEPxt9oEEWSEmCPWWZ2h4+mbW2evlxwTgRe6DYj5W2b5UbtQbEazJJ8h9s5zM0PqiVA==", null, false, "https://images.unsplash.com/photo-1500648767791-00dcc994a43e?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", "c287fca2-23df-4f43-9ec5-1a25436959ad", false, "john@email.com" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 2, 19, 17, 45, 29, 265, DateTimeKind.Local).AddTicks(7222));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2025, 2, 18, 17, 45, 29, 265, DateTimeKind.Local).AddTicks(7229));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDate",
                value: new DateTime(2025, 2, 19, 17, 45, 29, 266, DateTimeKind.Local).AddTicks(4649));

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 2, 19, 17, 45, 29, 266, DateTimeKind.Local).AddTicks(5595));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9bd65753-4ac3-437f-a1ba-e9320baf1097");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b06b7b66-b408-4915-94c1-90ee0f61d08d", "AQAAAAIAAYagAAAAED+yuvr0JvH1wJ273zjSvXEBGjUToLI0hTXYGiLIFMocmjpHVcJ1pwyBjON6FX4qmw==", "37282019-b350-4ce8-9b4a-5e64c927e55f" });

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
    }
}
