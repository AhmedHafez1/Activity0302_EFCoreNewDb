using AutoMapper;
using AutoMapper.QueryableExtensions;
using InventoryDatabaseCore;
using InventoryModels.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryDatabaseLayer
{
    public class InventoryDatabaseRepo : IInventoryDatabaseRepo
    {
        private readonly InventoryDbContext _context;
        private readonly IMapper _mapper;

        public InventoryDatabaseRepo(InventoryDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GetItemsForListingDto> GetItemsForListingFromProcedure(DateTime minDateValue, DateTime maxDateValue)
        {
            var minDateParam = new SqlParameter("minDate", minDateValue);
            var maxDateParam = new SqlParameter("maxDate", maxDateValue);
            return _context.ItemsForListing
                .FromSqlRaw("EXECUTE dbo.GetItemsForListing @minDate, @maxDate", minDateParam, maxDateParam)
                .ToList();
        }

        public List<GetItemsForListingWithDateDto> GetItemsForListingLinq(DateTime minDateValue, DateTime maxDateValue)
        {
            return _context.Items.Include(x => x.Category).AsEnumerable()
            .Select(x => new GetItemsForListingWithDateDto
            {
                CreatedDate = x.CreatedDate,
                CategoryName = x.Category.Name,
                Description = x.Description,
                IsActive = x.IsActive,
                IsDeleted = x.IsDeleted,
                Name = x.Name,
                Notes = x.Notes
            }).Where(x => x.CreatedDate >= minDateValue && x.CreatedDate <= maxDateValue)
            .AsQueryable().OrderBy(y => y.CategoryName).ThenBy(z => z.Name).
            ToList();
        }

        public AllItemsPipeDelimitedStringDto GetItemsPipeDelimitedString(bool isActive)
        {
            var isActiveParm = new SqlParameter("IsActive", 1);
            return _context.AllItemsOutput
            .FromSqlRaw("SELECT [dbo].[ItemNamesPipeDelimitedString](@IsActive) AllItems", isActiveParm)
            .FirstOrDefault();
        }

        public List<GetItemsTotalValueDto> GetItemsTotalValues(bool isActive)
        {
            var isActiveParm = new SqlParameter("IsActive", 1);
            return _context.GetItemsTotalValues
                .FromSqlRaw("SELECT * from [dbo].[GetItemsTotalValue] (@IsActive)", isActiveParm)
                .ToList();
        }

        public List<ItemsWithGenresDto> GetItemsWithGenres()
        {
            return _context.ItemsWithGenres.ToList();
        }

        public List<CategoryDto> ListCategoriesAndColors()
        {
            return _context.Categories
                     .Include(x => x.CategoryColor)
                     .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                     .ToList();
        }

        public List<ItemDto> ListInventory()
        {
            var items = _context.Items.AsEnumerable().OrderBy(x => x.Name).ToList();
            return _mapper.Map<List<ItemDto>>(items);
        }
    }
}
