using Datos;

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

            List<jugadores> listJugadores = new List<jugadores>();
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
        private bool ValidaCantidad(decimal cantidadApuesta, int idJugador)
        {
            if (cantidadApuesta < 1)
            {
                ShowMensajes("Debe ingresar una cantidad a apostar");
                return false;
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
                    return false;
                }
                else if (cantidadApuesta < ValorMinimo || cantidadApuesta > ValorMaximo)
                {
                    ShowMensajes($"El valor debe estar entre {Math.Round(ValorMaximo)} (8%)" +
                                   $" y {Math.Round(ValorMaximo)} (15%)");
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        protected void txtCantidad1_TextChanged(object sender, EventArgs e)
        {
            //ValidaCantidad(string.IsNullOrEmpty(txtCantidad1.Text) ? 0 : int.Parse(txtCantidad1.Text), int.Parse(ddlJugador1.SelectedValue));
        }

        protected void txtCantidad2_TextChanged(object sender, EventArgs e)
        {
            //ValidaCantidad(string.IsNullOrEmpty(txtCantidad2.Text) ? 0 : int.Parse(txtCantidad2.Text), int.Parse(ddlJugador1.SelectedValue));
        }

        protected void txtCantidad3_TextChanged(object sender, EventArgs e)
        {
            //ValidaCantidad(string.IsNullOrEmpty(txtCantidad1.Text) ? 0 : int.Parse(txtCantidad2.Text), int.Parse(ddlJugador1.SelectedValue));
        }

        public void ShowMensajes(string mensaje) => ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + mensaje + "');", true);

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (!string.IsNullOrEmpty(txtCantidad1.Text) && !string.IsNullOrEmpty(txtCantidad2.Text) && !string.IsNullOrEmpty(txtCantidad3.Text)
                && int.Parse(txtCantidad1.Text) > 0 && int.Parse(txtCantidad2.Text) > 0 && int.Parse(txtCantidad3.Text) > 0
                && int.Parse(ddlJugador1.SelectedValue) > 0 && int.Parse(ddlJugador2.SelectedValue) > 0 && int.Parse(ddlJugador3.SelectedValue) > 0
                  && ddlColor1.SelectedValue != "0" && ddlColor2.SelectedValue != "0" && ddlColor3.SelectedValue != "0")
            {
                var valida1 = ValidaCantidad(string.IsNullOrEmpty(txtCantidad1.Text) ? 0 : int.Parse(txtCantidad1.Text), int.Parse(ddlJugador1.SelectedValue));
                var valida2 = ValidaCantidad(string.IsNullOrEmpty(txtCantidad2.Text) ? 0 : int.Parse(txtCantidad2.Text), int.Parse(ddlJugador2.SelectedValue));
                var valida3 = ValidaCantidad(string.IsNullOrEmpty(txtCantidad3.Text) ? 0 : int.Parse(txtCantidad3.Text), int.Parse(ddlJugador3.SelectedValue));

                if (valida1 && valida2 && valida3)
                {
                    args.IsValid = true;
                    ShowMensajes("Correcto!!");
                }


            }
            else
            {

                args.IsValid = false;
                ShowMensajes("Debe rellenar todos los campos");
            }
        }
    }

    public enum Color
    {
        Verde,
        Rojo,
        Negro
    }




}
