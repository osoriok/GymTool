using GymTool.Areas.Memberships.Models;
using GymTool.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GymTool.Library
{
    public class LMembresia : ListObject
    {

        public LMembresia(
            ApplicationDbContext context)
        {
            _context = context;
            _usersRole = new LUsersRoles();
        }
        public List<SelectListItem> getMembresias(String iduser)
        {
            var userGimnasio = _context.TUsers.Where(u => u.UsuarioId.Equals(iduser)).ToList();
            var membresias = _context.TbMembresia.Where(u => u.GimnasioId.Equals(userGimnasio[0].GimnasioId)).ToList();
            List<SelectListItem> _selectLists = new List<SelectListItem>();
            membresias.ForEach(item =>
            {
                _selectLists.Add(new SelectListItem
                {
                    Value = item.IdMembresia.ToString(),
                    Text = item.Nombre
                });
            });
            return _selectLists;
        }

        public List<SelectListItem> getMembresiasCliente(String membresia, String iduser)
        {
            List<SelectListItem> membresiasLista = new List<SelectListItem>();
            membresiasLista.Add(new SelectListItem
            {
                Text = membresia
            });
            var membresias = getMembresias(iduser);
            membresias.ForEach(item =>
            {
                if (item.Text != membresia)
                {
                    membresiasLista.Add(new SelectListItem
                    {
                        Text = item.Text
                    });
                }
            });
            return membresiasLista;
        }


        public List<SelectListItem> getPeriodos()
        {
            String[] periodos = { "Día", "Semana", "Mes", "Año" };
            List<SelectListItem> _selectLists = new List<SelectListItem>();
            foreach (var periodo in periodos)
            {
                _selectLists.Add(new SelectListItem
                {
                    Value = periodo,
                    Text = periodo
                });
            }
            return _selectLists;
        }

        public List<SelectListItem> getPeriodo(String periodo)
        {
            String[] periodos = { "Día", "Semana", "Mes", "Año" };

            List<SelectListItem> periodosLista = new List<SelectListItem>();
            periodosLista.Add(new SelectListItem
            {
                Text = periodo
            });

            foreach (var period in periodos)
            {
                if (!period.Equals(periodo))
                {
                    periodosLista.Add(new SelectListItem
                    {
                        Value = period,
                        Text = period
                    });
                }
            }
            return periodosLista;
        }

        public List<InputModelRegister> getMembresiasMostrar(String valor, int id, String iduser)
        {
            var userGimnasio = _context.TUsers.Where(u => u.UsuarioId.Equals(iduser)).ToList();
            var membresias = _context.TbMembresia.Where(u => u.GimnasioId.Equals(userGimnasio[0].GimnasioId)).ToList();
            List<SelectListItem> _selectLists = new List<SelectListItem>();
            List<TbMembresia> listTbMembresias;
            var MembershipsList = new List<InputModelRegister>();

            if (valor == null && id.Equals(0))
            {
                listTbMembresias = _context.TbMembresia.Where(u => u.GimnasioId.Equals(userGimnasio[0].GimnasioId) && u.Estado.Equals(true)).ToList();
            }
            else
            {
                if (id.Equals(0))
                {
                    listTbMembresias = _context.TbMembresia.Where(u => (u.Nombre.StartsWith(valor) || u.Descripcion.StartsWith(valor)) && u.Estado.Equals(true)).ToList();
                }
                else
                {
                    listTbMembresias = _context.TbMembresia.Where(u => u.IdMembresia.Equals(id)).ToList();
                }
            }
            if (!listTbMembresias.Count.Equals(0))
            {
                foreach (var item in listTbMembresias)
                {
                    MembershipsList.Add(new InputModelRegister
                    {
                        IdMembresia = item.IdMembresia,
                        GimnasioId = item.GimnasioId,
                        Descripcion = item.Descripcion,
                        Nombre = item.Nombre,
                        Cantidad = item.Cantidad,
                        Monto = item.Monto,
                        Periodo = item.Periodo
                    });
                }
            }
            return MembershipsList;
        }

        public List<SelectListItem> getMembresia(int idmembresia)
        {
            List<SelectListItem> _selectList = new List<SelectListItem>();
            var membresias = _context.TbMembresia.Where(u => u.IdMembresia.Equals(idmembresia)).ToList();

            if (membresias.Count.Equals(0))
            {
                _selectList.Add(new SelectListItem
                {
                    Value = "0",
                    Text = "No hay membresías"
                });
            }
            else
            {
                foreach (var Data in membresias)
                {
                    _selectList.Add(new SelectListItem
                    {
                        Value = Data.IdMembresia.ToString(),
                        Text = Data.Nombre
                    });
                }
            }
            return _selectList;
        }

        public int getMembresiaId(String nombremembresia)
        {
            var membresias = _context.TbMembresia.Where(u => u.Nombre.Equals(nombremembresia)).ToList();
            var id = 0;
            if (!membresias.Count.Equals(0))
            {
                id = membresias[0].IdMembresia;
            }

            return id;
        }


    }
}
