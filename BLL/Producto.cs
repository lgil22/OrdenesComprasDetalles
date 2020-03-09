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
    public class ProductosBLL
    {
        public static bool Guardar(Producto productos)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.Producto.Add(productos) != null)
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

        public static bool Modificar(Producto productos)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                db.Entry(productos).State = EntityState.Modified;
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
                var eliminar = db.Producto.Find(id);
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

        public static Producto Buscar(int id)
        {
            Producto producto = new Producto();
            Contexto db = new Contexto();

            try
            {
                producto = db.Producto.Find(id);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                db.Dispose();
            }
            return producto;
        }

        public static bool CalcInventario(int id, int cantidad)
        {
            bool paso = false;
            Contexto db = new Contexto();
            Producto producto = new Producto();
            producto = db.Producto.Find(id);

            if (producto != null)
            {
                try
                {
                    if (producto.Inventario >= 0)
                        producto.Inventario = (producto.Inventario - cantidad);

                    db.Entry(producto).State = EntityState.Modified;
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
            }

            return paso;


        }
        public static List<Producto> GetList(Expression<Func<Producto, bool>> producto)
        {
            List<Producto> lista = new List<Producto>();
            Contexto db = new Contexto();
            try
            {
                lista = db.Producto.Where(producto).ToList();
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