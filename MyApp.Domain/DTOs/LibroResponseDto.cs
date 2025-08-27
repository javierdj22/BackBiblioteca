using System;
using System.Collections.Generic;

namespace MyApp.Domain.DTOs
{
    public class LibroResponseDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editorial { get; set; }
        public int AnioPublicacion { get; set; }
        public string Genero { get; set; }
        public List<CopiaLibroResponseDto> CopiasDisponibles { get; set; }
    }
}
