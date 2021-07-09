using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GymTool.Areas.Customers.Models;
using GymTool.Data;
using GymTool.Library;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using GymTool.Controllers;

namespace GymTool.Areas.Customers.Pages.Account
{
    [Authorize]
    [Area("Customers")]
    public class RegisterModel : PageModel
    {
        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _context;
        private LMembresia _clientMembership;
        private LCustomers _customer;

        private static InputModel _dataInput;
        private Uploadimage _uploadimage;
        private static InputModelRegister _dataClient1, _dataClient2;
        private IWebHostEnvironment _environment;
        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context,
            IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _environment = environment;
            _uploadimage = new Uploadimage();
            _clientMembership = new LMembresia(_context);
            _customer = new LCustomers(context);

        }
        public void OnGet(int id)
        {

            if (_signInManager.IsSignedIn(User))
            {
                var iduser = _userManager.GetUserId(User);

                if (id.Equals(0))
                {
                    _dataClient2 = null;
                    _dataInput = null;
                }
                if (_dataInput != null || _dataClient1 != null || _dataClient2 != null)
                {
                    if (_dataInput != null)
                    {
                        Input = _dataInput;
                        Input.membresiaLista = _clientMembership.getMembresias(iduser);
                        Input.Imagen = _dataClient2.Imagen;
                    }
                    else
                    {
                        if (_dataClient1 != null || _dataClient2 != null)
                        {
                            if (_dataClient2 != null)
                                _dataClient1 = _dataClient2;
                            Input = new InputModel
                            {
                                IdCliente = _dataClient1.IdCliente,
                                Nombre = _dataClient1.Nombre,
                                Apellidos = _dataClient1.Apellidos,
                                Cedula = _dataClient1.Cedula,
                                Correo = _dataClient1.Correo,
                                Telefono = _dataClient1.Telefono,
                                FechaNacimiento = _dataClient1.FechaNacimiento,
                                Direccion = _dataClient1.Direccion,
                                Imagen = _dataClient1.Imagen,
                                EmpleadoId = _dataClient1.EmpleadoId,
                                GimnasioId = _dataClient1.GimnasioId,
                                membresiaLista = _clientMembership.getMembresiasCliente(_dataClient1.Membresia, iduser),
                                FechaInscripcion = _dataClient1.FechaInscripcion,
                                Alcohol = _dataClient1.Alcohol,
                                Fuma = _dataClient1.Fuma,
                                DM = _dataClient1.DM,
                                HTA = _dataClient1.HTA,
                                Fx = _dataClient1.Fx,
                                Asma = _dataClient1.Asma,
                                Obesidad = _dataClient1.Obesidad,
                                IdExpediente = _dataClient1.IdExpediente
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
                    Input = new InputModel
                    {
                        membresiaLista = _clientMembership.getMembresias(iduser)
                    };
                }
            }

            if (_dataClient2 == null)
            {
                _dataClient2 = _dataClient1;
            }
            _dataClient1 = null;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel : InputModelRegister
        {
            public IFormFile AvatarImage { get; set; }
            public List<SelectListItem> membresiaLista { get; set; }
        }
        public async Task<IActionResult> OnPost(String DataCustomer, String accion, String accCliente)
        {
            if (DataCustomer == null)
            {
                if (_dataClient2 == null)
                {

                    if (await SaveAsync())
                    {
                        anularValores();

                        return Redirect("/Customers/Customers?area=Customers");
                    }
                    else
                    {
                        return Redirect("/Clientes/Registrar?id=1");
                    }
                }
                else
                {
                    if (accCliente.Equals("true"))
                    {

                        if (await UpdateAsync())
                        {
                            var url = $"/Clientes/Informacion?id={_dataClient2.IdCliente}";
                            anularValores();
                           
                            return Redirect(url);
                        }
                        else
                        {//
                            return Redirect("/Clientes/Registrar?id=1");
                        }
                    }
                    else
                    {
                        var url = $"/Clientes/Informacion?id={_dataClient2.IdCliente}";
                        return Redirect(url);
                    }
                }
            }
            else
            {
                _dataClient1 = JsonConvert.DeserializeObject<InputModelRegister>(DataCustomer);

                if (accion.Equals("Eliminar"))
                {
                    if (accCliente.Equals("true"))
                    {
                        if (await DeleteAsync())
                        {
                            anularValores();

                            return Redirect("/Customers/Customers?area=Customers");//Users/Users
                        }
                        else
                        {
                            var url = $"/Clientes/Informacion?id={_dataClient1.IdCliente}";
                            return Redirect(url);
                        }
                    }
                    else
                    {
                        var url = $"/Clientes/Informacion?id={_dataClient1.IdCliente}";
                        return Redirect(url);
                    }

                }
                else
                {
                    _dataClient2 = null;
                    _dataInput = null;
                    return Redirect("/Clientes/Registrar?id=1");
                }
            }
        }
        private async Task<bool> SaveAsync()
        {
            _dataInput = Input;
            var valor = false;
            var succes = false;
            var idgimnasio = 0;

            if (_signInManager.IsSignedIn(User))
            {

                if (ModelState.IsValid)
                {
                    var clientList = _context.TbCliente.Where(u => u.Cedula.Equals(Input.Cedula)).ToList();
                    if (clientList.Count.Equals(0))
                    {

                        var personalList = _context.TUsers.Where(u => u.Codigo.Equals(Input.EmpleadoCodigo)).ToList();
                        if (!personalList.Count.Equals(0))
                        {
                            var strategy = _context.Database.CreateExecutionStrategy();
                            await strategy.ExecuteAsync(async () =>
                            {
                                using (var transaction = _context.Database.BeginTransaction())
                                {

                                    try
                                    {
                                        var iduser = _userManager.GetUserId(User); //usuario admin iniciado
                                        var administrador = _context.TUsers.Where(u => u.UsuarioId.Equals(iduser)).ToList();//administrador
                                        var empleado = _context.TUsers.Where(u => u.Codigo.Equals(personalList[0].Codigo)).ToList();//administrador
                                        var membresiaid = _clientMembership.getMembresiaId(Input.Membresia);

                                        if (!administrador.Count.Equals(0))
                                        {
                                            idgimnasio = administrador[0].GimnasioId;//id gimnasio del administrador
                                            succes = true;
                                        }
                                        var imageByte = await _uploadimage.ByteAvatarImageAsync(
                                                        Input.AvatarImage, _environment, "images/images/default.png");

                                        if (succes)
                                        {

                                            var cliente = new TbCliente
                                            {
                                                Nombre = Input.Nombre,
                                                Apellidos = Input.Apellidos,
                                                Cedula = Input.Cedula,
                                                Correo = Input.Correo,
                                                Imagen = imageByte,
                                                Telefono = Input.Telefono,
                                                Direccion = Input.Direccion,
                                                FechaNacimiento = Input.FechaNacimiento,
                                                GimnasioId = idgimnasio,
                                                EmpleadoId = empleado[0].IdUsers,
                                                Estado = true
                                            };
                                            await _context.AddAsync(cliente);
                                            _context.SaveChanges();
                                            var expediente = new TbExpediente
                                            {
                                                DM = Input.DM,
                                                Fx = Input.Fx,
                                                HTA = Input.HTA,
                                                Fuma = Input.Fuma,
                                                Alcohol = Input.Alcohol,
                                                Asma = Input.Asma,
                                                Obesidad = Input.Obesidad,
                                                MembresiaId = membresiaid,
                                                FechaInscripcion = DateTime.Now,
                                                ClienteId = cliente.IdCliente,
                                                TbClienteIdCliente = cliente.IdCliente,
                                                TbCliente = cliente
                                            };
                                            await _context.AddAsync(expediente);
                                            _context.SaveChanges();
                                            transaction.Commit();
                                            _dataInput = null;
                                            valor = true;

                                        }
                                        else
                                        {
                                            valor = false;
                                            transaction.Rollback();
                                        }
                                    }
                                    catch (SqlException ex)
                                    {
                                        _dataInput.ErrorMessage = ex.Message;
                                        transaction.Rollback();
                                        valor = false;
                                    }
                                }
                            });


                        }
                        else
                        {
                            _dataInput.ErrorMessage = $"El código {Input.EmpleadoCodigo} no está registrado. ";
                            valor = false;
                        }

                    }
                    else
                    {
                        _dataInput.ErrorMessage = $"El cliente con cédula {Input.Cedula} ya está registrado. ";
                        valor = false;
                    }
                }
                else
                {
                    foreach (var modelState in ModelState.Values)
                    {
                        foreach (var error in modelState.Errors)
                        {
                            _dataInput.ErrorMessage += error.ErrorMessage;
                        }
                    }
                    valor = false;
                }
            }
            return valor;

        }

        private async Task<bool> UpdateAsync()
        {

            var valor = false;
            var strategy = _context.Database.CreateExecutionStrategy();
            byte[] imageByte = null;


            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    var personalList = _context.TUsers.Where(u => u.Codigo.Equals(Input.EmpleadoCodigo)).ToList();
                    if (!personalList.Count.Equals(0))
                    {

                        try
                        {
                            //var clientList = _customer.getTClient(Input.Cedula,_dataClient2.IdCliente);
                            var clientList = _context.TbCliente.Where(u => u.Cedula.Equals(Input.Cedula) && u.IdCliente != _dataClient2.IdCliente).ToList();

                            if (clientList.Count.Equals(0) )
                            {
                                if (Input.AvatarImage == null)
                                {
                                    imageByte = _dataClient2.Imagen;
                                }
                                else
                                {
                                    imageByte = await _uploadimage.ByteAvatarImageAsync(Input.AvatarImage, _environment, "");
                                }

                                var empleado = _context.TUsers.Where(u => u.Codigo.Equals(personalList[0].Codigo)).ToList();//administrador
                                var membresiaid = _clientMembership.getMembresiaId(Input.Membresia);

                                var t_client = new TbCliente
                                {
                                    IdCliente = _dataClient2.IdCliente,
                                    Nombre = Input.Nombre,
                                    Apellidos = Input.Apellidos,
                                    Cedula = Input.Cedula,
                                    Correo = Input.Correo,
                                    Imagen = imageByte,
                                    Telefono = Input.Telefono,
                                    Direccion = Input.Direccion,
                                    FechaNacimiento = Input.FechaNacimiento,
                                    GimnasioId = _dataClient2.GimnasioId,
                                    EmpleadoId = empleado[0].IdUsers,
                                    Estado = true
                                };
                                _context.Update(t_client);
                                _context.SaveChanges();
                                var t_expediente = new TbExpediente
                                {
                                    IdExpediente = _dataClient2.IdExpediente,
                                    DM = Input.DM,
                                    Fx = Input.Fx,
                                    HTA = Input.HTA,
                                    Fuma = Input.Fuma,
                                    Alcohol = Input.Alcohol,
                                    Asma = Input.Asma,
                                    Obesidad = Input.Obesidad,
                                    MembresiaId = membresiaid,
                                    FechaInscripcion = _dataClient2.FechaInscripcion,
                                    ClienteId = _dataClient2.IdCliente,
                                    TbClienteIdCliente = _dataClient2.IdCliente,
                                    TbCliente = t_client
                                };
                                _context.Update(t_expediente);
                                _context.SaveChanges();

                                transaction.Commit();
                                valor = true;

                            }
                            else
                            {
                                _dataInput = Input;
                                _dataInput.IdCliente = _dataClient2.IdCliente;
                                _dataInput.IdExpediente = _dataClient2.IdExpediente;
                                _dataInput.ErrorMessage = $"La cédula {Input.Cedula} ya esta registrado. ";
                                valor = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            _dataInput = Input;
                            _dataInput.IdCliente = _dataClient2.IdCliente;
                            _dataInput.IdExpediente = _dataClient2.IdExpediente;
                            _dataInput.ErrorMessage = ex.Message;
                            transaction.Rollback();
                            valor = false;
                        }
                    }
                    else
                    {
                        _dataInput = Input;
                        _dataInput.IdCliente = _dataClient2.IdCliente;
                        _dataInput.IdExpediente = _dataClient2.IdExpediente;
                        _dataInput.ErrorMessage = $"El código {Input.EmpleadoCodigo} no está registrado. ";
                        valor = false;
                    }
                }
            });


            return valor;
        }

        private async Task<bool> DeleteAsync()
        {
            var valor = false;
            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var t_client = new TbCliente
                        {
                            IdCliente = _dataClient1.IdCliente,
                            Nombre = _dataClient1.Nombre,
                            Apellidos = _dataClient1.Apellidos,
                            Cedula = _dataClient1.Cedula,
                            Correo = _dataClient1.Correo,
                            Imagen = _dataClient1.Imagen,
                            Telefono = _dataClient1.Telefono,
                            Direccion = _dataClient1.Direccion,
                            FechaNacimiento = _dataClient1.FechaNacimiento,
                            GimnasioId = _dataClient1.GimnasioId,
                            EmpleadoId = _dataClient1.EmpleadoId,
                            Estado = false
                        };
                        _context.Update(t_client);
                        _context.SaveChanges();
                        transaction.Commit();
                        valor = true;
                    }
                    catch (Exception ex)
                    {
                        _dataInput = Input;
                        _dataInput.IdCliente = _dataClient1.IdCliente;
                        _dataInput.IdExpediente = _dataClient1.IdExpediente;
                        _dataInput.ErrorMessage = ex.Message;
                        transaction.Rollback();
                        valor = false;
                    }
                }
            });

            return valor;
        }

        private void anularValores()
        {
            _dataClient2 = null;
            _dataClient1 = null;
            _dataInput = null;
        }
       

    }
}
