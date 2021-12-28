using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace InventoryModels
{
    public class Category : FullAuditModel
    {
        [StringLength(InventoryModelConstants.MAX_NAME_LENGTH)]
        public string Name { get; set; }
        public virtual List<Item> Items { get; set; } = new List<Item>();
        public virtual CategoryColor CategoryColor { get; set; }
        public int? CategoryColorId { get; set; }
    }
}
