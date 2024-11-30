using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShop.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfileImgUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    IsOnSale = table.Column<bool>(type: "bit", nullable: false),
                    DiscountPercentage = table.Column<int>(type: "int", nullable: true),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    ClothingTypeId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Products_ClothingTypes_ClothingTypeId",
                        column: x => x.ClothingTypeId,
                        principalTable: "ClothingTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentMethod = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdersProducts",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersProducts", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrdersProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdersProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
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

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfileImgUrl", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f", 0, "3ed08ec0-5c25-4bf8-8731-87024f2bedef", "bgdenibg@gmail.com", true, "Denis", "Tsranski", false, null, "BGDENIBG@GMAIL.COM", "BGDENIBG@GMAIL.COM", "AQAAAAIAAYagAAAAEJGd78fRY8r9+iTvw1ovPk/40HEHBbgNgXJC/roP4R1c5Yf8hBvPFGd6HHC/Mk321Q==", "+111111111111", true, null, "c869c6e6-fa7d-4792-ba9b-8aa8ab80cb64", false, "bgdenibg@gmail.com" },
                    { "8a914c36-ea3f-49f0-9ad3-3d32134b2f8c", 0, "26b495f8-6d2e-484a-b114-29bc14c3829c", "admin@onlineshop.com", true, "Admin", "User", false, null, "ADMIN@ONLINESHOP.COM", "ADMIN@ONLINESHOP.COM", "AQAAAAIAAYagAAAAEGVOyvTzBtg53f5s6aRnvzHRaP6XUU2F56CSEkoPcziR/6bvm9OVxIiLnKFfUmTWTA==", null, false, null, "563d1fd3-ecc4-4e71-ad61-d7f7a4ef5f44", false, "admin@onlineshop.com" }
                });

            migrationBuilder.InsertData(
                table: "ClothingTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "T-Shirt" },
                    { 2, "Jacket" },
                    { 3, "Shoes" },
                    { 4, "Jeans" },
                    { 5, "Shirts" },
                    { 6, "Shorts" }
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

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "IsCancelled", "IsCompleted", "OrderDate", "TotalAmount", "UserId" },
                values: new object[,]
                {
                    { 1, false, false, new DateTime(2024, 11, 30, 16, 20, 53, 957, DateTimeKind.Local).AddTicks(3219), 150.00m, "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f" },
                    { 2, false, true, new DateTime(2024, 11, 29, 16, 20, 53, 957, DateTimeKind.Local).AddTicks(3230), 75.50m, "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ClothingTypeId", "Description", "DiscountPercentage", "GenderId", "ImageUrl", "IsOnSale", "Name", "Price", "StockQuantity", "UserId" },
                values: new object[,]
                {
                    { 1, 3, "One of the best nike models ever created", null, 1, "https://static.nike.com/a/images/t_PDP_936_v1/f_auto,q_auto:eco/66d8f65e-6ecd-414c-bd03-e50a996f7de0/NIKE+AIR+MAX+PLUS.png", false, "Nike Air Max Plus", 1500.00m, 100, "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f" },
                    { 2, 2, "One of the greatest puffers ever created", null, 2, "https://images.stockx.com/images/Trapstar-Shooters-Hooded-Puffer-Black-Reflective.jpg?fit=fill&bg=FFFFFF&w=700&h=500&fm=webp&auto=compress&q=90&dpr=2&trim=color&updated_at=1673460322", false, "Trapstar Shooters Hooded Puffer Black", 75.00m, 50, "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f" }
                });

            migrationBuilder.InsertData(
                table: "ShoppingCarts",
                columns: new[] { "Id", "Amount", "PaymentDate", "Status", "UserId" },
                values: new object[] { 1, 1575.00m, new DateTime(2024, 11, 30, 16, 20, 53, 958, DateTimeKind.Local).AddTicks(6920), "Active", "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f" });

            migrationBuilder.InsertData(
                table: "OrdersProducts",
                columns: new[] { "OrderId", "ProductId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 1, 2, 19.99m },
                    { 1, 2, 1, 29.99m }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "OrderId", "PaymentDate", "PaymentMethod", "Status" },
                values: new object[,]
                {
                    { 1, 100.20m, 1, new DateTime(2024, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 1 },
                    { 2, 300.10m, 2, new DateTime(2024, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Comment", "ProductId", "Rating", "ReviewDate", "UserId" },
                values: new object[] { 1, "I really liked wearing these shoes. They are very comfortable", 1, 4, new DateTime(2024, 11, 30, 16, 20, 53, 958, DateTimeKind.Local).AddTicks(5953), "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f" });

            migrationBuilder.InsertData(
                table: "ShoppingCartsProducts",
                columns: new[] { "ProductId", "ShoppingCartId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersProducts_ProductId",
                table: "OrdersProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ClothingTypeId",
                table: "Products",
                column: "ClothingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_GenderId",
                table: "Products",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                table: "Products",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductId",
                table: "Reviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

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
                name: "OrdersProducts");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "ShoppingCartsProducts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");

            migrationBuilder.DropTable(
                name: "ClothingTypes");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8a914c36-ea3f-49f0-9ad3-3d32134b2f8c");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfileImgUrl",
                table: "AspNetUsers");
        }
    }
}
