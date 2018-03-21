<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Tienda._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <style>
    </style>
    <style>
        .tb5 {
            border: 1px solid #456879;
            border-radius: 6px;
            height: 30px;
            text-align: center;
        }
    </style>
    <style>
        .myGridClass {
            width: 100%;
            /*this will be the color of the odd row*/
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
        }

            /*data elements*/
            .myGridClass td {
                padding: 2px;
                border: solid 1px #c1c1c1;
                color: #717171;
            }

            /*header elements*/
            .myGridClass th {
                padding: 4px 2px;
                color: #fff;
                background: #424242;
                border-left: solid 1px #525252;
                font-size: 0.9em;
            }

            /*his will be the color of even row*/
            .myGridClass .myAltRowClass {
                background: #fcfcfc repeat-x top;
            }

            /*and finally, we style the pager on the bottom*/
            .myGridClass .myPagerClass {
                background: #424242;
            }

                .myGridClass .myPagerClass table {
                    margin: 5px 0;
                }

                .myGridClass .myPagerClass td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                }

                .myGridClass .myPagerClass a {
                    color: #666;
                    text-decoration: none;
                }

                    .myGridClass .myPagerClass a:hover {
                        color: #000;
                        text-decoration: none;
                    }
    </style>




    <style type="text/css">
        .boton_vaciar_carrito {
            text-decoration: none;
            padding: 2px;
            font-weight: 400;
            font-size: 15px;
            color: #ffffff;
            background-color: #ff0000;
            border-radius: 6px;
            border: 2px solid #ff0000;
        }

            .boton_vaciar_carrito:hover {
                color: #ff0000;
                background-color: #ffffff;
            }
    </style>
    <style type="text/css">
        .boton_agregar_pedido {
            text-decoration: none;
            padding: 2px;
            font-weight: 400;
            font-size: 15px;
            color: #ffffff;
            background-color: #77ac60;
            border-radius: 6px;
            border: 2px solid #77ac60;
            width: 150px;
        }

            .boton_agregar_pedido:hover {
                color: #77ac60;
                background-color: #ffffff;
            }
    </style>
    <style type="text/css">
        .boton_mas {
            text-decoration: none;
            padding: 2px;
            font-weight: 600;
            font-size: 15px;
            color: #ffffff;
            background-color: #77ac60;
            border-radius: 6px;
            border: 2px solid #77ac60;
            width: 30px;
        }

            .boton_mas:hover {
                color: #77ac60;
                background-color: #ffffff;
            }
    </style>





    <!-- MARGEN -->
    <br />
    <div class="form-group ">
        <asp:Label ID="lblMargen" runat="server" Text="Margen" CssClass="col-md-2 col-md-offset-1 control-label"></asp:Label>
        <div class="col-md-2">
            <asp:DropDownList ID="ddlMargen" runat="server"
                BackColor="WhiteSmoke"
                ForeColor="#000066"
                Font-Bold="false"
                Width="80"
                CssClass="selectpicker form-control show-tick"
                data-style="btn-default"
                AutoPostBack="true"
                OnSelectedIndexChanged="ddlMargen_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
        <asp:Button ID="btnVaciarCarrito" Width="150" runat="server" Text="Vaciar Carrito" CssClass="btn boton_vaciar_carrito" OnClick="btnVaciarCarrito_Click" />
    </div>

    <!-- SELECCIONAR CODIGO DEL PRODUCTO -->
    <div class="form-group">
        <asp:Label ID="lblProducto" runat="server" Text="SELECCIONAR CODIGO DEL PRODUCTO" CssClass="col-md-3 control-label"> </asp:Label>
        <div class="col-md-4">
            <asp:DropDownList ID="ddlProducto" runat="server"
                BackColor="WhiteSmoke"
                ForeColor="#000066"
                Font-Bold="false"
                CssClass="selectpicker form-control show-tick"
                data-style="btn-default"
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
                    <asp:TextBox ID="txtCantidad" Text="1" runat="server" CssClass="form-control tb5"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:Button ID="btnMasUno" runat="server" Text="+" CssClass="btn boton_mas" OnClick="btnMasUno_Click" />
                    <asp:Button ID="btnMenosUno" runat="server" Text="-" CssClass="btn boton_mas" OnClick="btnMenosUno_Click" />
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnAgregarAlPedido" runat="server" Text="Agregar al Pedido" CssClass="btn boton_agregar_pedido" OnClick="btnAgregarAlPedido_Click" />
                </div>
            </div>
            <asp:Panel ID="PanelCarrito" CssClass="panel panel-default" runat="server">
                <div class="panel-heading">
                    <h4>Carrito</h4>
                </div>
                <!--CARRITO-->
                <div class="form-group" id="areaImprimir">




                    <div class="form-group">
                        <br />
                        <asp:Label ID="lblTotalCarrito" runat="server" Text="TOTAL: " CssClass="col-md-2 alineaderecha"> </asp:Label>
                        <asp:Label ID="txtTotalCarrito" runat="server" CssClass="col-md-2 alineaizquierda" Font-Bold="true"> </asp:Label>

                        <asp:Label ID="lblTotalProductos" runat="server" Text="CANTIDAD PRODUCTOS: " CssClass="col-md-2 alineaderecha"> </asp:Label>
                        <asp:Label ID="txtTotalProductos" runat="server" CssClass="col-md-2 alineaizquierda" Font-Bold="true"> </asp:Label>
                    </div>
                    <div class="form-group">
                        <div class="col-md-10 col-md-offset-1 ">
                            <asp:GridView ID="dgvCarrito" runat="server" AutoGenerateColumns="false"
                                CssClass="myGridClass" BorderWidth="2px" EmptyDataText="Carrito vacio" ShowHeaderWhenEmpty="true"
                                OnRowDeleting="dgvCarrito_RowDeleting">
                                <Columns>

                                    <asp:BoundField HeaderStyle-BackColor="#cccccc" HeaderText="Codigo" DataField="codigoProducto" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100" />
                                    <asp:BoundField HeaderStyle-BackColor="#cccccc" HeaderText="Descripcion" DataField="nombreProducto" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="200" />
                                    <asp:BoundField HeaderStyle-BackColor="#cccccc" HeaderText="Cantidad" DataField="cantidad" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100" />
                                    <asp:BoundField HeaderStyle-BackColor="#cccccc" HeaderText="Precio" DataField="precio" ItemStyle-CssClass="t-cost" DataFormatString="{0:c}" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100" />
                                    <asp:CommandField HeaderStyle-BackColor="#cccccc" ShowDeleteButton="True" ButtonType="Image" DeleteImageUrl="~/image/deletered.png" HeaderStyle-Width="20" />
                                </Columns>

                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="col-md-12 col-md-offset-1" runat="server" id="logo">
                    <asp:Image ID="imagen1" runat="server" ImageUrl="~/image/logo.png" />
                </div>
                <asp:Button ID="Button1" runat="server" Text="Imprimir PRESUPUESTO" CssClass="btn btn-success" OnClientClick="printDiv('areaImprimir')" />
                <%--<input type="button" onclick="printDiv('areaImprimir')" value="Imprimir Presupuesto" />--%>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!--Script para IMPRIMIR -->
    <script>
        function printDiv(nombreDiv) {


            var contenido = document.getElementById('logo').innerHTML + document.getElementById(nombreDiv).innerHTML;
            var contenidoOriginal = document.body.innerHTML;

            document.body.innerHTML = contenido;

            window.print();

            document.body.innerHTML = contenidoOriginal;
        }
    </script>

</asp:Content>
