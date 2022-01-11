using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryModels.DTOs
{
    public class ItemDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Name,25} | {Description}";
        }
    }
}
