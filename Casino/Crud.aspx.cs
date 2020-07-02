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
                            var Id = int.Parse(hdfID.Value);
                            var Jugador = casino.jugadores.SingleOrDefault(j => j.id == Id);
                            if (Jugador.nombre != txtJugador.Text)
                            {
                                Jugador.nombre = txtJugador.Text;
                                Jugador.cantidad = txtJugador0.Text == "" ? 0 : int.Parse(txtJugador0.Text);
                            }
                            else
                            {
                                ShowMensajes("Modifique un nuevo nombre del jugador");
                                return;
                            }

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
            catch (Exception)
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

        public void ShowMensajes(string mensaje) => ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + mensaje + "');", true);

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
                txtJugador0.Text = Math.Round(Jugador.cantidad).ToString();
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
        /// <param name="casino">entidad a guardar</param>
        public void VerificaTran(casinoEntities casino)
        {
            if (casino.SaveChanges() > 0)
            {
                ShowMensajes("Cambios guardados exitosamente");
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
