using GymTool.Areas.Customers.Models;
using GymTool.Areas.Memberships.Models;
using GymTool.Areas.Principal.Models;
using GymTool.Areas.Users.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymTool.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        static DbContextOptions<ApplicationDbContext> _options;

        public ApplicationDbContext() : base(_options)
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TUsers> TUsers { get; set; }//tabla personal
        public DbSet<TbGimnasio> TbGimnasio { get; set; }//tabla gimnasio
        public DbSet<TbUsuarioGimnasio> TbUsuarioGimnasio { get; set; } //tabla usuario gimnasio
        public DbSet<TbMembresia> TbMembresia { get; set; }//tabla membresia
        public DbSet<TbCliente> TbCliente { get; set; }//tabla cliente
        public DbSet<TbExpediente> TbExpediente { get; set; }//tabla expediente de cliente
        public DbSet<TbAsistencia> TbAsistencia { get; set; }//tabla asistencia de cliente


    }
}
