using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDominio;

namespace CapaRepositorio
{
    public class ProductoRepo
    {
        public IEnumerable<Producto> MostrarProductos()
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                IEnumerable<Producto> result = modeloDeDominio.Productos.ToList();
                return result;
            }
        }

        public Producto ObtenerProducto(string cod)
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                Producto result = modeloDeDominio.Productos.Where(c => c.Codigo == cod).FirstOrDefault();

                return result;
            }
        }
        public Producto ObtenerProductoSegunIdProducto(int id)
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                Producto result = modeloDeDominio.Productos.Where(c => c.IdProducto == id).FirstOrDefault();

                return result;
            }
        }
        public void ActualizarProducto(Producto producto)
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                modeloDeDominio.AttachCopy(producto);
                modeloDeDominio.SaveChanges();
            }
        }
        public void EliminarListaProductos()
        {
            using (ModeloDeDominio modeloDeDominio = new ModeloDeDominio())
            {
                IEnumerable<Producto> result = modeloDeDominio.Productos.ToList();

                foreach (Producto prod in result)
                {
                    modeloDeDominio.Delete(prod);
                    modeloDeDominio.SaveChanges();
                }
            }
        }
    }
}
