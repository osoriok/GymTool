using System;
using System.ComponentModel.DataAnnotations;

namespace GymTool.Areas.Users.Models
{
    public class TUsers
    {
        [Key]
        public int IdUsers { get; set; }
        public String Nombre { get; set; }
        public String Apellidos { get; set; }
        public String Codigo { get; set; }
        public String Cedula { get; set; }
        public String Correo { get; set; }
        public String Telefono { get; set; }
        public String TelefonoEmergencia { get; set; }
        public String UsuarioId { get; set; }
        public int GimnasioId { get; set; }
        public bool Estado { get; set; }

    }
}
