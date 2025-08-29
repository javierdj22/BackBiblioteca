using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;

namespace MyApp.Infrastructure.Repositories
{
    public class PrestamoRepository : IPrestamoRepository
    {
        private readonly LibraryDbContext _context;

        public PrestamoRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Libro>> GetAllLibrosAsync()
        {
            return await _context.Libro
                .Include(l => l.CopiaLibro)
                .Select(l => new Libro
                {
                    Id = l.Id,
                    Titulo = l.Titulo,
                    Autor = l.Autor,
                    Editorial = l.Editorial,
                    AnioPublicacion = l.AnioPublicacion,
                    Genero = l.Genero,
                    CopiaLibro = l.CopiaLibro
                        .Where(c => c.Disponible)
                        .Select(c => new CopiaLibro
                        {
                            Id = c.Id,
                            CodigoBarras = c.CodigoBarras
                        }).ToList()
                })
                .ToListAsync();
        }

        public async Task<bool> RegistrarPrestamoAsync(Libro request)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Prestamos)
                .FirstOrDefaultAsync(c => c.Id == request.Id);

            if (cliente == null || cliente.EnListaNegra)
                throw new Exception("Cliente inválido o en lista negra.");

            var copias = await _context.CopiaLibro
                .Where(c => request.CopiaLibro.Contains(c) && c.Disponible)
                .ToListAsync();

            if (copias.Count != request.CopiaLibro.Count)
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
