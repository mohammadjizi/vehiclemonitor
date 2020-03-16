using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleApi.Core.Models;

namespace VehicleApi.Core.Repositories
{
    public interface IVehicleRepository
    {
       Task< Vehicle> Get(string vehicleId);

        Task Update(Vehicle vehicle);

       Task< IEnumerable<Vehicle>> GetAll();
    }
}
