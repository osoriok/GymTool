using GymTool.Areas.Users.Models;
using GymTool.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTool.Library
{
    public class LUser : ListObject
    {

        public LUser(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = context;
            _usersRole = new LUsersRoles();
        }

        public async Task<List<InputModelRegister>> getTUsuariosAsync(String valor, int id, String iduser)
        {
            var userGimnasio = _context.TUsers.Where(u => u.UsuarioId.Equals(iduser)).ToList();

            List<TUsers> listUser;
            List<SelectListItem> _listRoles;
            List<InputModelRegister> userList = new List<InputModelRegister>();
            if (valor == null && id.Equals(0))
            {
                listUser = _context.TUsers.Where( u => u.GimnasioId.Equals( userGimnasio[0].GimnasioId ) && !u.UsuarioId.Equals(userGimnasio[0].UsuarioId) && u.Estado.Equals(true)).ToList();
            }
            else
            {
                if (id.Equals(0))
                {
                    listUser = _context.TUsers.Where( u => (u.Cedula.StartsWith(valor) || u.Nombre.StartsWith(valor) ||
                    u.Apellidos.Contains(valor) || u.Correo.StartsWith(valor) ) && u.GimnasioId.Equals(userGimnasio[0].GimnasioId) && u.Estado.Equals(true) ).ToList();
                }
                else
                {
                    listUser = _context.TUsers.Where(u => u.IdUsers.Equals(id)).ToList();
                }
            }

            if (!listUser.Count.Equals(0))
            {
                foreach (var item in listUser)
                {
                    _listRoles = await _usersRole.getRole(_userManager, _roleManager, item.UsuarioId);

                    if( _listRoles[0].Text != "Administrador")
                    {
                        var user = _context.Users.Where(u => u.Id.Equals(item.UsuarioId)).ToList().Last();
                        userList.Add(new InputModelRegister
                        {
                            IdUsers = item.IdUsers,
                            Cedula = item.Cedula,
                            Codigo = item.Codigo,
                            Nombre = item.Nombre,
                            Apellidos = item.Apellidos,
                            Telefono = item.Telefono,
                            TelefonoEmergencia = item.TelefonoEmergencia,
                            Correo = item.Correo,
                            UsuarioId = item.UsuarioId,
                            GimnasioId = item.GimnasioId,
                            IdentityUser = user
                        });
                    }
                    _listRoles.Clear();
                }
            }
            return userList;
        }

        internal async Task<SignInResult> UserLoginAsync(InputModelLogin model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);

            return result;
        }


    }



}
