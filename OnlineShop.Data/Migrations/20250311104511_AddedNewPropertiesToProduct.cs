using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewPropertiesToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "SalesCount",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                columns: new[] { "CreatedDate", "SalesCount" },
                values: new object[] { new DateTime(2025, 3, 11, 12, 45, 10, 449, DateTimeKind.Local).AddTicks(8884), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "SalesCount" },
                values: new object[] { new DateTime(2025, 3, 11, 12, 45, 10, 449, DateTimeKind.Local).AddTicks(8909), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "SalesCount" },
                values: new object[] { new DateTime(2025, 3, 11, 12, 45, 10, 449, DateTimeKind.Local).AddTicks(8913), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "SalesCount" },
                values: new object[] { new DateTime(2025, 3, 11, 12, 45, 10, 449, DateTimeKind.Local).AddTicks(8920), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "SalesCount" },
                values: new object[] { new DateTime(2025, 3, 11, 12, 45, 10, 449, DateTimeKind.Local).AddTicks(8927), 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "SalesCount" },
                values: new object[] { new DateTime(2025, 3, 11, 12, 45, 10, 449, DateTimeKind.Local).AddTicks(8939), 0 });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SalesCount",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7ec4584c-ea3f-42e3-b862-2fb1e700fb6f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5f9f8286-a4b3-40d5-93db-2ff24f9d55e6", "AQAAAAIAAYagAAAAEJuYuNEJMrjrh735ZoGEvGOp4XoksFjwFAcBDWXOaUnzsXudFr1wUs0uS4SueXKyJA==", "a0887597-1c97-459c-b5ef-2b8bbed1c569" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9bd65753-4ac3-437f-a1ba-e9320baf1097",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3466f451-c1c7-46e5-834b-a0e5d2ddb8c9", "AQAAAAIAAYagAAAAEBlVCPDYpWm8nB4mYvnw6MAosDMa2IXEKQ75kkZxmYR9cAMw3ICmCzMNHRY0LVTLgQ==", "066669fe-b44a-4001-b0a7-916f2ab38b0a" });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2025, 3, 10, 21, 39, 34, 531, DateTimeKind.Local).AddTicks(3733));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1,
                column: "ReviewDate",
                value: new DateTime(2025, 3, 10, 21, 39, 34, 532, DateTimeKind.Local).AddTicks(7107));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2,
                column: "ReviewDate",
                value: new DateTime(2025, 3, 10, 21, 39, 34, 532, DateTimeKind.Local).AddTicks(7127));

            migrationBuilder.UpdateData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3,
                column: "ReviewDate",
                value: new DateTime(2025, 3, 10, 21, 39, 34, 532, DateTimeKind.Local).AddTicks(7130));

            migrationBuilder.UpdateData(
                table: "ShoppingCarts",
                keyColumn: "Id",
                keyValue: 1,
                column: "PaymentDate",
                value: new DateTime(2025, 3, 10, 21, 39, 34, 532, DateTimeKind.Local).AddTicks(8347));
        }
    }
}
