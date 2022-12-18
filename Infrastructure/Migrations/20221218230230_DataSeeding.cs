using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Todos",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.InsertData(
                table: "Todos",
                columns: new[] { "Id", "Category", "Color", "CreationDate", "Header", "IsDone" },
                values: new object[,]
                {
                    { 1, 3, "Red", new DateTime(2022, 12, 19, 3, 2, 29, 965, DateTimeKind.Local).AddTicks(1318), "Create a ticket", false },
                    { 2, 1, "Green", new DateTime(2022, 12, 19, 3, 2, 29, 965, DateTimeKind.Local).AddTicks(1327), "Request information", false }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Text", "TodoId" },
                values: new object[,]
                {
                    { 1, "First comment", 1 },
                    { 2, "Second comment", 1 },
                    { 3, "Third comment", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Todos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreationDate",
                table: "Todos",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
