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
    public partial class Login : System.Web.UI.Page
    {
        UsuarioNego usuarioNego = new UsuarioNego();
        DetallePedidoTemporalNego detallePedidoTemporalNego = new DetallePedidoTemporalNego();

        protected void Page_Load(object sender, EventArgs e)
        {
            //detallePedidoTemporalNego.BorrarListaDetallePedidoTemporal();
        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            Usuario usuario = ValidateUserDetail(txtusername.Text, txtpassword.Text);

            if (usuario != null)
            {
                Session["userlogin"] = txtusername.Text;

                Session["userid"] = Convert.ToString(usuario.IdUsuario);

                Session["margenid"] = Convert.ToString(usuario.Margen);

                Session["usermail"] = Convert.ToString(usuario.Mail);

                Session["usergrupo"] = Convert.ToString(usuario.Grupo);

                Session["usercuit"] = Convert.ToString(usuario.Cuit);

                Response.Redirect("Default.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Correct", "alert('Usuario/Contraseña incorrecta.')", true);
            }
        }

        public Usuario ValidateUserDetail(string username, string password)
        {
            return usuarioNego.ObtenerUsuario(username, password);
        }
    }
}