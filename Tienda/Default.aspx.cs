﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDominio;
using CapaNegocio;
using System.Data;

namespace Tienda
{
    public partial class _Default : Page
    {
        ProductoNego productoNego = new ProductoNego();
        PedidoNego pedidoNego = new PedidoNego();
        DetallePedidoNego detallePedidoNego = new DetallePedidoNego();
        DetallePedidoTemporalNego detallePedidoTemporalNego = new DetallePedidoTemporalNego();
        static Pedido carritoActual = new Pedido();

        static IList<DetallePedidoTemporal> listaTemporal = new List<DetallePedidoTemporal>();

        static int idProductoActual;
        static decimal? precioActual;

        static decimal? sumaTotalCarrito = 0;
        static int CantidadProductosCarrito = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CalcularSumaTotalCarrito();

                LlenarListaProductos();

                MostrarCarrito();
            }
        }
        public void CalcularSumaTotalCarrito()
        {
            listaTemporal = detallePedidoTemporalNego.MostrarDetallePedidosTemporal().ToList();

            sumaTotalCarrito = 0;
            CantidadProductosCarrito = 0;

            foreach (DetallePedidoTemporal dpt in listaTemporal)
            {
                sumaTotalCarrito = sumaTotalCarrito + dpt.Precio * dpt.Cantidad;
                CantidadProductosCarrito = CantidadProductosCarrito + 1;
            }

            txtTotalCarrito.Text = "$ " + Convert.ToString(sumaTotalCarrito);
            txtTotalProductos.Text = Convert.ToString(CantidadProductosCarrito);
        }

        private void LlenarListaProductos()
        {
            ddlProducto.DataSource = productoNego.MostrarProductos().ToList();
            ddlProducto.DataValueField = "codigo";
            ddlProducto.DataBind();
        }

        public void LimpiarCarritoTemporal()
        {
            detallePedidoTemporalNego.BorrarListaDetallePedidoTemporal();
            listaTemporal = detallePedidoTemporalNego.MostrarDetallePedidosTemporal().ToList();
            MostrarCarrito();
        }
        public void MostrarCarrito()
        {
            dgvCarrito.DataSource = listaTemporal;
            dgvCarrito.DataBind();
            CalcularSumaTotalCarrito();
        }

        protected void dgvCarrito_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index1 = Convert.ToInt32(e.RowIndex);

            int index2 = listaTemporal.ElementAt(index1).IdDetallePedidoTemporal;

            DetallePedidoTemporal dpt = detallePedidoTemporalNego.ObtenerDetallePedidoTemporal(index2);

            detallePedidoTemporalNego.BorrarDetallePedidoTemporal(dpt);
            txtTotalCarrito.Text = "$ "+Convert.ToString(sumaTotalCarrito);

            listaTemporal = detallePedidoTemporalNego.MostrarDetallePedidosTemporal().ToList();
            MostrarCarrito();
        }

        protected void btnAgregarAlPedido_Click(object sender, EventArgs e)
        {
            Producto producto = productoNego.ObtenerProducto(ddlProducto.Text);
            idProductoActual = producto.IdProducto;
            precioActual = producto.Precio;

            if (listaTemporal.Count == 0)
            {
                DetallePedidoTemporal detallePedidoTemporal = new DetallePedidoTemporal();

                detallePedidoTemporal.Cantidad = Convert.ToInt32(txtCantidad.Text);
                detallePedidoTemporal.IdProducto = producto.IdProducto;
                detallePedidoTemporal.Precio = producto.Precio;

                detallePedidoTemporalNego.GuardarDetallePedidoTemporal(detallePedidoTemporal);

                listaTemporal = detallePedidoTemporalNego.MostrarDetallePedidosTemporal().ToList();

                MostrarCarrito();

                CalcularSumaTotalCarrito();

                txtCantidad.Text = "1";
            }
            else
            {
                DetallePedidoTemporal filtroDpt = detallePedidoTemporalNego.FiltrarDetallePedidoTemporalSegunProducto(idProductoActual);

                if (filtroDpt == null)
                {
                    DetallePedidoTemporal detallePedidoTemporal = new DetallePedidoTemporal();

                    detallePedidoTemporal.Cantidad = Convert.ToInt32(txtCantidad.Text);
                    detallePedidoTemporal.IdProducto = producto.IdProducto;
                    detallePedidoTemporal.Precio = producto.Precio;

                    detallePedidoTemporalNego.GuardarDetallePedidoTemporal(detallePedidoTemporal);
                }
                else
                {
                    DetallePedidoTemporal detallePedidoTemporal = new DetallePedidoTemporal();

                    detallePedidoTemporal.IdDetallePedidoTemporal = filtroDpt.IdDetallePedidoTemporal;
                    detallePedidoTemporal.IdProducto = filtroDpt.IdProducto;
                    detallePedidoTemporal.Cantidad = filtroDpt.Cantidad + Convert.ToInt32(txtCantidad.Text);
                    detallePedidoTemporal.Precio = filtroDpt.Precio;

                    detallePedidoTemporalNego.ActualizarDetallePedidoTemporal(detallePedidoTemporal);
                }

                listaTemporal = detallePedidoTemporalNego.MostrarDetallePedidosTemporal().ToList();

                MostrarCarrito();

                CalcularSumaTotalCarrito();

                txtCantidad.Text = "1";
            }
        }
    }
}
