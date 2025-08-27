using MyApp.Domain.DTOs;
using MyApp.Domain.Entities;

namespace MyApp.Application.Services
{
    public interface ITokenService
    {
        string GenerateToken(Cliente usuario);
    }
}
