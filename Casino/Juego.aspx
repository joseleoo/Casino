﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Juego.aspx.cs" Inherits="Casino.Juego" %>

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
                <asp:Timer ID="Timer1" runat="server" Enabled="False" Interval="90000" OnTick="Timer1_Tick"></asp:Timer>
                <asp:HiddenField ID="hdfJugando" runat="server" Value="0"/>
                <div id="divJugadores" runat="server">
                <div  >
                    <asp:Label ID="Label1" runat="server" Text="Elija un nombre para el jugador #1"></asp:Label>
                    <asp:DropDownList ID="ddlJugador1" runat="server" CausesValidation="True"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlJugador1" Display="Dynamic" InitialValue="0" ErrorMessage="Seleccione un jugador" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="ddlJugador2" ControlToValidate="ddlJugador1" Display="Dynamic" ErrorMessage="Los jugadores deben ser diferentes" ForeColor="Red" Operator="NotEqual" SetFocusOnError="True"></asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToCompare="ddlJugador3" ControlToValidate="ddlJugador1" Display="Dynamic" ErrorMessage="Los jugadores deben ser diferentes" ForeColor="Red" Operator="NotEqual" SetFocusOnError="True"></asp:CompareValidator>
                    &nbsp;<asp:Label ID="lbl1" runat="server" Text="Ingrese cantidad"></asp:Label>
                    <asp:TextBox ID="txtCantidad1" runat="server" OnTextChanged="txtCantidad1_TextChanged" AutoPostBack="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Ingrese Cantidad" ControlToValidate="txtCantidad1" InitialValue="0" Display="Dynamic" ForeColor="Red" SetFocusOnError="True">Ingrese Cantidad</asp:RequiredFieldValidator>
                    <ajaxToolkit:FilteredTextBoxExtender ID="txtCantidad1_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtCantidad1" />
                    &nbsp;<asp:Label ID="Label6" runat="server" Text="Color"></asp:Label>
                    <asp:DropDownList ID="ddlColor1" runat="server" CausesValidation="True" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlColor1" Display="Dynamic" InitialValue="0" ErrorMessage="elija color" ForeColor="Red" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>

                <hr />
                <div>
                    <asp:Label ID="Label2" runat="server" Text="Elija un nombre para el jugador #2"></asp:Label>
                    <asp:DropDownList ID="ddlJugador2" runat="server" CausesValidation="True"></asp:DropDownList>
                    <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToCompare="ddlJugador3" ControlToValidate="ddlJugador2" Display="Dynamic" ErrorMessage="Los jugadores deben ser diferentes" ForeColor="Red" Operator="NotEqual" SetFocusOnError="True"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlJugador2" Display="Dynamic" ErrorMessage="Seleccione un jugador" ForeColor="Red" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    &nbsp;<asp:Label ID="Label3" runat="server" Text="Ingrese cantidad"></asp:Label>
                    <asp:TextBox ID="txtCantidad2" runat="server" OnTextChanged="txtCantidad2_TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCantidad2" Display="Dynamic" ErrorMessage="Ingrese Cantidad" ForeColor="Red" SetFocusOnError="True">Ingrese Cantidad</asp:RequiredFieldValidator>
                    <ajaxToolkit:FilteredTextBoxExtender ID="txtCantidad2_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtCantidad2" />
                    <asp:Label ID="Label7" runat="server" Text="Color"></asp:Label>
                    <asp:DropDownList ID="ddlColor2" runat="server" CausesValidation="True" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlColor2" Display="Dynamic" ErrorMessage="elija color" ForeColor="Red" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>
                <hr />
                <div>
                    <asp:Label ID="Label4" runat="server" Text="Elija un nombre para el jugador #3"></asp:Label>
                    <asp:DropDownList ID="ddlJugador3" runat="server" CausesValidation="True"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlJugador3" Display="Dynamic" ErrorMessage="Seleccione un jugador" ForeColor="Red" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    &nbsp;<asp:Label ID="Label5" runat="server" Text="Ingrese cantidad"></asp:Label>
                    <asp:TextBox ID="txtCantidad3" runat="server" OnTextChanged="txtCantidad3_TextChanged"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCantidad3" Display="Dynamic" ErrorMessage="Ingrese Cantidad" ForeColor="Red" SetFocusOnError="True">Ingrese Cantidad</asp:RequiredFieldValidator>
                    <ajaxToolkit:FilteredTextBoxExtender ID="txtCantidad3_FilteredTextBoxExtender" runat="server" FilterType="Numbers" TargetControlID="txtCantidad3" />
                    <asp:Label ID="Label8" runat="server" Text="Color"></asp:Label>
                    <asp:DropDownList ID="ddlColor3" runat="server" CausesValidation="True" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlColor3" Display="Dynamic" ErrorMessage="elija color" ForeColor="Red" InitialValue="0" SetFocusOnError="True"></asp:RequiredFieldValidator>
                </div>

                <hr />
                </div>
                <div id="divFinalJuego" runat="server" visible="false">
                    "Final del juego. Resultados en la parte inferior"
                </div>
                <div>
                    <asp:Button ID="Button1" runat="server" Text="Empezar" OnClick="Button1_Click" />

                    <asp:Button ID="btnReiniciar" runat="server" Text="Reiniciar" OnClick="btnReiniciar_Click" />

                </div>
                <hr />
                <div>
                    <h2>Ruleta (Color):<asp:Label ID="lblColorRuleta" runat="server" Text=""></asp:Label></h2>
                    <asp:Label ID="lbl" runat="server" Text="Jugador 1: "></asp:Label>
                    <asp:Label ID="lblNombreRes1" runat="server" Text=""></asp:Label><br />
                    <asp:Label ID="Label9" runat="server" Text="Color Seleccionado: "></asp:Label>
                    <asp:Label ID="lblColorRes1" runat="server" Text=""></asp:Label><br />
                    <asp:Label ID="Label10" runat="server" Text="Ganancia: "></asp:Label>
                    <asp:Label ID="lblGanancia1" runat="server" Text=""></asp:Label><br />
                    <asp:Label ID="Label11" runat="server" Text="Dinero actual: "></asp:Label>
                    <asp:Label ID="lblDineroActual1" runat="server" Text=""></asp:Label>
                    <hr />
                     <asp:Label ID="Label12" runat="server" Text="Jugador 2: "></asp:Label>
                    <asp:Label ID="lblNombreRes2" runat="server" Text=""></asp:Label><br />
                    <asp:Label ID="Label14" runat="server" Text="Color Seleccionado: "></asp:Label>
                    <asp:Label ID="lblColorRes2" runat="server" Text=""></asp:Label><br />
                    <asp:Label ID="Label16" runat="server" Text="Ganancia: "></asp:Label>
                    <asp:Label ID="lblGanancia2" runat="server" Text=""></asp:Label><br />
                    <asp:Label ID="Label18" runat="server" Text="Dinero actual: "></asp:Label>
                    <asp:Label ID="lblDineroActual2" runat="server" Text=""></asp:Label>

                         <hr />
                     <asp:Label ID="Label13" runat="server" Text="Jugador 3: "></asp:Label>
                    <asp:Label ID="lblNombreRes3" runat="server" Text=""></asp:Label><br />
                    <asp:Label ID="Label17" runat="server" Text="Color Seleccionado: "></asp:Label>
                    <asp:Label ID="lblColorRes3" runat="server" Text=""></asp:Label><br />
                    <asp:Label ID="Label20" runat="server" Text="Ganancia: "></asp:Label>
                    <asp:Label ID="lblGanancia3" runat="server" Text=""></asp:Label><br />
                    <asp:Label ID="Label22" runat="server" Text="Dinero actual: "></asp:Label>
                    <asp:Label ID="lblDineroActual3" runat="server" Text=""></asp:Label>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Crud.aspx">Ir al CRUD</asp:LinkButton>
    </form>
</body>
</html>
