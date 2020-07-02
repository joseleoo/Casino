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
                CargarSesionesResultados();

            }

        }

        private void CargarSesionesResultados()
        {
            if (Session["jugando"] != null)
            {
                if ((int)Session["jugando"] == 1)
                {
                    FinalizarJuego();
                    Session["jugando"] = 0;
                }
            }
            lblNombreRes1.Text = (string)Session["Nombre1"];
            lblDineroActual1.Text = (string)Session["dineroActual1"];
            lblGanancia1.Text = (string)Session["dineroActual1"];

            lblNombreRes2.Text = (string)Session["Nombre2"];
            lblDineroActual2.Text = (string)Session["dineroActual2"];
            lblGanancia2.Text = (string)Session["dineroActual2"];

            lblNombreRes3.Text = (string)Session["Nombre3"];
            lblDineroActual3.Text = (string)Session["dineroActual3"];
            lblGanancia3.Text = (string)Session["dineroActual3"];
        }

        private void CargasListas()
        {
            casinoEntities casino = new casinoEntities();
            #region colores

            List<ValorColor> lstColores = new List<ValorColor>();
            lstColores.Add(new ValorColor { nombre = "Seleccione", id = 0 });
            lstColores.AddRange(from color in casino.ValorColor
                                select color);

            ddlColor3.DataValueField = ddlColor2.DataValueField = ddlColor1.DataValueField = "id";
            ddlColor3.DataTextField = ddlColor2.DataTextField = ddlColor1.DataTextField = "nombre";
            ddlColor3.DataSource = ddlColor2.DataSource = ddlColor1.DataSource = lstColores;
            ddlColor1.DataBind();
            ddlColor2.DataBind();
            ddlColor3.DataBind();
            #endregion
            #region jugadores

            List<jugadores> jugadorSeleccione = new List<jugadores>();
            jugadorSeleccione.Add(new jugadores { nombre = "Seleccione", cantidad = 0, id = 0 });
            List<jugadores> listJugadores = new List<jugadores>();
            listJugadores.AddRange(jugadorSeleccione);
            listJugadores.AddRange(from jugador in casino.jugadores
                                   select jugador);

            ddlJugador3.DataValueField = ddlJugador2.DataValueField = ddlJugador1.DataValueField = "Id";
            ddlJugador3.DataTextField = ddlJugador2.DataTextField = ddlJugador1.DataTextField = "Nombre";
            ddlJugador3.DataSource = ddlJugador2.DataSource = ddlJugador1.DataSource = listJugadores;
                ddlJugador1.DataBind();
                ddlJugador2.DataBind();
                ddlJugador3.DataBind(); 
            #endregion

        }

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

                ddlJugador3.Enabled = ddlJugador2.Enabled = ddlJugador1.Enabled = false;

                #region Encabezado juego
                Timer1.Enabled = true;
                var rand = new Random();
                var ruleta = rand.Next(1, 100);
                var Colores = (Dictionary<int, Color>)Session["Colores"];
                var ColorRuleta = Colores[ruleta];
                lblColorRuleta.Text = ColorRuleta.ToString();

                Session["jugando"] = 1;
                #endregion

                string ganancia1;
                string dineroActual1;
                Jugar(ddlJugador1, ddlColor1, txtCantidad1, ColorRuleta, out ganancia1, out dineroActual1);
                Session["Nombre1"] = lblNombreRes1.Text = ddlJugador1.SelectedItem.Text;
                Session["dineroActual1"] = lblDineroActual1.Text = dineroActual1;
                Session["ganancia1"] = lblGanancia1.Text = ganancia1;
                lblColorRes1.Text = ddlColor1.SelectedItem.Text;


                string ganancia2;
                string dineroActual2;
                Jugar(ddlJugador2, ddlColor2, txtCantidad2, ColorRuleta, out ganancia2, out dineroActual2);
                Session["Nombre2"] = lblNombreRes2.Text = ddlJugador2.SelectedItem.Text;
                Session["dineroActual2"] = lblDineroActual2.Text = dineroActual2;
                Session["ganancia2"] = lblGanancia2.Text = ganancia2;
                lblColorRes2.Text = ddlColor2.SelectedItem.Text;

                string ganancia3;
                string dineroActual3;
                Jugar(ddlJugador3, ddlColor3, txtCantidad3, ColorRuleta, out ganancia3, out dineroActual3);
                Session["Nombre3"] = lblNombreRes3.Text = ddlJugador3.SelectedItem.Text;
                Session["dineroActual3"] = lblDineroActual3.Text = dineroActual3;
                Session["ganancia3"] = lblGanancia3.Text = ganancia3;
                lblColorRes3.Text = ddlColor3.SelectedItem.Text;
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

        public void FinalizarJuego()
        {
            divJugadores.Visible = false;
            divFinalJuego.Visible = true;
            Button1.Enabled = false;
            Timer1.Enabled = false;
            Session["jugando"] = 0;
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            FinalizarJuego();
        }
    }

    public enum Color
    {
        Verde,
        Rojo,
        Negro
    }




}
