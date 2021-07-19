using GymTool.Areas.Customers.Models;
using GymTool.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymTool.Library
{
    public class LCustomers : ListObject
    {
        public LCustomers(ApplicationDbContext context)
        {
            _context = context;
            _membresia = new LMembresia(_context);
        }
        public List<InputModelRegister> getTClientes(String valor, int id,String iduser)
        {
            var userGimnasio = _context.TUsers.Where(u => u.UsuarioId.Equals(iduser)).ToList();

            List<TbCliente> listTbClientes;
            List<TbExpediente> listTbExpedientes;
            List<SelectListItem> _listMembresias;

            var ClientesList = new List<InputModelRegister>();
            int contador = 0;
            if (valor == null && id.Equals(0))
            {
                listTbClientes = _context.TbCliente.Where( u => u.GimnasioId.Equals(userGimnasio[0].GimnasioId) && u.Estado.Equals(true) ).ToList();
            }
            else
            {
                if (id.Equals(0))
                {
                    listTbClientes = _context.TbCliente.Where(u => (u.Cedula.Contains(valor) || u.Nombre.Contains(valor) ||
                        u.Apellidos.Contains(valor) || u.Direccion.Contains(valor) || u.Correo.Contains(valor)) && u.GimnasioId.Equals(userGimnasio[0].GimnasioId) && u.Estado.Equals(true)  ).ToList();
                }
                else
                {
                    listTbClientes = _context.TbCliente.Where(u => u.IdCliente.Equals(id) && u.GimnasioId.Equals(userGimnasio[0].GimnasioId) &&  u.Estado.Equals(true)).ToList();
                }
            }
            if (!listTbClientes.Count.Equals(0))
            {
                foreach (var item in listTbClientes)
                {
                    listTbExpedientes = _context.TbExpediente.Where(u => u.ClienteId.Equals(item.IdCliente)).ToList();
                    _listMembresias = _membresia.getMembresia(listTbExpedientes[0].MembresiaId);

                    var personal = _context.TUsers.Where(u => u.IdUsers.Equals(item.EmpleadoId)).ToList();
                    int edad = DateTime.Today.AddTicks(-item.FechaNacimiento.Ticks).Year - 1;
                    ClientesList.Add(new InputModelRegister
                    {
                        IdCliente = item.IdCliente,
                        GimnasioId = item.GimnasioId,
                        EmpleadoId = item.EmpleadoId,
                        EmpleadoNombre = personal[0].Nombre + " " + personal[0].Apellidos,
                        EmpleadoCodigo = personal[0].Codigo,
                        Cedula = item.Cedula,
                        Nombre = item.Nombre,
                        Apellidos = item.Apellidos,
                        Telefono = item.Telefono,
                        FechaNacimiento = item.FechaNacimiento,
                        Direccion = item.Direccion,
                        Correo = item.Correo,
                        Imagen = item.Imagen,
                        IdExpediente = listTbExpedientes[0].IdExpediente,
                        Membresia = _listMembresias[0].Text,
                        Fuma = listTbExpedientes[0].Fuma,
                        Alcohol = listTbExpedientes[0].Alcohol,
                        DM = listTbExpedientes[0].DM,
                        HTA = listTbExpedientes[0].HTA,
                        Fx = listTbExpedientes[0].Fx,
                        Asma = listTbExpedientes[0].Asma,
                        Obesidad = listTbExpedientes[0].Obesidad,
                        Edad = edad.ToString()
                    });
                    contador++;
                }
            }
           
            return ClientesList;
        }

        public List<TbCliente> getClientUser(int idEmpleado)
        {
            var listTClients = new List<TbCliente>();
            listTClients = _context.TbCliente.Where(u => u.EmpleadoId == idEmpleado).ToList();

            return listTClients;
        }

    }
}

