using Datos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Casino
{
    public partial class Crud : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargarGrid();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            casinoEntities casino = new casinoEntities();

            try
            {
                if (txtJugador.Text != "")
                {
                    casino.jugadores.Add(new jugadores { nombre = txtJugador.Text, cantidad = 10000 });
                    var res = casino.SaveChanges();
                    if (res > 0)
                    {
                        ShowMensajes("jugador guardado exitosamente");
                    }
                    txtJugador.Text = "";
                    CargarGrid();
                }
                else
                {
                    ShowMensajes("Escriba el nombre del jugador");
                    txtJugador.Focus();
                    
                }
            }
            catch (Exception ex)
            {
                ShowMensajes("Error al guardar intente de nuevo");
            }
        }

        public void ShowMensajes(string mensaje) =>
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + mensaje + "');", true);

        private void CargarGrid()
        {
            casinoEntities casino = new casinoEntities();
            var listJugadores = (from jugador in casino.jugadores
                                 select jugador).ToList();

            gvjugadores.DataSource = listJugadores;

            gvjugadores.DataBind();
       
        }

        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            String strCodUsu = String.Empty;

            if (e.CommandName == "EliminarUsu")
            {
                //int index = Convert.ToInt32(e.CommandArgument);
                //GridViewRow row = gvUsuarios.Rows[index];

                //strCodUsu = gvUsuarios.DataKeys[index].Values["CodUsuario"].ToString();

                //if (clsCNEmpresasUsu.eliminarCNEmpresasUsu(txtCodigo.Text, strCodUsu, strCon))
                //{
                //    cargarGridUsuarios();
                //    ddlUsuario.Focus();
                //    mostrarMensaje(ucMensajesUsu, "Permisos derogados correctamente, Para la empresa: " + txtCodigo.Text + " Usuario: " + strCodUsu, Controls_Mensajes.DisplayOption.Success);
                //}
            }
        }
    }
}