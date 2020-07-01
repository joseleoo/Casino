<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Crud.aspx.cs" Inherits="Casino.Crud" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

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
                    <asp:Label runat="server" Text="Nombre del jugador"></asp:Label>
                    <asp:TextBox runat="server" placeholder="ingrese nombre del jugador" ID="txtJugador"></asp:TextBox>
                    <asp:Button runat="server" Text="Guardar" ID="btnGuardar" OnClick="btnGuardar_Click" />
                    <asp:Button ID="btnLimpiar" runat="server" Text="Borrar" OnClick="btnLimpiar_Click" />
                </div>
                <hr />
                <div>
                    <asp:GridView ID="gvjugadores" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-striped" DataKeyNames="id" GridLines="None" OnRowCommand="gvjugadores_RowCommand">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnModificar" runat="server" CssClass="btn btn-default btn-xs" CommandName="Modificar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ValidationGroup="SinValidacion" ToolTip="Modificar registro">
                                            <i aria-hidden="true" class="glyphicon glyphicon-pencil"></i>Editar
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="10px" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnEliminar" runat="server" CssClass="btn btn-default btn-xs" CommandName="Eliminar" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ValidationGroup="SinValidacion" ToolTip="Eliminar registro">
                                            <i aria-hidden="true" class="glyphicon glyphicon-trash"></i>Eliminar
                                    </asp:LinkButton>
                                    <ajaxToolkit:ConfirmButtonExtender ID="lbtnEliminar_ConfirmButtonExtender" runat="server" ConfirmText="¿Desea eliminar registro?" TargetControlID="lbtnEliminar" />
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
                    <asp:HiddenField ID="hdfID" runat="server" />
                    <asp:HiddenField ID="hdfModificando" runat="server" />
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </form>


</body>
</html>
