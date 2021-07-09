using GymTool.Areas.Users.Models;
using GymTool.Controllers;
using GymTool.Data;
using GymTool.Library;
using GymTool.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTool.Areas.Users.Controllers
{
    [Area("Users")]
    [Authorize]
    public class UsersController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;

        private LUser _user;
        private static DataPaginador<InputModelRegister> models;

        public UsersController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _user = new LUser(userManager, signInManager, roleManager, context);
        }

        public IActionResult Users(int id, String filtrar, int registros)
        {
            if (_signInManager.IsSignedIn(User) && User.IsInRole("Administrador"))
            {
                var iduser = _userManager.GetUserId(User);
                Object[] objects = new Object[3];
                var data = _user.getTUsuariosAsync(filtrar, 0, iduser);
                if (0 < data.Result.Count)
                {
                    var url = Request.Scheme + "://" + Request.Host.Value;
                    objects = new LPaginador<InputModelRegister>().paginador(data.Result,
                        id, registros, "Users", "Users", "Users", url);
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

        public async Task<IActionResult> Logout()
        {
            if (_signInManager.IsSignedIn(User) )
            {
                await _signInManager.SignOutAsync();
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");

        }
    }
}
