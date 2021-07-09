using GymTool.Areas.Memberships.Models;
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

namespace GymTool.Areas.Memberships.Controllers
{
    [Area("Memberships")]

    public class MembershipsController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;
        private static DataPaginador<InputModelRegister> models;
        private LMembresia _lmembresia;

        public MembershipsController(
            UserManager<IdentityUser> userManager,
           SignInManager<IdentityUser> signInManager,
           ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _lmembresia = new LMembresia(context);
        }

        public IActionResult Memberships(int id, String filtrar)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var iduser = _userManager.GetUserId(User);

                Object[] objects = new Object[3];
                var data = _lmembresia.getMembresiasMostrar( filtrar, id , iduser);
                if (0 < data.Count)
                {
                    var url = Request.Scheme + "://" + Request.Host.Value;
                    objects = new LPaginador<InputModelRegister>().paginador(data,
                        id, 10, "Memberships", "Memberships", "Memberships", url);
                }
                else
                {
                    objects[0] = "No hay datos que mostrar";
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
