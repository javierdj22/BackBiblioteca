using MyApp.Application.DTOs;

namespace MyApp.Application.Interfaces
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDto>> GetAllAsync();
        Task<ClienteDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(ClienteDto dto);
        Task<bool> UpdateAsync(int id, ClienteDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
