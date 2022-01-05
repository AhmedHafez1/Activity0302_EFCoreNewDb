using InventoryDatabaseCore;
using InventoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryDataMigrator
{
    internal class BuildCategories
    {
        private readonly InventoryDbContext _context;
        public BuildCategories(InventoryDbContext context)
        {
            _context = context;
        }
        public void ExecuteSeed()
        {
            if (_context.Categories.Count() == 0)
            {
                _context.Categories.AddRange(
                new Category()
                {
                    IsActive = true,
                    IsDeleted = false,
                    Name = "Movies",
                    CategoryColor = new CategoryColor() { ColorValue = "Blue" }
                },
                new Category()
                {
                    IsActive = true,
                    IsDeleted = false,
                    Name = "Books",
                    CategoryColor = new CategoryColor() { ColorValue = "Red" }
                },
                new Category()
                {
                    IsActive = true,
                    IsDeleted = false,
                    Name = "Games",
                    CategoryColor = new CategoryColor() { ColorValue = "Green" }
                }
                );

                _context.SaveChanges();

                var movies = _context.Categories.FirstOrDefault(x => x.Name.ToLower().Equals("movies"));
                var blue = _context.CategoryColors.FirstOrDefault(x => x.ColorValue.ToLower().Equals("blue"));
                movies.CategoryColorId = blue.Id;

                var books = _context.Categories.FirstOrDefault(x => x.Name.ToLower().Equals("books"));
                var red = _context.CategoryColors.FirstOrDefault(x => x.ColorValue.ToLower().Equals("red"));
                books.CategoryColorId = red.Id;

                var games = _context.Categories.FirstOrDefault(x => x.Name.ToLower().Equals("games"));
                var green = _context.CategoryColors.FirstOrDefault(x => x.ColorValue.ToLower().Equals("green"));
                games.CategoryColorId = green.Id;

                _context.SaveChanges();
            }
        }
    }
}