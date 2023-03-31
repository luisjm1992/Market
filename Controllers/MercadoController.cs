using System.Net;
using Market.Context;
using Market.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Market.Controllers
{
    
    [Route("controller/market")]
    [ApiController]
    public class MercadoController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public MercadoController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }


        [HttpPost]
        public async Task<ActionResult> LoadMarket(Mercado mercado)
        {
            try
            {
                _dataContext.Mercados.Add(mercado);
                await _dataContext.SaveChangesAsync();
                return Ok("Datos Ingresados");
            }
            catch(Exception ex)
            {
                return BadRequest($"Ha ocurrido un error al procesar la solicitud. {ex.Message}");
            }
            
        }


        [HttpGet]
        public async Task<ActionResult<List<Mercado>>> GetMarket()
        {
            try
            {
                 var mercados = await _dataContext.Mercados
                .ToListAsync();
            return Ok(mercados);
            }
            catch(Exception ex)
            {
                return BadRequest($"Error en la solicitud {ex.Message}");
            }
           
        }


         [HttpGet("{id}")]
        public async Task<ActionResult<Mercado>> UpdateMarket(int id)
        {   
            try
            {
                var existe = await _dataContext.Mercados.FindAsync(id);
                if(existe == null)
                {
                    return NotFound();
                }
                return Ok(existe);
            }
            catch(Exception ex)
            {
                return BadRequest($"Error en la solicitud {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Mercado>> GetMarketByID(int id)
        {
            try
            { 
                var existe = await _dataContext.Mercados.FindAsync(id);

                if(existe == null)
                {
                    return NotFound();
                }

                _dataContext.Mercados.Remove(existe);
                await _dataContext.SaveChangesAsync();
                return Ok("El elemento ha sido eliminado ");
            }
            catch(Exception ex)
            {
                 return BadRequest($"Error en la solicitud {ex.Message}");
            }
            
        }

    
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateMarket(int id,Mercado mercado)
        {
            try
            {
                var existingMarket = await _dataContext.Mercados.FindAsync(id);
                if (existingMarket == null)
                {
                     return NotFound();
                }
                existingMarket.NameMarket = mercado.NameMarket;
                existingMarket.PriceMarket = mercado.PriceMarket;
                _dataContext.Mercados.Update(existingMarket);
                await _dataContext.SaveChangesAsync();
                return Ok(existingMarket);

            }
            catch(Exception ex)
            {
                return BadRequest($"Error en la solicitud {ex.Message}");
            }

        }

    }
}