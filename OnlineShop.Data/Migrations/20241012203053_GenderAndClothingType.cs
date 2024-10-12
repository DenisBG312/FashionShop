using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShop.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class GenderAndClothingType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClothingTypeId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ClothingTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClothingTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ClothingTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "T-Shirt" },
                    { 2, "Jacket" },
                    { 3, "Shoes" }
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Men" },
                    { 2, "Women" },
                    { 3, "Kids" }
                });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "OrderDate", "UserId" },
                values: new object[] { new DateTime(2024, 10, 12, 23, 30, 53, 371, DateTimeKind.Local).AddTicks(443), "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "OrderDate", "UserId" },
                values: new object[] { new DateTime(2024, 10, 11, 23, 30, 53, 371, DateTimeKind.Local).AddTicks(453), "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ClothingTypeId", "Description", "GenderId", "ImageUrl", "Name" },
                values: new object[] { 3, "One of the best nike models ever created", 1, "https://static.nike.com/a/images/t_PDP_936_v1/f_auto,q_auto:eco/66d8f65e-6ecd-414c-bd03-e50a996f7de0/NIKE+AIR+MAX+PLUS.png", "Nike Air Max Plus" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ClothingTypeId", "Description", "GenderId", "ImageUrl", "Name" },
                values: new object[] { 2, "One of the greatest puffers ever created", 2, "https://images.stockx.com/images/Trapstar-Shooters-Hooded-Puffer-Black-Reflective.jpg?fit=fill&bg=FFFFFF&w=700&h=500&fm=webp&auto=compress&q=90&dpr=2&trim=color&updated_at=1673460322", "Trapstar Shooters Hooded Puffer Black" });

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PaymentDate", "UserId" },
                values: new object[] { new DateTime(2024, 10, 12, 23, 30, 53, 371, DateTimeKind.Local).AddTicks(7291), "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f" });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ClothingTypeId",
                table: "Products",
                column: "ClothingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_GenderId",
                table: "Products",
                column: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ClothingTypes_ClothingTypeId",
                table: "Products",
                column: "ClothingTypeId",
                principalTable: "ClothingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Genders_GenderId",
                table: "Products",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ClothingTypes_ClothingTypeId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Genders_GenderId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ClothingTypes");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropIndex(
                name: "IX_Products_ClothingTypeId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_GenderId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ClothingTypeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "OrderDate", "UserId" },
                values: new object[] { new DateTime(2024, 10, 8, 21, 7, 48, 399, DateTimeKind.Local).AddTicks(2371), "2a2d1de5-de58-4b33-a40e-71770a2b9479" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "OrderDate", "UserId" },
                values: new object[] { new DateTime(2024, 10, 7, 21, 7, 48, 399, DateTimeKind.Local).AddTicks(2379), "2a2d1de5-de58-4b33-a40e-71770a2b9479" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "One of the fastest phones ever created", "https://api.technopolis.bg/medias/Product-details-main-502118.jpg?context=bWFzdGVyfHJvb3R8MjE5ODA1fGltYWdlL2pwZWd8YUROaUwyZzVZUzh6TkRFME1UUTVORGt3TWpneE5DOVFjbTlrZFdOMExXUmxkR0ZwYkhNdGJXRnBibDgxTURJeE1UZ3VhbkJufGRkNDUwZDJhOWEyZDg0MGMxMDJiZjdjYjMzZWM0ODhkMjBlNmNkYzUzOWJhNWRhMDI1MWYwMTJmMTU1NGM5NWY", "iPhone 15 Pro Max" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "ImageUrl", "Name" },
                values: new object[] { "One of the greatest parfumes ever created", "https://parfium.bg/3095-large_default/versace-eros-toaletna-voda-za-myje.jpg", "Versace Eros" });

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "PaymentDate", "UserId" },
                values: new object[] { new DateTime(2024, 10, 8, 21, 7, 48, 399, DateTimeKind.Local).AddTicks(8355), "2a2d1de5-de58-4b33-a40e-71770a2b9479" });
        }
    }
}
