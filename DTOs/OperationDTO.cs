using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Market.Models;

namespace Market.DTOs
{
    //COLOCAR LO QUE SE NECESITA
    public class OperationDTO
    {
        [Required(ErrorMessage ="{0} Este parametro es requerido")]
        public string Fecha { get; set; }
        
        [Required(ErrorMessage ="{0} Este parametro es requerido")]
        [EnumDataType(typeof(TipoOperacion))]
        public TipoOperacion TpOperation { get; set; }
        //foreingKey
        [ForeignKey("IdMercado")]
        [Required(ErrorMessage ="{0} Este parametro es requerido")]
        public int IdMercado { get; set; }

        [ForeignKey("IdSentimiento")]
        [Required(ErrorMessage ="{0} Este parametro es requerido")]
        public int IdSentimiento { get; set; }
        
        [Required(ErrorMessage ="{0} Este parametro es requerido")]
        public int Stop { get; set; } 

        [Required(ErrorMessage ="{0} Este parametro es requerido")]
        public double PrecioEntrada { get; set; }

        [Required(ErrorMessage ="{0} Este parametro es requerido")]
        public double PrecioSalida { get; set; }

        //public double DineroOperacion { get; private set; }
    
    }
}
