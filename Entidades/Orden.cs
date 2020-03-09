using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrdenesCompras.Entidades
{
    public class Orden
    {
        [Key]
        public int OrdenId { get; set; }

     //   [ForeignKey("Cliente")]
        public int ClienteId { get; set; }

      //  [ForeignKey("Producto")]
        public DateTime OrdenFecha { get; set; }
        public string  Sede { get; set; }
        public decimal Total { get; set; }

      //  [ForeignKey("OrdenId")]

        public virtual List<OrdenDetalles> Detalles { get; set; } = new List<OrdenDetalles>();


        public Orden()
        {
            OrdenId = 0;
            ClienteId = 0;
            OrdenFecha = DateTime.Now;
            Sede = string.Empty;
            Total = 0;
            Detalles = new List<OrdenDetalles>();
        }
    }
}
