namespace MyApp.Application.DTOs
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string NombreCompleto => $"{Nombres} {Apellidos}";

        public string NumeroDocumento { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Ubigeo { get; set; } = string.Empty;
        public bool EnListaNegra { get; set; }
    }
}
