using Amazon.DynamoDBv2.DataModel;

namespace VehicleApi.Core.Models
{
    [DynamoDBTable("Vehicles")]
    public class Vehicle
    {
        [DynamoDBHashKey]
        public  string Id { get; set; }

        //[DynamoDBProperty]
        //public  string RegistrationNumber { get; set; }

        [DynamoDBProperty]
        public VehicleStatus Status { get; set; }
    }
}
