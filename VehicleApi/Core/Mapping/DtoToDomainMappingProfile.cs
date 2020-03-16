using AutoMapper;
using VehicleApi.Core.Dtos;
using VehicleApi.Core.Models;

namespace CustomerApi.Core.Mapping
{
    public class DtoToDomainMappingProfile:Profile
    {
        public DtoToDomainMappingProfile()
        {
            CreateMap<VehicleDto, Vehicle>();
        }
    }
}
