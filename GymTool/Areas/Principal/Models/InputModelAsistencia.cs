using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace GymTool.Areas.Principal.Models
{
    public class InputModelAsistencia
    {
        [Required(ErrorMessage = "Seleccione un cliente. ")]
        public string clienteNombre { get; set; }
        public int clienteId { get; set; }
        [Required(ErrorMessage = "Selecciona la fecha de asistencia.")]
        public DateTime FechaAsistencia { set; get; }
        [Required(ErrorMessage = "Ingrese la hora de asistencia.")]
        public string Hora { set; get; }
        [TempData]
        public string ErrorMessage { get; set; }
    }
}
