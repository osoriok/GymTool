using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GymTool.Areas.Customers.Models
{
    public class InputModelRegister
    {
        //variables personal 
        [Required(ErrorMessage = "El campo C&#243;digo de empleado es obligatorio. ")]
        public string EmpleadoCodigo { get; set; }

        //variables cedula
        [Required(ErrorMessage = "El campo Cédula es obligatorio. ")]
        public string Cedula { set; get; }

        //variables nombre
        [Required(ErrorMessage = "El campo Nombre es obligatorio. ")]
        public string Nombre { set; get; }

        //variables apelllidos
        [Required(ErrorMessage = "El campo Apellidos es obligatorio. ")]
        public string Apellidos { set; get; }

        //variables Correo
        [EmailAddress(ErrorMessage = "El Correo no es una dirección de correo electrónico válida. ")]
        public string Correo { set; get; }

        //variables Correo
        public string Direccion { set; get; }

        //variables Telefono
        [Required(ErrorMessage = "El campo Teléfono es obligatorio. ")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{2})[-. ]?([0-9]{4})$", ErrorMessage = "El formato del Tel&eacute;fono ingresado no es v&aacute;lido. ")]
        public string Telefono { set; get; }

        //variables Fechas
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { set; get; }

        public string Edad { set; get; }
        public DateTime FechaInscripcion { set; get; }
        //variables Imagen
        public byte[] Imagen { get; set; }

        //variables id
        public int IdCliente { get; set; }
        public int EmpleadoId { get; set; }
        public String EmpleadoNombre { get; set; }

        public int GimnasioId { get; set; }

        //datos de expediente
        public int IdExpediente { get; set; }

        //variables membres&iacute;a
        [Required(ErrorMessage = "Seleccione una membresía. ")]
        public string Membresia { get; set; }

        //variables booleanas
        public bool Fuma { set; get; }
        public bool Alcohol { set; get; }
        public bool DM { set; get; }
        public bool HTA { set; get; }
        public bool Fx { set; get; }
        public bool Asma { set; get; }
        public bool Obesidad { set; get; }

        [TempData]
        public string ErrorMessage { get; set; }

    }
}
