using AutoMapper;
using MTM.Application.Services.Inventory.Dto;
using MTM.Domain.Inventories.ValueObjects;
using MTM.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTM.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<InventoryDto, Inventory>().ForMember(s => s.Id, opt => opt.MapFrom(w => w.Id));
            CreateMap<Inventory, InventoryDto>().ForMember(s => s.Id, opt => opt.MapFrom(w => Guid.NewGuid()));
            
        }
    }
}
