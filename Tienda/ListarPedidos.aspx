<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListarPedidos.aspx.cs" Inherits="Tienda.ListarPedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <asp:Panel ID="Panel1" CssClass="panel panel-default" runat="server" HorizontalAlign="Left">
            <div class="panel-heading">
                <h3>PEDIDOS REALIZADOS</h3>
            </div>
            <br />
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
    </div>

</asp:Content>
