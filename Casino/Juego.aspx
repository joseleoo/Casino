<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Juego.aspx.cs" Inherits="Casino.Juego" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <asp:Label ID="Label1" runat="server" Text="Elija un nombre para el jugador #1"></asp:Label>
                    <asp:DropDownList ID="ddlJugador1" runat="server"></asp:DropDownList>
                    <asp:Label ID="lbl1" runat="server" Text="Ingrese cantidad"></asp:Label>
                    <asp:TextBox ID="txtCantidad1" runat="server" OnTextChanged="txtCantidad1_TextChanged" AutoPostBack="true"></asp:TextBox>
                    <asp:Label ID="Label6" runat="server" Text="Color"></asp:Label>
                    <asp:DropDownList ID="ddlColor1" runat="server">
                        <asp:ListItem Value="0">Seleccione</asp:ListItem>
                        <asp:ListItem Value="15">Verde</asp:ListItem>
                        <asp:ListItem Value="2">Rojo</asp:ListItem>
                        <asp:ListItem Value="2">Negro</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <hr />
                <div>
                    <asp:Label ID="Label2" runat="server" Text="Elija un nombre para el jugador #2"></asp:Label>
                    <asp:DropDownList ID="ddlJugador2" runat="server"></asp:DropDownList>
                    <asp:Label ID="Label3" runat="server" Text="Ingrese cantidad"></asp:Label>
                    <asp:TextBox ID="txtCantidad2" runat="server" OnTextChanged="txtCantidad2_TextChanged"></asp:TextBox>
                    <asp:Label ID="Label7" runat="server" Text="Color"></asp:Label>
                    <asp:DropDownList ID="ddlColor2" runat="server"></asp:DropDownList>
                </div>
                <hr />
                <div>
                    <asp:Label ID="Label4" runat="server" Text="Elija un nombre para el jugador #3"></asp:Label>
                    <asp:DropDownList ID="ddlJugador3" runat="server"></asp:DropDownList>
                    <asp:Label ID="Label5" runat="server" Text="Ingrese cantidad"></asp:Label>
                    <asp:TextBox ID="txtCantidad3" runat="server" OnTextChanged="txtCantidad3_TextChanged"></asp:TextBox>
                    <asp:Label ID="Label8" runat="server" Text="Color"></asp:Label>
                    <asp:DropDownList ID="ddlColor3" runat="server"></asp:DropDownList>
                </div>
                <div>
                    <asp:Button ID="Button1" runat="server" Text="Empezar" />

                </div>
                <hr />


            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
