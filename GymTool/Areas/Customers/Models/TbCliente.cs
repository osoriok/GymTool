using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GymTool.Areas.Customers.Models
{
    public class TbCliente
    {
        [Key]
        public int IdCliente { set; get; }
        public int GimnasioId { set; get; }
        public int EmpleadoId { set; get; }
        public string Cedula { set; get; }
        public string Nombre { set; get; }
        public string Apellidos { set; get; }
        public string Telefono { set; get; }
        public DateTime FechaNacimiento { set; get; }
        public string Direccion { set; get; }
        public string Correo { set; get; }
        public bool Estado { set; get; }
        public byte[] Imagen { get; set; }
        public List<TbExpediente> TbExpediente { get; set; }
    }
}
