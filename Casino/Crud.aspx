<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Crud.aspx.cs" Inherits="Casino.Crud" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:Label runat="server" Text="Nombre del jugador"></asp:Label>
            <asp:TextBox runat="server" placeholder="ingrese nombre del jugador" ID="txtJugador"></asp:TextBox>
            <asp:Button runat="server" Text="Guardar" ID="btnGuardar" OnClick="btnGuardar_Click" />
        </div>

        <div>
            <asp:GridView ID="gvjugadores" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-striped" DataKeyNames="id" GridLines="None" OnRowCommand="gvUsuarios_RowCommand">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnEliminar" runat="server" CssClass="btn btn-default btn-xs" CommandName="EliminarUsu" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ValidationGroup="SinValidacion" ToolTip="Eliminar registro">
                                            <i aria-hidden="true" class="glyphicon glyphicon-trash"></i>
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" />
                    <asp:BoundField DataField="Nombre" HeaderText="Jugador" SortExpression="Jugador" />
                    <asp:BoundField DataField="cantidad" HeaderText="Cantidad" SortExpression="Cantidad" />

                </Columns>
                <EmptyDataTemplate>No existen registros.</EmptyDataTemplate>
                <PagerStyle CssClass="bs-pagination" />
            </asp:GridView>

        </div>
    </form>
</body>
</html>
