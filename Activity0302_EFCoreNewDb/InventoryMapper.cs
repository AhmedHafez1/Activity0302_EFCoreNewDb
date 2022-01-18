using AutoMapper;
using InventoryModels;
using InventoryModels.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Activity0302_EFCoreNewDb
{
    public class InventoryMapper : Profile
    {
        public InventoryMapper()
        {
            CreateMaps();
        }

        private void CreateMaps()
        {
            CreateMap<Item, ItemDto>().ReverseMap();

            CreateMap<Category, CategoryDto>()
                .ForMember(x => x.Category, opt => opt.MapFrom(y => y.Name))
                .ReverseMap()
                .ForMember(y => y.Name, opt => opt.MapFrom(x => x.Category));


            CreateMap<CategoryColor, CategoryColorDto>()
                .ForMember(x => x.Color, opt => opt.MapFrom(y => y.ColorValue))
                .ReverseMap()
                .ForMember(y => y.ColorValue, opt => opt.MapFrom(x => x.Color));

            CreateMap<Item, CreateOrUpdateItemDto>()
                .ReverseMap();
                //.ForMember(x => x.Category, opt => opt.Ignore());
        }
    }
}
