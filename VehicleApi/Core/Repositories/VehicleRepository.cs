using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleApi.Core.Models;

namespace VehicleApi.Core.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly RegionEndpoint _regionEndPoint;
        private readonly IHostingEnvironment _environment;

        public VehicleRepository(IConfiguration configuration, IHostingEnvironment environment)
        {
            _environment = environment;

            _regionEndPoint =
                RegionEndpoint.GetBySystemName(configuration.GetSection("AWS").GetValue<string>("Region"));


        }

        public async Task<Vehicle> Get(string vehicleId)
        {
            if (_environment.IsProduction())
            {
                using (var client = new AmazonDynamoDBClient(GetAWSEnvironmentCredentials(), _regionEndPoint))
                {
                    using (var context = new DynamoDBContext(client))
                    {
                        return await context.LoadAsync<Vehicle>(vehicleId);
                    }
                }
            }
            else
            {
                using (var client = new AmazonDynamoDBClient(_regionEndPoint))
                {
                    using (var context = new DynamoDBContext(client))
                    {
                        return await context.LoadAsync<Vehicle>(vehicleId);
                    }
                }

            }
        }

        public async Task<IEnumerable<Vehicle>> GetAll()
        {
            if (_environment.IsProduction())
            {
                using (var client = new AmazonDynamoDBClient(GetAWSEnvironmentCredentials(), _regionEndPoint))
                {
                    using (var context = new DynamoDBContext(client))
                    {
                        return await context.ScanAsync<Vehicle>(new List<ScanCondition>()).GetRemainingAsync();

                    }
                }
            }
            else
            {
                using (var client = new AmazonDynamoDBClient( _regionEndPoint))
                {
                    using (var context = new DynamoDBContext(client))
                    {
                        return await context.ScanAsync<Vehicle>(new List<ScanCondition>()).GetRemainingAsync();

                    }
                }

            }
        }

        public async Task Update(Vehicle vehicle)
        {
            if (_environment.IsProduction())
            {
                using (var client = new AmazonDynamoDBClient(GetAWSEnvironmentCredentials(), _regionEndPoint))
                {
                    using (var context = new DynamoDBContext(client))
                    {
                        await context.SaveAsync(vehicle);
                    }
                }
            }
            else
            {
                using (var client = new AmazonDynamoDBClient( _regionEndPoint))
                {
                    using (var context = new DynamoDBContext(client))
                    {
                        await context.SaveAsync(vehicle);
                    }
                }

            }
        }


        private BasicAWSCredentials GetAWSEnvironmentCredentials()
        {
            EnvironmentVariablesAWSCredentials envCredentials = new EnvironmentVariablesAWSCredentials();
            ImmutableCredentials imCredentials = envCredentials.FetchCredentials();
            BasicAWSCredentials basicCredentials =
                new BasicAWSCredentials(imCredentials.AccessKey, imCredentials.SecretKey);

            return basicCredentials;
        }

    }
}
