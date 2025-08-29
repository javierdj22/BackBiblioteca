using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace MyApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _service;

        public ClientesController(IClienteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clientes = await _service.GetAllAsync();
            if (clientes == null || clientes.Count() == 0)
                throw new KeyNotFoundException();  // Lanza una KeyNotFoundException sin mensaje

            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cliente = await _service.GetByIdAsync(id);
            if (cliente == null)
                throw new KeyNotFoundException();  // Lanza una KeyNotFoundException sin mensaje

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClienteDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));  // Lanza una ArgumentNullException sin mensaje

            var id = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, new { statusCode = 200 });  // Solo el estado, sin mensaje
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClienteDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));  // Lanza una ArgumentNullException sin mensaje

            if (dto.Id != id)
                throw new ArgumentException();  // Lanza una ArgumentException sin mensaje

            var updated = await _service.UpdateAsync(id, dto);
            if (!updated)
                throw new KeyNotFoundException();  // Lanza una KeyNotFoundException sin mensaje

            return Ok();  // Solo el estado de la operación
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                throw new KeyNotFoundException();  // Lanza una KeyNotFoundException sin mensaje

            return Ok();  // Solo el estado de la operación
        }
    }
}
