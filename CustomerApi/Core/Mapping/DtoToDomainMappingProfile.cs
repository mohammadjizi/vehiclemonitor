using AutoMapper;
using CustomerApi.Core.Dtos;
using CustomerApi.Core.Models;

namespace CustomerApi.Core.Mapping
{
    public class DtoToDomainMappingProfile:Profile
    {
        public DtoToDomainMappingProfile()
        {
            CreateMap<CustomerDto, Customer>();
            CreateMap<CustomerOwnershipDto, CustomerOwnership>();
        }
    }
}
