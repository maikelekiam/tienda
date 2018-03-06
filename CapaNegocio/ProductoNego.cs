﻿using System;
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

    }
}
