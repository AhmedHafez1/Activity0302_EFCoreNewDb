using InventoryDatabaseCore;
using InventoryHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace InventoryDataMigrator
{
    class Program
    {
        static IConfigurationRoot _configuration;
        static DbContextOptionsBuilder<InventoryDbContext> _optionsBuilder;
        static void BuildOptions()
        {
            _configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
            _optionsBuilder = new DbContextOptionsBuilder<InventoryDbContext>();
            _optionsBuilder.UseSqlServer(_configuration.GetConnectionString("InventoryManager"));
        }

        static void Main(string[] args)
        {
            BuildOptions();
            EnsureAndRunMigrations();
            ExecuteCustomSeedData(); 
        }

        static void EnsureAndRunMigrations()
        {
            using (var db = new InventoryDbContext(_optionsBuilder.Options))
            {
                db.Database.Migrate();
            }
        }

        private static void ExecuteCustomSeedData()
        {
            using (var context = new InventoryDbContext(_optionsBuilder.
            Options))
            {
                var buildCategories = new BuildCategories(context);
                buildCategories.ExecuteSeed();
                var buildItems = new BuildItems(context);
                buildItems.ExecuteSeed();
            }
        }
    }
}
