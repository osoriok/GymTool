using GymTool.Controllers;
using GymTool.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymTool.Areas.Principal.Controllers
{
    [Area("Principal")]
    public class PrincipalController : Controller
    {

        private SignInManager<IdentityUser> _signInManager;

        public PrincipalController(
            SignInManager<IdentityUser> signInManager,
            ApplicationDbContext context)
        {
            _signInManager = signInManager;
        }
        public IActionResult Principal()
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
    }
}