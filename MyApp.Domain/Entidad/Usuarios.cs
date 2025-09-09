using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Domain.Entities
{
    [Table("usuarios")]
    public class Usuarios
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password_hash { get; set; }
    }
}
