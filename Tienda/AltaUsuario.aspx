<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AltaUsuario.aspx.cs" Inherits="Tienda.AltaUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container">
        <asp:Panel ID="PanelAltaUsuario" CssClass="panel panel-default" runat="server">
            <div class="panel-heading">
                <h3>Nuevo USUARIO</h3>
            </div>
            <!--AGREGAR EL DROPDOWNLIST-->
            <!--LISTA CON LOS USUARIOS DE LA BD-->
            <div class="form-group">
                <br />
                <div class="col-md-4 col-md-offset-2">
                    <asp:DropDownList ID="ddlUsuarios" runat="server"
                        Width="280"
                        BackColor="WhiteSmoke"
                        ForeColor="#000066"
                        Font-Bold="true"
                        CssClass="form-control"
                        DataTextField="nombre"
                        AutoPostBack="True"
                        AppendDataBoundItems="true"
                        OnSelectedIndexChanged="ddlUsuarios_SelectedIndexChanged">
                        <asp:ListItem Value="-1">&lt;Seleccione un Usuario&gt;</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div style="margin-left: auto; margin-right: auto; text-align: right;">

                    <div class="col-md-1 col-md-offset-1">
                        <asp:Label ID="lblIdU" runat="server" Text="ID = " CssClass="control-label"> </asp:Label>
                    </div>
                </div>
                <div style="margin-left: auto; margin-right: auto; text-align: left;">
                    <div class="col-md-2">
                        <asp:Label ID="lblIdUsuario" runat="server" Text="" Font-Bold="true" CssClass="control-label"> </asp:Label>
                    </div>
                </div>
            </div>
            <!-- NOMBRE -->
            <div class="form-group">
                <br />
                <asp:Label ID="lblNombre" runat="server" Text="NOMBRE" CssClass="col-md-2 col-xs-6 control-label"> </asp:Label>
                <div class="col-md-6 col-xs-12">
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox><br />
                </div>
            </div>
            <!-- CONTRASEÑA -->
            <div class="form-group">
                <asp:Label ID="lblContrasenia" runat="server" Text="CONTRASEÑA" CssClass="col-md-2 col-xs-6 control-label"> </asp:Label>
                <div class="col-md-6 col-xs-12">
                    <asp:TextBox ID="txtContrasenia" runat="server" CssClass="form-control"></asp:TextBox><br />
                </div>
            </div>
            <!-- GRUPO -->
            <div class="form-group">
                <asp:Label ID="lblGrupo" runat="server" Text="GRUPO" CssClass="col-md-2 col-xs-6 control-label"> </asp:Label>
                <div class="col-md-6 col-xs-12">
                    <asp:TextBox ID="txtGrupo" runat="server" CssClass="form-control"></asp:TextBox><br />
                </div>
            </div>
            <!-- MAIL -->
            <div class="form-group">
                <asp:Label ID="lblMail" runat="server" Text="MAIL" CssClass="col-md-2 col-xs-6 control-label"> </asp:Label>
                <div class="col-md-6 col-xs-12">
                    <asp:TextBox ID="txtMail" runat="server" CssClass="form-control"></asp:TextBox><br />
                </div>
            </div>
            <!--BOTONES GUARDAR Y ACTUALIZAR-->
            <div class="form-group">
                <div class="col-md-2 col-md-offset-2">
                    <br />
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-primary form-control" OnClick="btnGuardar_Click" />
                </div>
                <div class="col-md-2">
                    <br />
                    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CssClass="btn btn-danger form-control" OnClick="btnActualizar_Click" />
                </div>
            </div>
            <!--GRILLA-->
            <div class="form-group">
                <div class="col-md-9 col-md-offset-1">
                    <asp:GridView ID="dgvUsuario" runat="server" AutoGenerateColumns="false"
                        CssClass="table table-hover table-bordered" BorderWidth="4px">
                        <Columns>
                            <asp:BoundField HeaderText="Nombre" DataField="nombre" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Grupo" DataField="grupo" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Margen" DataField="margen" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Mail" DataField="mail" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
