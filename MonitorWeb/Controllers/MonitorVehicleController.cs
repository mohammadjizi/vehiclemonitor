using Microsoft.AspNetCore.Mvc;
using MonitorWeb.ServiceClients;
using MonitorWeb.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MonitorWeb.Controllers
{
    [Route("api/[controller]")]
    public class MonitorVehicleController : Controller
    {
        private readonly IVehicleApiClient _vehicleApiClient;
        private readonly ICustomerApiClient _customerApiClient;

        public MonitorVehicleController(IVehicleApiClient vehicleApiClient, ICustomerApiClient customerApiClient)
        {
            _vehicleApiClient = vehicleApiClient;
            _customerApiClient = customerApiClient;
        }

        [HttpGet("[action]")]
        public IEnumerable<MonitorViewModel> GetVehicles()
        {
            var vehicles = _vehicleApiClient.GetVehicles().Result;
            var customers = _customerApiClient.GetCustomers().Result;
            var customerOwnerships = _customerApiClient.GetCustomerOwnerships().Result;

            var monitorList = new List<MonitorViewModel>();

            foreach (var ownership in customerOwnerships)
            {
                var monitor = new MonitorViewModel();
                monitor.OwnsershipId = ownership.Id;
                monitor.VehicleId = ownership.VehicleId;
                monitor.CustomerId = ownership.CustomerId;
                monitor.CustomerName = customers.FirstOrDefault(c => c.Id == ownership.CustomerId).Name;
                monitor.CustomerAddress = customers.FirstOrDefault(c => c.Id == ownership.CustomerId).Address;
                monitor.VehicleStatus = vehicles.FirstOrDefault(c => c.Id == monitor.VehicleId).Status;

                monitorList.Add(monitor);
            }

            return monitorList.ToArray();
        }

        [HttpPut("[action]")]
        public IEnumerable<MonitorViewModel> PingVehicles([FromBody] IEnumerable<MonitorViewModel> monitorList)
        {
            List<VehicleViewModel> vehiclesToUpdate = (from monitor in monitorList
                select new VehicleViewModel() {Id = monitor.VehicleId, Status = monitor.VehicleStatus}).ToList();

            var updatedVehicles = _vehicleApiClient.UpdateVehicles(vehiclesToUpdate).Result;

            foreach (var monitor in monitorList)
            {
                monitor.VehicleStatus = updatedVehicles.First(c => c.Id == monitor.VehicleId).Status;
            }

            return monitorList.ToArray();
        }

    }
}
