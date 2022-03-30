using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTicketsDpr.Models
{
    [Table("USUARIOSTICKET")]
    public class UsuarioTicket
    {
        [Key]
        [Column("IDUSUARIO")]
        public int Idusuario { get; set; }
        [Column("NOMBRE")]
        public String Nombre { get; set; }
        [Column("APELLIDOS")]
        public String Apellidos { get; set; }
        [Column("EMAIL")]
        public String Email { get; set; }
        [Column("USERNAME")]
        public String Username { get; set; }
        [Column("PASSWORD")]
        public String Password { get; set; }
    }
}
