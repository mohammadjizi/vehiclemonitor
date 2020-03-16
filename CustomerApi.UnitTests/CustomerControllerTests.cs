using AutoMapper;
using CustomerApi.Controllers;
using CustomerApi.Core.Dtos;
using CustomerApi.Core.Models;
using CustomerApi.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerApi.UnitTests
{
    [TestFixture]
    public class CustomerControllerTests
    {
        private CustomerController _customerController;
        private Mock<ICustomerRepository> _customerRepository;
        private Mock<ICustomerOwnershipRepository> _customerOwnershipRepository;
        private Mock<IMapper> _mapper;

        [SetUp]
        public void SetUp()
        {
            _customerRepository=new Mock<ICustomerRepository>();
            _customerOwnershipRepository=new Mock<ICustomerOwnershipRepository>();
            _mapper=new Mock<IMapper>();

            _customerController = new CustomerController(_customerRepository.Object,
                _customerOwnershipRepository.Object, _mapper.Object);
        }

        [Test]
        public void GetCustomers_WhenCalled_ReturnActionResultOfCustomerDtoList()
        {
            List<Customer> cList=new List<Customer>();
            cList.Add(new Customer(){ Address = "Cementvägen 8, 111 11 Södertälje", Id = 1, Name = "Kalles Grustransporter AB" });

            _customerRepository.Setup(c => c.GetAll()).ReturnsAsync(cList);

            List<CustomerDto> cdList=new List<CustomerDto>();
            cdList.Add(new CustomerDto() { Address = "Cementvägen 8, 111 11 Södertälje", Id = 1, Name = "Kalles Grustransporter AB" });

            _mapper.Setup(c => c.Map<List<CustomerDto>>(cList)).Returns(cdList);              
               
            var result = _customerController.GetCustomers();

            Assert.That(result, Is.InstanceOf<Task<ActionResult<IEnumerable<CustomerDto>>>>());
        }

        [Test]
        public void GetCustomerOwnerships_WhenCalled_ReturnActionResultOfCustomerOwnershipDtoList()
        {
            List<CustomerOwnership> coList = new List<CustomerOwnership>();
            coList.Add(new CustomerOwnership() { CustomerId = 1, Id = "ABC123", VehicleId = "YS2R4X20005399401" });

            _customerOwnershipRepository.Setup(c => c.GetAll()).ReturnsAsync(coList);

            List<CustomerOwnershipDto> codList = new List<CustomerOwnershipDto>();
            codList.Add(new CustomerOwnershipDto() { CustomerId = 1, Id = "ABC123", VehicleId = "YS2R4X20005399401" });

            _mapper.Setup(c => c.Map<List<CustomerOwnershipDto>>(coList)).Returns(codList);

            var result = _customerController.GetCustomerOwnerships();

            Assert.That(result, Is.InstanceOf<Task<ActionResult<IEnumerable<CustomerOwnershipDto>>>>());
        }
    }
}
