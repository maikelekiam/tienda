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
    public partial class AltaUsuario : System.Web.UI.Page
    {
        UsuarioNego usuarioNego = new UsuarioNego();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if ((string)Session["userlogin"].ToString() == "Admin")
                {
                    PanelAltaUsuario.Visible = true;
                }
                else
                {
                    PanelAltaUsuario.Visible = false;
                }

                btnGuardar.Visible = true;
                btnActualizar.Visible = false;

                MostrarListas();

                MostrarGrillaUsuarios();
            }
        }
        public void MostrarListas()
        {
            ddlUsuarios.DataSource = usuarioNego.MostrarUsuarios().ToList();
            ddlUsuarios.DataValueField = "idUsuario";
            ddlUsuarios.DataBind();
        }
        public void MostrarGrillaUsuarios()
        {
            dgvUsuario.DataSource = usuarioNego.MostrarUsuarios().ToList();
            dgvUsuario.DataBind();
        }

        protected void ddlUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
           

            Usuario usuario = usuarioNego.ObtenerUsuario(Convert.ToInt32(ddlUsuarios.SelectedItem.Value));

            lblIdUsuario.Text = ddlUsuarios.SelectedValue.ToString();

            CargarUsuario(usuario);
        }
        public void CargarUsuario(Usuario usuario)
        {
            if (usuario != null)
            {
                btnGuardar.Visible = false;
                btnActualizar.Visible = true;

                txtNombre.Text = usuario.Nombre;
                txtContrasenia.Text = usuario.Contrasenia;
                txtGrupo.Text = Convert.ToString(usuario.Grupo);
                txtMail.Text = usuario.Mail;
            }
            else
            {
                btnGuardar.Visible = true;
                btnActualizar.Visible = false;

                txtNombre.Text = "";
                txtContrasenia.Text = "";
                txtGrupo.Text = "";
                txtMail.Text = "";
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarUsuario();
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            btnGuardar.Visible = true;
            btnActualizar.Visible = false;

            ActualizarUsuario();
        }
        private void GuardarUsuario()
        {
            Usuario usuario = new Usuario();

            usuario.Nombre = txtNombre.Text;
            usuario.Contrasenia = txtContrasenia.Text;
            usuario.Grupo = Convert.ToInt32(txtGrupo.Text);
            usuario.Margen = 0;
            usuario.Mail = txtMail.Text;

            usuarioNego.GuardarUsuario(usuario);

            MostrarGrillaUsuarios();
        }
        private void ActualizarUsuario()
        {
            Usuario usuario = new Usuario();

            usuario.IdUsuario = Convert.ToInt32(lblIdUsuario.Text);
            usuario.Nombre = txtNombre.Text;
            usuario.Contrasenia = txtContrasenia.Text;
            usuario.Grupo = Convert.ToInt32(txtGrupo.Text);
            usuario.Margen = 0;
            usuario.Mail = txtMail.Text;

            usuarioNego.ActualizarUsuario(usuario);

            MostrarGrillaUsuarios();
        }
    }
}