using System.ComponentModel.DataAnnotations;

namespace Market.Models
{
    public class Sentimiento
    {
        [Key]
        public int IdSentimiento { get; set; }

        [Required(ErrorMessage ="{0} Este parametro es requerido")]
        [MaxLength(10)]
        public string NameSentimiento { get; set; }

        public List<Operation> OperationList { get; set; }
    }
}