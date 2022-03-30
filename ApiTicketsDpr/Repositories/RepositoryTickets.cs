using ApiTicketsDpr.Data;
using ApiTicketsDpr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTicketsDpr.Repositories
{
    public class RepositoryTickets
    {

        private TicketsContext context;

        public RepositoryTickets(TicketsContext context)
        {
            this.context = context;
        }

        public List<Ticket> GetTicketsFromUser(int IdUsuario)
        {
            var consulta = from data in this.context.Tickets where data.IdUsuario == IdUsuario select data;

            if (consulta.Count() == 0)
            {
                return null;
            }else
            {
                return consulta.ToList();
            }

           
        }

        public Ticket FindTicket(int idTicket)
        {
            var consulta = from data in this.context.Tickets where data.IdTicket == idTicket select data;

            return consulta.FirstOrDefault();
        
        }

        public void CreateUser(UsuarioTicket u)
        {
            u.Idusuario = this.GetNewId();
            this.context.Usuarios.Add(u);
            this.context.SaveChanges();
        }

        public int GetNewId()
        {
            int max = this.context.Usuarios.Max(x => x.Idusuario);
            return max + 1;
        }

       public UsuarioTicket ExisteUsuario(String Username, String Password)
        {
            var consulta = from data in this.context.Usuarios where data.Email == Username && data.Password == Password select data;

            return consulta.FirstOrDefault();
        }

    }
}
