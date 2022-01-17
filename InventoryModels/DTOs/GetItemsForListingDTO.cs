using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryModels.DTOs
{
    public class GetItemsForListingDto
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string Notes { get; set; } = "";
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = true;
        public string CategoryName { get; set; } = "";
    }
}
