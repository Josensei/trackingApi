﻿using trackingApi.Services;
using trackingApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace trackingApi.Controllers
{
    [ApiController]
    [Route("api/Pedidos")]
    public class PedidosController : ControllerBase
    {
        private readonly PedidosService _pedidosService;
        public PedidosController(PedidosService pedidosService) =>
            _pedidosService = pedidosService;

        [HttpGet]
        public async Task<List<Pedido>> Get()=>
            await _pedidosService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Pedido>> Get(string id)
        {
            var pedido = await _pedidosService.GetAsync(id);
            if (pedido is null)
            {
                return NotFound();
            }
            return pedido;
        }
        [HttpPost]
        public async Task<IActionResult> Post( Pedido newPedido)
        {
            await _pedidosService.CreateAsync(newPedido);

            return CreatedAtAction(nameof(Get), new { id = newPedido.Id }, newPedido);
        }
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Pedido updatedPedido)
        {
            var pedido= await _pedidosService.GetAsync(id);
            if(pedido is null)
            {
                return NotFound();
            }
            updatedPedido.Id=pedido.Id;
               
            await _pedidosService.UpdateAsync(id, updatedPedido);
            return NoContent();
        }
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var pedido = await _pedidosService.GetAsync(id);
            if(pedido is null)
            {
                return NotFound();
            }
            await _pedidosService.RemoveAsync(id);
            return NoContent();
        }
    }
}
