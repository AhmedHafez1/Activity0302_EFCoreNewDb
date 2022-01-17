using InventoryModels.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryBusinessLayer
{
    interface IItemsService
    {
        List<GetItemsForListingWithDateDto> GetItemsForListingLinq(DateTime minDateValue, DateTime maxDateValue);
        List<GetItemsForListingDto> GetItemsForListingFromProcedure(DateTime minDateValue, DateTime maxDateValue);
        AllItemsPipeDelimitedStringDto GetItemsPipeDelimitedString(bool isActive);
        List<GetItemsTotalValueDto> GetItemsTotalValues(bool isActive);
        List<ItemsWithGenresDto> GetItemsWithGenres();
        List<CategoryDto> ListCategoriesAndColors();
        List<ItemDto> ListInventory();
    }
}
