using GymTool.Areas.Customers.Models;
using GymTool.Controllers;
using GymTool.Data;
using GymTool.Library;
using GymTool.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTool.Areas.Customers.Controllers
{
    [Area("Customers")]

    public class CustomersController : Controller
    {
        private LCustomers _customer;
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private static DataPaginador<InputModelRegister> models;

        public CustomersController(
            UserManager<IdentityUser> userManager,
           SignInManager<IdentityUser> signInManager,
           ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _customer = new LCustomers(context);
        }
        public IActionResult Customers(int id, String filtrar, int registros)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var iduser = _userManager.GetUserId(User);

                Object[] objects = new Object[3];
                var data = _customer.getTClientes(filtrar, 0, iduser);
                if (0 < data.Count)
                {
                    var url = Request.Scheme + "://" + Request.Host.Value;
                    objects = new LPaginador<InputModelRegister>().paginador(data,
                        id, registros, "Customers", "Customers", "Customers", url);
                }
                else
                {
                    objects[0] = "0";
                    objects[1] = "No hay datos que mostrar";
                    objects[2] = new List<InputModelRegister>();
                }
                models = new DataPaginador<InputModelRegister>
                {
                    List = (List<InputModelRegister>)objects[2],
                    Pagi_info = (String)objects[0],
                    Pagi_navegacion = (String)objects[1],
                    Input = new InputModelRegister(),
                };
                return View(models);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

        }
    }
}
