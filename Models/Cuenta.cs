using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Market.Models
{
    public class Cuenta
    {
        [Key]
        public int IdCuenta { get; set; }
        public string Fecha { get; set; }
        public float DineroIngresado { get; set; }
        public float DineroRetirado { get; set; }
        public float DineroGanado { get; set; }
        public float DineroPerdido { get; set; }
        public float TotalMensual { get; set; }
        public int OperacionesHechas { get; set; }
        public int OperacionesGanadas { get; set; }
        public int OperacionesPerdidas { get; set; }
        public int OperacionesBe { get; set; }
        public float TasaAciertos { get; set; }
        public float TasaPerdidas { get; set; }
        public float TasaBe { get; set; }
        public int RatioGanador { get; set; }
        public int RatioPerdedor { get; set; }

        // Propiedades de navegación
        public ICollection<Operation> Operaciones { get; set; }
        public string UsuarioId { get; set; }
        public IdentityUser Usuario { get; set; }

    }
}