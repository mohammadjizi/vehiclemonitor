using Amazon.DynamoDBv2.DataModel;

namespace CustomerApi.Core.Models
{
    [DynamoDBTable("CustomerOwnerships")]
    public class CustomerOwnership
    {
        [DynamoDBHashKey]
        public string Id { get; set; }

        [DynamoDBProperty]
        public string VehicleId { get; set; }

        [DynamoDBProperty]
        public int CustomerId { get; set; }
    }
}
