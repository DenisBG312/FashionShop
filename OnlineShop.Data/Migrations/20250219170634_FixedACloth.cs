using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedACloth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "145a6cd8-b1bf-4410-a4af-0b48d792b2e9", "AQAAAAIAAYagAAAAEEA3u2gVFDABq73alU5835qszgv7jb6aj9mPbS6IMwaw4I11jgL5SSnZvOjY+NXYNw==", "ca674063-436f-415d-83be-b9c6f23b9ab4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9bd65753-4ac3-437f-a1ba-e9320baf1097",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "628cef2f-529c-400d-b686-8752053b25f9", "AQAAAAIAAYagAAAAEMN0mMi6CG+cJa3cCMmnLULMtI+oWPZhpH6GnyxsDuDtEHZuRxFqmq0tEphYNqDDKA==", "a89df985-f3dc-4687-8f4a-d18d5e9014dc" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 2, 19, 19, 6, 34, 104, DateTimeKind.Local).AddTicks(9531));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2025, 2, 18, 19, 6, 34, 104, DateTimeKind.Local).AddTicks(9539));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1591047139829-d91aecb6caea?q=80&w=1936&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D");

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDate",
                value: new DateTime(2025, 2, 19, 19, 6, 34, 105, DateTimeKind.Local).AddTicks(6145));

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 2, 19, 19, 6, 34, 105, DateTimeKind.Local).AddTicks(7071));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1605732440685-d0654d81aa30?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D");

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
    }
}
