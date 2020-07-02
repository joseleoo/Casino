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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Ingrese Cantidad" ControlToValidate="txtCantidad1" Display="Dynamic" ForeColor="Red" SetFocusOnError="True">Ingrese Cantidad</asp:RequiredFieldValidator>
                    <ajaxToolkit:FilteredTextBoxExtender ID="txtCantidad1_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtCantidad1" />
                    &nbsp;<asp:Label ID="Label6" runat="server" Text="Color"></asp:Label>
                    <asp:DropDownList ID="ddlColor1" runat="server">
                        <asp:ListItem Value="0" Selected="True">Seleccione</asp:ListItem>
                        <asp:ListItem Value="15">Verde</asp:ListItem>
                        <asp:ListItem Value="2">Rojo</asp:ListItem>
                        <asp:ListItem Value="2">Negro</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlColor1" Display="Dynamic" ErrorMessage="elija color" ForeColor="#990000"></asp:RequiredFieldValidator>
                </div>
                <hr />
                <div>
                    <asp:Label ID="Label2" runat="server" Text="Elija un nombre para el jugador #2"></asp:Label>
                    <asp:DropDownList ID="ddlJugador2" runat="server"></asp:DropDownList>
                    <asp:Label ID="Label3" runat="server" Text="Ingrese cantidad"></asp:Label>
                    <asp:TextBox ID="txtCantidad2" runat="server" OnTextChanged="txtCantidad2_TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCantidad2" Display="Dynamic" ErrorMessage="Ingrese Cantidad" ForeColor="Red" SetFocusOnError="True">Ingrese Cantidad</asp:RequiredFieldValidator>
                    <ajaxToolkit:FilteredTextBoxExtender ID="txtCantidad2_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtCantidad2" />
                    <asp:Label ID="Label7" runat="server" Text="Color"></asp:Label>
                    <asp:DropDownList ID="ddlColor2" runat="server">
                        <asp:ListItem Selected="True" Value="0">Seleccione</asp:ListItem>
                        <asp:ListItem Value="15">Verde</asp:ListItem>
                        <asp:ListItem Value="2">Rojo</asp:ListItem>
                        <asp:ListItem Value="15">Negro</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <hr />
                <div>
                    <asp:Label ID="Label4" runat="server" Text="Elija un nombre para el jugador #3"></asp:Label>
                    <asp:DropDownList ID="ddlJugador3" runat="server"></asp:DropDownList>
                    <asp:Label ID="Label5" runat="server" Text="Ingrese cantidad"></asp:Label>
                    <asp:TextBox ID="txtCantidad3" runat="server" OnTextChanged="txtCantidad3_TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCantidad3" Display="Dynamic" ErrorMessage="Ingrese Cantidad" ForeColor="Red" SetFocusOnError="True">Ingrese Cantidad</asp:RequiredFieldValidator>
                    <ajaxToolkit:FilteredTextBoxExtender ID="txtCantidad3_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtCantidad3" />
                    <asp:Label ID="Label8" runat="server" Text="Color"></asp:Label>
                    <asp:DropDownList ID="ddlColor3" runat="server">
                        <asp:ListItem Value="0">Seleccione</asp:ListItem>
                        <asp:ListItem Value="15">Verde</asp:ListItem>
                        <asp:ListItem Value="2">Rojo</asp:ListItem>
                        <asp:ListItem Value="2">Negro</asp:ListItem>
                    </asp:DropDownList>
                </div>
              
                <hr />
                  <div>
                    <asp:Button ID="Button1" runat="server" Text="Empezar" />

                      <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="CustomValidator" OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator>

                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
