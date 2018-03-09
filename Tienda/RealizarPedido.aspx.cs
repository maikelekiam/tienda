﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDominio;
using CapaNegocio;

namespace Tienda
{
    public partial class RealizarPedido : System.Web.UI.Page
    {
        ProductoNego productoNego = new ProductoNego();
        PedidoNego pedidoNego = new PedidoNego();
        DetallePedidoNego detallePedidoNego = new DetallePedidoNego();
        DetallePedidoTemporalNego detallePedidoTemporalNego = new DetallePedidoTemporalNego();

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
            dgvCarrito.DataSource = detallePedidoTemporalNego.MostrarDetallePedidosTemporal().Where(c => c.IdUsuario == idUsuarioActual).ToList();
            dgvCarrito.DataBind();

            CalcularSumaTotalCarrito();
        }

        protected void btnConfirmarCompra_Click(object sender, EventArgs e)
        {
            if (txtNumeroPedido.Text != "")
            {
                listaTemporal = detallePedidoTemporalNego.MostrarDetallePedidosTemporal().Where(c => c.IdUsuario == idUsuarioActual).ToList();

                if (listaTemporal.Count == 0)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Correct", "alert('El Carro no debe estar vacio.')", true);
                }
                else
                {
                    GuardarPedido();
                    LimpiarPedido();
                    detallePedidoTemporalNego.BorrarListaDetallePedidoTemporal(idUsuarioActual);
                    Response.Redirect("Default.aspx");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Correct", "alert('Ingrese Codigo de Pedido.')", true);
            }
        }

        public void GuardarPedido()
        {
            Pedido pedido = new Pedido();

            listaTemporal = detallePedidoTemporalNego.MostrarDetallePedidosTemporal().Where(c => c.IdUsuario == idUsuarioActual).ToList();

            pedido.IdUsuario = idUsuarioActual;
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
            listaTemporal = detallePedidoTemporalNego.MostrarDetallePedidosTemporal().Where(c => c.IdUsuario == idUsuarioActual).ToList();

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
            
            dgvPedidosRealizados.DataSource = pedidoNego.MostrarPedidos().Where(c => c.IdUsuario == idUsuarioActual).ToList();
            dgvPedidosRealizados.DataBind();

            dgvPedidosRealizados.Columns[0].Visible = false;
        }

        protected void dgvPedidosRealizados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            idPedidoSeleccionado = Convert.ToInt32(dgvPedidosRealizados.Rows[e.RowIndex].Cells[0].Text);

            Response.Redirect("MostrarPedido.aspx");
        }
    }
}