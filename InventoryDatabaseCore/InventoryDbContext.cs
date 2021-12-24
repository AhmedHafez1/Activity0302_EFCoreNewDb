using System;
using System.IO;
using InventoryModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace InventoryDatabaseCore
{
    public class InventoryDbContext : DbContext
    {
        private static IConfigurationRoot _configuration;

        public DbSet<Item> Items { get; set; }
        public InventoryDbContext() : base()
        {
        }
        public InventoryDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                _configuration = builder.Build();

                var connectionString = _configuration.GetConnectionString("InventoryManager");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
