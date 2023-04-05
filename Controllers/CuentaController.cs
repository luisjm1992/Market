using System.Text;
using System.Security.Cryptography;
using System.Security.Claims;
using Market.Context;
using Market.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Market.Controllers
{
    [Route("api/cuenta")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;

        public CuentaController(DataContext dataContext, UserManager<IdentityUser> userManager,
            IConfiguration configuration, SignInManager<IdentityUser> signInManager)
        {
            this._dataContext = dataContext;
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }



        [HttpPost("registrar")]
        public async Task<ActionResult<RespuestaAutenticacion>>Registrar(CredencialesUsuarios credencialesUsuarios)
        {
            var usuario = new IdentityUser
            {
                UserName = credencialesUsuarios.Correo,
                Email = credencialesUsuarios.Correo
            
            };

            var resultado = await userManager.CreateAsync(usuario, credencialesUsuarios.Contraseña);

            if(resultado.Succeeded)
            {
                //---Retornar Jwt. 
                return ConstruirToken(credencialesUsuarios);

            }
            else
            {
                return BadRequest(resultado.Errors);
            }
        }


        [HttpPost("login")]
        public async Task<ActionResult<RespuestaAutenticacion>>Login(CredencialesUsuarios credencialesUsuarios)
        {
            var resultado = await signInManager.PasswordSignInAsync(credencialesUsuarios.Correo, 
                credencialesUsuarios.Contraseña, isPersistent:false, lockoutOnFailure: false);


            if(resultado.Succeeded)
            {
                return ConstruirToken(credencialesUsuarios);
            }
            else
            {
                return BadRequest("Login Incorreto");
            }
        }
        private RespuestaAutenticacion ConstruirToken(CredencialesUsuarios credencialesUsuarios)
        {
            //creamos claims
            var claims = new List<Claim>()
            {
                new Claim("email", credencialesUsuarios.Correo)
            };

            // Jwt
            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llaveJwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);
            var expriracion = DateTime.UtcNow.AddHours(48);

            //creamos tokken
            var securitytoken = new JwtSecurityToken(issuer:null, audience:null, claims:claims,
                expires:expriracion, signingCredentials: creds);

            return new RespuestaAutenticacion()
            {
                Token = new JwtSecurityTokenHandler()
                    .WriteToken(securitytoken),
                Expiracion = expriracion
            };
        }




    }
}