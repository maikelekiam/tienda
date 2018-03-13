<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AltaProducto.aspx.cs" Inherits="Tienda.AltaProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel ID="PanelProyectos" runat="server">
        <br />
        <div class="col-md-6">
            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
            <asp:Button Text="Cargar BD Completa" CssClass="btn btn-success" OnClick="Upload1" runat="server" />
        </div>
        <div class="col-md-6">
            <asp:FileUpload ID="FileUpload2" runat="server" CssClass="form-control" />
            <asp:Button Text="Actualizar Algunos Productos" CssClass="btn btn-success" OnClick="Upload2" runat="server" />
        </div>
    </asp:Panel>


    <div class="col-md-10 col-md-offset-1">
        <br />
        <asp:GridView ID="dgvProducto" runat="server" AutoGenerateColumns="true"
            CssClass="table table-hover" BorderWidth="2px">
            <Columns>
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
