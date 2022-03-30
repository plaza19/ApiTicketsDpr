using ApiTicketsDpr.Models;
using ApiTicketsDpr.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ApiTicketsDpr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private MediaTypeWithQualityHeaderValue Header;
        private RepositoryTickets repo;
        

        public TicketController(RepositoryTickets repo)
        {
            this.repo = repo;
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }



        [HttpGet]
        [Authorize]
        [Route("/TicketsUsuario/{idUsuario}")]
        public ActionResult<List<Ticket>> GetTicketsUsuario(int idUsuario)
        {
            return this.repo.GetTicketsFromUser(idUsuario);
        }

        [HttpPost]
        [Authorize]
        [Route("/CreateTicket")]
        public async Task CreateTicket(Ticket t)
        {
            String urlFlow = "https://prod-254.westeurope.logic.azure.com:443/workflows/3e35df18219e4969b40bde2997fa0ca0/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=v1Z1H3HC7l0kSHrJNX7QEcA5mrunSE_cXFjxjViRV8E";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                String json = JsonConvert.SerializeObject(t);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(urlFlow, content);

            }
        }

        [HttpGet]
        [Authorize]
        [Route("/FindTicket/{ticketid}")]
        public ActionResult<Ticket> FindTicket(int ticketid)
        {
            Ticket t = this.repo.FindTicket(ticketid);
            if (t != null)
            {
                return t;
            }else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("/CrearUsuario")]
        public ActionResult CrearUsuario(UsuarioTicket u)
        {
            this.repo.CreateUser(u);
            return Ok();
        }

        [HttpGet]
        [Authorize]
        [Route("/GetUser")]
        public ActionResult<UsuarioTicket> GetUser()
        {
            UsuarioTicket u = JsonConvert.DeserializeObject<UsuarioTicket>(HttpContext.User.FindFirst("Userdata").Value);
            return u;
        }

        [HttpPost]
        [Authorize]
        [Route("/ProcessTicket")]
        public ActionResult ProcessTicket()
        {
            return Ok();
        }

    }
}
