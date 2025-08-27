namespace MyApp.Domain.DTOs
{
    public class ClienteResponseDto
    {
        public int Id { get; set; }
        public string NombreCompleto => $"{Nombres} {Apellidos}";
        public string NumeroDocumento { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        public string Ubigeo { get; set; }
        public bool EnListaNegra { get; set; }

        public string Nombres { get; set; }
        public string Apellidos { get; set; }
    }
}
