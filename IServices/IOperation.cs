namespace Market.IServices
{
    public interface IOperation
    {
        double calcularOperacion(double precioEntrada, double precioSalida, int tpOperation, double precioMercado);
        string calificar(double resultado);
        int puntosMercado(double dineroGanado, double precioMercado);
        
    }
}