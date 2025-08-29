using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Domain.Entities;
using MyApp.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Application.Services
{
    public class PrestamoService : IPrestamoService
    {
        private readonly IPrestamoRepository _prestamoRepository;

        public PrestamoService(IPrestamoRepository prestamoRepository)
        {
            _prestamoRepository = prestamoRepository;
        }

        public async Task<List<LibroDto>> GetAllLibrosAsync()
        {
            var libros = await _prestamoRepository.GetAllLibrosAsync();
            return libros.Select(MapToDto).ToList();
        }

        public async Task<bool> RegistrarPrestamoAsync(LibroDto request)
        {
            var libroEntity = MapToEntity(request);
            return await _prestamoRepository.RegistrarPrestamoAsync(libroEntity);
        }

        private LibroDto MapToDto(Libro libro)
        {
            return new LibroDto
            {
                Id = libro.Id,
                Titulo = libro.Titulo,
                Autor = libro.Autor,
                Editorial = libro.Editorial,
                AnioPublicacion = libro.AnioPublicacion,
                Genero = libro.Genero,
                CopiasDisponibles = libro.CopiaLibro?.Select(copia => new CopiaLibroDto
                {
                    Id = copia.Id,
                    CodigoBarras = copia.CodigoBarras,
                    Disponible = copia.Disponible,
                    LibroId = copia.LibroId,
                }).ToList() ?? new List<CopiaLibroDto>()
            };
        }

        private Libro MapToEntity(LibroDto dto)
        {
            return new Libro
            {
                Id = dto.Id,
                Titulo = dto.Titulo,
                Autor = dto.Autor,
                Editorial = dto.Editorial,
                AnioPublicacion = dto.AnioPublicacion,
                Genero = dto.Genero,
                CopiaLibro = dto.CopiasDisponibles?.Select(copiaDto => new CopiaLibro
                {
                    Id = copiaDto.Id,
                    CodigoBarras = copiaDto.CodigoBarras,
                    Disponible = copiaDto.Disponible,
                    LibroId = copiaDto.LibroId
                }).ToList() ?? new List<CopiaLibro>()
            };
        }
    }
}
