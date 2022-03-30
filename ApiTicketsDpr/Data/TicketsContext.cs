using ApiTicketsDpr.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTicketsDpr.Data
{
    public class TicketsContext : DbContext
    {

        public TicketsContext(DbContextOptions<TicketsContext> options) : base(options)
        {

        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<UsuarioTicket> Usuarios { get; set; }

    }
}
