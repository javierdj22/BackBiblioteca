namespace MyApp.Application.DTOs
{
    public class LibroDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editorial { get; set; }
        public int AnioPublicacion { get; set; }
        public string Genero { get; set; }
        public List<CopiaLibroDto> CopiasDisponibles { get; set; }
    }
}
