using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymTool.Areas.Memberships.Models;
using GymTool.Data;
using GymTool.Library;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GymTool.Areas.Memberships.Pages.Account
{
    public class DetailsModel : PageModel
    {
        private SignInManager<IdentityUser> _signInManager;
        UserManager<IdentityUser> _userManager;

        LMembresia _membresia;
        public DetailsModel(
              UserManager<IdentityUser> userManager,
              SignInManager<IdentityUser> signInManager,
              ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _membresia = new LMembresia(context);
        }
        public void OnGet(int id)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var iduser = _userManager.GetUserId(User);

                var data = _membresia.getMembresiasMostrar(null, id, iduser);
                if (0 < data.Count)
                {
                    Input = new InputModel
                    {
                        DataMembership = data.ToList().Last(),
                    };
                }
            }
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            public InputModelRegister DataMembership { get; set; }
        }
    }
}
