using System.ComponentModel.DataAnnotations;
using Market.Models;

namespace Market.DTOs
{
    public class OptenerSentimientoDTO
    {
        [Key]
        public int IdSentimiento { get; set; }
        public string NameSentimiento { get; set; }

    }
}