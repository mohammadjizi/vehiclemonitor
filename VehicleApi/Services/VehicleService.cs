using VehicleApi.Data.Models;
using VehicleApi.Data.Repositories;

namespace VehicleApi.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        internal VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public VehicleStatus GetStatus(string vehicleId)
        {
            return _vehicleRepository.Get(vehicleId).Result.Status;
        }
    }
}
