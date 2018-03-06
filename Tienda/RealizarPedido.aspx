<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RealizarPedido.aspx.cs" Inherits="Tienda.RealizarPedido" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel ID="PanelCarrito" CssClass="panel panel-default" runat="server">
        <div class="panel-heading">
            <h4>Pedido</h4>
        </div>
        <!--USUARIO-->
        <br />
        <div class="form-group">
            <asp:Label ID="lblNombreUsuario" runat="server" Text="USUARIO" CssClass="col-md-2 alineaderecha"></asp:Label>
            <asp:Label ID="txtNombreUsuario" runat="server" CssClass="col-md-2 alineaizquierda" Font-Bold="true"></asp:Label>
        </div>
        <!--NUMERO PEDIDO-->
        <div class="form-group">
            <asp:Label ID="lblNumeroPedido" runat="server" Text="CODIGO PEDIDO" CssClass="col-md-2 control-label"></asp:Label>
            <div class="col-md-4">
                <asp:TextBox ID="txtNumeroPedido" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
        </div>

        <!-- FECHA DE PEDIDO -->
        <div class="form-group">
            <asp:Label ID="lblFechaPedido" runat="server" Text="FECHA" CssClass="col-md-2 control-label"></asp:Label>
            <div class="col-md-6">
                <asp:DropDownList ID="ddlDia" runat="server"
                    CssClass="selectpicker form-control show-tick"
                    data-live-search="false"
                    data-width="90">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlMes" runat="server"
                    CssClass="selectpicker form-control show-tick"
                    data-live-search="false"
                    data-width="150">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlAnio" runat="server"
                    CssClass="selectpicker form-control show-tick"
                    data-live-search="false"
                    data-width="100">
                </asp:DropDownList>
            </div>
        </div>


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
        <!-- TOTAL DE PEDIDO -->
        <div class="form-group">
            <asp:Label ID="lblTotalTitulo" runat="server" Text="TOTAL EN $$$: " CssClass="col-md-2 alineaderecha"> </asp:Label>
            <asp:Label ID="txtTotalCarrito" runat="server" CssClass="col-md-2 alineaizquierda" Font-Bold="true"> </asp:Label>
        </div>
        <div class="form-group">
            <asp:Button runat="server" ID="btnConfirmarCompra" Text="CONFIRMAR COMPRA" CssClass="btn btn-success  col-md-offset-1" OnClick="btnConfirmarCompra_Click" />
        </div>

    </asp:Panel>

    <!-- PANEL DE PEDIDOS REALIZADOS -->
    <asp:Panel ID="PanelPedidosRealizados" CssClass="panel panel-default" runat="server">
        <div class="panel-heading">
            <h4>Pedidos Realizados</h4>
        </div>
        <!--PEDIDOS REALIZADOS-->
        <div class="form-group">
            <div class="col-md-4 col-md-offset-1">
                <br />
                <asp:GridView ID="dgvPedidosRealizados" runat="server" AutoGenerateColumns="false"
                    CssClass="table table-hover" BorderWidth="2px" EmptyDataText="No existen pedidos almacenados"
                    OnRowDeleting="dgvPedidosRealizados_RowDeleting"
                    ShowHeaderWhenEmpty="true">
                    <Columns>
                        <asp:BoundField HeaderText="ID" DataField="idPedido" ItemStyle-HorizontalAlign="Left" />
                        <asp:BoundField HeaderStyle-BackColor="#cccccc" HeaderText="Codigo Pedido" DataField="numeroPedido" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100" />
                        <asp:ButtonField HeaderText="Detalle" CommandName="delete" HeaderStyle-BackColor="#cccccc" ControlStyle-Width="50" HeaderStyle-Width="100" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ControlStyle-CssClass="glyphicon glyphicon-search" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </asp:Panel>

</asp:Content>
