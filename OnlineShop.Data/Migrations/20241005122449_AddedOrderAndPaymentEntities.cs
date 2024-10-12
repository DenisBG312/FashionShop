using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShop.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedOrderAndPaymentEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
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

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "OrderDate", "TotalAmount", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 5, 15, 24, 49, 194, DateTimeKind.Local).AddTicks(3170), 150.00m, "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f" },
                    { 2, new DateTime(2024, 10, 4, 15, 24, 49, 194, DateTimeKind.Local).AddTicks(3178), 75.50m, "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f" }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "OrderId", "PaymentDate", "PaymentMethod", "Status" },
                values: new object[,]
                {
                    { 1, 100.20m, 1, new DateTime(2024, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 1 },
                    { 2, 300.10m, 2, new DateTime(2024, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
