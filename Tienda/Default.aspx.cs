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
        UsuarioNego usuarioNego = new UsuarioNego();
        PresupuestoNego presupuestoNego = new PresupuestoNego();
        static Pedido carritoActual = new Pedido();

        static IList<DetallePedidoTemporal> listaTemporal = new List<DetallePedidoTemporal>();
        static IList<PresupuestoTemporal> listaMargen = new List<PresupuestoTemporal>();
        public List<int> listaPorcentajes = new List<int> { 0, 10, 20, 30, 40, 50 };

        static int idProductoActual;
        static decimal? precioActual;
        static int idUsuarioActual;

        static decimal? sumaTotalCarrito = 0;
        static int CantidadProductosCarrito = 0;

        int margen;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LlenarListaPorcentajes();
                LlenarListaProductos();
                idUsuarioActual = Convert.ToInt32(Session["userid"]);
                margen = Convert.ToInt32(Session["margenid"]);
                ddlMargen.Text = Convert.ToString(margen);

                MostrarCarrito();
                //CalcularSumaTotalCarrito();
                //CalcularCarritoMargen();
            }
        }




        public void CalcularSumaTotalCarrito()
        {
            //listaTemporal = detallePedidoTemporalNego.MostrarDetallePedidosTemporal().Where(c => c.IdUsuario == (Convert.ToInt32(Session["userid"]))).ToList();
            listaMargen = presupuestoNego.MostrarPresupuestosTemporal().Where(c => c.IdUsuario == (Convert.ToInt32(Session["userid"]))).ToList();

            sumaTotalCarrito = 0;
            CantidadProductosCarrito = 0;

            foreach (PresupuestoTemporal x in listaMargen)
            {
                sumaTotalCarrito = sumaTotalCarrito + x.Precio * x.Cantidad;
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
            listaMargen = presupuestoNego.MostrarPresupuestosTemporal().Where(c => c.IdUsuario == (Convert.ToInt32(Session["userid"]))).ToList();

            dgvCarrito.DataSource = listaMargen;
            dgvCarrito.DataBind();

            CalcularSumaTotalCarrito();
        }

        protected void dgvCarrito_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int index1 = Convert.ToInt32(e.RowIndex);

            int index2 = listaTemporal.ElementAt(index1).IdDetallePedidoTemporal;

            DetallePedidoTemporal dpt = detallePedidoTemporalNego.ObtenerDetallePedidoTemporal(index2);
            //agrego para la listamargen
            PresupuestoTemporal presu = presupuestoNego.ObtenerPresupuestoTemporal(index2);
            presupuestoNego.BorrarPresupuestoTemporal(presu);
            //fin agrego para listamargen

            detallePedidoTemporalNego.BorrarDetallePedidoTemporal(dpt);

            //txtTotalCarrito.Text = "$ " + Convert.ToString(sumaTotalCarrito);

            //listaTemporal = detallePedidoTemporalNego.MostrarDetallePedidosTemporal().Where(c => c.IdUsuario == (Convert.ToInt32(Session["userid"]))).ToList();
            
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
            }


            //rutina para la listamargen
            precioActual = producto.Precio * (1 + (Convert.ToDecimal(Session["margenid"])) / 100);
            if (listaMargen.Count == 0)
            {
                PresupuestoTemporal presupuestoTemporal = new PresupuestoTemporal();

                presupuestoTemporal.Cantidad = Convert.ToInt32(txtCantidad.Text);
                presupuestoTemporal.IdProducto = producto.IdProducto;
                presupuestoTemporal.Precio = precioActual;
                presupuestoTemporal.IdUsuario = Convert.ToInt32(Session["userid"]);

                presupuestoNego.GuardarPresupuestoTemporal(presupuestoTemporal);

                listaMargen = presupuestoNego.MostrarPresupuestosTemporal().Where(c => c.IdUsuario == (Convert.ToInt32(Session["userid"]))).ToList();
            }
            else
            {
                PresupuestoTemporal filtroDpt = presupuestoNego.FiltrarPresupuestoTemporalSegunProducto(idProductoActual, (Convert.ToInt32(Session["userid"])));

                if (filtroDpt == null)
                {
                    PresupuestoTemporal presupuestoTemporal = new PresupuestoTemporal();

                    presupuestoTemporal.Cantidad = Convert.ToInt32(txtCantidad.Text);
                    presupuestoTemporal.IdProducto = producto.IdProducto;
                    presupuestoTemporal.Precio = precioActual;
                    presupuestoTemporal.IdUsuario = Convert.ToInt32(Session["userid"]);

                    presupuestoNego.GuardarPresupuestoTemporal(presupuestoTemporal);
                }
                else
                {
                    PresupuestoTemporal presupuestoTemporal = new PresupuestoTemporal();

                    presupuestoTemporal.IdPresupuestoTemporal = filtroDpt.IdPresupuestoTemporal;
                    presupuestoTemporal.IdProducto = filtroDpt.IdProducto;
                    presupuestoTemporal.Cantidad = filtroDpt.Cantidad + Convert.ToInt32(txtCantidad.Text);
                    presupuestoTemporal.Precio = precioActual;
                    presupuestoTemporal.IdUsuario = Convert.ToInt32(Session["userid"]);

                    presupuestoNego.ActualizarPresupuestoTemporal(presupuestoTemporal);
                }

                listaMargen = presupuestoNego.MostrarPresupuestosTemporal().Where(c => c.IdUsuario == (Convert.ToInt32(Session["userid"]))).ToList();
            }

            MostrarCarrito();

            CalcularSumaTotalCarrito();

            txtCantidad.Text = "1";
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
        private void LlenarListaPorcentajes()
        {
            ddlMargen.DataSource = listaPorcentajes;
            ddlMargen.DataBind();
        }

        protected void ddlMargen_SelectedIndexChanged(object sender, EventArgs e)
        {
            margen = Convert.ToInt32(ddlMargen.SelectedValue);

            Session["margenid"] = Convert.ToString(margen);

            ActualizarMargenUsuario();

            CalcularCarritoMargen();
        }
        public void ActualizarMargenUsuario()
        {
            margen = Convert.ToInt32(ddlMargen.SelectedValue);

            usuarioNego.ActualizarMargenUsuario(margen, Convert.ToInt32(Session["userid"]));
        }

        public void CalcularCarritoMargen()
        {
            listaMargen = presupuestoNego.MostrarPresupuestosTemporal().Where(c => c.IdUsuario == (Convert.ToInt32(Session["userid"]))).ToList();

            foreach (PresupuestoTemporal presu in listaMargen)
            {
                Producto produ = productoNego.ObtenerProductoSegunIdProducto(Convert.ToInt32(presu.IdProducto));

                PresupuestoTemporal presupuestoTemporal = new PresupuestoTemporal();

                presupuestoTemporal.IdPresupuestoTemporal = presu.IdPresupuestoTemporal;
                presupuestoTemporal.IdProducto = produ.IdProducto;
                presupuestoTemporal.Cantidad = presu.Cantidad;
                presupuestoTemporal.Precio = produ.Precio * (1 + (Convert.ToDecimal(Session["margenid"])) / 100);
                presupuestoTemporal.IdUsuario = Convert.ToInt32(Session["userid"]);

                presupuestoNego.ActualizarPresupuestoTemporal(presupuestoTemporal);
            }

            MostrarCarrito();
        }
    }
}
