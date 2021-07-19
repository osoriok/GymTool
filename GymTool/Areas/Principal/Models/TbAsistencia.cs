using GymTool.Areas.Customers.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GymTool.Areas.Principal.Models
{
    public class TbAsistencia
    {
        [Key]
        public int IdAsistencia { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinal { get; set; }
        public int ClienteId { get; set; }
        public int TbClienteIdCliente { set; get; }
        public TbCliente TbCliente { get; set; }
    }
}
