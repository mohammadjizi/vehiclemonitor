using AutoMapper;
using VehicleApi.Core.Dtos;
using VehicleApi.Core.Models;

namespace CustomerApi.Core.Mapping
{
    public class DomainToDtoMappingProfile:Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Vehicle, VehicleDto>();
        }
    }
}
