
using MyApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyApp.Domain.Interfaces
{
    public interface IPrestamoRepository
    {
        Task<List<Libro>> GetAllLibrosAsync();
        Task<bool> RegistrarPrestamoAsync(Libro request);
    }
}