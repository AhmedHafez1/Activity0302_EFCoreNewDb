﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryModels.DTOs
{
    public class CategoryDto
    {
        public string Category { get; set; }
        public CategoryColorDto CategoryColor { get; set; }
    }
}
