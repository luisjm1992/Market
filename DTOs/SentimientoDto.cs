using System.ComponentModel.DataAnnotations;
using Market.Models;

namespace Market.DTOs
{
    public class SentimientoDto
    {
        [Required(ErrorMessage ="{0} Este parametro es requerido")]
        [MaxLength(10)]
        public string NameSentimiento { get; set; }

    }
}