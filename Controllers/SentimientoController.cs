using System.Net;
using Market.Context;
using Market.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Market.Controllers
{
    [Route("sentimiento")]
    [ApiController]
    public class SentimientoController : ControllerBase
    {
        public DataContext _dataContext { get; set; }
        public SentimientoController(DataContext _dataContext)
        {
            this._dataContext = _dataContext;
        }


        [HttpPost("api/postfeeling")]
        public async Task<ActionResult> PostSentimiento(Sentimiento sentimiento)
        {
            try
            {
                _dataContext.Sentimientos.Add(sentimiento);
                await _dataContext.SaveChangesAsync();
                return Ok("Datos ingresados");
            }
            catch(Exception ex)
            {
                 return BadRequest($"Ha ocurrido un error al procesar la solicitud. {ex.Message}");
            }
        }


        [HttpGet("api/getfeeling")]
        public async Task<ActionResult<List<Sentimiento>>> GetSentimiento()
        {
            try
            {
                var sentimiento = await _dataContext.Sentimientos.ToListAsync();
                return Ok(sentimiento);
            }
            catch(Exception ex)
            {
                 return BadRequest($"Ha ocurrido un error al procesar la solicitud. {ex.Message}");
            }

        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Sentimiento>> GetSentimiento(int id)
        {
            try
            {
                var existe = await _dataContext.Sentimientos.FindAsync(id);
                if(existe == null)
                {
                    return NotFound();
                }
                return Ok(existe);
            }
            catch(Exception ex)
            {
                return BadRequest($"Ha ocurrido un error al procesar la solicitud. {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Sentimiento>> DeleteSentimiento(int id)
        {
            try
            {
                var existeSentimiento = await _dataContext.Sentimientos.FindAsync(id);

                if(existeSentimiento == null)
                {
                    return NotFound("El elemento no existe");
                }
                _dataContext.Sentimientos.Remove(existeSentimiento);
                await _dataContext.SaveChangesAsync();
                return Ok("Elemento eliminado");
            }
            catch(Exception ex)
            {
                return BadRequest($"Ha ocurrido un error al procesar la solicitud. {ex.Message}");
            }
           
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatSentimiento(int id,Sentimiento sentimiento)
        {
            try
            {
                var existingSentimiento = await _dataContext.Sentimientos.FindAsync(id);
                if (existingSentimiento == null)
                {
                    return NotFound();
                }

                existingSentimiento.NameSentimiento = sentimiento.NameSentimiento;
                await _dataContext.SaveChangesAsync();

                return Ok(existingSentimiento);
            }
            catch(Exception ex)
            {
                return BadRequest($"Ha ocurrido un error al procesar la solicitud. {ex.Message}");
            }
        }
    
    }
}
