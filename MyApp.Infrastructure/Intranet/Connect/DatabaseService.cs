using Microsoft.EntityFrameworkCore;  // <- Esta línea es clave
using MyApp.Domain.DTOs;
using MyApp.Domain.Entities;
using System.Collections.Generic;

public class LibraryDbContext : DbContext
{
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Prestamo> Prestamos { get; set; }
    public DbSet<DetallePrestamo> DetallePrestamos { get; set; }
    public DbSet<CopiaLibro> CopiaLibro { get; set; }
    public DbSet<Libro> Libro { get; set; }
}
