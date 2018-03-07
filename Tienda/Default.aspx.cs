using System;
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
        static IList<DetallePedidoTemporal> listaTemporalMargen = new List<DetallePedidoTemporal>();
        public List<int> listaPorcentajes = new List<int> { 0, 10, 20, 30, 40, 50 };

        static int idProductoActual;
        static decimal? precioActual;
        static int idUsuarioActual;

        static decimal? sumaTotalCarrito = 0;
        static int CantidadProductosCarrito = 0;

        int margen = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                idUsuarioActual = Convert.ToInt32(Session["userid"]);

                CalcularSumaTotalCarrito();

                LlenarListaProductos();
                LlenarListaPoecentajes();

                MostrarCarrito();
            }
        }
        public void CalcularSumaTotalCarrito()
        {
            listaTemporal = detallePedidoTemporalNego.MostrarDetallePedidosTemporal().Where(c => c.IdUsuario == (Convert.ToInt32(Session["userid"]))).ToList();

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
            detallePedidoTemporalNego.BorrarListaDetallePedidoTemporal(Convert.ToInt32(Session["userid"]));
            listaTemporal = detallePedidoTemporalNego.MostrarDetallePedidosTemporal().Where(c => c.IdUsuario == (Convert.ToInt32(Session["userid"]))).ToList();
            MostrarCarrito();
        }
        public void MostrarCarrito()
        {
            dgvCarrito.DataSource = detallePedidoTemporalNego.MostrarDetallePedidosTemporal().Where(c => c.IdUsuario == (Convert.ToInt32(Session["userid"]))).ToList(); ;
            dgvCarrito.DataBind();

            CalcularSumaTotalCarrito();
        }

        protected void dgvCarrito_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index1 = Convert.ToInt32(e.RowIndex);

            int index2 = listaTemporal.ElementAt(index1).IdDetallePedidoTemporal;

            DetallePedidoTemporal dpt = detallePedidoTemporalNego.ObtenerDetallePedidoTemporal(index2);

            detallePedidoTemporalNego.BorrarDetallePedidoTemporal(dpt);
            txtTotalCarrito.Text = "$ " + Convert.ToString(sumaTotalCarrito);

            listaTemporal = detallePedidoTemporalNego.MostrarDetallePedidosTemporal().Where(c => c.IdUsuario == (Convert.ToInt32(Session["userid"]))).ToList();

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
                detallePedidoTemporal.IdUsuario = Convert.ToInt32(Session["userid"]);

                detallePedidoTemporalNego.GuardarDetallePedidoTemporal(detallePedidoTemporal);

                listaTemporal = detallePedidoTemporalNego.MostrarDetallePedidosTemporal().Where(c => c.IdUsuario == (Convert.ToInt32(Session["userid"]))).ToList();

                MostrarCarrito();

                CalcularSumaTotalCarrito();

                txtCantidad.Text = "1";
            }
            else
            {
                DetallePedidoTemporal filtroDpt = detallePedidoTemporalNego.FiltrarDetallePedidoTemporalSegunProducto(idProductoActual, (Convert.ToInt32(Session["userid"])));

                if (filtroDpt == null)
                {
                    DetallePedidoTemporal detallePedidoTemporal = new DetallePedidoTemporal();

                    detallePedidoTemporal.Cantidad = Convert.ToInt32(txtCantidad.Text);
                    detallePedidoTemporal.IdProducto = producto.IdProducto;
                    detallePedidoTemporal.Precio = producto.Precio;
                    detallePedidoTemporal.IdUsuario = Convert.ToInt32(Session["userid"]);

                    detallePedidoTemporalNego.GuardarDetallePedidoTemporal(detallePedidoTemporal);
                }
                else
                {
                    DetallePedidoTemporal detallePedidoTemporal = new DetallePedidoTemporal();

                    detallePedidoTemporal.IdDetallePedidoTemporal = filtroDpt.IdDetallePedidoTemporal;
                    detallePedidoTemporal.IdProducto = filtroDpt.IdProducto;
                    detallePedidoTemporal.Cantidad = filtroDpt.Cantidad + Convert.ToInt32(txtCantidad.Text);
                    detallePedidoTemporal.Precio = filtroDpt.Precio;
                    detallePedidoTemporal.IdUsuario = Convert.ToInt32(Session["userid"]);

                    detallePedidoTemporalNego.ActualizarDetallePedidoTemporal(detallePedidoTemporal);
                }

                listaTemporal = detallePedidoTemporalNego.MostrarDetallePedidosTemporal().Where(c => c.IdUsuario == (Convert.ToInt32(Session["userid"]))).ToList();

                MostrarCarrito();

                CalcularSumaTotalCarrito();

                txtCantidad.Text = "1";
            }
        }

        protected void btnMasUno_Click(object sender, EventArgs e)
        {
            txtCantidad.Text = Convert.ToString(Convert.ToInt32(txtCantidad.Text) + 1);
        }

        protected void btnMenosUno_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtCantidad.Text) > 1)
            {
                txtCantidad.Text = Convert.ToString(Convert.ToInt32(txtCantidad.Text) - 1);
            }
        }
        private void LlenarListaPoecentajes()
        {
            ddlMargen.DataSource = listaPorcentajes;
            ddlMargen.DataBind();
        }

        protected void btnMargenAplicar_Click(object sender, EventArgs e)
        {
            margen = Convert.ToInt32(ddlMargen.SelectedValue);




        }
    }
}
