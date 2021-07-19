using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymTool.Areas.Memberships.Models;
using GymTool.Controllers;
using GymTool.Data;
using GymTool.Library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace GymTool.Areas.Memberships.Pages.Account
{
    [Authorize]
    [Area("Memberships")]
    public class RegisterModel : PageModel
    {

        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;
        private ApplicationDbContext _context;
        public static InputModel _dataInput;
        private static InputModelRegister _dataMembership1, _dataMembership2;
        private LMembresia _membership;
        private String roleAdmin = "Administrador";


        public RegisterModel(
           UserManager<IdentityUser> userManager,
           SignInManager<IdentityUser> signInManager,
           ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _membership = new LMembresia(context);
        }

        public void OnGet(int id)
        {
            if (_signInManager.IsSignedIn(User))
            {

                if (id.Equals(0))
                {
                    _dataMembership2 = null;
                    _dataInput = null;
                }
                if (_dataInput != null || _dataMembership1 != null || _dataMembership2 != null)
                {
                    if (_dataInput != null)
                    {
                        Input = _dataInput;
                        Input.periodosLista = _membership.getPeriodos();
                    }
                    else
                    {
                        if (_dataMembership1 != null || _dataMembership2 != null)
                        {
                            if (_dataMembership2 != null)
                                _dataMembership1 = _dataMembership2;
                            Input = new InputModel
                            {
                                IdMembresia = _dataMembership1.IdMembresia,
                                Nombre = _dataMembership1.Nombre,
                                Descripcion = _dataMembership1.Descripcion,
                                Cantidad = _dataMembership1.Cantidad,
                                Periodo = _dataMembership1.Periodo,
                                periodosLista = _membership.getPeriodo(_dataMembership1.Periodo),
                                Monto = _dataMembership1.Monto,
                                GimnasioId = _dataMembership1.GimnasioId
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
                        periodosLista = _membership.getPeriodos()
                    };
                }

                if (_dataMembership2 == null)
                {
                    _dataMembership2 = _dataMembership1;
                }

                _dataMembership1 = null;
            }
        }

        public async Task<IActionResult> OnPost(String dataMembership, String accion, String accMembresia)
        {
            if (_signInManager.IsSignedIn(User))
            {
                if (dataMembership == null)
                {
                    if (_dataMembership2 == null)
                    {
                        if (await SaveAsync())
                        {
                            anularValores();
                            return Redirect("/Memberships/Memberships?area=Memberships");//Users/Users
                        }
                        else
                        {
                            return Redirect("/Membresia/Registrar?id=1");///onget con el id
                        }
                    }
                    else
                    {
                        
                        if (accMembresia.Equals("true"))
                        {

                            if (await UpdateAsync())
                            {
                                var url = $"/Membresia/Informacion?id={_dataMembership2.IdMembresia}";
                                anularValores();

                                return Redirect(url);
                            }
                            else
                            {
                                return Redirect("/Membresia/Registrar?id=1");
                            }
                        }
                        else
                        {

                            var url = $"/Membresia/Informacion?id={_dataMembership2.IdMembresia}";
                            anularValores();

                            return Redirect(url);
                        }

                    }
                }
                else
                {
                    _dataMembership1 = JsonConvert.DeserializeObject<InputModelRegister>(dataMembership);

                    if (accion.Equals("Eliminar"))
                    {
                        if (accMembresia.Equals("true"))
                        {
                            if (await DeleteAsync())
                            {
                                anularValores();

                                return Redirect("/Memberships/Memberships?area=Memberships");//Users/Users
                            }
                            else
                            {
                                var url = $"/Membresia/Informacion?id={_dataMembership1.IdMembresia}";
                                anularValores();

                                return Redirect(url);
                            }
                        }
                        else
                        {
                            var url = $"/Membresia/Informacion?id={_dataMembership1.IdMembresia}";
                            anularValores();
                            return Redirect(url);
                        }

                    }
                    else
                    {
                        _dataMembership2 = null;
                        _dataInput = null;
                        return Redirect("/Membresia/Registrar?id=1");

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

            if (_signInManager.IsSignedIn(User))
            {
                _dataInput = Input;
                var idgimnasio = 0;

                if (_signInManager.IsSignedIn(User))
                {
                    if (ModelState.IsValid)
                    {

                        var iduser = _userManager.GetUserId(User); //usuario admin iniciado
                        var administrador = _context.TUsers.Where(u => u.UsuarioId.Equals(iduser)).ToList();//administrador

                        if (!administrador.Count.Equals(0))
                        {
                            idgimnasio = administrador[0].GimnasioId;//id gimnasio del administrador
                            var memberList = _context.TbMembresia.Where(u => u.Nombre.Equals(Input.Nombre) && u.GimnasioId.Equals(idgimnasio)).ToList();
                            if (memberList.Count.Equals(0))
                            {

                                var strategy = _context.Database.CreateExecutionStrategy();
                                await strategy.ExecuteAsync(async () =>
                                {
                                    using (var transaction = _context.Database.BeginTransaction())
                                    {
                                        try
                                        {
                                            
                                            var t_membership = new TbMembresia
                                            {
                                                Nombre = Input.Nombre,
                                                Descripcion = Input.Descripcion,
                                                Monto = Input.Monto,
                                                Cantidad = Input.Cantidad,
                                                Periodo = Input.Periodo,
                                                GimnasioId = idgimnasio,
                                                Estado = true
                                            };
                                            await _context.AddAsync(t_membership);
                                            _context.SaveChanges();

                                            transaction.Commit();
                                            _dataInput = null;
                                            valor = true;
                                            
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
                                _dataInput.ErrorMessage = $"La membresía con nombre {Input.Nombre} ya está registrada. ";
                                valor = false;
                            }
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

            if (_signInManager.IsSignedIn(User))
            {

                var strategy = _context.Database.CreateExecutionStrategy();
                if (_signInManager.IsSignedIn(User))
                {

                    var memberList = _context.TbMembresia.Where(u => u.Nombre.Equals(Input.Nombre) && u.IdMembresia != _dataMembership2.IdMembresia).ToList();
                    if (memberList.Count.Equals(0))
                    {

                        await strategy.ExecuteAsync(async () =>
                        {
                            using (var transaction = _context.Database.BeginTransaction())
                            {

                                try
                                {

                                    var t_membership = new TbMembresia
                                    {
                                        IdMembresia = _dataMembership2.IdMembresia,
                                        Nombre = Input.Nombre,
                                        Descripcion = Input.Descripcion,
                                        Monto = Input.Monto,
                                        Cantidad = Input.Cantidad,
                                        Periodo = Input.Periodo,
                                        GimnasioId = _dataMembership2.GimnasioId,
                                        Estado = true
                                    };
                                    _context.Update(t_membership);
                                    _context.SaveChanges();

                                    transaction.Commit();
                                    valor = true;

                                }
                                catch (Exception ex)
                                {
                                    igualarValoresUpdate(ex.Message);
                                    transaction.Rollback();
                                    valor = false;
                                }

                            }
                        });

                    }
                    else
                    {
                        igualarValoresUpdate($"La membresía con nombre {Input.Nombre} ya está registrada. ");
                        valor = false;
                    }

                }
            }
            return valor;
        }


        private async Task<bool> DeleteAsync()
        {
            var valor = false;

            if (_signInManager.IsSignedIn(User) && User.IsInRole(roleAdmin))
            {

                var strategy = _context.Database.CreateExecutionStrategy();
                await strategy.ExecuteAsync(async () =>
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            int cont = 0;
                            var clientList = _context.TbCliente.Where(u => u.Estado.Equals(true)).ToList();
                            var expedienteList = new List<Customers.Models.TbExpediente>();
                            if (!clientList.Count.Equals(0))
                            {
                                foreach (var client in clientList)
                                {
                                    expedienteList = _context.TbExpediente.Where(u => u.MembresiaId.Equals(_dataMembership1.IdMembresia) && u.ClienteId.Equals(client.IdCliente)).ToList();
                                    if (!expedienteList.Count.Equals(0))
                                    {
                                        cont++;
                                    }
                                }


                            }
                            if (cont == 0)
                            {
                                var t_membership = new TbMembresia
                                {
                                    IdMembresia = _dataMembership1.IdMembresia,
                                    Nombre = _dataMembership1.Nombre,
                                    Descripcion = _dataMembership1.Descripcion,
                                    Cantidad = _dataMembership1.Cantidad,
                                    Periodo = _dataMembership1.Periodo,
                                    Monto = _dataMembership1.Monto,
                                    GimnasioId = _dataMembership1.GimnasioId,
                                    Estado = false
                                };
                                _context.Update(t_membership);
                                _context.SaveChanges();

                                transaction.Commit();
                                valor = true;

                            }
                            else
                            {
                                igualarValoresDelete($"Existen clientes activos con la membresía que deseas eliminar. ");
                                valor = false;
                            }

                        }
                        catch (Exception ex)
                        {
                            igualarValoresDelete(ex.Message);
                            transaction.Rollback();
                            valor = false;
                        }
                    }
                });
            }
            return valor;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel : InputModelRegister
        {
            public List<SelectListItem> periodosLista { get; set; }

        }

        private void anularValores()
        {
            _dataMembership2 = null;
            _dataMembership1 = null;
            _dataInput = null;
        }

        private void igualarValoresUpdate(String mensaje)
        {
            _dataInput = Input;
            _dataInput.IdMembresia = _dataMembership2.IdMembresia;
            _dataInput.ErrorMessage = mensaje;

        }

        private void igualarValoresDelete(String mensaje)
        {
            _dataInput = Input;
            _dataInput.IdMembresia = _dataMembership1.IdMembresia;
            _dataInput.ErrorMessage = mensaje;
        }
    }
}
