using InventoryDatabaseCore;
using InventoryHelpers;
using InventoryModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;

namespace Activity0302_EFCoreNewDb
{
    class Program
    {
        static IConfigurationRoot _configuration;
        static DbContextOptionsBuilder<InventoryDbContext> _optionsBuilder;
        static void Main(string[] args)
        {
            BuildOptions();
            DealeteAllItems();
            InsertItems();
            UpdateItems();
            ListInventory();
            GetItemsForListing();
            GetItemsForListingWithParams();
        }

        static void GetItemsForListing()
        {
            using (var db = new InventoryDbContext(_optionsBuilder.Options))
            {
                var results = db.ItemsForListing.FromSqlRaw("EXECUTE dbo.GetItemsForListing").ToList();

                foreach (var item in results)
                {
                    Console.WriteLine($"ITEM {item.Name} - {item.Description}");
                }
            }
        }

        static void GetItemsForListingWithParams()
        {
            using (var db = new InventoryDbContext(_optionsBuilder.Options))
            {
                var minDate = new SqlParameter("minDate", new DateTime(2022, 1, 1));
                var maxDate = new SqlParameter("maxDate", new DateTime(2022, 1, 4));

                var results = db.ItemsForListing.FromSqlRaw("EXECUTE dbo.GetItemsForListing @minDate, @maxDate", minDate, maxDate).ToList();

                foreach (var item in results)
                {
                    Console.WriteLine($"ITEM {item.Name} - {item.Description} - {item.CategoryName}");
                }
            }
        }

        static void BuildOptions()
        {
            _configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
            _optionsBuilder = new DbContextOptionsBuilder<InventoryDbContext>();
            _optionsBuilder.UseSqlServer(_configuration.GetConnectionString("InventoryManager"));
        }

        static void DealeteAllItems()
        {
            using (var db = new InventoryDbContext())
            {
                var items = db.Items.ToList();
                foreach (var item in items)
                {
                    item.LastModifiedUserId = 1;
                }
                db.Items.RemoveRange(items);
                db.SaveChanges();
            }
        }
        static void ListInventory()
        {
            using (var dbContext = new InventoryDbContext(_optionsBuilder.Options))
            {
                dbContext.Items.Take(5).OrderBy(x => x.Name).ToList()
               .ForEach(i => Console.WriteLine($"{i.Id} - {i.Name}"));
            }

        }

        static void InsertItems()
        {
            var items = new List<Item>
            {
                new Item { Name = "Top Gun", IsActive = true, Description="I feel the need, the need for speed" },
                new Item { Name = "Batman Begins", IsActive = true, Description = "Attitude reflects leadership" },
                new Item { Name = "Inception", IsActive = true, Description = "You either die the hero or live long" },
                new Item { Name = "Star Wars: The Empire Strikes Back", IsActive = true, Description = "He will join us or die, master" },
                new Item { Name = "Remember the Titans", IsActive = true, Description = "Attitude reflects leadership" }
            };

            using (var dbContext = new InventoryDbContext(_optionsBuilder.Options))
            {
                foreach (var item in items)
                {
                    item.CreatedByUserId = 1;
                }
                dbContext.Items.AddRange(items);
                dbContext.SaveChanges();
            }
        }

        static void UpdateItems()
        {
            using (var db = new InventoryDbContext(_optionsBuilder.Options))
            {
                var items = db.Items.ToList();
                foreach (var item in items)
                {
                    item.LastModifiedUserId = 1;
                    item.CurrentOrFinalPrice = 9.99M;
                }
                db.Items.UpdateRange(items);
                db.SaveChanges();

            }
        }
    }
}
