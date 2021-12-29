using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryModels
{
    public class ItemGenre : FullAuditModel
    {
        [Required]
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        [Required]
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
