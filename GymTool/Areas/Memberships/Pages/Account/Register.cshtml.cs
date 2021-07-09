using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymTool.Areas.Memberships.Models;
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
                Input = new InputModel {
                    periodosLista = _membership.getPeriodos()
                };
            }

            if (_dataMembership2 == null)
            {
                _dataMembership2 = _dataMembership1;
            }

            _dataMembership1 = null;

        }

        public async Task<IActionResult> OnPost(String dataMembership, String accion, String accMembresia)
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
                    //if (User.IsInRole("Administrador"))
                    //{ 
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
                        return Redirect(url);
                    }

                    //}
                    //else
                    //{
                    //    return Redirect("/Users/Users?area=Users");
                    //}

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
                            return Redirect(url);
                        }
                    }
                    else
                    {
                        var url = $"/Membresia/Informacion?id={_dataMembership1.IdMembresia}";
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
                                    succes = true;
                                }

                                if (succes)
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
            if (_signInManager.IsSignedIn(User))
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
                            _dataInput = Input;
                            _dataInput.IdMembresia = _dataMembership2.IdMembresia;
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
                            _dataInput = Input;
                            _dataInput.IdMembresia = _dataMembership1.IdMembresia;
                            _dataInput.ErrorMessage = $"Existen clientes activos con esta membresía. ";
                            valor = false;
                        }

                    }
                    catch (Exception ex)
                    {
                        _dataInput = Input;
                        _dataInput.IdMembresia = _dataMembership1.IdMembresia;
                        _dataInput.ErrorMessage = ex.Message;
                        transaction.Rollback();
                        valor = false;
                    }
                }
            });

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
    }
}
