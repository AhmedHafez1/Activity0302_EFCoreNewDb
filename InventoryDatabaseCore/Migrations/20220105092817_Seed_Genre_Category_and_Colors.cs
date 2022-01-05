using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryDatabaseCore.Migrations
{
    public partial class Seed_Genre_Category_and_Colors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "CreatedByUserId", "CreatedDate", "IsActive", "IsDeleted", "LastModifiedDate", "LastModifiedUserId", "Name" },
                values: new object[,]
                {
                    { 6, null, new DateTime(2022, 1, 5, 11, 28, 17, 44, DateTimeKind.Local).AddTicks(1933), true, false, null, null, "Fantasy" },
                    { 7, null, new DateTime(2022, 1, 5, 11, 28, 17, 45, DateTimeKind.Local).AddTicks(1260), true, false, null, null, "Sci/Fi" },
                    { 8, null, new DateTime(2022, 1, 5, 11, 28, 17, 45, DateTimeKind.Local).AddTicks(1274), true, false, null, null, "Horror" },
                    { 9, null, new DateTime(2022, 1, 5, 11, 28, 17, 45, DateTimeKind.Local).AddTicks(1276), true, false, null, null, "Comedy" },
                    { 10, null, new DateTime(2022, 1, 5, 11, 28, 17, 45, DateTimeKind.Local).AddTicks(1288), true, false, null, null, "Drama" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "CreatedByUserId", "CreatedDate", "IsActive", "IsDeleted", "LastModifiedDate", "LastModifiedUserId", "Name" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2022, 1, 5, 11, 13, 42, 47, DateTimeKind.Local).AddTicks(446), true, false, null, null, "Fantasy" },
                    { 2, null, new DateTime(2022, 1, 5, 11, 13, 42, 47, DateTimeKind.Local).AddTicks(9938), true, false, null, null, "Sci/Fi" },
                    { 3, null, new DateTime(2022, 1, 5, 11, 13, 42, 47, DateTimeKind.Local).AddTicks(9953), true, false, null, null, "Horror" },
                    { 4, null, new DateTime(2022, 1, 5, 11, 13, 42, 47, DateTimeKind.Local).AddTicks(9956), true, false, null, null, "Comedy" },
                    { 5, null, new DateTime(2022, 1, 5, 11, 13, 42, 47, DateTimeKind.Local).AddTicks(9958), true, false, null, null, "Drama" }
                });
        }
    }
}
