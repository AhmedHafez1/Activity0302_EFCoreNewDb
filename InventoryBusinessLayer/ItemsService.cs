using InventoryDatabaseLayer;
using InventoryModels.DTOs;
using System;
using System.Collections.Generic;
using InventoryDatabaseCore;
using AutoMapper;
using InventoryModels;

namespace InventoryBusinessLayer
{
    public class ItemsService : IItemsService
    {
        private readonly IInventoryDatabaseRepo _dbRepo;
        private readonly IMapper _mapper;

        public ItemsService(InventoryDbContext context, IMapper mapper)
        {
            _dbRepo = new InventoryDatabaseRepo(context, mapper);
            _mapper = mapper;
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
            return _mapper.Map<List<ItemDto>>(_dbRepo.ListInventory());
        }

        public int InsertOrUpdateItem(CreateOrUpdateItemDto item)
        {
            if (item.CategoryId <= 0)
            {
                throw new ArgumentException("Please set the category id before insert or update");
            }
            return _dbRepo.InsertOrUpdateItem(_mapper.Map<Item>(item));
        }

        public void InsertOrUpdateItems(List<CreateOrUpdateItemDto> items)
        {
            _dbRepo.InsertOrUpdateItems(_mapper.Map<List<Item>>(items));
        }

        public void DeleteItem(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Please set a valid item id before deleting");
            }
            _dbRepo.DeleteItem(id);
        }

        public void DeleteItems(List<int> itemIds)
        {
            try
            {
                _dbRepo.DeleteItems(itemIds);
            }
            catch (Exception ex)
            {
                //TODO: better logging/not squelching
                Console.WriteLine($"The transaction has failed: {ex.Message}");
            }
        }
    }
}
