using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryDatabaseCore.Migrations
{
    public partial class updateItem_setMinMaxValuesOnQuantityAndPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
                                WHERE CONSTRAINT_NAME='CK_Items_Quantity_Minimum')
                                BEGIN
                                ALTER TABLE [dbo].[Items] ADD CONSTRAINT CK_Items_Quantity_Minimum
                                CHECK (Quantity >= 0)
                                END
                                IF NOT EXISTS(SELECT *
                                FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
                                WHERE CONSTRAINT_NAME='CK_Items_Quantity_Maximum')
                                BEGIN
                                ALTER TABLE [dbo].[Items] ADD CONSTRAINT CK_Items_Quantity_Maximum
                                CHECK (Quantity <= 1000)
                                END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF EXISTS(SELECT *
                                FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
                                WHERE CONSTRAINT_NAME='CK_Items_Quantity_Minimum')
                                BEGIN
                                ALTER TABLE [dbo].[Items] DROP CONSTRAINT CK_Items_Quantity_Minimum
                                END");
            migrationBuilder.Sql(@"IF NOT EXISTS(SELECT *
                                FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS
                                WHERE CONSTRAINT_NAME='CK_Items_Quantity_Maximum')
                                BEGIN
                                ALTER TABLE [dbo].[Items] DROP CONSTRAINT CK_Items_Quantity_Maximum
                                END");
        }
    }
}
