using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GymTool.Areas.Memberships.Models
{
    public class InputModelRegister
    {

        //variables Nombre
        [Required(ErrorMessage = "El campo nombre es obligatorio. ")]
        public string Nombre { set; get; }

        //variables Descripcion
        [Required(ErrorMessage = "El campo descripci&oacute;n es obligatorio. ")]
        public string Descripcion { set; get; }

        //variables Monto
        [Required(ErrorMessage = "El campo monto es obligatorio. ")]
        [RegularExpression(@"^(0|[1-9]\d*)$", ErrorMessage = "S&oacute;lo se permiten n&uacute;meros. ")]
        public string Monto { set; get; }

        //variables Monto
        [Required(ErrorMessage = "El campo cantidad es obligatorio. ")]
        [RegularExpression(@"^(0|[1-9]\d*)$", ErrorMessage = "S&oacute;lo se permiten n&uacute;meros. ")]
        public String Cantidad { set; get; }

        //variables Monto
        [Required(ErrorMessage = "Selecciona un periodo. ")]
        public string Periodo { set; get; }
        public int GimnasioId { get; set; }
        public int CantidadClientes { get; set; }
        public int IdMembresia { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
    }
}
