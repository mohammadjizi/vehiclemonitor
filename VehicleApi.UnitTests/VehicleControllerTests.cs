using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleApi.Controllers;
using VehicleApi.Core.Dtos;
using VehicleApi.Core.Models;
using VehicleApi.Core.Repositories;

namespace VehicleApi.UnitTests
{
    [TestFixture]
    public class VehicleControllerTests
    {
        private VehicleController _vehicleController;
        private Mock<IVehicleRepository> _vehicleRepository;
        private Mock<IMapper> _mapper;

        [SetUp]
        public void SetUp()
        {
            _vehicleRepository = new Mock<IVehicleRepository>();
            _mapper = new Mock<IMapper>();

            _vehicleController = new VehicleController(_vehicleRepository.Object, _mapper.Object);
        }

        [Test]
        public void GetVehicles_WhenCalled_ReturnActionResultOfVehicleDtoList()
        {
            List<Vehicle> vList = new List<Vehicle>();
            vList.Add(new Vehicle() { Id = "YS2R4X20005399401",Status = VehicleStatus.Connected  });

            _vehicleRepository.Setup(c => c.GetAll()).ReturnsAsync(vList);

            List<VehicleDto> vdList = new List<VehicleDto>();
            vdList.Add(new VehicleDto() { Id = "YS2R4X20005399401", Status = VehicleStatus.Connected });

            _mapper.Setup(c => c.Map<List<VehicleDto>>(vList)).Returns(vdList);

            var result = _vehicleController.GetVehicles();

            Assert.That(result, Is.InstanceOf<Task<ActionResult<IEnumerable<VehicleDto>>>>());
        }

        [Test]
        public void UpdateVehicles_WithEmptyIds_ReturnsActionResultOfBadRequest()
        {
            List<Vehicle> vList = new List<Vehicle>();
            vList.Add(new Vehicle() { Id = "", Status = VehicleStatus.Connected });

            _vehicleRepository.Setup(c => c.GetAll()).ReturnsAsync(vList);

            List<VehicleDto> vdList = new List<VehicleDto>();
            vdList.Add(new VehicleDto() { Id = "", Status = VehicleStatus.Connected });

            _mapper.Setup(c => c.Map<List<VehicleDto>>(vList)).Returns(vdList);

            var result = _vehicleController.UpdateVehicles(vdList);


            Assert.That(result.Result.Result, Is.InstanceOf<BadRequestObjectResult>());
        }

        [Test]
        public void UpdateVehicles_WithWrongIds_ReturnsActionResultOfNotFound()
        {
            List<Vehicle> vList = new List<Vehicle>();
            vList.Add(new Vehicle() { Id = "YS2R4X20005399408", Status = VehicleStatus.Connected });

            _vehicleRepository.Setup(c => c.GetAll()).ReturnsAsync(vList);

            List<VehicleDto> vdList = new List<VehicleDto>();
            vdList.Add(new VehicleDto() { Id = "YS2R4X20005399408", Status = VehicleStatus.Connected });

            _mapper.Setup(c => c.Map<List<VehicleDto>>(vList)).Returns(vdList);

            var result = _vehicleController.UpdateVehicles(vdList);


            Assert.That(result.Result.Result, Is.InstanceOf<NotFoundObjectResult>());
        }

        [Test]
        public void UpdateVehicles_WhenCalled_ReturnActionResultOfVehicleDtoList()
        {
            List<Vehicle> vList = new List<Vehicle>();
            vList.Add(new Vehicle() { Id = "YS2R4X20005399401", Status = VehicleStatus.Connected });

            _vehicleRepository.Setup(c => c.GetAll()).ReturnsAsync(vList);

            List<VehicleDto> vdList = new List<VehicleDto>();
            vdList.Add(new VehicleDto() { Id = "YS2R4X20005399401", Status = VehicleStatus.Connected });

            _mapper.Setup(c => c.Map<List<VehicleDto>>(vList)).Returns(vdList);

            var result = _vehicleController.UpdateVehicles(vdList);

            Assert.That(result, Is.InstanceOf<Task<ActionResult<IEnumerable<VehicleDto>>>>());
        }

    }
}
