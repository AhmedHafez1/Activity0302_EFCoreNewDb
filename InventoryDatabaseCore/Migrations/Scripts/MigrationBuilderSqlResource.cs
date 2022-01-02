using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using System.IO;
using System.Reflection;
using System.Text;

namespace InventoryDatabaseCore.Migrations.Scripts
{
    public static class MigrationBuilderSqlResource
    {
        public static OperationBuilder<SqlOperation> SqlResource(this MigrationBuilder mb, string relativeFileName)
        {
            using (var stream = Assembly.GetAssembly(typeof(MigrationBuilderSqlResource)).GetManifestResourceStream(relativeFileName))
{
                using (var ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    var data = ms.ToArray();
                    var text = Encoding.UTF8.GetString(data, 3,
                    data.Length - 3);
                    return mb.Sql(text);
                }
            }
        }
    }
}
