using Amazon.DynamoDBv2.DataModel;

namespace VehicleApi.Core.Models
{
    [DynamoDBTable("RegistrationNumbers")]
    public class RegistrationNumber
    {
        [DynamoDBHashKey]
        public string Id { get; set; }

        [DynamoDBHashKey]
        public string VehicleId { get; set; }

        [DynamoDBHashKey]
        public string CustomerId { get; set; }
    }
}
