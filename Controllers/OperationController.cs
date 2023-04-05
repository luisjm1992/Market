using System.Security.Claims;
using System.Diagnostics;
using System.Net;
using AutoMapper;
using Market.Context;
using Market.DTOs;
using Market.IServices;
using Market.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Market.Controllers
{
    [Route("api/operacion")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

            //codigo para crear objeto
            [HttpPost("crearOperacion")]
            public async Task<ActionResult> PostOperation(OperationDTO operationdto)
            {
                try
                {
                    // optener el identificador

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

            [HttpGet("optenerOperacion")]
            public async Task<ActionResult<List<OptenerOpDTO>>> GetOperaion()
            {
                try
                {
                    var operation = await _dataContext.Operations
                        .ToListAsync();

                    return mapper.Map<List<OptenerOpDTO>>(operation);   
                }
                catch(Exception ex)
                {
                    return BadRequest($"Error en la solicitud {ex.Message}");
                }
            }

            [HttpGet("{id}")]
            public async Task<ActionResult<OptenerOpDTO>> GetOperationById(int id)
            {
                try
                {
                    var existOperation = await _dataContext.Operations
                        .FindAsync(id);

                    if(existOperation == null)
                    {
                        return NotFound();
                    }
                    return Ok(existOperation);
                }
                catch(Exception ex)
                {
                    return BadRequest($"Ha ocurrido un error al procesar la solicitud. {ex.Message}");
                }
            }

            [HttpPut("{id}")]
            public async Task<ActionResult> UpdateOperation(OperationDTO operationDTO, int id)
            {
                try
                {
                    var existsOpeartion = await _dataContext.Operations
                        .FindAsync(id);

                    if(existsOpeartion == null)
                    {
                        return NotFound();
                    }

                    var actualizar = mapper.Map<Operation>(operationDTO);

                    existsOpeartion.Fecha = operationDTO.Fecha;
                    existsOpeartion.TpOperation = operationDTO.TpOperation;
                    existsOpeartion.IdMercado = operationDTO.IdMercado;
                    existsOpeartion.IdSentimiento = operationDTO.IdSentimiento;
                    existsOpeartion.Stop = operationDTO.Stop;
                    existsOpeartion.PrecioEntrada = operationDTO.PrecioEntrada;
                    existsOpeartion.PrecioSalida = operationDTO.PrecioSalida;
                    
                    
                    _dataContext.Update(actualizar);
                    await _dataContext.SaveChangesAsync();
                    return Ok("Elemento Actualizado");

                }
                catch(Exception ex)
                {
                    return BadRequest($"Ha ocurrido un error al procesar la solicitud. {ex.Message}");
                }
            }

            [HttpDelete("{id}")]
            public async Task<ActionResult> DeleteOperation(int id)
            {
                try
                {
                    var existingOperation = await _dataContext.Operations.FindAsync(id);

                    if (existingOperation == null)
                    {
                        return NotFound();
                    }

                    _dataContext.Operations.Remove(existingOperation);
                    await _dataContext.SaveChangesAsync();
                    return NoContent();
                }
                catch(Exception ex)
                {
                    return BadRequest($"Ha ocurrido un error al procesar la solicitud. {ex.Message}");
                }
            }
    }
}