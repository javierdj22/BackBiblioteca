using MyApp.Domain.DTOs;
using MyApp.Domain.Entities;
using System.Threading.Tasks;

namespace MyApp.Application.Services
{
    public interface IClienteService
    {
        Task<List<ClienteResponseDto>> GetAllAsync();
        Task<ClienteResponseDto> GetByIdAsync(int id);
        Task<int> CreateAsync(ClienteRequestDto dto);
        Task<bool> UpdateAsync(int id, ClienteRequestDto dto);
        Task<bool> DeleteAsync(int id);
    }

}
