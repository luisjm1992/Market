using System.Diagnostics;
using System.Net;
using AutoMapper;
using Market.Context;
using Market.DTOs;
using Market.IServices;
using Market.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Market.Controllers
{
    [Route("controller/operacion")]
    [ApiController]
    public class OperationController : ControllerBase
    {
        public DataContext _dataContext { get; set; }
        private readonly IOperation _operationService;
        private readonly IMapper mapper;

        public OperationController(DataContext _dataContext, IOperation operationService, IMapper mapper )
        {
            _operationService = operationService;
            this.mapper = mapper;
            this._dataContext = _dataContext;
        }


            [HttpPost]
            public async Task<ActionResult> PostOperation(OperationDTO operationdto)
            {
                try
                {
                    var mercado = await _dataContext.Mercados.FindAsync(operationdto.IdMercado);
                    double precioMercado = mercado.PriceMarket;
                    var datoEntrada = operationdto.PrecioEntrada;
                    var datoSalida = operationdto.PrecioSalida;
                    var idOperacion = (int)operationdto.TpOperation;
                    var sentimiento = await _dataContext.Sentimientos.FindAsync(operationdto.IdSentimiento);
                    var resultado = _operationService.calcularOperacion(datoEntrada, datoSalida, idOperacion, precioMercado);
                    var operation = mapper.Map<Operation>(operationdto);
                    operation.DineroOperacion = resultado;
                    operation.Puntos = _operationService.puntosMercado(resultado, precioMercado);
                    operation.Resultado = _operationService.calificar(resultado);
                    
                    _dataContext.Add(operation);
                    await _dataContext.SaveChangesAsync();
                    return Ok();
                }
                catch(Exception ex)
                {
                     return BadRequest($"Error : {ex.Message}");
                }
            }

            [HttpGet]
            public async Task<List<OptenerOpDTO>> GetOperaion()
            {
                var operation = await _dataContext.Operations.ToListAsync();
                return mapper.Map<List<OptenerOpDTO>>(operation);
            }
            

            //crear metodo para hacer el resumen de las operaciones. 
            //https://www.youtube.com/watch?v=uy_xd3Xwt5M&ab_channel=JuanGCarmona
    }
}