using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OrdenesCompras.DAL;
using OrdenesCompras.Entidades;
using System.Linq;
using System.Linq.Expressions;

namespace OrdenesCompras.BLL
{
    public class ClienteBLL
    {
        public static bool Guardar(Cliente cliente)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Cliente.Add(cliente) != null)
                    paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Modificar(Cliente cliente)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                db.Entry(cliente).State = EntityState.Modified;
                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                var eliminar = db.Cliente.Find(id);
                db.Entry(eliminar).State = EntityState.Deleted;
                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static Cliente Buscar(int id)
        {
            Cliente cliente = new Cliente();
            Contexto db = new Contexto();

            try
            {
                cliente = db.Cliente.Find(id);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.Dispose();
            }
            return cliente;
        }

        public static List<Cliente> GetList(Expression<Func<Cliente, bool>> cliente)
        {
            List<Cliente> lista = new List<Cliente>();
            Contexto db = new Contexto();
            try
            {
                lista = db.Cliente.Where(cliente).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.Dispose();
            }

            return lista;
        }
    }
}
