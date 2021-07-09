using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GymTool.Areas.Users.Models
{
    public class InputModelRegister
    {
        //variables nombre
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string Nombre { get; set; }

        //variables apellido
        [Required(ErrorMessage = "El campo Apellidos es obligatorio")]
        public string Apellidos { get; set; }

        //variables codigo
        public string Codigo { get; set; }

        //variables identificaci&oacute;n
        [Required(ErrorMessage = "El campo C&eacute;dula es obligatorio")]
        public string Cedula { get; set; }

        //variables telefono
        [Required(ErrorMessage = "El campo Tel&eacute;fono es obligatorio.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{2})[-. ]?([0-9]{4})$", ErrorMessage = "El formato del Tel&eacute;fono ingresado no es v&aacute;lido.")]
        public string Telefono { get; set; }

        //variables telefono de emergencia
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{2})\)?[-. ]?([0-9]{2})[-. ]?([0-9]{4})$", ErrorMessage = "El formato del Tel&eacute;fono Emergencia ingresado no es v&aacute;lido.")]
        public string TelefonoEmergencia { get; set; }

        //variables corrreo
        [Required(ErrorMessage = "El campo Correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El Correo no es una direcci&oacute;n de correo electr&oacute;nico v&aacute;lida.")]
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
