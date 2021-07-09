using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GymTool.Areas.Memberships.Models
{
    public class TbMembresia
    {
        [Key]
        public int IdMembresia { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }
        public String Monto { get; set; }
        public String Cantidad { get; set; }
        public String Periodo { get; set; }
        public int GimnasioId { get; set; }
        public bool Estado { get; set; }

    }
}
