using InventoryDatabaseCore;
using InventoryHelpers;
using InventoryModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using AutoMapper;
using InventoryModels.DTOs;
using AutoMapper.QueryableExtensions;
using InventoryBusinessLayer;

namespace Activity0302_EFCoreNewDb
{
    class Program
    {
        static IConfigurationRoot _configuration;
        static DbContextOptionsBuilder<InventoryDbContext> _optionsBuilder;

        private static MapperConfiguration _mapperConfig;
        private static IMapper _mapper;
        private static IServiceProvider _serviceProvider;

        private static List<CategoryDto> _categories;
        static void Main(string[] args)
        {
            BuildOptions();
            BuildMapper();

            var minDate = new DateTime(2020, 1, 1);
            var maxDate = new DateTime(2021, 1, 1);

            using (var db = new InventoryDbContext(_optionsBuilder.Options))
            {
                var svc = new ItemsService(db, _mapper);

                Console.WriteLine("List Inventory");
                var inventory = svc.ListInventory();
                inventory.ForEach(x => Console.WriteLine($"New Item: {x}"));

                Console.WriteLine("List inventory with Linq");
                var items = svc.GetItemsForListingLinq(minDate, maxDate);
                items.ForEach(x => Console.WriteLine($"ITEM| {x.CategoryName}|{ x.Name} - { x.Description}"));

                Console.WriteLine("List Inventory from procedure");
                var procItems = svc.GetItemsForListingFromProcedure(minDate, maxDate);
                procItems.ForEach(x => Console.WriteLine($"ITEM| {x.Name} -{ x.Description}"));

                Console.WriteLine("Item Names Pipe Delimited String");
                var pipedItems = svc.GetItemsPipeDelimitedString(true);
                Console.WriteLine(pipedItems.AllItems);

                Console.WriteLine("Get Items Total Values");
                var totalValues = svc.GetItemsTotalValues(true);
                totalValues.ForEach(item => Console.WriteLine($"New Item] {item.Id,-10}" +
                $"|{item.Name,-50}" +
                $"|{item.Quantity,-4}" +
                $"|{item.TotalValue,-5}"));

                Console.WriteLine("Get Items With Genres");
                var itemsWithGenres = svc.GetItemsWithGenres();
                itemsWithGenres.ForEach(item => Console.WriteLine($"New Item] {item.Id,-10}" +
                $"|{item.Name,-50}" + $"|{item.Genre?.ToString().PadLeft(4)}"));

                Console.WriteLine("List Categories And Colors");
                var categoriesAndColors = svc.ListCategoriesAndColors();
                categoriesAndColors.ForEach(c => Console.WriteLine($"{c.Category} | { c.CategoryColor.Color}"));
                _categories = categoriesAndColors;

                Console.WriteLine("Would you like to create items?");
                var createItems = Console.ReadLine().StartsWith("y", StringComparison.OrdinalIgnoreCase);
                if (createItems)
                {
                    Console.WriteLine("Adding new Item(s)");
                    CreateMultipleItems(svc);
                    Console.WriteLine("Items added");
                    inventory = svc.ListInventory();
                    inventory.ForEach(x => Console.WriteLine($"Item: {x}"));
                }

                Console.WriteLine("Would you like to update items?");
                var updateItems = Console.ReadLine().StartsWith("y", StringComparison.OrdinalIgnoreCase);
                if (updateItems)
                {
                    Console.WriteLine("Updating Item(s)");
                    UpdateMultipleItems(svc);
                    Console.WriteLine("Items updated");
                    inventory = svc.ListInventory();
                    inventory.ForEach(x => Console.WriteLine($"Item: {x}"));
                }

                Console.WriteLine("Would you like to delete items?");
                var deleteItems = Console.ReadLine().StartsWith("y", StringComparison.
                OrdinalIgnoreCase);


                if (deleteItems)
                {
                    Console.WriteLine("Deleting Item(s)");
                    DeleteMultipleItems(svc);
                    Console.WriteLine("Items Deleted");
                    inventory = svc.ListInventory();
                    inventory.ForEach(x => Console.WriteLine($"Item: {x}"));
                }
            }
            Console.WriteLine("Program Complete");
        }
        static void BuildOptions()
        {
            _configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
            _optionsBuilder = new DbContextOptionsBuilder<InventoryDbContext>();
            _optionsBuilder.UseSqlServer(_configuration.GetConnectionString("InventoryManager"));
        }

        static void BuildMapper()
        {
            _mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<InventoryMapper>();
            });
            _mapperConfig.AssertConfigurationIsValid();
            _mapper = _mapperConfig.CreateMapper();
        }

        private static void CreateMultipleItems(IItemsService svc)
        {
            Console.WriteLine("Would you like to create items as a batch?");
            bool batchCreate = Console.ReadLine().StartsWith("y", StringComparison.
            OrdinalIgnoreCase);
            var allItems = new List<CreateOrUpdateItemDto>();
            bool createAnother = true;
            while (createAnother == true)
            {
                var newItem = new CreateOrUpdateItemDto();
                Console.WriteLine("Creating a new item.");
                Console.WriteLine("Please enter the name");
                newItem.Name = Console.ReadLine();
                Console.WriteLine("Please enter the description");
                newItem.Description = Console.ReadLine();
                Console.WriteLine("Please enter the notes");

                newItem.Notes = Console.ReadLine();
                Console.WriteLine("Please enter the Category [B]ooks, [M]ovies, [G]ames");
                newItem.CategoryId = GetCategoryId(Console.ReadLine().Substring(0, 1).ToUpper());
                if (!batchCreate)
                {
                    svc.InsertOrUpdateItem(newItem);
                }
                else
                {
                    allItems.Add(newItem);
                }
                Console.WriteLine("Would you like to create another item?");
                createAnother = Console.ReadLine().StartsWith("y", StringComparison.OrdinalIgnoreCase);
                if (batchCreate && !createAnother)
                {
                    svc.InsertOrUpdateItems(allItems);
                }
            }
        }

        private static int GetCategoryId(string input)
        {
            switch (input)
            {
                case "B":
                    return _categories.FirstOrDefault(x => x.Category.ToLower().Equals("books"))?.Id ?? -1;
                case "M":
                    return _categories.FirstOrDefault(x => x.Category.ToLower().Equals("movies"))?.Id ?? -1;
                case "G":
                    return _categories.FirstOrDefault(x => x.Category.ToLower().Equals("games"))?.Id ?? -1;
                default:
                    return -1;
            }
        }

        public static void UpdateMultipleItems(IItemsService svc)
        {
            Console.WriteLine("Would you like to update items as a batch?");
            bool batchUpdate = Console.ReadLine().StartsWith("y", StringComparison.OrdinalIgnoreCase);
            var allItems = new List<CreateOrUpdateItemDto>();
            bool updateAnother = true;
            while (updateAnother == true)
            {
                Console.WriteLine("Items");
                Console.WriteLine("Enter the ID number to update");
                Console.WriteLine("*******************************");
                var items = svc.ListInventory();
                items.ForEach(x => Console.WriteLine($"ID: {x.Id} | {x.Name}"));

                Console.WriteLine("*******************************");
                int id = 0;
                if (int.TryParse(Console.ReadLine(), out id))
                {
                    var itemMatch = items.FirstOrDefault(x => x.Id == id);
                    if (itemMatch != null)
                    {
                        var updItem = _mapper.Map<CreateOrUpdateItemDto>(_mapper.Map<Item>(itemMatch));
                        Console.WriteLine("Enter the new name [leave blank to keep existing]");
                        var newName = Console.ReadLine();
                        updItem.Name = !string.IsNullOrWhiteSpace(newName) ? newName : updItem.Name;
                        Console.WriteLine("Enter the new desc [leave blank to keep existing]");
                        var newDesc = Console.ReadLine();
                        updItem.Description = !string.IsNullOrWhiteSpace(newDesc) ? newDesc : updItem.Description;
                        Console.WriteLine("Enter the new notes [leave blank to keep existing]");
                        var newNotes = Console.ReadLine();
                        updItem.Notes = !string.IsNullOrWhiteSpace(newNotes) ? newNotes : updItem.Notes;
                        Console.WriteLine("Toggle Item Active Status? [y/n]");
                        var toggleActive = Console.ReadLine().Substring(0, 1).Equals("y", StringComparison.OrdinalIgnoreCase);
                        if (toggleActive)
                        {
                            updItem.IsActive = !updItem.IsActive;
                        }
                        Console.WriteLine("Enter the category - [B]ooks, [M]ovies, [G]ames, or[N]o Change");
                        var userChoice = Console.ReadLine().Substring(0, 1).ToUpper();

                        updItem.CategoryId = userChoice.Equals("N", StringComparison.OrdinalIgnoreCase) ? updItem.CategoryId : GetCategoryId(userChoice);
                        if (!batchUpdate)
                        {
                            svc.InsertOrUpdateItem(updItem);
                        }
                        else
                        {
                            allItems.Add(updItem);
                        }
                    }
                }
                Console.WriteLine("Would you like to update another?");
                updateAnother = Console.ReadLine().StartsWith("y", StringComparison.OrdinalIgnoreCase);
                if (batchUpdate && !updateAnother)
                {
                    svc.InsertOrUpdateItems(allItems);
                }
            }
        }

        public static void DeleteMultipleItems(IItemsService svc)
        {
            Console.WriteLine("Would you like to delete items as a batch?");
            bool batchDelete = Console.ReadLine().StartsWith("y", StringComparison.OrdinalIgnoreCase);
            var allItems = new List<int>();
            bool deleteAnother = true;
            while (deleteAnother == true)
            {
                Console.WriteLine("Items");
                Console.WriteLine("Enter the ID number to delete");
                Console.WriteLine("*******************************");
                var items = svc.ListInventory();
                items.ForEach(x => Console.WriteLine($"ID: {x.Id} | {x.Name}"));
                Console.WriteLine("*******************************");
                if (batchDelete && allItems.Any())
                {
                    Console.WriteLine("Items scheduled for delete");
                    allItems.ForEach(x => Console.Write($"{x},"));
                    Console.WriteLine();
                    Console.WriteLine("*******************************");
                }

                int id = 0;
                if (int.TryParse(Console.ReadLine(), out id))
                {
                    var itemMatch = items.FirstOrDefault(x => x.Id == id);
                    if (itemMatch != null)
                    {
                        if (batchDelete)
                        {
                            if (!allItems.Contains(itemMatch.Id))
                            {
                                allItems.Add(itemMatch.Id);
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Are you sure you want to delete the item { itemMatch.Id}-{ itemMatch.Name}");
                        if (Console.ReadLine().StartsWith("y", StringComparison.OrdinalIgnoreCase))
                            {
                                svc.DeleteItem(itemMatch.Id);
                                Console.WriteLine("Item Deleted");
                            }
                        }
                    }
                }
                Console.WriteLine("Would you like to delete another item?");
                deleteAnother = Console.ReadLine().StartsWith("y", StringComparison.OrdinalIgnoreCase);
                if (batchDelete && !deleteAnother)
                {
                    Console.WriteLine("Are you sure you want to delete the following items: ");
                    allItems.ForEach(x => Console.Write($"{x},"));

                    Console.WriteLine();
                    if (Console.ReadLine().StartsWith("y", StringComparison.OrdinalIgnoreCase))
                    {
                        svc.DeleteItems(allItems);
                        Console.WriteLine("Items Deleted");
                    }
                }
            }
        }

    }
}