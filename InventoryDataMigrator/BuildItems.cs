using InventoryDatabaseCore;
using InventoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryDataMigrator
{
    public class BuildItems
    {
        private readonly InventoryDbContext _context;

        public BuildItems(InventoryDbContext context) { _context = context; }

        public void ExecuteSeed()
        {
            if (_context.Items.Count() == 0)
            {
                var movie = _context.Categories.FirstOrDefault(x => x.Name.ToLower() == "movies");
                var book = _context.Categories.FirstOrDefault(x => x.Name.ToLower() == "books");
                var game = _context.Categories.FirstOrDefault(x => x.Name.ToLower() == "games");

                var scifi = _context.Genres.FirstOrDefault(x => x.Name.ToLower() == "sci/fi");
                var fantasy = _context.Genres.FirstOrDefault(x => x.Name.ToLower() == "fantasy");
                var horror = _context.Genres.FirstOrDefault(x => x.Name.ToLower() == "horror");
                var comedy = _context.Genres.FirstOrDefault(x => x.Name.ToLower() == "comedy");
                var drama = _context.Genres.FirstOrDefault(x => x.Name.ToLower() == "drama");

                var createdDate = DateTime.Now;

                _context.Items.AddRange(
                new Item()
                {
                    CategoryId = movie.Id,
                    CreatedDate = createdDate,
                    CurrentOrFinalPrice = 19.99m,
                    IsActive = true,
                    IsDeleted = false,
                    IsOnSale = false,
                    Name = "Top Gun",
                    Description = "I feel the need, the need for speed",
                    PurchasedDate = createdDate,
                    PurchasePrice = 18.50m,
                    Quantity = 1,

                    ItemGenres = new List<ItemGenre> {
                new ItemGenre { GenreId = comedy.Id }
                }
                },
                new Item()
                {
                    CategoryId = movie.Id,
                    CreatedDate = createdDate,
                    CurrentOrFinalPrice = 12.99m,
                    IsActive = true,
                    IsDeleted = false,
                    IsOnSale = true,
                    Name = "Batman Begins",
                    Description = "Why do we fall, Bruce?",
                    PurchasedDate = createdDate,
                    PurchasePrice = 14.50m,
                    Quantity = 4,
                    ItemGenres = new List<ItemGenre> {
                new ItemGenre { GenreId = scifi.Id } ,
                new ItemGenre { GenreId = drama.Id }
                }
                },
                new Item()
                {
                    CategoryId = book.Id,
                    CreatedDate = createdDate,
                    CurrentOrFinalPrice = 35.99m,
                    IsActive = true,
                    IsDeleted = false,
                    IsOnSale = true,
                    Name = "Practical Entity Framework",
                    Description = "The book that teaches practical application with EF",
                    PurchasedDate = createdDate,

                    PurchasePrice = 44.50m,
                    Quantity = 100
                },
                new Item()
                {
                    CategoryId = book.Id,
                    CreatedDate = createdDate,
                    CurrentOrFinalPrice = 6.99m,
                    IsActive = true,
                    IsDeleted = false,
                    IsOnSale = false,
                    Name = "The Lord of the Rings",
                    Description = "The fellowship of the Ring",
                    PurchasedDate = createdDate,
                    PurchasePrice = 12.50m,
                    Quantity = 7,
                    ItemGenres = new List<ItemGenre> {
                new ItemGenre { GenreId = scifi.Id },
                new ItemGenre { GenreId = fantasy.Id }
                }
                },
                new Item()
                {
                    CategoryId = game.Id,
                    CreatedDate = createdDate,
                    CurrentOrFinalPrice = 23.99m,
                    IsActive = true,
                    IsDeleted = false,
                    IsOnSale = false,
                    Name = "Battlefield 5",
                    Description = "First person shooter",
                    PurchasedDate = createdDate,
                    PurchasePrice = 44.50m,
                    Quantity = 17,

                    ItemGenres = new List<ItemGenre> {
                new ItemGenre { GenreId = scifi.Id }
                }
                },
                new Item()
                {
                    CategoryId = game.Id,
                    CreatedDate = createdDate,
                    CurrentOrFinalPrice = 0.00m,
                    IsActive = true,
                    IsDeleted = false,
                    IsOnSale = false,
                    Name = "World Of Tanks",
                    Description = "AN MMO WW2 Tanks First-Person Shooter",
                    PurchasedDate = createdDate,
                    PurchasePrice = 0.00m,
                    Quantity = 1
                }
                );
                _context.SaveChanges();
            }
        }
    }
}