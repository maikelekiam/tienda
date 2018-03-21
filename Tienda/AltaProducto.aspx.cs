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
    public partial class AltaProducto : System.Web.UI.Page
    {
        ProductoNego productoNego = new ProductoNego();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["usergrupo"].ToString() == "1")
                {
                    PanelProyectos.Visible = true;
                    PanelProductos.Visible = true;
                }
                else
                {
                    PanelProyectos.Visible = false;
                    PanelProductos.Visible = false;
                }
            }
            MostrarFrillaProductos();
        }

        protected void Upload1(object sender, EventArgs e)
        {
            //primero borro la base de datos de PRODUCTO
            productoNego.EliminarListaProductos();


            //Upload and save the file
            string csvPath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(csvPath);

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] { 
            new DataColumn("codigo", typeof(string)),
            new DataColumn("nombre", typeof(string)),
            new DataColumn("precio", typeof(decimal)),
            new DataColumn("stock",typeof(int)) });


            string csvData = File.ReadAllText(csvPath);
            foreach (string row in csvData.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    dt.Rows.Add();
                    int i = 0;
                    foreach (string cell in row.Split(';'))
                    {
                        dt.Rows[dt.Rows.Count - 1][i] = cell;
                        i++;
                    }
                }
            }


            //AVERIGUAR SI SE PUEDE GUARDAR UNO X UNO A VER SI ES LENTO O RAPIDO



            //string consString = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            //string consString = "data source=COPADE_077\\SQLEXPRESS;initial catalog=tienda;integrated security=True";
            string consString = "workstation id=tienda2009.mssql.somee.com;packet size=4096;user id=vaarribas2;pwd=Gkhv156!;data source=tienda2009.mssql.somee.com;persist security info=False;initial catalog=tienda2009";
            using (SqlConnection con = new SqlConnection(consString))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                {
                    //Set the database table name
                    sqlBulkCopy.DestinationTableName = "dbo.Producto";
                    con.Open();
                    sqlBulkCopy.WriteToServer(dt);
                    con.Close();
                }
            }
            Response.Redirect("AltaProducto.aspx");
        }

        protected void Upload2(object sender, EventArgs e)
        {
            //Upload and save the file
            string csvPath = Server.MapPath("~/Files/") + Path.GetFileName(FileUpload2.PostedFile.FileName);
            FileUpload2.SaveAs(csvPath);

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[4] { 
            new DataColumn("codigo", typeof(string)),
            new DataColumn("nombre", typeof(string)),
            new DataColumn("precio", typeof(decimal)),
            new DataColumn("stock",typeof(int)) });


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

                string codigoCsv = row[0].ToString();
                decimal precioCsv = Convert.ToDecimal(row[2].ToString());

                Producto producto = productoNego.ObtenerProducto(codigoCsv);

                if (producto != null)
                {
                    Producto prod = new Producto();

                    prod.Codigo = producto.Codigo;
                    prod.Nombre = producto.Nombre;
                    prod.Precio = precioCsv;
                    prod.Stock = producto.Stock;

                    productoNego.ActualizarProducto(prod);
                }
                else if (producto == null)
                {
                    Producto prod = new Producto();

                    prod.Codigo = row[0].ToString(); ;
                    prod.Nombre = row[1].ToString(); ;
                    prod.Precio = Convert.ToDecimal(row[2].ToString());
                    prod.Stock = Convert.ToInt32(row[3].ToString());

                    productoNego.GuardarProducto(prod);
                }
            }

            Response.Redirect("AltaProducto.aspx");

        }
        public void MostrarFrillaProductos()
        {
            dgvProducto.DataSource = productoNego.MostrarProductos().ToList();
            dgvProducto.DataBind();
        }
    }
}