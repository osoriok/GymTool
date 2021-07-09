using GymTool.Areas.Users.Models;
using GymTool.Controllers;
using GymTool.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GymTool.Areas.Users.Pages.Account
{
    [Authorize]
    [Area("Users")]
    public class RegisterModel : PageModel
    {
        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;
        private ApplicationDbContext _context;
        public static InputModel _dataInput;
        private static InputModelRegister _dataUser1, _dataUser2;


        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public void OnGet(int id)
        {
            if (_signInManager.IsSignedIn(User) && User.IsInRole("Administrador"))
            {
                if (id.Equals(0))
                {
                    _dataUser2 = null;
                    _dataInput = null;
                }
                if (_dataInput != null || _dataUser1 != null || _dataUser2 != null)
                {
                    if (_dataInput != null)
                    {
                        Input = _dataInput;
                    }
                    else
                    {
                        if (_dataUser1 != null || _dataUser2 != null)
                        {
                            if (_dataUser2 != null)
                                _dataUser1 = _dataUser2;
                            Input = new InputModel
                            {
                                IdUsers = _dataUser1.IdUsers,
                                Cedula = _dataUser1.Cedula,
                                Codigo = _dataUser1.Codigo,
                                Nombre = _dataUser1.Nombre,
                                Apellidos = _dataUser1.Apellidos,
                                Telefono = _dataUser1.Telefono,
                                TelefonoEmergencia = _dataUser1.TelefonoEmergencia,
                                Correo = _dataUser1.Correo,
                                GimnasioId = _dataUser1.GimnasioId,
                                UsuarioId = _dataUser1.UsuarioId
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

                if (_dataUser2 == null)
                {
                    _dataUser2 = _dataUser1;
                }

                _dataUser1 = null;
            }
        }


        public async Task<IActionResult> OnPost(String dataUser, String accion, String accUsuario)
        {
            if (_signInManager.IsSignedIn(User) && User.IsInRole("Administrador"))
            {

                if (dataUser == null)
                {
                    if (_dataUser2 == null)
                    {
                        if (await SaveAsync())
                        {
                            anularValores();

                            return Redirect("/Users/Users?area=Users");//Users/Users
                        }
                        else
                        {
                            return Redirect("/Personal/Registrar?id=1");///onget con el id
                        }
                    }
                    else
                    { 
                        if (accUsuario.Equals("true"))
                        {

                            if (await UpdateAsync())
                            {
                                var url = $"/Personal/Informacion?id={_dataUser2.IdUsers}";
                                anularValores();

                                return Redirect(url);
                            }
                            else
                            {
                                return Redirect("/Personal/Registrar?id=1");
                            }
                        }
                        else
                        {

                            var url = $"/Personal/Informacion?id={_dataUser2.IdUsers}";
                            return Redirect(url);
                        }

                    }
                }
                else
                {
                    _dataUser1 = JsonConvert.DeserializeObject<InputModelRegister>(dataUser);

                    if (accion.Equals("Eliminar"))
                    {
                        if (accUsuario.Equals("true"))
                        {
                            if (await DeleteAsync())
                            {
                                anularValores();
                                return Redirect("/Users/Users?area=Users");//Users/Users
                            }
                            else
                            {
                                var url = $"/Personal/Informacion?id={_dataUser1.IdUsers}";
                                return Redirect(url);
                            }
                        }
                        else
                        {
                            var url = $"/Personal/Informacion?id={_dataUser1.IdUsers}";
                            return Redirect(url);
                        }

                    }
                    else
                    {
                        _dataUser2 = null;
                        _dataInput = null;
                        return Redirect("/Personal/Registrar?id=1");

                    }

                }
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        private async Task<bool> SaveAsync()
        {
            var valor = false;

            if (_signInManager.IsSignedIn(User) && User.IsInRole("Administrador"))
            {
                _dataInput = Input;
                var succes = false;
                var perosnalUser = new IdentityUser();
                var idgimnasio = 0;
                if (_signInManager.IsSignedIn(User))
                {
                    if (ModelState.IsValid)
                    {
                        var userList = _context.TUsers.Where(u => u.Cedula.Equals(Input.Cedula)).ToList();
                        if (userList.Count.Equals(0))
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

                                        if (!administrador.Count.Equals(0))
                                        {
                                            idgimnasio = administrador[0].GimnasioId;//id gimnasio del administrador
                                            var userGimnasio = _context.TbUsuarioGimnasio.Where(u => u.GimnasioId.Equals(idgimnasio)).ToList();//usuario del gimnasio
                                            perosnalUser = _userManager.Users.Where(u => u.Id.Equals(userGimnasio[0].UsuarioId)).ToList().Last();//usuario del personal
                                            succes = true;
                                        }

                                        var codigo = "0000";
                                        while (succes)
                                        {
                                            codigo = GenerateRandomCodigo().ToString();
                                            var codigolist = _context.TUsers.Where(u => u.Codigo.Equals(codigo)).ToList();
                                            if (userList.Count.Equals(0))
                                            {
                                                succes = false;
                                            }
                                        }

                                        if (!succes)
                                        {
                                            var t_user = new TUsers
                                            {
                                                Cedula = Input.Cedula,
                                                Codigo = codigo,
                                                Nombre = Input.Nombre,
                                                Apellidos = Input.Apellidos,
                                                Correo = Input.Correo,
                                                Telefono = Input.Telefono,
                                                TelefonoEmergencia = Input.TelefonoEmergencia,
                                                GimnasioId = idgimnasio,
                                                UsuarioId = perosnalUser.Id,
                                                Estado = true
                                            };
                                            await _context.AddAsync(t_user);
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
                                    catch (Exception ex)
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
                            _dataInput.ErrorMessage = $"El empleado con cédula {Input.Cedula} ya está registrado. ";
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
            }
            return valor;
        }

        private async Task<bool> UpdateAsync()
        {
            var valor = false;

            if (_signInManager.IsSignedIn(User) && User.IsInRole("Administrador"))
            {
                var strategy = _context.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            var userList = _context.TUsers.Where(u => u.Cedula.Equals(Input.Cedula) && u.IdUsers != _dataUser2.IdUsers).ToList();
                            if (userList.Count.Equals(0))
                            {
                                var t_user = new TUsers
                                {
                                    IdUsers = _dataUser2.IdUsers,
                                    Cedula = Input.Cedula,
                                    Codigo = _dataUser2.Codigo,
                                    Nombre = Input.Nombre,
                                    Apellidos = Input.Apellidos,
                                    Correo = Input.Correo,
                                    Telefono = Input.Telefono,
                                    TelefonoEmergencia = Input.TelefonoEmergencia,
                                    GimnasioId = _dataUser2.GimnasioId,
                                    UsuarioId = _dataUser2.UsuarioId,
                                    Estado = true
                                };
                                _context.Update(t_user);
                                _context.SaveChanges();

                                transaction.Commit();

                                valor = true;

                            }
                            else
                            {
                                _dataInput = Input;
                                _dataInput.IdUsers = _dataUser2.IdUsers;
                                _dataInput.ErrorMessage = $"El empleado con cédula {Input.Cedula} ya está registrado. ";
                                valor = false;
                            }
                        }
                        catch (Exception ex)
                        {
                            _dataInput = Input;
                            _dataInput.IdUsers = _dataUser2.IdUsers;
                            _dataInput.ErrorMessage = ex.Message;
                            transaction.Rollback();
                            valor = false;
                        }
                    }

                });
            }
            return valor;
        }

        private async Task<bool> DeleteAsync()
        {
            var valor = false;

            if (_signInManager.IsSignedIn(User) && User.IsInRole("Administrador"))
            {
                var strategy = _context.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            var t_user = new TUsers
                            {
                                IdUsers = _dataUser1.IdUsers,
                                Cedula = _dataUser1.Cedula,
                                Codigo = _dataUser1.Codigo,
                                Nombre = _dataUser1.Nombre,
                                Apellidos = _dataUser1.Apellidos,
                                Correo = _dataUser1.Correo,
                                Telefono = _dataUser1.Telefono,
                                TelefonoEmergencia = _dataUser1.TelefonoEmergencia,
                                GimnasioId = _dataUser1.GimnasioId,
                                UsuarioId = _dataUser1.UsuarioId,
                                Estado = false
                            };
                            _context.Update(t_user);
                            _context.SaveChanges();

                            transaction.Commit();
                            valor = true;
                        }
                        catch (Exception ex)
                        {
                            _dataInput = Input;
                            _dataInput.IdUsers = _dataUser1.IdUsers;
                            _dataInput.ErrorMessage = ex.Message;
                            transaction.Rollback();
                            valor = false;
                        }
                    }
                });
            }

            return valor;
        }

        public int GenerateRandomCodigo()
        {
            int _min = 0000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel : InputModelRegister
        {

        }

        private void anularValores()
        {
            _dataUser2 = null;
            _dataUser1 = null;
            _dataInput = null;
        }
    }
}
