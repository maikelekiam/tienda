<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MostrarPedido.aspx.cs" Inherits="Tienda.MostrarPedido" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <div class="col-md-12 col-md-offset-1" runat="server" id="logo">
            <asp:Image ID="imagen1" runat="server" ImageUrl="~/image/logo.png" />
        </div>
        <asp:Panel ID="Panel1" CssClass="panel panel-default" runat="server" HorizontalAlign="Left">

            <div class="panel-heading">
                <h3>Detalle del PEDIDO</h3>
            </div>
            <br />
            <div class="form-group" id="areaImprimir">
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
                <div class="form-group">
                    <!--CARRITO-->
                    <div class="col-md-10 col-md-offset-1">
                        <br />
                        <asp:GridView ID="dgvCarrito" runat="server" AutoGenerateColumns="false"
                            CssClass="table table-hover" BorderWidth="2px" EmptyDataText="Carrito vacio" ShowHeaderWhenEmpty="true">
                            <Columns>
                                <asp:BoundField HeaderStyle-BackColor="#cccccc" HeaderText="Codigo" DataField="codigoProducto" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100" />
                                <asp:BoundField HeaderStyle-BackColor="#cccccc" HeaderText="Descripcion" DataField="nombreProducto" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200" />
                                <asp:BoundField HeaderStyle-BackColor="#cccccc" HeaderText="Cantidad" DataField="cantidad" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100" />
                                <asp:BoundField HeaderStyle-BackColor="#cccccc" HeaderText="Precio" DataField="precio" ItemStyle-CssClass="t-cost" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

        </asp:Panel>
        <asp:Button ID="Button1" runat="server" Text="Imprimir PEDIDO" CssClass="btn btn-success" OnClientClick="printDiv('areaImprimir')" />
        <%--        <input type="button" onclick="printDiv('areaImprimir2')" value="Imprimir PEDIDO" />--%>
    </div>

    <script>
        function printDiv(nombreDiv) {

            //var c = document.getElementById('logo').innerHTML;
            var contenido = document.getElementById(nombreDiv).innerHTML;
            var contenidoOriginal = document.body.innerHTML;

            document.body.innerHTML = contenido;

            window.print();

            document.body.innerHTML = contenidoOriginal;
        }
    </script>
</asp:Content>
