using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Market.Models;

namespace Market.DTOs
{
    public class OptenerOpDTO
    {
        public int IdOperation { get; set; }
        public String Fecha { get; set; }
        public TipoOperacion TpOperation { get; set; }
        public int IdMercado { get; set; }
        public int IdSentimiento { get; set; }
        public int Stop { get; set; } 
        public double PrecioEntrada { get; set; }
        public double PrecioSalida { get; set; }
        public int Puntos { get; set; }
        public double DineroOperacion { get; set; }
        public string Resultado { get; set; }
    }
}