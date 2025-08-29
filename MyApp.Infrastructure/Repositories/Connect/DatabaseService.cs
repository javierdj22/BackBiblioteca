using Microsoft.EntityFrameworkCore; 
using MyApp.Domain.Entities;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Prestamo> Prestamos { get; set; }
    public DbSet<DetallePrestamo> DetallePrestamos { get; set; }
    public DbSet<CopiaLibro> CopiaLibro { get; set; }
    public DbSet<Libro> Libro { get; set; }
}
