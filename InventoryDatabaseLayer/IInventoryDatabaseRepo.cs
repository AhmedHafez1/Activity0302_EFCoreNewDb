using InventoryModels.DTOs;
using System;
using System.Collections.Generic;

namespace InventoryDatabaseLayer
{
    public interface IInventoryDatabaseRepo
    {
        List<GetItemsForListingDto> GetItemsForListingFromProcedure(DateTime dateDateValue, DateTime maxDateValue);
        List<GetItemsForListingWithDateDto> GetItemsForListingLinq(DateTime minDateValue, DateTime maxDateValue);
        AllItemsPipeDelimitedStringDto GetItemsPipeDelimitedString(bool isActive);
        List<GetItemsTotalValueDto> GetItemsTotalValues(bool isActive);
        List<ItemsWithGenresDto> GetItemsWithGenres();
        List<CategoryDto> ListCategoriesAndColors();
        List<ItemDto> ListInventory();
    }
}
