using System.Collections.Generic;
using System.Threading.Tasks;
using MyApp.Domain.Entities;

namespace MyApp.Domain.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> GetAllAsync();
        Task<Cliente?> GetByIdAsync(int id);
        Task<int> CreateAsync(Cliente cliente);
        Task<bool> UpdateAsync(int id, Cliente cliente);
        Task<bool> DeleteAsync(int id);
    }
}
