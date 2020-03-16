using VehicleApi.Data.Models;

namespace VehicleApi.Services
{
    internal interface IVehicleService
    {
        VehicleStatus GetStatus(string vehicleId);
    }
}
