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
    public partial class ListarPedidos : System.Web.UI.Page
    {
        PedidoNego pedidoNego = new PedidoNego();
        public static int idPedidoSeleccionado;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                MostrarPedidosRealizados();
            }
        }

        public void MostrarPedidosRealizados()
        {
            dgvPedidosRealizados.Columns[0].Visible = true;

            dgvPedidosRealizados.DataSource = pedidoNego.MostrarPedidos().Where(c => c.IdUsuario == Login.idUsuario).ToList();
            dgvPedidosRealizados.DataBind();

            dgvPedidosRealizados.Columns[0].Visible = false;
        }

        protected void dgvPedidosRealizados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            idPedidoSeleccionado = Convert.ToInt32(dgvPedidosRealizados.Rows[e.RowIndex].Cells[0].Text);

            RealizarPedido.idPedidoSeleccionado = idPedidoSeleccionado;

            Response.Redirect("MostrarPedido.aspx");
        }
    }
}