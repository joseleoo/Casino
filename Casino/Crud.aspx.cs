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
                    switch (hdfModificando.Value)
                    {
                        case nameof(opera.modifica):
                            var Id =int.Parse( hdfID.Value);
                            var Jugador = casino.jugadores.SingleOrDefault(j => j.id == Id);            
                            Jugador.nombre = txtJugador.Text;
                            break;
                        default:
                            casino.jugadores.Add(new jugadores { nombre = txtJugador.Text, cantidad = 10000 });
                            break;
                    }

                  VerificaTran(casino);
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

        private void Limpiar()
        {
            txtJugador.Text = "";
            hdfModificando.Value = "";
            CargarGrid();
        }

        public void ShowMensajes(string mensaje) =>ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + mensaje + "');", true);

        private void CargarGrid()
        {
            casinoEntities casino = new casinoEntities();
            var listJugadores = (from jugador in casino.jugadores
                                 select jugador).ToList();

            gvjugadores.DataSource = listJugadores;
            gvjugadores.DataBind();

        }

        public enum opera
        {
            modifica,
            inserta
        }

        protected void gvjugadores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            txtJugador.Text = "";
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvjugadores.Rows[index];
            hdfID.Value = gvjugadores.DataKeys[index].Values["id"].ToString();
            var Id = int.Parse(hdfID.Value);
            casinoEntities casino = new casinoEntities();
            var Jugador = casino.jugadores.SingleOrDefault(j => j.id == Id);

            if (e.CommandName == "Modificar")
            { 
                txtJugador.Text = Jugador.nombre;
                hdfModificando.Value = nameof(opera.modifica);
            }
            else if (e.CommandName == "Eliminar")
            {
                casino.jugadores.Remove(Jugador);              
                VerificaTran(casino);
            }
         
        }

        /// <summary>
        /// /verifica si la transaccion fu exitoisa
        /// </summary>
        /// <param name="casino"></param>
        private void VerificaTran(casinoEntities casino)
        {
            if (casino.SaveChanges() > 0)
            {
                ShowMensajes("Transaccion  exitosa");
            }
            else
            {
                ShowMensajes("No se pudo intente de nuevo");
            }
            Limpiar();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
    }
}
