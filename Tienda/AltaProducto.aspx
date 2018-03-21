<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AltaProducto.aspx.cs" Inherits="Tienda.AltaProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Panel ID="PanelProyectos" CssClass="panel panel-default" runat="server">
        <div class="form-group">
            <div class="col-md-6">
                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                <asp:Button Text="Cargar BD Completa" CssClass="btn btn-success" OnClick="Upload1" runat="server" />
            </div>
            <div class="col-md-6">
                <asp:FileUpload ID="FileUpload2" runat="server" CssClass="form-control" />
                <asp:Button Text="Actualizar Algunos Productos" CssClass="btn btn-success" OnClick="Upload2" runat="server" />
            </div>
        </div>
    </asp:Panel>

    <asp:Panel ID="PanelProductos" CssClass="panel panel-success" runat="server">
        <div class="panel-heading">
            <h2>Base de PRODUCTOS</h2>
        </div>
        <div class="form-group">
            <br />
            <div class="col-md-10 col-md-offset-1">
                <asp:GridView ID="dgvProducto" runat="server" AutoGenerateColumns="true"
                    CssClass="table table-hover" BorderWidth="2px">
                    <Columns>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
