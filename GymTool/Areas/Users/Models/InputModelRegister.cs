using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GymTool.Areas.Users.Models
{
    public class InputModelRegister
    {
        //variables nombre
        [Required(ErrorMessage = "El campo nombre es obligatorio.")]
        public string Nombre { get; set; }

        //variables apellido
        [Required(ErrorMessage = "El campo apellidos es obligatorio.")]
        public string Apellidos { get; set; }

        //variables codigo
        public string Codigo { get; set; }

        //variables identificaci&oacute;n
        [Required(ErrorMessage = "El campo c&eacute;dula es obligatorio.")]
        public string Cedula { get; set; }

        //variables telefono
        [Required(ErrorMessage = "El campo tel&eacute;fono es obligatorio.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{2})[-. ]?([0-9]{4})$", ErrorMessage = "S&oacute;lo se permiten n&uacute;meros.")]
        public string Telefono { get; set; }

        //variables telefono de emergencia
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{2})[-. ]?([0-9]{4})$", ErrorMessage = "S&oacute;lo se permiten n&uacute;meros.")]
        public string TelefonoEmergencia { get; set; }

        //variables corrreo
        [Required(ErrorMessage = "El campo correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Form&actue;ato de correo electr&oacute;nico no v&acute;lido. ")]
        public string Correo { get; set; }

        //variables id
        public int IdUsers { get; set; }
        public string UsuarioId { get; set; }
        public int GimnasioId { get; set; }
        public IdentityUser IdentityUser { get; set; }
        [TempData]
        public string ErrorMessage { get; set; }

    }
}
