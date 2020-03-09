using Microsoft.EntityFrameworkCore;
//using OrdenesCompras.Entidades;
using Microsoft.Data.Sqlite;
using OrdenesCompras.Entidades;

namespace OrdenesCompras.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Orden> Orden { get; set; }
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            ///optionsBuilder.UseSqlServer(@"Server = .\SqlExpress; Database = PersonasDb; Trusted_Connection = True; ");
            optionsBuilder.UseSqlite(@" Data Source = Compras.db");
        }

    }

}
