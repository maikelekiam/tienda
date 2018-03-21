using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDominio;
using CapaRepositorio;

namespace CapaNegocio
{
    public class ProductoNego
    {
        ProductoRepo productoRepo = new ProductoRepo();

        public IEnumerable<Producto> MostrarProductos()
        {
            return productoRepo.MostrarProductos();
        }

        public Producto ObtenerProducto(string cod)
        {
            return productoRepo.ObtenerProducto(cod);
        }
        public Producto ObtenerProductoSegunIdProducto(string cod)
        {
            return productoRepo.ObtenerProductoSegunIdProducto(cod);
        }
        public void ActualizarProducto(Producto producto)
        {
            productoRepo.ActualizarProducto(producto);
        }
        public void EliminarListaProductos()
        {
            productoRepo.EliminarListaProductos();
        }
        public void GuardarProducto(Producto producto)
        {
            productoRepo.GuardarProducto(producto);
        }
    }
}
