namespace Market.Models
{
    public class Cuenta
    {
        public int IdCuenta { get; set; }
        public int IdOperacion { get; set; }
        public int IdPersona { get; set; }
        public int DineroIngresado { get; set; }
        public int DineroGanado { get; set; }
        public int DineroPerdido { get; set; }
        public int TotalMensual { get; set; }
        public int OperacionesHechas { get; set; }
        public int OperacionesGanadas { get; set; }
        public int OperacionesPerdidas { get; set; }
        public int OperacionesBe { get; set; }
        public int TasaAciertos { get; set; }
        public int TasaPerdidas { get; set; }
        public int TasaBe { get; set; }
        public int RatioGanador { get; set; }
        public int RatioPerdedor { get; set; }
    }
}