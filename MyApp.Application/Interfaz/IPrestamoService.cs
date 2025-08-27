
using MyApp.Domain.DTOs;
using MyApp.Domain.Entities;

public interface IPrestamoService
{
    Task<List<LibroResponseDto>> GetAllLibrosAsync();
    Task<bool> RegistrarPrestamoAsync(PrestamoRequestDto request);
}
