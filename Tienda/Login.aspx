﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Tienda.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%--<script src="Scripts/jquery-1.10.2.js"></script>--%>
    <%--<script src="js/jquery-1.12.3.js"></script>--%>
    <script src="js/jquery-2.1.1.min.js"></script>
    <%--<link href="Content/bootstrap.css" rel="stylesheet" />--%>
    <%--<link href="css/bootstrap.css" rel="stylesheet" />--%>
    <link href="js/bootstrap/css/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server" class="form-horizontal">
        <div class="container">
            <br />
            <br />
            <br />
            <br />
            <div class="col-md-4 col-md-offset-4">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <br />
                        <div class="form-group">
                            <div class="col-md-12 col-md-offset-1">
                                <h2>Ingreso al Sistema</h2>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-10 col-md-offset-1">
                                <asp:TextBox ID="txtusername" runat="server" CssClass="form-control" placeholder="Usuario"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-10 col-md-offset-1">
                                <asp:TextBox ID="txtpassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Contraseña"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-md-10 col-md-offset-1">
                                <asp:Button ID="btnlogin" runat="server"
                                    Text="Ingresar" OnClick="btnlogin_Click" CssClass="btn btn-default btn-lg btn-block" />
                            </div>
                        </div>
                        <asp:Label ID="lblSesionAbierta" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>