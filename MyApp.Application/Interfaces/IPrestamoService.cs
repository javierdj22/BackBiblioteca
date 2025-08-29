using MyApp.Application.DTOs;

namespace MyApp.Application.Interfaces
{
    public interface IPrestamoService
    {
        Task<List<LibroDto>> GetAllLibrosAsync();
        Task<bool> RegistrarPrestamoAsync(LibroDto request);
    }
}