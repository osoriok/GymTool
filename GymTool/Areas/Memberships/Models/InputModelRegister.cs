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
        [Required(ErrorMessage = "El campo Nombre es obligatorio. ")]
        public string Nombre { set; get; }

        //variables Descripcion
        [Required(ErrorMessage = "El campo Descripcion es obligatorio. ")]
        public string Descripcion { set; get; }

        //variables Monto
        [Required(ErrorMessage = "El campo Monto es obligatorio. ")]
        [RegularExpression(@"^(0|[1-9]\d*)$", ErrorMessage = "El formato del Monto ingresado no es válido. ")]
        public string Monto { set; get; }

        //variables Monto
        [Required(ErrorMessage = "El campo Cantidad es obligatorio. ")]
        public String Cantidad { set; get; }

        //variables Monto
        [Required(ErrorMessage = "Seleccione un periodo. ")]
        public string Periodo { set; get; }
        public int GimnasioId { get; set; }

        public int IdMembresia { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
    }
}
