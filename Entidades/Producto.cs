using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OrdenesCompras.Entidades
{
    public class Producto
    {

        [Key]
        public int ProductoId { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public decimal Inventario { get; set; }

        public Producto()
        {
            ProductoId = 0;
            Descripcion = string.Empty;
            // Telefono = string.Empty;
            Precio = 0;
            Inventario = 0;

    }

    }
}