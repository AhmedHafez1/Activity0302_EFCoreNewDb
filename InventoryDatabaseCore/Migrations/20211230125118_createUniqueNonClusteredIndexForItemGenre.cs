using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryDatabaseCore.Migrations
{
    public partial class createUniqueNonClusteredIndexForItemGenre : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ItemGenre_ItemId",
                table: "ItemGenre");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGenre_ItemId_GenreId",
                table: "ItemGenre",
                columns: new[] { "ItemId", "GenreId" },
                unique: true)
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ItemGenre_ItemId_GenreId",
                table: "ItemGenre");

            migrationBuilder.CreateIndex(
                name: "IX_ItemGenre_ItemId",
                table: "ItemGenre",
                column: "ItemId");
        }
    }
}
