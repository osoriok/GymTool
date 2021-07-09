using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GymTool.Areas.Users.Models
{
    public class InputModelLogin
    {
        [Required(ErrorMessage = "El campo correo electr&oacute;nico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electr&oacute;nico no es una direcci&oacute;n de correo electr&oacute;nico v&aacute;lida.")]
        public string Email { get; set; }

        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "El campo contraseña es obligatorio.")]
        [StringLength(100, ErrorMessage = "El n&uacute;mero de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        public string Password { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }
    }
}
