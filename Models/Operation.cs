using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Market.Models
{
    public class Operation
    {
        [Key]
        public int IdOperation { get; set; }

        [Required(ErrorMessage ="{0} Este parametro es requerido")]
        public string Fecha { get; set; }

        [Required(ErrorMessage ="{0} Este parametro es requerido")]
        [EnumDataType(typeof(TipoOperacion))]
        public TipoOperacion TpOperation { get; set; }

        [Required(ErrorMessage ="{0} Este parametro es requerido")]
        public int IdMercado { get; set; }

        [Required(ErrorMessage ="{0} Este parametro es requerido")]
        public int IdSentimiento { get; set; }
        
        [Required(ErrorMessage ="{0} Este parametro es requerido")]
        public int Stop { get; set; } 

        [Required(ErrorMessage ="{0} Este parametro es requerido")]
        public double PrecioEntrada { get; set; }

        [Required(ErrorMessage ="{0} Este parametro es requerido")]
        public double PrecioSalida { get; set; }

        public int Puntos { get; set; }
        public double DineroOperacion { get; set; }
        public string Resultado { get; set; }
        
        // Propiedades de navegación
        public int IdCuenta { get; set; }
        public Cuenta Cuenta { get; set; }
        
    }
}