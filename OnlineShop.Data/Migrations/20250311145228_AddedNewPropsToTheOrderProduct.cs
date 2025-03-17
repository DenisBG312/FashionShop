using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewPropsToTheOrderProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdersProducts",
                table: "OrdersProducts");

            migrationBuilder.DeleteData(
                table: "OrdersProducts",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "OrdersProducts",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "OrdersProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdersProducts",
                table: "OrdersProducts",
                columns: new[] { "OrderId", "ProductId", "SizeId" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "54b916cf-76f4-4f1b-901d-a76430928bf7", "AQAAAAIAAYagAAAAEJ2hYCEM5piOylxtDVsB3+oejBIZ8LwOJbjvWqPmejUH5k+1V7bVuXp3ki1lDsAxBA==", "8008a192-446c-4e94-a2b1-c0100d372da7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9bd65753-4ac3-437f-a1ba-e9320baf1097",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d90c71c2-f126-40b6-bc86-ab31e272683c", "AQAAAAIAAYagAAAAED6U97yPDysygaFnxlHiqia1d8DDDQ9u8yaNf8bJzxUXq0LUjyGOW0Ypdmej1bxPFg==", "3c2c7496-91ed-4d77-ac57-4fb821058ccd" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 3, 11, 16, 52, 27, 293, DateTimeKind.Local).AddTicks(119));

            migrationBuilder.InsertData(
                table: "OrdersProducts",
                columns: new[] { "OrderId", "ProductId", "SizeId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 2, 2, 39.99m },
                    { 1, 2, 1, 1, 65.00m }
                });

            migrationBuilder.InsertData(
                table: "ProductSizes",
                columns: new[] { "ProductId", "SizeId", "StockQuantity" },
                values: new object[,]
                {
                    { 1, 1, 10 },
                    { 2, 2, 5 },
                    { 3, 3, 19 },
                    { 4, 1, 10 },
                    { 5, 4, 5 },
                    { 6, 1, 19 }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 11, 16, 52, 27, 293, DateTimeKind.Local).AddTicks(8051));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 11, 16, 52, 27, 293, DateTimeKind.Local).AddTicks(8074));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 11, 16, 52, 27, 293, DateTimeKind.Local).AddTicks(8078));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 11, 16, 52, 27, 293, DateTimeKind.Local).AddTicks(8083));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 11, 16, 52, 27, 293, DateTimeKind.Local).AddTicks(8086));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedDate",
                value: new DateTime(2025, 3, 11, 16, 52, 27, 293, DateTimeKind.Local).AddTicks(8091));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDate",
                value: new DateTime(2025, 3, 11, 16, 52, 27, 294, DateTimeKind.Local).AddTicks(2201));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReviewDate",
                value: new DateTime(2025, 3, 11, 16, 52, 27, 294, DateTimeKind.Local).AddTicks(2217));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "ReviewDate",
                value: new DateTime(2025, 3, 11, 16, 52, 27, 294, DateTimeKind.Local).AddTicks(2219));

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 3, 11, 16, 52, 27, 294, DateTimeKind.Local).AddTicks(3842));

            migrationBuilder.CreateIndex(
                name: "IX_OrdersProducts_SizeId",
                table: "OrdersProducts",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersProducts_Sizes_SizeId",
                table: "OrdersProducts",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdersProducts_Sizes_SizeId",
                table: "OrdersProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdersProducts",
                table: "OrdersProducts");

            migrationBuilder.DropIndex(
                name: "IX_OrdersProducts_SizeId",
                table: "OrdersProducts");

            migrationBuilder.DeleteData(
                table: "OrdersProducts",
                keyColumns: new[] { "OrderId", "ProductId", "SizeId" },
                keyColumnTypes: new[] { "int", "int", "int" },
                keyValues: new object[] { 1, 1, 2 });

            migrationBuilder.DeleteData(
                table: "OrdersProducts",
                keyColumns: new[] { "OrderId", "ProductId", "SizeId" },
                keyColumnTypes: new[] { "int", "int", "int" },
                keyValues: new object[] { 1, 2, 1 });

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumns: new[] { "ProductId", "SizeId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumns: new[] { "ProductId", "SizeId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumns: new[] { "ProductId", "SizeId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumns: new[] { "ProductId", "SizeId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumns: new[] { "ProductId", "SizeId" },
                keyValues: new object[] { 5, 4 });

            migrationBuilder.DeleteData(
                table: "ProductSizes",
                keyColumns: new[] { "ProductId", "SizeId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "OrdersProducts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdersProducts",
                table: "OrdersProducts",
                columns: new[] { "OrderId", "ProductId" });

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

            migrationBuilder.InsertData(
                table: "OrdersProducts",
                columns: new[] { "OrderId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 2, 39.99m },
                    { 1, 2, 1, 65.00m }
                });

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
        }
    }
}
