using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymTool.Areas.Customers.Models;
using GymTool.Data;
using GymTool.Library;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GymTool.Areas.Customers.Pages.Account
{
    public class DetailsModel : PageModel
    {

        private SignInManager<IdentityUser> _signInManager;
        UserManager<IdentityUser> _userManager;
        private LCustomers _customer;
       

        public DetailsModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ApplicationDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _customer = new LCustomers( context);
        }

        public void OnGet(int id)
        {
            if (_signInManager.IsSignedIn(User))
            {
                var iduser = _userManager.GetUserId(User);

                var data = _customer.getTClientes(null, id, iduser);
                if (0 < data.Count)
                {
                    Input = new InputModel
                    {
                        DataCustomer = data.ToList().Last(),
                    };
                }
            }
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            public InputModelRegister DataCustomer { get; set; }
        }

    }
}
