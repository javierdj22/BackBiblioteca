using System;
using System.Collections.Generic;

namespace MyApp.Domain.Entities
{
    public class Prestamo
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaDevolucion { get; set; }

        public Cliente Cliente { get; set; }
        public ICollection<DetallePrestamo> Detalles { get; set; }
    }
}
