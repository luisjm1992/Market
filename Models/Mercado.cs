using System.ComponentModel.DataAnnotations;

namespace Market.Models
{
    public class Mercado
    {
        [Key]
        public int IdMercado { get; set; }

        [Required(ErrorMessage ="{0} Este parametro es requerido")]
        public string NameMarket { get; set; }

        [Required(ErrorMessage ="{0} Este parametro es requerido")]
        public double PriceMarket { get; set; }

        public List<Operation> OperationList { get; set; }
    }
}