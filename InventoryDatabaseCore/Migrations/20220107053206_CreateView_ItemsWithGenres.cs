using InventoryDatabaseCore.Migrations.Scripts;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryDatabaseCore.Migrations
{
    public partial class CreateView_ItemsWithGenres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlResource("InventoryDatabaseCore.Migrations.Scripts.Views.ItemsWithGenres.ItemsWithGenres.v0.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS dbo.ItemsWithGenres");
        }
    }
}
