using InventoryDatabaseCore;
using InventoryModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Activity0302_EFCoreNewDb
{
    class Program
    {
        static IConfigurationRoot _configuration;
        static DbContextOptionsBuilder<InventoryDbContext> _optionsBuilder;
        static void Main(string[] args)
        {
            BuildOptions();
            // InsertItems();
            ListInventory();
        }
        static void BuildOptions()
        {
            _configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
            _optionsBuilder = new DbContextOptionsBuilder<InventoryDbContext>();
            _optionsBuilder.UseSqlServer(_configuration.GetConnectionString("InventoryManager"));
        }
        static void ListInventory()
        {
            using (var dbContext = new InventoryDbContext(_optionsBuilder.Options))
            {
                dbContext.Items.Take(5).ToList()
               .ForEach(i => Console.WriteLine($"{i.Id} - {i.Name}"));
            }

        }

        static void InsertItems()
        {
            var items = new List<Item>
            {
                new Item { Name = "Top Gun" },
                new Item { Name = "Batman Begins" },
                new Item { Name = "Inception" },
                new Item { Name = "Star Wars: The Empire Strikes Back" },
                new Item { Name = "Remember the Titans" }
            };

            using (var dbContext = new InventoryDbContext(_optionsBuilder.Options))
            {
                dbContext.Items.AddRange(items);
                dbContext.SaveChanges();
            }
        }
    }
}
