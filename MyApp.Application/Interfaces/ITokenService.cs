using MyApp.Application.DTOs;

namespace MyApp.Application.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateJwtTokenAsync(LoginDto loginDto);
    }
}