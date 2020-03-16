using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MonitorWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MonitorWeb.ServiceClients
{
    public class CustomerApiClient: ICustomerApiClient
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly string _baseAddress;

        public CustomerApiClient(IConfiguration configuration , HttpClient client, IHostingEnvironment environment)
        {
            _client = client;
            _configuration = configuration;

            if (environment.IsDevelopment())
            {
                _baseAddress = configuration.GetSection("CustomerApi").GetValue<string>("BaseUrl"); 

            }
            else
            {
                _baseAddress = Environment.GetEnvironmentVariable("CustomerApiUrl");

            }

        }

        public async Task<IEnumerable<CustomerViewModel>> GetCustomers()
        {
            var response = await _client.GetAsync(new Uri($"{_baseAddress}"));

            return await response.Content.ReadAsAsync<IEnumerable<CustomerViewModel>>();
        }

        public async Task<IEnumerable<CustomerOwnershipViewModel>> GetCustomerOwnerships()
        {
            var response = await _client.GetAsync(new Uri($"{_baseAddress}/GetCustomerOwnerships"));

            return await response.Content.ReadAsAsync<IEnumerable<CustomerOwnershipViewModel>>();
        }
    }
}
