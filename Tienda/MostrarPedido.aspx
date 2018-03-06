<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MostrarPedido.aspx.cs" Inherits="Tienda.MostrarPedido" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <asp:Panel ID="Panel1" CssClass="panel panel-default" runat="server" HorizontalAlign="Left">
            <div class="panel-heading">
                <h3>Detalle del PEDIDO</h3>
            </div>
            <br />
            <!-- NUMERO DE PEDIDO -->
            <div class="form-group">
                <asp:Label ID="lblPedidoTitulo" runat="server" Text="NUMERO PEDIDO" CssClass="col-md-2 alineaderecha"></asp:Label>
                <asp:Label ID="lblNumeroPedido" runat="server" CssClass="col-md-4 alineaizquierda" Font-Bold="True"></asp:Label>
            </div>
            <!-- FECHA DE PEDIDO -->
            <div class="form-group">
                <asp:Label ID="lblFechaTitulo" runat="server" Text="FECHA" CssClass="col-md-2 alineaderecha"></asp:Label>
                <asp:Label ID="lblFechaPedido" runat="server" CssClass="col-md-4 alineaizquierda" Font-Bold="true"></asp:Label>
            </div>
            <!-- TOTAL DE PEDIDO -->
            <div class="form-group">
                <asp:Label ID="lblTotalTitulo" runat="server" Text="TOTAL: " CssClass="col-md-2 alineaderecha"> </asp:Label>
                <asp:Label ID="lblTotalCarrito" runat="server" CssClass="col-md-2 alineaizquierda" Font-Bold="true"> </asp:Label>
            </div>

            <%--<asp:Button ID="BtnImprimir" runat="server" Text="Imprimir" CausesValidation="False" OnClientClick="return false;" UseSubmitBehavior="False" />
            <br />
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" AsyncRendering="False" InteractivityPostBackMode="AlwaysSynchronous" ShowPrintButton="False">
            </rsweb:ReportViewer>--%>

            <div class="form-group">
                <!--CARRITO-->
                <div class="col-md-10 col-md-offset-1">
                    <br />
                    <asp:GridView ID="dgvCarrito" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-hover" BorderWidth="2px" EmptyDataText="Carrito vacio" ShowHeaderWhenEmpty="true">
                        <Columns>
                            <asp:TemplateField HeaderStyle-BackColor="#cccccc" HeaderText="Producto" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Producto.Codigo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-BackColor="#cccccc" HeaderText="Descripcion" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Producto.Nombre") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderStyle-BackColor="#cccccc" HeaderText="Cantidad" DataField="cantidad" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100" />
                            <asp:BoundField HeaderStyle-BackColor="#cccccc" HeaderText="Precio" DataField="precio" ItemStyle-CssClass="t-cost" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
