using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class CompositeKeyEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartsProducts_Sizes_SizeId",
                table: "ShoppingCartsProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCartsProducts",
                table: "ShoppingCartsProducts");

            migrationBuilder.DeleteData(
                table: "ShoppingCartsProducts",
                keyColumns: new[] { "ProductId", "ShoppingCartId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ShoppingCartsProducts",
                keyColumns: new[] { "ProductId", "ShoppingCartId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.AlterColumn<int>(
                name: "SizeId",
                table: "ShoppingCartsProducts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCartsProducts",
                table: "ShoppingCartsProducts",
                columns: new[] { "ShoppingCartId", "ProductId", "SizeId" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "43ebc70d-92f9-4285-8bdb-eb6eeed2b6cd", "AQAAAAIAAYagAAAAEC50qj4zIfAC2k5ON2m3LNtfjTbwSmYk7oH9+hpEcavXPDVIIll/VyhbrtOVEbVsBg==", "e1a1636d-1818-458a-b8eb-98b837da19f8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9bd65753-4ac3-437f-a1ba-e9320baf1097",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "007caa66-5e22-4ad5-b49a-a061f3aaf8af", "AQAAAAIAAYagAAAAEGDwOkq7GSNc6c8li3cXaS+L7RTeyO3BZPMI2oY9PM3rG2k+BAW4tc403/v24qxbDQ==", "a34dd494-482e-4050-b188-8bd42f302370" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 3, 11, 14, 43, 53, 582, DateTimeKind.Local).AddTicks(9995));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 11, 14, 43, 53, 583, DateTimeKind.Local).AddTicks(7318));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 11, 14, 43, 53, 583, DateTimeKind.Local).AddTicks(7343));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 11, 14, 43, 53, 583, DateTimeKind.Local).AddTicks(7347));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 11, 14, 43, 53, 583, DateTimeKind.Local).AddTicks(7352));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 11, 14, 43, 53, 583, DateTimeKind.Local).AddTicks(7355));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 11, 14, 43, 53, 583, DateTimeKind.Local).AddTicks(7420));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDate",
                value: new DateTime(2025, 3, 11, 14, 43, 53, 584, DateTimeKind.Local).AddTicks(1864));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReviewDate",
                value: new DateTime(2025, 3, 11, 14, 43, 53, 584, DateTimeKind.Local).AddTicks(1882));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "ReviewDate",
                value: new DateTime(2025, 3, 11, 14, 43, 53, 584, DateTimeKind.Local).AddTicks(1884));

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 3, 11, 14, 43, 53, 584, DateTimeKind.Local).AddTicks(2813));

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartsProducts_Sizes_SizeId",
                table: "ShoppingCartsProducts",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartsProducts_Sizes_SizeId",
                table: "ShoppingCartsProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCartsProducts",
                table: "ShoppingCartsProducts");

            migrationBuilder.AlterColumn<int>(
                name: "SizeId",
                table: "ShoppingCartsProducts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCartsProducts",
                table: "ShoppingCartsProducts",
                columns: new[] { "ShoppingCartId", "ProductId" });

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

            migrationBuilder.InsertData(
                table: "ShoppingCartsProducts",
                columns: new[] { "ProductId", "ShoppingCartId", "Quantity", "SizeId" },
                values: new object[,]
                {
                    { 1, 1, 1, null },
                    { 2, 1, 1, null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartsProducts_Sizes_SizeId",
                table: "ShoppingCartsProducts",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id");
        }
    }
}
