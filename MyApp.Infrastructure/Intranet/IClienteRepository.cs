using MyApp.Domain.DTOs;
using MyApp.Domain.Entities;
using System.Threading.Tasks;

namespace MyApp.Domain.Repositories
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> GetAllAsync();
        Task<Cliente?> GetByIdAsync(int id);
        Task AddAsync(Cliente cliente);
        Task UpdateAsync(Cliente cliente);
        Task DeleteAsync(Cliente cliente);
        Task<bool> ExistsAsync(int id);
    }
}
