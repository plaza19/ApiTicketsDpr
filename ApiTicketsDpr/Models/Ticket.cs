using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTicketsDpr.Models
{
    [Table("TICKETS")]
    public class Ticket
    {
        [Key]
        [Column("IDTICKET")]
        public int IdTicket {get;set;}
        [Column("IDUSUARIO")]
        public int IdUsuario {get;set;}
        [Column("IMPORTE")]
        public String Importe {get;set;}
        [Column("PRODUCTO")]
        public String Producto {get;set;}
        [Column("FILENAME")]
        public String Filename { get; set; }
        [Column("STORAGEPATH")]
        public String StoragePath { get; set; }
        [Column("FECHA")]
        public DateTime Fecha { get; set; }


}
}
