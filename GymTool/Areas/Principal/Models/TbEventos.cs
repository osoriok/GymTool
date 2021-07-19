using GymTool.Areas.Customers.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GymTool.Areas.Principal.Models
{
    public class TbEventos
    {
        public int cantidadAsistencias { get; set; }
        public string horaAsistencia { get; set; }
        public string fechaInicio { get; set; }
        public string fechaFinal { get; set; }
        public List<TbCliente> listaCliente { get; set; }

    }
}
