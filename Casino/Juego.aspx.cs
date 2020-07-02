using Datos;
using Logica;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Casino
{
    public partial class Juego : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CargaColores();
            CargasListas();
        }

        private void CargasListas()
        {


            List<jugadores> jugadorSeleccione = new List<jugadores>();
            jugadorSeleccione.Add(new jugadores { nombre = "Seleccione", cantidad = 0, id = 0 });

            List<jugadores> listJugadores= new List<jugadores>();
            listJugadores.AddRange(jugadorSeleccione);

            casinoEntities casino = new casinoEntities();
            listJugadores.AddRange(from jugador in casino.jugadores
                                 select jugador);

            

            ddlJugador3.DataValueField = ddlJugador2.DataValueField = ddlJugador1.DataValueField = "Id";
            ddlJugador3.DataTextField = ddlJugador2.DataTextField = ddlJugador1.DataTextField = "Nombre";
            ddlJugador3.DataSource = ddlJugador2.DataSource = ddlJugador1.DataSource = listJugadores;
            ddlJugador1.DataBind();
            ddlJugador2.DataBind();
            ddlJugador3.DataBind();

            ddlColor3.DataSource = ddlColor2.DataSource = ddlColor1.DataSource;


        }

//        public string Ruleta(Dictionary<string, Color> colores)
//        {
////            Random random = new Random();
////random.Next()


////            return "";
//        }

        private static void CargaColores()
        {
            Dictionary<string, Color> Colores = new Dictionary<string, Color>();

            Colores.Add(Guid.NewGuid().ToString(), Color.Verde);
            Colores.Add(Guid.NewGuid().ToString(), Color.Verde);
            for (int i = 1; i <= 49; i++)
            {
                Colores.Add(Guid.NewGuid().ToString(), Color.Rojo);
                Colores.Add(Guid.NewGuid().ToString(), Color.Negro);
            }

        }
        /// <summary>
        /// válida la cantidad de dinero a apostar
        /// </summary>
        /// <param name="cantidadApuesta">cantidad a apostar</param>
        /// <param name="idJugador"> id del jugador</param>
        private void ValidaCantidad(decimal cantidadApuesta, int idJugador)
        {
            if (cantidadApuesta < 1)
            {
                ShowMensajes("Debe ingresar una cantidad a apostar");
                return;
            }
            else
            {
                casinoEntities casino = new casinoEntities();

                var Cantidad = (from jugador in casino.jugadores
                                where jugador.id == idJugador
                                select jugador.cantidad).First();

                decimal ValorMinimo, ValorMaximo = 0;
                ValorMinimo = (Cantidad * 8) / 100;
                ValorMaximo = (Cantidad * 15) / 100;

                if (Cantidad == 0)
                {
                    ShowMensajes("No tiene dinero para jugar");
                    return;
                }
                else if (cantidadApuesta < ValorMinimo || cantidadApuesta > ValorMaximo)
                {
                    ShowMensajes($"El valor debe estar entre {Math.Round(ValorMaximo)} (8%)" +
                                   $" y {Math.Round(ValorMaximo)} (15%)");
                    return;
                }
                else
                {
                    ShowMensajes("Si puede jugar");
                }
            }
        }

        protected void txtCantidad1_TextChanged(object sender, EventArgs e)
        {
            ValidaCantidad(string.IsNullOrEmpty(txtCantidad1.Text) ? 0 : int.Parse(txtCantidad1.Text), int.Parse(ddlJugador1.SelectedValue));
        }

        protected void txtCantidad2_TextChanged(object sender, EventArgs e)
        {
            ValidaCantidad(string.IsNullOrEmpty(txtCantidad2.Text) ? 0 : int.Parse(txtCantidad2.Text), int.Parse(ddlJugador1.SelectedValue));
        }

        protected void txtCantidad3_TextChanged(object sender, EventArgs e)
        {
            ValidaCantidad(string.IsNullOrEmpty(txtCantidad1.Text) ? 0 : int.Parse(txtCantidad2.Text), int.Parse(ddlJugador1.SelectedValue));
        }

        public void ShowMensajes(string mensaje) => ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + mensaje + "');", true);
    }

    public enum Color
    {
        Verde,
        Rojo,
        Negro
    }




}
