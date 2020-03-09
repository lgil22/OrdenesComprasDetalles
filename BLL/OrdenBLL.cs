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
    public class OrdenBLL
    {
        public static bool Guardar(Orden orden)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Orden.Add(orden) != null)
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

        public static bool Modificar(Orden orden)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                db.Database.ExecuteSqlRaw($"Delete FROM OrdenesDetalles where OrdenId = {orden.OrdenId}");
                foreach (var item in orden.Detalles)
                {
                    db.Entry(item).State = EntityState.Added;
                }
                db.Entry(orden).State = EntityState.Modified;
                paso = (db.SaveChanges() > 0);
               
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
                var eliminar = db.Orden.Find(id);
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

        public static Orden Buscar(int id)
        {
            Orden orden = new Orden();
            Contexto db = new Contexto();

            try
            {
                orden = db.Orden.Where(x => x.OrdenId == id).
                     Include(y => y.Detalles).SingleOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.Dispose();
            }
            return orden;
        }

        public static List<Orden> GetList(Expression<Func<Orden, bool>> orden)
        {
            List<Orden> lista = new List<Orden>();
            Contexto db = new Contexto();
            try
            {
                lista = db.Orden.Where(orden).ToList();
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
