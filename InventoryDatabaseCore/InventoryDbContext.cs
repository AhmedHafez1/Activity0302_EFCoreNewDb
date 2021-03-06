using System;
using System.Diagnostics;
using System.IO;
using InventoryModels;
using InventoryModels.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared;

namespace InventoryDatabaseCore
{
    public class InventoryDbContext : DbContext
    {
        private static IConfigurationRoot _configuration;

        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryColor> CategoryColors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<GetItemsForListingDto> ItemsForListing { get; set; }
        public DbSet<AllItemsPipeDelimitedStringDto> AllItemsOutput { get; set; }
        public DbSet<GetItemsTotalValueDto> GetItemsTotalValues { get; set; }
        public DbSet<ItemsWithGenresDto> ItemsWithGenres { get; set; }

        public InventoryDbContext() : base()
        {
        }
        public InventoryDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Unique, Non-clustered Index for ItemGenre relationships
            modelBuilder.Entity<ItemGenre>()
            .HasIndex(ig => new { ig.ItemId, ig.GenreId })
            .IsUnique()
            .IsClustered(false);

            modelBuilder.Entity<GetItemsForListingDto>(x =>
            {
                x.HasNoKey();
                x.ToView("ItemsForListing");
            });

            modelBuilder.Entity<AllItemsPipeDelimitedStringDto>(x =>
            {
                x.HasNoKey();
                x.ToView("AllItemsOutput");
            });

            modelBuilder.Entity<GetItemsTotalValueDto>(x =>
            {
                x.HasNoKey();
                x.ToView("GetItemsTotalValues");
            });

            var createdDate = new DateTime(2022, 01, 01);
            modelBuilder.Entity<Genre>(x =>
            {
                x.HasData(
                new Genre()
                {
                    Id = 6,
                    CreatedDate = createdDate,
                    IsActive = true,
                    IsDeleted = false,
                    Name = "Fantasy"
                },
                new Genre()
                {
                    Id = 7,
                    CreatedDate = createdDate,
                    IsActive = true,
                    IsDeleted = false,
                    Name = "Sci/Fi"
                },
                new Genre()
                {
                    Id = 8,
                    CreatedDate = createdDate,
                    IsActive = true,
                    IsDeleted = false,
                    Name = "Horror"
                },
                new Genre()
                {
                    Id = 9,
                    CreatedDate = createdDate,
                    IsActive = true,
                    IsDeleted = false,
                    Name = "Comedy"
                },
                new Genre()
                {
                    Id = 10,
                    CreatedDate = createdDate,
                    IsActive = true,
                    IsDeleted = false,
                    Name = "Drama"
                }
                );
            });

            modelBuilder.Entity<ItemsWithGenresDto>(x =>
            {
                x.HasNoKey();
                x.ToView("ItemsWithGenres");
            });
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

        public override int SaveChanges()
        {
            var tracker = ChangeTracker;

            foreach (var entry in tracker.Entries())
            {
                if (entry.Entity is FullAuditModel entityReference)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entityReference.CreatedDate = DateTime.Now;
                            break;
                        case EntityState.Deleted:
                        case EntityState.Modified:
                            entityReference.LastModifiedDate = DateTime.Now;
                            break;
                        default:
                            break;
                    }
                }
            }

            return base.SaveChanges();
        }
    }
}
