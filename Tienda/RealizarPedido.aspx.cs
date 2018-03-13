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
    public partial class RealizarPedido : System.Web.UI.Page
    {
        ProductoNego productoNego = new ProductoNego();
        PedidoNego pedidoNego = new PedidoNego();
        DetallePedidoNego detallePedidoNego = new DetallePedidoNego();
        DetallePedidoTemporalNego detallePedidoTemporalNego = new DetallePedidoTemporalNego();
        PresupuestoNego presupuestoNego = new PresupuestoNego();

        static IList<DetallePedidoTemporal> listaTemporal = new List<DetallePedidoTemporal>();
        static decimal? sumaTotalCarrito = 0;

        public static List<int> listaDia = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 };
        public static List<string> listaMes = new List<string> { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Setiembre", "Octubre", "Noviembre", "Diciembre" };
        public static List<int> listaAnio = new List<int>();

        public static int idPedidoSeleccionado;

        static int idUsuarioActual;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int anioHoy = DateTime.Now.Year;
                for (int i = 0; i < 100; i++) { listaAnio.Add(anioHoy - i); }

                idUsuarioActual = Convert.ToInt32(Session["userid"]);

                LlenarFechas();

                MostrarCarrito();

                CalcularSumaTotalCarrito();

                txtNombreUsuario.Text = Session["userlogin"].ToString();

                MostrarPedidosRealizados();
            }
        }


        private void LlenarFechas()
        {
            ddlDia.DataSource = listaDia;
            ddlDia.DataBind();
            ddlMes.DataSource = listaMes;
            ddlMes.DataBind();

            ddlAnio.DataSource = listaAnio;
            ddlAnio.DataBind();

            ddlDia.Text = Convert.ToString(DateTime.Today.Day);
            ddlMes.Text = listaMes.ElementAt(Convert.ToInt32(DateTime.Today.Month) - 1);
            ddlAnio.Text = Convert.ToString(DateTime.Today.Year);
        }

        public void MostrarCarrito()
        {
            dgvCarrito.DataSource = detallePedidoTemporalNego.MostrarDetallePedidosTemporal().Where(c => c.IdUsuario == Convert.ToInt32(Session["userid"])).ToList();
            dgvCarrito.DataBind();

            CalcularSumaTotalCarrito();
        }

        protected void btnConfirmarCompra_Click(object sender, EventArgs e)
        {
            if (txtNumeroPedido.Text != "")
            {
                listaTemporal = detallePedidoTemporalNego.MostrarDetallePedidosTemporal().Where(c => c.IdUsuario == Convert.ToInt32(Session["userid"])).ToList();

                if (listaTemporal.Count == 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Correct", "alert('El Carro no debe estar vacio.')", true);
                }
                else
                {
                    GuardarPedido();

                    EnviarCorreo();

                    LimpiarPedido();

                    VaciarCarrito();

                    //detallePedidoTemporalNego.BorrarListaDetallePedidoTemporal(idUsuarioActual);
                    Response.Redirect("Default.aspx");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Correct", "alert('Debe Ingresar un Codigo de Pedido.')", true);
            }
        }

        public void GuardarPedido()
        {
            Pedido pedido = new Pedido();

            listaTemporal = detallePedidoTemporalNego.MostrarDetallePedidosTemporal().Where(c => c.IdUsuario == Convert.ToInt32(Session["userid"])).ToList();

            pedido.IdUsuario = Convert.ToInt32(Session["userid"]);
            pedido.NumeroPedido = txtNumeroPedido.Text;
            pedido.PedidoDia = Convert.ToInt32(ddlDia.Text);
            pedido.PedidoMes = ddlMes.Text;
            pedido.PedidoAnio = Convert.ToInt32(ddlAnio.Text);

            int idPedidoTemporal = pedidoNego.GuardarPedido(pedido);

            foreach (DetallePedidoTemporal dpt in listaTemporal)
            {
                DetallePedido detallePedido = new DetallePedido();

                detallePedido.IdPedido = idPedidoTemporal;
                detallePedido.IdProducto = dpt.IdProducto;
                detallePedido.Cantidad = dpt.Cantidad;
                detallePedido.Precio = dpt.Precio;

                detallePedidoNego.GuardarDetallePedido(detallePedido);
            }
        }

        public void LimpiarPedido()
        {
            txtNombreUsuario.Text = "";
            txtNumeroPedido.Text = "";
            ddlDia.Text = Convert.ToString(DateTime.Today.Day);
            ddlMes.Text = listaMes.ElementAt(Convert.ToInt32(DateTime.Today.Month) - 1);
            ddlAnio.Text = Convert.ToString(DateTime.Today.Year);
        }
        public void CalcularSumaTotalCarrito()
        {
            listaTemporal = detallePedidoTemporalNego.MostrarDetallePedidosTemporal().Where(c => c.IdUsuario == Convert.ToInt32(Session["userid"])).ToList();

            sumaTotalCarrito = 0;

            foreach (DetallePedidoTemporal dpt in listaTemporal)
            {
                sumaTotalCarrito = sumaTotalCarrito + dpt.Precio * dpt.Cantidad;
            }

            txtTotalCarrito.Text = Convert.ToString(sumaTotalCarrito);
        }
        public void MostrarPedidosRealizados()
        {
            dgvPedidosRealizados.Columns[0].Visible = true;

            dgvPedidosRealizados.DataSource = pedidoNego.MostrarPedidos().Where(c => c.IdUsuario == Convert.ToInt32(Session["userid"])).ToList();
            dgvPedidosRealizados.DataBind();

            dgvPedidosRealizados.Columns[0].Visible = false;
        }

        protected void dgvPedidosRealizados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            idPedidoSeleccionado = Convert.ToInt32(dgvPedidosRealizados.Rows[e.RowIndex].Cells[0].Text);

            Response.Redirect("MostrarPedido.aspx");
        }
        public void EnviarCorreo()
        {
            //string from = txtfrom.Text;
            //string pass = txtpassword.Text;
            //string to = txtto.Text;
            //string msn = txtmensaje.Text;

            //new Email().enviarCorreo(from, pass, to, msn);


            //msn = string.Format("Nombre: {1}" + "\n" + "Correo: {2}" + "\n" + "Mensaje:{3}" + "\n", "\n", "juan", "juan@hotmail.com", "mensaje");

            //msn = string.Format("<HTML><h1>{1}</h1></HTML>", "Miguel", txtTotalCarrito.Text);

            //new Email().enviarCorreo("victor.alejandro.arribas@gmail.com", "vaaa2018", "miarcamone@gmail.com", msn);

            string msn = "<HTML><p1><h3>Estimado Sr. " + Session["userlogin"].ToString() + "</h3></p1>";
            
            msn += "Nos comunicamos con Ud. para informarle que su pedido se ha realizado con exito.";
            msn += "<br /><br />";
            msn += "A continuacion detallamos el mismo: ";
            msn += "<br /><br />";
            msn += "<table border CELLPADDING=8 CELLSPACING=0><TR><TH>Codigo</TH><TH>Producto</TH><TH>Cantidad</TH><TH>Precio</TH></TR>";

            foreach (DetallePedidoTemporal dpt in listaTemporal)
            {
                string nombre = productoNego.ObtenerProductoSegunIdProducto(Convert.ToInt32(dpt.IdProducto)).Nombre;
                string codigo = productoNego.ObtenerProductoSegunIdProducto(Convert.ToInt32(dpt.IdProducto)).Codigo;
                msn += "<tr><TD ALIGN=center>" + codigo + "</Td><TD>" + nombre + "</Td><TD ALIGN=right>" + dpt.Cantidad + "</Td><TD ALIGN=right>" + "$ " + dpt.Precio + "</Td></tr>";
            }
            msn += "</TABLE>";

            //msn += "<br />";
            msn += "<p1><h3>CODIGO DEL PEDIDO: " + txtNumeroPedido.Text + "</h3></p1>";

            //msn += "<br />";
            msn += "<p1><h3>FECHA: " + ddlDia.Text + " de " + ddlMes.Text + " de " + ddlAnio.Text + "</h3></p1>";

            //msn += "<br />";
            msn += "<p1><h3>TOTAL A PAGAR: $" + Convert.ToString(sumaTotalCarrito) + "</h3></p1>";

            msn += "<br />";
            msn += "Saludamos a Ud. muy Atte.";

            msn += "<br /><br />";
            msn += "LA EMPRESA</HTML>";

            new Email().enviarCorreo("victor.alejandro.arribas@gmail.com", "vaaa2018", Session["usermail"].ToString(), msn);
        }
        public void VaciarCarrito()
        {
            int id=Convert.ToInt32(Session["userid"].ToString());

            detallePedidoTemporalNego.BorrarListaDetallePedidoTemporal(id);
            presupuestoNego.BorrarListaPresupuestoTemporal(id);
        }
    }
}