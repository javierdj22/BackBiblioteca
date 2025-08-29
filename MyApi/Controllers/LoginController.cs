using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Interfaces;
using MyApp.Application.DTOs;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace MyApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public LoginController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }


        // POST: api/clientes/token
        [HttpPost("token")]
        public async Task<IActionResult> GenerateToken([FromBody] LoginDto loginDto)
        {
            if (loginDto == null)
                throw new ArgumentNullException(nameof(loginDto));

            var token = await _tokenService.GenerateJwtTokenAsync(loginDto);
            if (string.IsNullOrEmpty(token))
                return Unauthorized(new { message = "Credenciales inválidas." });

            return Ok(new { token });
        }
    }
}
