using InventoryDatabaseLayer;
using InventoryModels.DTOs;
using System;
using System.Collections.Generic;
using InventoryDatabaseCore;
using AutoMapper;

namespace InventoryBusinessLayer
{
    public class ItemsService : IItemsService
    {
        private readonly IInventoryDatabaseRepo _dbRepo;

        public ItemsService(InventoryDbContext context, IMapper mapper)
        {
            _dbRepo = new InventoryDatabaseRepo(context, mapper);
        }
        public List<GetItemsForListingDto> GetItemsForListingFromProcedure(DateTime minDateValue, DateTime maxDateValue)
        {
            return _dbRepo.GetItemsForListingFromProcedure(minDateValue, maxDateValue);
        }

        public List<GetItemsForListingWithDateDto> GetItemsForListingLinq(DateTime minDateValue, DateTime maxDateValue)
        {
            return _dbRepo.GetItemsForListingLinq(minDateValue, maxDateValue);
        }

        public AllItemsPipeDelimitedStringDto GetItemsPipeDelimitedString(bool isActive)
        {
            return _dbRepo.GetItemsPipeDelimitedString(isActive);
        }

        public List<GetItemsTotalValueDto> GetItemsTotalValues(bool isActive)
        {
            return _dbRepo.GetItemsTotalValues(isActive);
        }

        public List<ItemsWithGenresDto> GetItemsWithGenres()
        {
            return _dbRepo.GetItemsWithGenres();
        }

        public List<CategoryDto> ListCategoriesAndColors()
        {
            return _dbRepo.ListCategoriesAndColors();
        }

        public List<ItemDto> ListInventory()
        {
            return _dbRepo.ListInventory();
        }
    }
}
