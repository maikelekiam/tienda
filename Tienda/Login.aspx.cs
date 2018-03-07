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

        //public static int? grupo;
        //public static string nombreUsuario;
        //public static int? idUsuario;

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

                
                //grupo = usuario.Grupo;
                //nombreUsuario = usuario.Nombre;
                //idUsuario = usuario.IdUsuario;

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