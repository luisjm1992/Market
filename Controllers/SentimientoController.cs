using System.Net;
using AutoMapper;
using Market.Context;
using Market.DTOs;
using Market.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Market.Controllers
{
    [Route("api/sentimiento")]
    [ApiController]
    public class SentimientoController : ControllerBase
    {
        public DataContext _dataContext { get; set; }
                private readonly IMapper mapper;
        public SentimientoController(DataContext _dataContext, IMapper mapper )
        {
            this._dataContext = _dataContext;
            this.mapper = mapper;
        }


        [HttpPost("crearSentimiento")]
        public async Task<ActionResult> PostSentimiento(SentimientoDto sentimientoDto)
        {
            try
            {
                var sentimiento = mapper.Map<Sentimiento>(sentimientoDto);
                _dataContext.Sentimientos.Add(sentimiento);
                await _dataContext.SaveChangesAsync();
                return Ok("Datos ingresados");
            }
            catch(Exception ex)
            {
                 return BadRequest($"Ha ocurrido un error al procesar la solicitud. {ex.Message}");
            }
        }


        [HttpGet("optenerSentimiento")]
        public async Task<ActionResult<List<OptenerSentimientoDTO>>> GetSentimiento()
        {
            try
            {
                var existeSentimiento = await _dataContext.Sentimientos.ToListAsync();
                return mapper.Map<List<OptenerSentimientoDTO>>(existeSentimiento);
            }
            catch(Exception ex)
            {
                 return BadRequest($"Ha ocurrido un error al procesar la solicitud. {ex.Message}");
            }

        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<OptenerSentimientoDTO>> GetSentimiento(int id)
        {
            try
            {
                var existeSentimiento = await _dataContext.Sentimientos.FindAsync(id);
                if(existeSentimiento == null)
                {
                    return NotFound();
                }
                return Ok(existeSentimiento);
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
        public async Task<ActionResult> UpdatSentimiento(OptenerSentimientoDTO optenerSentimientoDTO, int id)
        {
            try
            {
                var existingSentimiento = await _dataContext.Sentimientos.FindAsync(id);
                if (existingSentimiento == null)
                {
                    return NotFound();
                }

                var sentimiento = mapper.Map<Sentimiento>(optenerSentimientoDTO);
                existingSentimiento.NameSentimiento = optenerSentimientoDTO.NameSentimiento;
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
