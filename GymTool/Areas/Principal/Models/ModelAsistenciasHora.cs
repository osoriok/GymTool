using GymTool.Areas.Customers.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GymTool.Areas.Principal.Models
{
    public class ModelAsistenciasHora
    {

        public string titulo { get; set; }
        public string fechaInicio { get; set; }
        public string descripcion { get; set; }
        public List<TbCliente> listaClientes { get; set; }
    }
}
