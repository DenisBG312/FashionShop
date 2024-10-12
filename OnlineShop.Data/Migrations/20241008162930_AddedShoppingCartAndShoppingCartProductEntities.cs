using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShop.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedShoppingCartAndShoppingCartProductEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartsProducts",
                columns: table => new
                {
                    ShoppingCartId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartsProducts", x => new { x.ShoppingCartId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ShoppingCartsProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingCartsProducts_ShoppingCarts_ShoppingCartId",
                        column: x => x.ShoppingCartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                table: "ShoppingCarts",
                columns: new[] { "Id", "Amount", "PaymentDate", "Status", "UserId" },
                values: new object[] { 1, 1575.00m, new DateTime(2024, 10, 8, 19, 29, 29, 753, DateTimeKind.Local).AddTicks(3580), "Active", "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f" });

            migrationBuilder.InsertData(
                table: "ShoppingCartsProducts",
                columns: new[] { "ProductId", "ShoppingCartId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_UserId",
                table: "ShoppingCarts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartsProducts_ProductId",
                table: "ShoppingCartsProducts",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingCartsProducts");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "OrderDate", "UserId" },
                values: new object[] { new DateTime(2024, 10, 5, 16, 2, 9, 687, DateTimeKind.Local).AddTicks(3025), "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "OrderDate", "UserId" },
                values: new object[] { new DateTime(2024, 10, 4, 16, 2, 9, 687, DateTimeKind.Local).AddTicks(3033), "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f" });
        }
    }
}
