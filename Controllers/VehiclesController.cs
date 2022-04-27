﻿using trackingApi.Services;
using trackingApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace trackingApi.Controllers
{
    [ApiController]
    [Route("api/Vehicles")]
    public class VehiclesController : ControllerBase
    {
        private readonly VehiclesService _vehiclesService;
        public VehiclesController(VehiclesService vehiclesService) =>
                _vehiclesService = vehiclesService;

        [HttpGet]
        public async Task<List<Vehicle>> Get() =>
            await _vehiclesService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Vehicle>> Get(string id)
        {
            var vehicle = await _vehiclesService.GetAsync(id);
            if (vehicle is null)
            {
                return NotFound();
            }
            return vehicle;
        }
        [HttpPost]
        public async Task<IActionResult> Post(Vehicle newVehicle)
        {
            await _vehiclesService.CreateAsync(newVehicle);

            return CreatedAtAction(nameof(Get), new { id = newVehicle.Id }, newVehicle);
        }
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id,Vehicle updatedVehicle) 
        {
            var vehicle = await _vehiclesService.GetAsync(id); 
            if (vehicle is null)
            {
                return NotFound();
            }
            updatedVehicle.Id = vehicle.Id;

            await _vehiclesService.UpdateAsync(id, updatedVehicle);

            return NoContent();
        }
        [HttpPut("{id:length(24)}/Location")]
        public async Task<IActionResult> UpdateLocation(string id, string gps)
        {
            Location location;
            Vehicle coche = new Vehicle();
            Console.WriteLine(gps);
            
            try
            {
                location = new Location(gps);
            }
            catch (Exception ex)
            {
               // Console.WriteLine(ex);
                return BadRequest(ex.Message);
            };
            Console.WriteLine(location.Lat);
            Console.WriteLine(location.Long);
            var vehicle = await _vehiclesService.GetAsync(id);
            if (vehicle is null)
            {
                return NotFound();
            }
            

            return NoContent();
        }


        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var vehicle = await _vehiclesService.GetAsync(id);
            if (vehicle is null)
            { 
                return NotFound();
            }
            await _vehiclesService.RemoveAsync(id);
            return NoContent();
        }

    }
}
