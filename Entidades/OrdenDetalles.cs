using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace OrdenesCompras.Entidades
{
    public class OrdenDetalles
    {
        [Key]
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public int OrdenId { get; set; }
        public int Cantidad { get; set; }

        public OrdenDetalles()
        {
            Id = 0;
            ProductoId = 0;
            OrdenId = 0;
            Cantidad = 0;
        }
    }
}
