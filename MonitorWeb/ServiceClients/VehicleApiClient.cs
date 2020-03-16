using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using MonitorWeb.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MonitorWeb.ServiceClients
{
    public class VehicleApiClient : IVehicleApiClient
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;
        private readonly string _baseAddress;

        public VehicleApiClient(IConfiguration configuration, HttpClient client, IHostingEnvironment environment)
        {
            _client = client;
            _configuration = configuration;

            if (environment.IsDevelopment())
            {
                _baseAddress = configuration.GetSection("VehicleApi").GetValue<string>("BaseUrl");

            }
            else
            {
                _baseAddress = Environment.GetEnvironmentVariable("VehicleApiUrl");

            }
        }

        public async Task<IEnumerable<VehicleViewModel>> GetVehicles()
        {
            var response = await _client.GetAsync(new Uri($"{_baseAddress}"));
            return await response.Content.ReadAsAsync<IEnumerable<VehicleViewModel>>();
        }

        public async Task<IEnumerable<VehicleViewModel>> UpdateVehicles(IEnumerable<VehicleViewModel> vehicles)
        {
            var jsonModel = JsonConvert.SerializeObject(vehicles);
            var response = await _client.PutAsync(new Uri($"{_baseAddress}/UpdateVehicles"),
                new StringContent(jsonModel, Encoding.UTF8, "application/json"));
            
            return await response.Content.ReadAsAsync<IEnumerable<VehicleViewModel>>();
        }

    }
}
