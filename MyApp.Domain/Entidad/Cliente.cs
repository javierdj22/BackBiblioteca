using System.Collections.Generic;

namespace MyApp.Domain.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string NumeroDocumento { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Ubigeo { get; set; }
        public bool EnListaNegra { get; set; } = false;
        public ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
    }

}
