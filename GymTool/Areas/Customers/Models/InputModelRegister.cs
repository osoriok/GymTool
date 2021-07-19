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
        [Required(ErrorMessage = "El campo c&#243;digo de empleado es obligatorio. ")]
        public string EmpleadoCodigo { get; set; }

        //variables cedula
        [Required(ErrorMessage = "El campo c&eacute;dula es obligatorio. ")]
        public string Cedula { set; get; }

        //variables nombre
        [Required(ErrorMessage = "El campo nombre es obligatorio. ")]
        public string Nombre { set; get; }

        //variables apelllidos
        [Required(ErrorMessage = "El campo apellidos es obligatorio. ")]
        public string Apellidos { set; get; }

        //variables Correo
        [EmailAddress(ErrorMessage = "Form&actue;ato de correo electr&oacute;nico no v&acute;lido. ")]
        public string Correo { set; get; }

        //variables Correo
        public string Direccion { set; get; }

        //variables Telefono
        [Required(ErrorMessage = "El campo tel&eacute;fono es obligatorio. ")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{2})[-. ]?([0-9]{4})$", ErrorMessage = "S&oacute;lo se permiten n&uacute;meros. ")]
        public string Telefono { set; get; }

        //variables Fechas
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { set; get; }

        public string Edad { set; get; }
        public DateTime FechaInscripcion { set; get; }
        public string fechaInscirpion { set; get; }
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
        [Required(ErrorMessage = "Selecciona una membres&iacute;a. ")]
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
