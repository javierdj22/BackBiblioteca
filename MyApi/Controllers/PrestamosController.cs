using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Interfaces;
using MyApp.Application.DTOs;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace MyApp.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PrestamosController : ControllerBase
    {
        private readonly IPrestamoService _prestamoService;

        public PrestamosController(IPrestamoService prestamoService)
        {
            _prestamoService = prestamoService;
        }

        // GET: api/prestamos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _prestamoService.GetAllLibrosAsync();
            if (result == null || result.Count == 0)
                throw new KeyNotFoundException();  // Lanza la excepción sin mensaje

            return Ok(result);
        }

        // POST: api/prestamos
        [HttpPost]
        public async Task<IActionResult> RegistrarPrestamo([FromBody] LibroDto request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));  // Lanza una excepción sin mensaje

            var result = await _prestamoService.RegistrarPrestamoAsync(request);
            if (!result)
                throw new InvalidOperationException();  // Lanza una excepción sin mensaje

            return Ok();  // Solo el estado de la operación
        }
    }
}
