using GymTool.Areas.Principal.Models;
using GymTool.Controllers;
using GymTool.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTool.Areas.Principal.Controllers
{
    [Authorize]
    [Area("Principal")]
    public class PrincipalController : Controller
    {

        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;
        private ApplicationDbContext _context;

        public static InputModel _dataInput;
        public static InputModelAsistencia _dataAsistencia1, _dataAsistencia2;
        private String roleAdmin = "Administrador";

        public PrincipalController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;

        }
        public IActionResult Principal( )
        {
            if (_signInManager.IsSignedIn(User))
            {
                return View();
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

        }

        public JsonResult GetAsistencias()
        {
            JsonResult result = new JsonResult(null);
            var asistencias = new List<TbAsistencia>();
            var asistenciasGimnasio = new List<TbAsistencia>();
            var clientes = new List<Customers.Models.TbCliente>();
            var eventos = new List<TbEventos>();


            if (_signInManager.IsSignedIn(User))
            {
                var iduser = _userManager.GetUserId(User);
                var personal = _context.TUsers.Where(u => u.UsuarioId.Equals(iduser)).ToList();//administrador

                asistencias = _context.TbAsistencia.ToList();

                if (!asistencias.Count.Equals(0))
                {
                    clientes = _context.TbCliente.ToList();
                    foreach (var asistencia in asistencias)
                    {
                        foreach (var cliente in clientes)
                        {

                            if (cliente.Estado.Equals(true) && cliente.IdCliente.Equals(asistencia.ClienteId) && cliente.GimnasioId.Equals(personal[0].GimnasioId))
                            {
                                asistenciasGimnasio.Add(asistencia);
                            }
                        }
                    }
                    eventos = obtenerEventos(asistenciasGimnasio);
                }
            }

            try
            {
                result = new JsonResult(eventos);
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }

            return result;
        }

        public List<TbEventos> obtenerEventos(List<TbAsistencia> asistencias) {

            var eventos = new List<TbEventos>();
            List<string> fechas = new List<string>();
            var agendada = false;

            foreach (var asistencia in asistencias)
            {
                var contador = 1;
                string hora = asistencia.FechaInicio.ToString("hh:mm tt");
                hora = hora + " - " + asistencia.FechaFinal.ToString("hh:mm tt");
                var fechaincio = asistencia.FechaInicio.ToString("yyyy-MM-dd HH:mm:ss");
                var fechafinal = asistencia.FechaFinal.ToString("yyyy-MM-dd HH:mm:ss");

                var fechainciohora = asistencia.FechaInicio.ToString("yyyy-MM-dd HH");
                var fechafinalhora = asistencia.FechaFinal.ToString("yyyy-MM-dd HH");

                agendada = false;
                var clientes = new List<Customers.Models.TbCliente>();
                var cliente = _context.TbCliente.Where(u => u.IdCliente == asistencia.ClienteId && u.Estado.Equals(true)).ToList();

                foreach (var fecha in fechas)
                {
                    if (fechainciohora.Equals(fecha))
                    {
                        agendada = true;
                    }
                }

                if (!agendada) {
                    if (!cliente.Count.Equals(0))
                        clientes.Add(cliente[0]);

                    foreach (var asis in asistencias)
                    {
                        if (asis.FechaInicio.ToString("yyyy-MM-dd HH").Equals(asistencia.FechaInicio.ToString("yyyy-MM-dd HH")) && asis.IdAsistencia != asistencia.IdAsistencia)
                        {
                            cliente = _context.TbCliente.Where(u => u.IdCliente == asis.ClienteId && u.Estado.Equals(true)).ToList();
                            clientes.Add(cliente[0]);
                            contador++;
                        }
                    }
                    fechas.Add(asistencia.FechaInicio.ToString("yyyy-MM-dd HH"));
                    eventos.Add(new TbEventos
                    {
                        cantidadAsistencias = contador,
                        fechaInicio = fechaincio,
                        fechaFinal = fechafinal,
                        horaAsistencia = hora,
                        listaCliente = clientes
                    });
                }

            }

            return eventos;
        }
        //action de registrar asistencia
        [HttpGet]
        public IActionResult Agregar() {
            InputModelAsistencia asistencia = new InputModelAsistencia();

            return PartialView("_AsistenciaModelPartial", asistencia);
        }

        [HttpGet]
        public IActionResult Mostrar( )
        {
            ModelAsistenciasHora asistencias = new ModelAsistenciasHora();

            return PartialView("_AsistenciasHoraModelPartial", asistencias);
        }


        //get clientes en autocomplete
        public JsonResult GetClientes(string term)
        {
            JsonResult result = new JsonResult(null);

            if (_signInManager.IsSignedIn(User))
            {
                var clientes = _context.TbCliente.Where(u => (u.Cedula.Contains(term) || u.Nombre.Contains(term) ||
                       u.Apellidos.Contains(term) || u.Direccion.Contains(term) || u.Correo.Contains(term)) && u.Estado.Equals(true)).Select(u => u.Nombre).Take(5).ToList();
                try
                {
                    result = new JsonResult(clientes);
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
            }

            return result;
        }


        public void OnGet(int id)
        {
            if (_signInManager.IsSignedIn(User))
            {
                if (id.Equals(0))
                {
                    _dataAsistencia2 = null;
                    _dataInput = null;
                }
                if (_dataInput != null || _dataAsistencia1 != null || _dataAsistencia2 != null)
                {
                    if (_dataInput != null)
                    {
                        Input = _dataInput;
                    }
                    else
                    {
                        if (_dataAsistencia1 != null || _dataAsistencia2 != null)
                        {
                            if (_dataAsistencia2 != null)
                                _dataAsistencia1 = _dataAsistencia2;
                            Input = new InputModel
                            {
                                clienteNombre = _dataAsistencia1.clienteNombre,
                                FechaAsistencia = _dataAsistencia1.FechaAsistencia,
                                Hora = _dataAsistencia1.Hora
                            };
                            if (_dataInput != null)
                            {
                                Input.ErrorMessage = _dataInput.ErrorMessage;
                            }
                        }
                    }

                }
                else
                {
                    Input = new InputModel { };
                }

                if (_dataAsistencia2 == null)
                {
                    _dataAsistencia2 = _dataAsistencia1;
                }
                _dataAsistencia1 = null;
            }
        }



        private async Task<bool> ValidarCliente()
        {
            var valor = false;
            if (_signInManager.IsSignedIn(User))
            {
                _dataInput = Input;
                _dataInput.ErrorMessage = $"El cliente {Input.clienteNombre} no está registrado. ";

                //if (ModelState.IsValid)
                //{
                    //var clientList = _context.TbCliente.Where(u => Input.clienteNombre.Equals( u.Cedula) ).ToList();
                    //if (!clientList.Count.Equals(0))
                    //{
                    //    valor = true;
                    //}
                    //else
                    //{
                        //_dataInput.ErrorMessage = $"El cliente {Input.clienteNombre} no está registrado. ";
                        //valor = false;
                    //}
                //}

            }
            return valor;
        }

        private async Task<bool> SaveAsync()
        {
            var valor = true;
            if (_signInManager.IsSignedIn(User))
            {
                _dataInput = Input;

                if (ModelState.IsValid)
                {

                }


            }
            return valor;
        }


        private void anularValores()
        {
            _dataAsistencia2 = null;
            _dataAsistencia1 = null;
            _dataInput = null;
        }


        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel : InputModelAsistencia
        {

        }

    }
}