using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GymTool.Areas.Users.Models
{
    public class TbUsuarioGimnasio
    {
        [Key]
        public int IdUsuarioGimnasio { get; set; }
        public String UsuarioId { get; set; }
        public int GimnasioId { get; set; }
    }
}
