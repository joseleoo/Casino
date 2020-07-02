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
        Dictionary<int, Color> Colores = new Dictionary<int, Color>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargaColores();
                CargasListas();
            }
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

        }

        //        public string Ruleta(Dictionary<string, Color> colores)
        //        {
        ////            Random random = new Random();
        ////random.Next()


        ////            return "";
        //        }

        private void CargaColores()
        {
            Dictionary<int, Color> Colores = new Dictionary<int, Color>();

            Colores.Add(1, Color.Verde);
            Colores.Add(2, Color.Verde);
            for (int i = 3; i <= 51; i++)
            {
                Colores.Add(i, Color.Rojo);

            }

            for (int i = 52; i <= 100; i++)
            {
                Colores.Add(i, Color.Negro);

            }

            Session["Colores"] = Colores;
        }
        /// <summary>
        /// válida la cantidad de dinero a apostar
        /// </summary>
        /// <param name="cantidadApuesta">cantidad a apostar</param>
        /// <param name="idJugador"> id del jugador</param>
        private bool ValidaCantidad(TextBox txtCantidad, int idJugador)
        {
            if (idJugador == 0)
                return ResultadoValidaCantidad(txtCantidad, "Debe seleccionar un jugador");

            var cantidadApuesta = string.IsNullOrEmpty(txtCantidad.Text) ? 0 : decimal.Parse(txtCantidad.Text);

            if (cantidadApuesta < 1)
            {
                return ResultadoValidaCantidad(txtCantidad, "Debe ingresar una cantidad a apostar");
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
                    return ResultadoValidaCantidad(txtCantidad, "No tiene dinero para jugar");
                }
                else if (cantidadApuesta < ValorMinimo || cantidadApuesta > ValorMaximo)
                {
                    return ResultadoValidaCantidad(txtCantidad, $"El valor debe estar entre ${Math.Round(ValorMinimo, 1)} (8%)" +
                                   $" y ${Math.Round(ValorMaximo, 1)} (15%)");
                }
                else
                {
                    return true;
                }
            }
        }

        private bool ResultadoValidaCantidad(TextBox txtCantidad, string mensaje)
        {
            ShowMensajes(mensaje);
            txtCantidad.Focus();
            return false;
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                if (!ValidaCantidad(txtCantidad1, int.Parse(ddlJugador1.SelectedValue)))
                {
                    return;
                }

                if (!ValidaCantidad(txtCantidad2, int.Parse(ddlJugador2.SelectedValue)))
                {
                    return;
                }

                if (!ValidaCantidad(txtCantidad3, int.Parse(ddlJugador3.SelectedValue)))
                {
                    return;
                }

                #region Encabezado juego
                var rand = new Random();
                var ruleta = rand.Next(1, 100);
                var Colores = (Dictionary<int, Color>)Session["Colores"];
                var ColorRuleta = Colores[ruleta];
                lblColorRuleta.Text = ColorRuleta.ToString(); 
                #endregion

                string ganancia1;
                string dineroActual1;
                Jugar(ddlJugador1, ddlColor1, txtCantidad1, ColorRuleta, out ganancia1, out dineroActual1);
                lblNombreRes1.Text = ddlJugador1.SelectedItem.Text;
                lblColorRes1.Text = ddlColor1.SelectedItem.Text;
                lblDineroActual1.Text = dineroActual1;
                lblGanancia1.Text = ganancia1;

                string ganancia2;
                string dineroActual2;
                Jugar(ddlJugador2, ddlColor2, txtCantidad2, ColorRuleta, out ganancia2, out dineroActual2);
                lblNombreRes2.Text = ddlJugador2.SelectedItem.Text;
                lblColorRes2.Text = ddlColor2.SelectedItem.Text;
                lblDineroActual2.Text = dineroActual2;
                lblGanancia2.Text = ganancia2;

                string ganancia3;
                string dineroActual3;
                Jugar(ddlJugador3, ddlColor3, txtCantidad3, ColorRuleta, out ganancia3, out dineroActual3);
                lblNombreRes3.Text = ddlJugador3.SelectedItem.Text;
                lblColorRes3.Text = ddlColor3.SelectedItem.Text;
                lblDineroActual3.Text = dineroActual2;
                lblGanancia3.Text = ganancia2;
            }
        }

        /// <summary>
        /// realiza el jugego por un jugador
        /// </summary>
        /// <param name="dropJugador">lista del jugador</param>
        /// <param name="dropColor">lista del color</param>
        /// <param name="txtCantidadJugadorApuesta">cantidad</param>
        /// <param name="ColorRuleta">Color ramdom</param>
        /// <param name="stGanancia">donde quedara guardada la salida</param>
        /// <param name="stlblDineroActual">donde quedaará guardado el dinero acual</param>
        private void Jugar(DropDownList dropJugador, DropDownList dropColor, TextBox txtCantidadJugadorApuesta,
            Color ColorRuleta, out string stGanancia, out string stlblDineroActual)
        {
            casinoEntities casino = new casinoEntities();
            var idJugador = int.Parse(dropJugador.SelectedValue);
            var Jugador = casino.jugadores.SingleOrDefault(j => j.id == idJugador);

            decimal recuperado = CantidadRecuperadXColor(ColorRuleta);

            var cantidadApuesta = int.Parse(txtCantidadJugadorApuesta.Text);
            var ganancia = recuperado * cantidadApuesta;
            var mensajeGanancia = "";

            if (dropColor.SelectedItem.Text == ColorRuleta.ToString())
            {
                Jugador.cantidad = Jugador.cantidad + ganancia;
                mensajeGanancia = $"{recuperado} veces lo apostado: ${Math.Round(ganancia, 1)}";
            }
            else
            {
                Jugador.cantidad = Jugador.cantidad - cantidadApuesta;
                mensajeGanancia = "$0";
            }

            casino.SaveChanges();
            stGanancia = mensajeGanancia;
            stlblDineroActual = $"${Math.Round(Jugador.cantidad, 1)}";
        }

        /// <summary>
        /// devuelve la ganancia dependiedo del color
        /// </summary>
        /// <param name="ColorRuleta"></param>
        /// <returns></returns>
        private static decimal CantidadRecuperadXColor(Color ColorRuleta)
        {
            decimal recuperado = 0;
            if (ColorRuleta == Color.Verde)
                recuperado = 15;
            else if (ColorRuleta == Color.Rojo || ColorRuleta == Color.Negro)
                recuperado = 2;
            return recuperado;
        }

    }

    public enum Color
    {
        Verde,
        Rojo,
        Negro
    }




}
