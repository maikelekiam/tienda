using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDominio;
using CapaNegocio;

namespace Tienda
{
    public partial class MostrarPedido : System.Web.UI.Page
    {
        PedidoNego pedidoNego = new PedidoNego();
        DetallePedidoNego detallePedidoNego = new DetallePedidoNego();
        static decimal? sumaTotalCarrito = 0;

        static IList<DetallePedido> listaProductos = new List<DetallePedido>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            MostrarPedidoSeleccionado();

            logo.Visible = false;
        }

        public void MostrarPedidoSeleccionado()
        {
            Pedido pedido = pedidoNego.ObtenerPedido(RealizarPedido.idPedidoSeleccionado);

            lblNumeroPedido.Text = pedido.NumeroPedido;

            lblFechaPedido.Text = Convert.ToString(pedido.PedidoDia) + " de " + Convert.ToString(pedido.PedidoMes) + " de " + Convert.ToString(pedido.PedidoAnio);

            listaProductos = detallePedidoNego.MostrarDetallePedidos().Where(c => c.IdPedido == pedido.IdPedido).ToList();

            CalcularSumaTotalCarrito();

            dgvCarrito.DataSource = listaProductos;
            dgvCarrito.DataBind();
        }
        public void CalcularSumaTotalCarrito()
        {
            sumaTotalCarrito = 0;

            foreach (DetallePedido dpt in listaProductos)
            {
                sumaTotalCarrito = sumaTotalCarrito + dpt.Precio * dpt.Cantidad;
            }

            lblTotalCarrito.Text = "$ " + Convert.ToString(sumaTotalCarrito);
        }
    }
}