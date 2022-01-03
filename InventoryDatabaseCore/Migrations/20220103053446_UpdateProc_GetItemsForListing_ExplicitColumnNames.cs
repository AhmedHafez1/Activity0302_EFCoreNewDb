using Microsoft.EntityFrameworkCore.Migrations;
using InventoryDatabaseCore.Migrations.Scripts;
    
namespace InventoryDatabaseCore.Migrations
{
    public partial class UpdateProc_GetItemsForListing_ExplicitColumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlResource("InventoryDatabaseCore.Migrations.Scripts.Procedures.GetItemsForListing.GetItemsForListing.v2.sql");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.SqlResource("InventoryDatabaseCore.Migrations.Scripts.Procedures.GetItemsForListing.GetItemsForListing.v1.sql");
        }
    }
}
