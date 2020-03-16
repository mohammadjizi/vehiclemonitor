using AutoMapper;
using CustomerApi.Core.Dtos;
using CustomerApi.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerOwnershipRepository _customerOwnershipRepository;

        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepository, 
            ICustomerOwnershipRepository customerOwnershipRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _customerOwnershipRepository = customerOwnershipRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task< ActionResult<IEnumerable<CustomerDto>>> GetCustomers()
        {
            var customerList= await _customerRepository.GetAll();

            return _mapper.Map<List<CustomerDto>>(customerList);
        }

        [HttpGet]
        [Route("GetCustomerOwnerships")]
        public async Task< ActionResult<IEnumerable<CustomerOwnershipDto>>> GetCustomerOwnerships()
        {
            var customerOwnershipList = await _customerOwnershipRepository.GetAll();

            return _mapper.Map<List<CustomerOwnershipDto>>(customerOwnershipList);
        }

    }
}
