using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaDominio;
using CapaNegocio;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Tienda
{
    public partial class AltaUsuario : System.Web.UI.Page
    {
        UsuarioNego usuarioNego = new UsuarioNego();
        static string usuarioControlNombre;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if ((string)Session["usergrupo"].ToString() == "1")
                {
                    PanelAltaUsuario.Visible = true;
                    PanelClientes.Visible = true;
                }
                else
                {
                    PanelAltaUsuario.Visible = false;
                    PanelClientes.Visible = false;
                }

                btnGuardar.Visible = true;
                btnActualizar.Visible = false;

                MostrarListas();

                MostrarGrillaUsuarios();
            }
        }
        public void MostrarListas()
        {
            ddlUsuarios.DataSource = usuarioNego.MostrarUsuarios().ToList().OrderBy(c=>c.Nombre);
            ddlUsuarios.DataValueField = "idUsuario";
            ddlUsuarios.DataBind();
        }
        public void MostrarGrillaUsuarios()
        {
            dgvUsuario.DataSource = usuarioNego.MostrarUsuarios().ToList().OrderBy(c => c.Nombre);
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
                usuarioControlNombre = usuario.Nombre;

                btnGuardar.Visible = false;
                btnActualizar.Visible = true;

                txtNombre.Text = usuario.Nombre;
                txtContrasenia.Text = usuario.Contrasenia;
                txtGrupo.Text = Convert.ToString(usuario.Grupo);
                txtMail.Text = usuario.Mail;
                txtCuit.Text = usuario.Cuit;
            }
            else
            {
                btnGuardar.Visible = true;
                btnActualizar.Visible = false;

                LimpiarFormularioUsuario();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            btnGuardar.Visible = true;
            btnActualizar.Visible = false;

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
            if (txtNombre.Text != "" && txtContrasenia.Text != "" && txtMail.Text != "" && txtGrupo.Text != "")
            {
                Usuario usuarioControl = usuarioNego.ControlarDuplicadoUsuario(txtNombre.Text);

                if (usuarioControl == null)
                {
                    Usuario usuario = new Usuario();

                    usuario.Nombre = txtNombre.Text;
                    usuario.Contrasenia = txtContrasenia.Text;
                    usuario.Grupo = Convert.ToInt32(txtGrupo.Text);
                    usuario.Margen = 0;
                    usuario.Mail = txtMail.Text;
                    usuario.Cuit = txtCuit.Text;

                    usuarioNego.GuardarUsuario(usuario);

                    MostrarGrillaUsuarios();

                    LimpiarFormularioUsuario();
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "Correct", "alert('El Nombre de Usuario ya existe. Ingrese otro.')", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Correct", "alert('Debe completar todos los campos.')", true);
            }
        }

        private void ActualizarUsuario()
        {
            if (txtNombre.Text != "" && txtContrasenia.Text != "" && txtMail.Text != "" && txtGrupo.Text != "" && txtCuit.Text != "")
            {
                Usuario usuarioControl = usuarioNego.ControlarDuplicadoUsuario(txtNombre.Text);

                if (usuarioControl == null)
                {
                    //significa que no esta en la base, entonces se puede actualizar
                    // => ACTUALIZO
                    Usuario usuario = new Usuario();

                    usuario.IdUsuario = Convert.ToInt32(lblIdUsuario.Text);
                    usuario.Nombre = txtNombre.Text;
                    usuario.Contrasenia = txtContrasenia.Text;
                    usuario.Grupo = Convert.ToInt32(txtGrupo.Text);
                    usuario.Margen = 0;
                    usuario.Mail = txtMail.Text;
                    usuario.Cuit = txtCuit.Text;

                    usuarioNego.ActualizarUsuario(usuario);

                    MostrarGrillaUsuarios();

                    LimpiarFormularioUsuario();

                    Response.Redirect("AltaUsuario.aspx");

                    ddlUsuarios.Text = "-1";

                }
                else if (usuarioControl != null)
                {
                    //1ra Opcion: el nombre ingresado es igual al nombre cargado
                    if (usuarioControlNombre == usuarioControl.Nombre)
                    {
                        //Actualizo
                        Usuario usuario = new Usuario();

                        usuario.IdUsuario = Convert.ToInt32(lblIdUsuario.Text);
                        usuario.Nombre = txtNombre.Text;
                        usuario.Contrasenia = txtContrasenia.Text;
                        usuario.Grupo = Convert.ToInt32(txtGrupo.Text);
                        usuario.Margen = 0;
                        usuario.Mail = txtMail.Text;
                        usuario.Cuit = txtCuit.Text;

                        usuarioNego.ActualizarUsuario(usuario);

                        MostrarGrillaUsuarios();

                        LimpiarFormularioUsuario();

                        Response.Redirect("AltaUsuario.aspx");

                        ddlUsuarios.Text = "-1";
                    }
                    else if (usuarioControlNombre != usuarioControl.Nombre)
                    {
                        lblGrupo.Text = usuarioControlNombre;
                        lblMail.Text = usuarioControl.Nombre;

                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Correct", "alert('El Nombre de Usuario ya existe. Ingrese otro.')", true);

                        btnGuardar.Visible = false;
                        btnActualizar.Visible = true;
                    }
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "Correct", "alert('Complete todos los campos.')", true);

                btnGuardar.Visible = false;
                btnActualizar.Visible = true;
            }
        }
        public void LimpiarFormularioUsuario()
        {
            txtNombre.Text = "";
            txtContrasenia.Text = "";
            txtGrupo.Text = "";
            txtMail.Text = "";
            txtCuit.Text = "";            
        }

        protected void Upload2(object sender, EventArgs e)
        {
            //Upload and save the file
            string csvPath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload2.PostedFile.FileName);
            FileUpload2.SaveAs(csvPath);

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[6] { 
            new DataColumn("cuit", typeof(string)),
            new DataColumn("usuario", typeof(string)),
            new DataColumn("contrasenia",typeof(string)),
            new DataColumn("grupo",typeof(int)),
            new DataColumn("margen",typeof(int)),
            new DataColumn("mail",typeof(string))
            });


            string csvData = File.ReadAllText(csvPath);

            //Execute a loop over the rows.  
            foreach (string row in csvData.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    dt.Rows.Add();
                    int i = 0;

                    //Execute a loop over the columns.  
                    foreach (string cell in row.Split(';'))
                    {
                        dt.Rows[dt.Rows.Count - 1][i] = cell;
                        i++;
                    }
                }
            }

            foreach (DataRow row in dt.Rows)
            {
                //Producto producto = new Producto();

                string cuitCsv = row[0].ToString();
                //decimal precioCsv = Convert.ToDecimal(row[2].ToString());

                Usuario usuario = usuarioNego.ObtenerUsuarioSegunCuit(cuitCsv);

                if (usuario != null)
                {
                    Usuario usu = new Usuario();

                    usu.IdUsuario = usuario.IdUsuario;
                    usu.Nombre = usuario.Nombre;
                    usu.Contrasenia = usuario.Contrasenia;
                    usu.Margen = usuario.Margen;
                    usu.Grupo = usuario.Grupo;
                    usu.Mail = usuario.Mail;
                    usu.Cuit=usuario.Cuit;

                    usuarioNego.ActualizarUsuario(usu);
                }
                else if (usuario == null)
                {
                    Usuario usu = new Usuario();

                    usu.Cuit = row[0].ToString();
                    usu.Nombre = row[1].ToString();
                    usu.Contrasenia = row[2].ToString();
                    usu.Grupo = Convert.ToInt32(row[3].ToString());
                    usu.Margen = Convert.ToInt32(row[4].ToString());
                    usu.Mail = row[5].ToString();

                    usuarioNego.GuardarUsuario(usu);
                }
            }
            Response.Redirect("AltaUsuario.aspx");
        }
    }
}