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
        private readonly PedidosService _pedidosService;
        private readonly EmailService _emailService;
        
        public VehiclesController(VehiclesService vehiclesService, PedidosService pedidosService, EmailService emailService)
        {
            _vehiclesService = vehiclesService;
            _pedidosService = pedidosService;
            _emailService = emailService;
        }

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
        [HttpGet("{id:length(24)}/Distance")]
        public async Task<ActionResult<double>> GetDistance(string id, string PedidoID )
        {
            double distancia = 0;

            var vehicle = await _vehiclesService.GetAsync(id); 
            var pedido = await _pedidosService.GetAsync(id);

            if(vehicle is null) 
            {
                return NotFound(); 
            }
            if (!vehicle.pedidos.Contains(PedidoID))
            {
                return BadRequest("El pedido no esta en este vehiculo");
            }
            
            distancia = vehicle.locations.Last().Long;
            //uso la longitud como muestra, en realidad se calcularía la distancia con alguna API

            //He visto que se puede calcular la distancia entre unas coordenadas y una dirección con la api de google maps,
            //el problema es que para ello me tenia que registrar para consegir una api Key y necesitaba tarjeta de credito,
            //asi que no queria perder tiempo con ello ni jugarme un posible cobro.

            return distancia;
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
            MyLocation location;
            Console.WriteLine(gps);
            
            try
            {
                location = new MyLocation(gps);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            };
            Console.WriteLine(location.Lat);
            Console.WriteLine(location.Long);
            var vehicle = await _vehiclesService.GetAsync(id);
            if (vehicle is null)
            {
                return NotFound();
            }
            vehicle.locations.Add(location);
            
            await _vehiclesService.UpdateAsync(id, vehicle);
            // en primera instancia pense en utilizar el email para notificar la posicion , pero es poco practico
            // await _emailService.SendEmailAsync("joclas.16@gmail.com", "Pedido", "la ubicacion de su pedido ha cambiado");

            return NoContent();
        }
       
        [HttpPut("{id:length(24)}/Pedidos")]
        public async Task<IActionResult> addOrder(string id, string pedidoID)
        {
            var vehicle = await _vehiclesService.GetAsync(id);
            if (vehicle is null)
            {
                return NotFound();
            }
            var pedido = await _pedidosService.GetAsync(pedidoID);
            if (pedido is null) 
            {
                return NotFound();
            }
            if (vehicle.pedidos.Contains(pedidoID))
            {
                return BadRequest("Pedido ya en el vehiculo");
            }
            vehicle.pedidos.Add(pedidoID);
            await _vehiclesService.UpdateAsync(id, vehicle);

            return NoContent();
        }
        [HttpPut("{id:length(24)}/DropPedidos")]
        public async Task<IActionResult> dropOrder(string id, string pedidoID)
        {
            var vehicle = await _vehiclesService.GetAsync(id);
            if (vehicle is null)
            {
                return NotFound();
            }
            var pedido = await _pedidosService.GetAsync(pedidoID);
            if (pedido is null)
            {
                return NotFound();
            }
            if (vehicle.pedidos.Count() > 0)
            {
                vehicle.pedidos.Remove(pedidoID);

                await _vehiclesService.UpdateAsync(id, vehicle);
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
