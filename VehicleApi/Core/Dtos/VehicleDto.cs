using VehicleApi.Core.Models;

namespace VehicleApi.Core.Dtos
{
    public class VehicleDto
    {
        public string Id { get; set; }

        //public string RegistrationNumber { get; set; }

        public VehicleStatus Status { get; set; }
    }
}
