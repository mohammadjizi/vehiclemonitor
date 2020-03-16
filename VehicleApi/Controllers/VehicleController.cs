using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleApi.Core.Dtos;
using VehicleApi.Core.Models;
using VehicleApi.Core.Repositories;

namespace VehicleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;

        public VehicleController(IVehicleRepository vehicleRepository, IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleDto>>> GetVehicles()
        {
            var vehicleList = await _vehicleRepository.GetAll();

            return _mapper.Map<List<VehicleDto>>(vehicleList);
        }



        [HttpPut("UpdateVehicles")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VehicleDto>>> UpdateVehicles(
            [FromBody] IEnumerable<VehicleDto> vehicleDtos)
        {
            var emptyIdList = vehicleDtos.Where(c => string.IsNullOrEmpty(c.Id));
            if (emptyIdList.Count() > 0)
            {
                return BadRequest(emptyIdList);
            }

            var wrongStatusList = vehicleDtos.Where(c =>
                c.Status != VehicleStatus.Connected && c.Status != VehicleStatus.Disconnected);
            if (wrongStatusList.Count() > 0)
            {
                return BadRequest(wrongStatusList);
            }

            foreach (var vDto in vehicleDtos)
            {
                List<string> idListNotFound = new List<string>();

                Vehicle vehicle = await _vehicleRepository.Get(vDto.Id);
                if (vehicle == null)
                {
                    idListNotFound.Add(vDto.Id);
                }

                if (idListNotFound.Count() > 0)
                {
                    return NotFound(vehicleDtos.Where(c => idListNotFound.Contains(c.Id)));
                }
            }

            Random r = new Random();

            var vehicles = (IEnumerable<Vehicle>) _mapper.Map(vehicleDtos, typeof(IEnumerable<VehicleDto>),
                typeof(IEnumerable<Vehicle>));

            foreach (var vehicle in vehicles)
            {
                var status = r.Next(0, 2);

                vehicle.Status = Enum.Parse<VehicleStatus>(Convert.ToString((status)));
                await _vehicleRepository.Update(vehicle);
            }


            vehicleDtos = (IEnumerable<VehicleDto>) _mapper.Map(vehicles, typeof(IEnumerable<Vehicle>),
                typeof(IEnumerable<VehicleDto>));

            return Ok(vehicleDtos);
        }

    }
}
