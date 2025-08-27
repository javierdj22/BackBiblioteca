using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Domain.DTOs;
using MyApp.Domain.Entities;

namespace MyApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrestamosController : ControllerBase
    {
        private readonly IPrestamoService _prestamoService;

        public PrestamosController(IPrestamoService prestamoService)
        {
            _prestamoService = prestamoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _prestamoService.GetAllLibrosAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarPrestamo([FromBody] PrestamoRequestDto request)
        {
            try
            {
                var result = await _prestamoService.RegistrarPrestamoAsync(request);
                return Ok(new { success = result, message = "Préstamo registrado con éxito." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
