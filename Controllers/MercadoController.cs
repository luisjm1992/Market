using System.Security.Claims;
using System.Net;
using AutoMapper;
using Market.Context;
using Market.DTOs;
using Market.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Market.Controllers
{
    
    [Route("api/mercado")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MercadoController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;

        public MercadoController(DataContext dataContext, IMapper mapper, UserManager<IdentityUser> userManager )
        {
            this._dataContext = dataContext;
            this.mapper = mapper;
            this.userManager = userManager;
        }


       [HttpPost("crearMercado")]
        public async Task<ActionResult> LoadMarket(MercadoDTO mercadoDto)
        {
            try
            {
                var mercado = mapper.Map<Mercado>(mercadoDto);
                var usuarioActual = await userManager.GetUserAsync(HttpContext.User);
                mercado.UserId = usuarioActual;
                _dataContext.Mercados.Add(mercado);
                await _dataContext.SaveChangesAsync();
                return Ok("Datos Ingresados");
            }
            catch(Exception ex)
            {
                return BadRequest($"Ha ocurrido un error al procesar la solicitud. {ex.Message}");
            }
        }




        [HttpGet("optenerMercado")]
        public async Task<ActionResult<List<OptenerMercadoDTO>>> GetMarket()
        {
            try
            {
               var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtiene el Id del usuario actual
                var mercados = await _dataContext.Mercados
                    .Where(m => m.UserId.Id == userId) // Filtra los mercados por el Id del usuario actual
                    .ToListAsync();

                return mapper.Map<List<OptenerMercadoDTO>>(mercados);
            }
            catch(Exception ex)
            {
                return BadRequest($"Error en la solicitud {ex.Message}");
            }
        }



         [HttpGet("{id}")]
        public async Task<ActionResult<OptenerMercadoDTO>> UpdateMarket(int id)
        {   
            try
            {
                var existe = await _dataContext.Mercados
                    .FindAsync(id);
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
        public async Task<ActionResult> UpdateMarket(int id,OptenerMercadoDTO optenerMercadoDTO)
        {
            try
            {
                var existingMarket = await _dataContext.Mercados.FindAsync(id);
                if (existingMarket == null)
                {
                     return NotFound();
                }

                var actualizar = mapper.Map<Mercado>(optenerMercadoDTO);

                existingMarket.NameMarket = optenerMercadoDTO.NameMarket;
                existingMarket.PriceMarket = optenerMercadoDTO.PriceMarket;

               _dataContext.Update(existingMarket);
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