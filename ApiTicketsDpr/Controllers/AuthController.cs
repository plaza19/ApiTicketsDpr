using ApiEmpleadosOAuth.Helpers;
using ApiTicketsDpr.Models;
using ApiTicketsDpr.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiTicketsDpr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private HelperOAuthToken helper;
        private RepositoryTickets repo;

        public AuthController(RepositoryTickets repo, HelperOAuthToken helper)
        {
            this.repo = repo;
            this.helper = helper;
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult Login(LoginModel model)
        {
            UsuarioTicket usuario = this.repo.ExisteUsuario(model.Username, model.Password);
            if (usuario == null)
            {
                return Unauthorized();
            }
            else
            {
                
                SigningCredentials credentials = new SigningCredentials(this.helper.GetKeyToken(), SecurityAlgorithms.HmacSha256);
                
                String jsonUsuario = JsonConvert.SerializeObject(usuario);

                Claim[] claims = new Claim[]
                {
                    new Claim("Userdata", jsonUsuario)
                };

                JwtSecurityToken token = new JwtSecurityToken(
                    claims: claims,
                    issuer: helper.Issuer,
                    audience: helper.Audience,
                    signingCredentials: credentials,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    notBefore: DateTime.UtcNow
                    );

                return Ok(new
                {
                    response = new JwtSecurityTokenHandler().WriteToken(token)
                });

            }
        }

}
}
