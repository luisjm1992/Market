using System.Diagnostics;
using System;
using Market.Models;

namespace Market.IServices
{
    public class OperationCl : IOperation
    {
        Operation objOp = new Operation();

        public double calcularOperacion(double precioEntrada, double precioSalida, int tpOperation, double precioMercado)
        {
               double resultado = 0;
               TipoOperacion tipoOperacion = (TipoOperacion)tpOperation;

                if(tipoOperacion == TipoOperacion.compra)
                {
                    resultado = ((precioSalida - precioEntrada) * precioMercado);
                }
                else if(tipoOperacion == TipoOperacion.venta)
                {
                    resultado = ((precioEntrada - precioSalida) * precioMercado);
                }
                
                return resultado;
        }

        public string calificar(double resultado)
        {
            string calific = "";

            if(resultado < 0)
            {
                calific = "OP_PERDIDA";
            }
            else if(resultado > 0)
            {
                calific = "OP_GANADA";
            }
            else
            {
                calific = "BREAK_EVEN";
            }

            return calific;
        }

        public int puntosMercado(double dineroGanado, double precioMercado)
        {
            int puntos = 0;
            return puntos =(int)(dineroGanado/precioMercado);
        }
    }
}