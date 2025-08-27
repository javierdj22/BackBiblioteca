using MyApp.Domain.DTOs;
using MyApp.Domain.Entities;
using MyApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Application.Services
{
    public class PrestamoService : IPrestamoService
    {
        private readonly LibraryDbContext _context;

        public PrestamoService(LibraryDbContext context)
        {
            _context = context;
        }
        public async Task<List<LibroResponseDto>> GetAllLibrosAsync()
        {
            return await _context.Libro
                .Include(l => l.CopiaLibro) // Incluir las copias asociadas al libro
                .Select(l => new LibroResponseDto
                {
                    Id = l.Id,
                    Titulo = l.Titulo,
                    Autor = l.Autor,
                    Editorial = l.Editorial,
                    AnioPublicacion = l.AnioPublicacion,
                    Genero = l.Genero,
                    CopiasDisponibles = l.CopiaLibro.Where(c => c.Disponible == true) // Filtrar las copias disponibles
                                                 .Select(c => new CopiaLibroResponseDto
                                                 {
                                                     Id = c.Id,
                                                     CodigoBarras = c.CodigoBarras
                                                 }).ToList()
                })
                .ToListAsync();
        }

        public async Task<bool> RegistrarPrestamoAsync(PrestamoRequestDto request)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Prestamos)
                .FirstOrDefaultAsync(c => c.Id == request.ClienteId);

            if (cliente == null || cliente.EnListaNegra)
                throw new Exception("Cliente inválido o en lista negra.");

            var copias = await _context.CopiaLibro
                .Where(c => request.CopiaLibroIds.Contains(c.Id) && c.Disponible)
                .ToListAsync();

            if (copias.Count != request.CopiaLibroIds.Count)
                throw new Exception("Una o más copias no están disponibles.");

            var prestamo = new Prestamo
            {
                ClienteId = cliente.Id,
                FechaPrestamo = DateTime.Now,
                FechaDevolucion = DateTime.Now.AddDays(7),
                Detalles = copias.Select(c => new DetallePrestamo
                {
                    CopiaLibroId = c.Id
                }).ToList()
            };

            copias.ForEach(c => c.Disponible = false);

            _context.Prestamos.Add(prestamo);
            await _context.SaveChangesAsync();

            return true;
        }
    }

}
