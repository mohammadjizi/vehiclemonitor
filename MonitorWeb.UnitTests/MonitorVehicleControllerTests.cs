using MonitorWeb.Controllers;
using MonitorWeb.ServiceClients;
using MonitorWeb.ViewModels;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MonitorWeb.UnitTests
{
    [TestFixture]
    public class MonitorVehicleControllerTests
    {
        private MonitorVehicleController _monitorViewModelController;
        private Mock<IVehicleApiClient> _vehicleApiClient;
        private Mock<ICustomerApiClient> _customerApiClient;

        [SetUp]
        public void SetUp()
        {
            _vehicleApiClient = new Mock<IVehicleApiClient>();
            _customerApiClient = new Mock<ICustomerApiClient>();

            _monitorViewModelController = new MonitorVehicleController(_vehicleApiClient.Object, _customerApiClient.Object);
        }

        [Test]
        public void GetVehicles_WhenCalled_ReturnMonitorViewModelList()
        {
            List<VehicleViewModel> vList = new List<VehicleViewModel>();
            vList.Add(new VehicleViewModel() { Id = "YS2R4X20005399401", Status = VehicleStatus.Connected });

            _vehicleApiClient.Setup(c => c.GetVehicles()).ReturnsAsync(vList);

            List<CustomerViewModel> cList = new List<CustomerViewModel>();
            cList.Add(new CustomerViewModel() { Address = "Cementvägen 8, 111 11 Södertälje", Id = 1, Name = "Kalles Grustransporter AB" });

            _customerApiClient.Setup(c => c.GetCustomers()).ReturnsAsync(cList);

            List<CustomerOwnershipViewModel> coList = new List<CustomerOwnershipViewModel>();
            coList.Add(new CustomerOwnershipViewModel() { CustomerId = 1, Id = "ABC123", VehicleId = "YS2R4X20005399401" });

            _customerApiClient.Setup(c => c.GetCustomerOwnerships()).ReturnsAsync(coList);

            var result = _monitorViewModelController.GetVehicles();

            Assert.That(result, Is.InstanceOf<IEnumerable<MonitorViewModel>>());
        }

        [Test]
        public void PingVehicles_WhenCalled_ReturnMonitorViewModelList()
        {
            List<VehicleViewModel> vList = new List<VehicleViewModel>();
            vList.Add(new VehicleViewModel() { Id = "YS2R4X20005399401", Status = VehicleStatus.Connected });

            List<CustomerViewModel> cList = new List<CustomerViewModel>();
            cList.Add(new CustomerViewModel()
            { Address = "Cementvägen 8, 111 11 Södertälje", Id = 1, Name = "Kalles Grustransporter AB" });

            List<CustomerOwnershipViewModel> coList = new List<CustomerOwnershipViewModel>();
            coList.Add(new CustomerOwnershipViewModel()
            { CustomerId = 1, Id = "ABC123", VehicleId = "YS2R4X20005399401" });

            List<VehicleViewModel> updatedVList = new List<VehicleViewModel>();
            updatedVList.Add(new VehicleViewModel() { Id = "YS2R4X20005399401", Status = VehicleStatus.Disconnected });

            _vehicleApiClient.Setup(c => c.UpdateVehicles(It.IsAny<List<VehicleViewModel>>()))
                .ReturnsAsync(updatedVList);

            List<MonitorViewModel> mList = new List<MonitorViewModel>();

            foreach (var ownership in coList)
            {
                var monitor = new MonitorViewModel();
                monitor.OwnsershipId = ownership.Id;
                monitor.VehicleId = ownership.VehicleId;
                monitor.CustomerId = ownership.CustomerId;
                monitor.CustomerName = cList.FirstOrDefault(c => c.Id == ownership.CustomerId).Name;
                monitor.CustomerAddress = cList.FirstOrDefault(c => c.Id == ownership.CustomerId).Address;
                monitor.VehicleStatus = vList.FirstOrDefault(c => c.Id == monitor.VehicleId).Status;

                mList.Add(monitor);
            }


            var result = _monitorViewModelController.PingVehicles(mList);

            Assert.That(result, Is.InstanceOf<IEnumerable<MonitorViewModel>>());
        }

    }
}
