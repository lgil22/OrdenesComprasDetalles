using System;

using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace OrdenesCompras.Entidades
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }
        public string Nombres { get; set; }
        public string Cedula { get; set; }
        public string Telefono { get; set; }
  
        public DateTime FechaNacimiento { get; set; }

        public Cliente()
        {
            ClienteId = 0;
            Nombres = string.Empty;
            Cedula = string.Empty;
            Telefono = string.Empty;
            FechaNacimiento = DateTime.Now;
        }
    }
}