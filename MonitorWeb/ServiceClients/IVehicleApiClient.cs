using MonitorWeb.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonitorWeb.ServiceClients
{
    public interface IVehicleApiClient
    {
       //Task<string>  GetStatus(string vehicleId);

       //void UpdateStatus(VehicleViewModel vehicle);

       Task< IEnumerable<VehicleViewModel>> GetVehicles();

        Task<IEnumerable<VehicleViewModel>> UpdateVehicles(IEnumerable<VehicleViewModel> vehicles);
    }
}
