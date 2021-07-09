using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GymTool.Areas.Users.Models
{
    public class TbGimnasio
    {
        [Key]
        public int IdGimnasio { get; set; }
        public String Nombre { get; set; }
        public String Direccion { get; set; }
        public String Estado { get; set; }
    }
}
