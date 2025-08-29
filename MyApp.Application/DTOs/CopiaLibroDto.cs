using System;
using System.Collections.Generic;

namespace MyApp.Application.DTOs
{
    public class CopiaLibroDto
    {
        public int Id { get; set; }
        public string CodigoBarras { get; set; }
        public bool Disponible { get; set; }
        public int LibroId { get; set; }
    }
}
