using System.ComponentModel.DataAnnotations;

namespace Market.DTOs
{
    public class CredencialesUsuarios
    {
        [Required]
        [EmailAddress]
        public string Correo { get; set; }
        [Required]
        public string Contraseña { get; set; }
    }
}