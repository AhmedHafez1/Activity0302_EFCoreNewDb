using Microsoft.EntityFrameworkCore.Migrations;
using InventoryDatabaseCore.Migrations.Scripts;
namespace InventoryDatabaseCore.Migrations
{
    public partial class CreateFunction_GetItemsTotalValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlResource("InventoryDatabaseCore.Migrations.Scripts.Functions.GetItemsTotalValue.GetItemsTotalValue.v0.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS dbo.GetItemsTotalValue");
        }
    }
}
