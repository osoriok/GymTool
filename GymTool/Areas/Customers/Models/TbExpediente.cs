using System;
using System.ComponentModel.DataAnnotations;

namespace GymTool.Areas.Customers.Models
{
    public class TbExpediente
    {

        [Key]
        public int IdExpediente { set; get; }
        public int ClienteId { set; get; }
        public int MembresiaId { set; get; }
        public DateTime FechaInscripcion { set; get; }
        public bool Alcohol { set; get; }
        public bool Fuma { set; get; }
        public bool DM { set; get; }
        public bool HTA { set; get; }
        public bool Fx { set; get; }
        public bool Asma { set; get; }
        public bool Obesidad { set; get; }
        public int TbClienteIdCliente { set; get; }
        public TbCliente TbCliente { get; set; }

    }
}
