using System;
using Shared;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace InventoryModels
{
    public class Item : FullAuditModel
    {
        [Required]
        [StringLength(InventoryModelConstants.MAX_NAME_LENGTH)]
        public string Name { get; set; }

        [Range(InventoryModelConstants.MINIMUM_QUANTITY, InventoryModelConstants.MAXIMUM_QUANTITY)]
        public int Quantity { get; set; }

        [StringLength(InventoryModelConstants.MAX_DESCRIPTION_LENGTH)]
        public string Description { get; set; }

        [StringLength(InventoryModelConstants.MAX_NOTES_LENGTH, MinimumLength = 10)]
        public string Notes { get; set; }

        public bool IsOnSale { get; set; }

        public DateTime? PurchasedDate { get; set; }
        public DateTime? SoldDate { get; set; }
        
        [Range(InventoryModelConstants.MINIMUM_PRICE, InventoryModelConstants.MAXIMUM_PRICE)]
        public decimal? PurchasePrice { get; set; }

        [Range(InventoryModelConstants.MINIMUM_PRICE, InventoryModelConstants.MINIMUM_PRICE)]
        public decimal? CurrentOrFinalPrice { get; set; }

        public virtual Category Category { get; set; }
        public int? CategoryId { get; set; }

        public virtual List<ItemGenre> ItemGenres { get; set; } = new List<ItemGenre>();
    }
}
