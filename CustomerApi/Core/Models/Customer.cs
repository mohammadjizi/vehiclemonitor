using Amazon.DynamoDBv2.DataModel;

namespace CustomerApi.Core.Models
{
    [DynamoDBTable("Customers")]
    public class Customer
    {
        [DynamoDBHashKey]
        public int Id { get; set; }

        [DynamoDBProperty]
        public string Name { get; set; }

        [DynamoDBProperty]
        public string Address { get; set; }

        //[DynamoDBProperty]
        //public IEnumerable<string> VehicleIdList { get; set; }
    }
}
