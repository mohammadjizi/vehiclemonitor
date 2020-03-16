using AutoMapper;
using CustomerApi.Core.Dtos;
using CustomerApi.Core.Models;

namespace CustomerApi.Core.Mapping
{
    public class DomainToDtoMappingProfile:Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerOwnership, CustomerOwnershipDto>();
        }
    }
}
