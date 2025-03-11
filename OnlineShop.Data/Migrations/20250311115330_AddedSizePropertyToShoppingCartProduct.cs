using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedSizePropertyToShoppingCartProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "ShoppingCartsProducts",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "373fc455-a6a3-43ab-9251-75240241ee0e", "AQAAAAIAAYagAAAAEEwlD+HTbEWUEpz1qZnLWhyBoG7VqKSqdBcHZ6LaVXvbTFJIGr60TAwiCkeRCty8dQ==", "b1eb32be-88f3-427c-b0d7-6b0db2c6c8fb" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9bd65753-4ac3-437f-a1ba-e9320baf1097",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "65f0f868-88c7-4ad4-a0e8-eec1b027aafa", "AQAAAAIAAYagAAAAEKuuplDcTMmFgZt68ocnUI7/bEc+eM35UcTuabuVF3JKz7DC4EGc1lKSENqjuPNzFQ==", "7854dc75-da03-4da0-afbb-22290c018dd7" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 3, 11, 13, 53, 29, 751, DateTimeKind.Local).AddTicks(4316));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 11, 13, 53, 29, 752, DateTimeKind.Local).AddTicks(397));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 11, 13, 53, 29, 752, DateTimeKind.Local).AddTicks(422));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 11, 13, 53, 29, 752, DateTimeKind.Local).AddTicks(426));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 11, 13, 53, 29, 752, DateTimeKind.Local).AddTicks(430));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 11, 13, 53, 29, 752, DateTimeKind.Local).AddTicks(434));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 11, 13, 53, 29, 752, DateTimeKind.Local).AddTicks(440));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDate",
                value: new DateTime(2025, 3, 11, 13, 53, 29, 752, DateTimeKind.Local).AddTicks(4577));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReviewDate",
                value: new DateTime(2025, 3, 11, 13, 53, 29, 752, DateTimeKind.Local).AddTicks(4595));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "ReviewDate",
                value: new DateTime(2025, 3, 11, 13, 53, 29, 752, DateTimeKind.Local).AddTicks(4597));

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 3, 11, 13, 53, 29, 752, DateTimeKind.Local).AddTicks(5859));

            migrationBuilder.UpdateData(
                table: "ShoppingCartsProducts",
                keyColumns: new[] { "ProductId", "ShoppingCartId" },
                keyValues: new object[] { 1, 1 },
                column: "SizeId",
                value: null);

            migrationBuilder.UpdateData(
                table: "ShoppingCartsProducts",
                keyColumns: new[] { "ProductId", "ShoppingCartId" },
                keyValues: new object[] { 2, 1 },
                column: "SizeId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartsProducts_SizeId",
                table: "ShoppingCartsProducts",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartsProducts_Sizes_SizeId",
                table: "ShoppingCartsProducts",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartsProducts_Sizes_SizeId",
                table: "ShoppingCartsProducts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartsProducts_SizeId",
                table: "ShoppingCartsProducts");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "ShoppingCartsProducts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3e12ab5e-c1dd-4f47-9dd4-76b6aaf2e115", "AQAAAAIAAYagAAAAEP2t4tFwBpm9xt+uWpk82+utJ3Cd8ypk3SrnPRnBXYJvHxQyTsAyvEvMAjzVaJE9jw==", "bf4c6185-1ee8-418f-ab3a-a2563ff2dca0" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9bd65753-4ac3-437f-a1ba-e9320baf1097",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "61fa0bd3-b653-48c9-9cd4-7745a2b367f9", "AQAAAAIAAYagAAAAEINaCW6NB5GJfATN0HaczikZvKAhbXsMZVPfDkWKvBpIXvIfZ9bxRdkS5Ot2gJPD6w==", "e9b47e6c-b19e-4924-b2d8-a78af2affacd" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 3, 11, 12, 45, 10, 448, DateTimeKind.Local).AddTicks(6154));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 11, 12, 45, 10, 449, DateTimeKind.Local).AddTicks(8884));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 11, 12, 45, 10, 449, DateTimeKind.Local).AddTicks(8909));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 11, 12, 45, 10, 449, DateTimeKind.Local).AddTicks(8913));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 11, 12, 45, 10, 449, DateTimeKind.Local).AddTicks(8920));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 11, 12, 45, 10, 449, DateTimeKind.Local).AddTicks(8927));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 11, 12, 45, 10, 449, DateTimeKind.Local).AddTicks(8939));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDate",
                value: new DateTime(2025, 3, 11, 12, 45, 10, 450, DateTimeKind.Local).AddTicks(3707));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReviewDate",
                value: new DateTime(2025, 3, 11, 12, 45, 10, 450, DateTimeKind.Local).AddTicks(3727));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "ReviewDate",
                value: new DateTime(2025, 3, 11, 12, 45, 10, 450, DateTimeKind.Local).AddTicks(3730));

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 3, 11, 12, 45, 10, 450, DateTimeKind.Local).AddTicks(4854));
        }
    }
}
