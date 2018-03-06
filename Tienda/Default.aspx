<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Tienda._Default" %>




<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <!-- SELECCIONAR CODIGO DEL PRODUCTO -->
    <div class="form-group">
        <br />
        <asp:Label ID="lblProducto" runat="server" Text="SELECCIONAR CODIGO DEL PRODUCTO" CssClass="col-md-3 control-label"> </asp:Label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlProducto" runat="server"
                BackColor="WhiteSmoke"
                ForeColor="#000066"
                Font-Bold="false"
                CssClass="selectpicker form-control show-tick"
                data-live-search="true"
                AutoPostBack="false"
                AppendDataBoundItems="true">
            </asp:DropDownList>
        </div>
    </div>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="form-group ">
                <asp:Label ID="lblCantidad" runat="server" Text="CANTIDAD" CssClass="col-md-3 control-label"> </asp:Label>
                <div class="col-md-1">
                    <asp:TextBox ID="txtCantidad" Text="1" runat="server" CssClass="form-control "></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnAgregarAlPedido" runat="server" Text="AGREGAR AL PEDIDO" CssClass="btn btn-success" OnClick="btnAgregarAlPedido_Click" />
                </div>
            </div>

            <asp:Panel ID="PanelCarrito" CssClass="panel panel-default" runat="server">
                <div class="panel-heading">
                    <h4>Carrito</h4>
                </div>
                <!--CARRITO-->
                <div class="form-group">
                    <br />
                    <asp:Label ID="lblTotalCarrito" runat="server" Text="TOTAL: " CssClass="col-md-2 alineaderecha"> </asp:Label>
                    <asp:Label ID="txtTotalCarrito" runat="server" CssClass="col-md-2 alineaizquierda" Font-Bold="true"> </asp:Label>

                    <asp:Label ID="lblTotalProductos" runat="server" Text="CANTIDAD PRODUCTOS: " CssClass="col-md-2 alineaderecha"> </asp:Label>
                    <asp:Label ID="txtTotalProductos" runat="server" CssClass="col-md-2 alineaizquierda" Font-Bold="true"> </asp:Label>
                </div>
                <div class="form-group">
                    <div class="col-md-10 col-md-offset-1">
                        <asp:GridView ID="dgvCarrito" runat="server" AutoGenerateColumns="false"
                            
                            CssClass="table table-hover" BorderWidth="2px" EmptyDataText="Carrito vacio" ShowHeaderWhenEmpty="true"
                            OnRowDeleting="dgvCarrito_RowDeleting">
                            <Columns>
                                <asp:TemplateField HeaderStyle-BackColor="#cccccc" HeaderText="Producto" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Producto.Codigo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-BackColor="#cccccc" HeaderText="Descripcion" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="150">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Producto.Nombre") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderStyle-BackColor="#cccccc" HeaderText="Cantidad" DataField="cantidad" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100" />
                                <asp:BoundField HeaderStyle-BackColor="#cccccc" HeaderText="Precio" DataField="precio" ItemStyle-CssClass="t-cost" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100" />
                                <asp:CommandField HeaderStyle-BackColor="#cccccc" ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/image/deletered.png" HeaderStyle-Width="20" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
