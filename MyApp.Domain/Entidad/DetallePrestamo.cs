namespace MyApp.Domain.Entities
{
    public class DetallePrestamo
    {
        public int Id { get; set; }
        public int PrestamoId { get; set; }
        public int CopiaLibroId { get; set; }
        public Prestamo Prestamo { get; set; }
        public CopiaLibro CopiaLibro { get; set; }
    }

}
