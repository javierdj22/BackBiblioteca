using System.Collections.Generic;

namespace MyApp.Domain.DTOs
{
    public class PrestamoRequestDto
    {
        public int ClienteId { get; set; }
        public List<int> CopiaLibroIds { get; set; } // Máximo 3
    }

}
