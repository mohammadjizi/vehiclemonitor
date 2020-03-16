using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using CustomerApi.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerApi.Core.Repositories
{
    public class CustomerOwnershipRepository : ICustomerOwnershipRepository
    {
        private readonly RegionEndpoint _regionEndPoint;
        private readonly IHostingEnvironment _environment;

        public CustomerOwnershipRepository(IConfiguration configuration, IHostingEnvironment environment)
        {
            _environment = environment;

            _regionEndPoint =
                RegionEndpoint.GetBySystemName(configuration.GetSection("AWS").GetValue<string>("Region"));

        }

        public async Task<IEnumerable<CustomerOwnership>> GetAll()
        {
            if (_environment.IsProduction())
            {
                EnvironmentVariablesAWSCredentials envCredentials = new EnvironmentVariablesAWSCredentials();
                ImmutableCredentials imCredentials = envCredentials.FetchCredentials();
                BasicAWSCredentials basicCredentials =
                    new BasicAWSCredentials(imCredentials.AccessKey, imCredentials.SecretKey);


                using (var client = new AmazonDynamoDBClient(basicCredentials, _regionEndPoint))
                {
                    using (var context = new DynamoDBContext(client))
                    {
                        return await context.ScanAsync<CustomerOwnership>(new List<ScanCondition>()).GetRemainingAsync();

                    }
                }
            }
            else
            {

                using (var client = new AmazonDynamoDBClient( _regionEndPoint))
                {
                    using (var context = new DynamoDBContext(client))
                    {
                        return await context.ScanAsync<CustomerOwnership>(new List<ScanCondition>()).GetRemainingAsync();

                    }
                }
            }


            
        }
    }
}
