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
                    return ResultadoValidaCantidad(txtCantidad, $"El valor debe estar entre ${Math.Round(ValorMinimo,1)} (8%)" +
                                   $" y ${Math.Round(ValorMaximo,1)} (15%)");
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

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //if (!string.IsNullOrEmpty(txtCantidad1.Text) && !string.IsNullOrEmpty(txtCantidad2.Text) && !string.IsNullOrEmpty(txtCantidad3.Text)
            //    && int.Parse(txtCantidad1.Text) > 0 && int.Parse(txtCantidad2.Text) > 0 && int.Parse(txtCantidad3.Text) > 0
            //    && int.Parse(ddlJugador1.SelectedValue) > 0 && int.Parse(ddlJugador2.SelectedValue) > 0 && int.Parse(ddlJugador3.SelectedValue) > 0
            //      && ddlColor1.SelectedValue != "0" && ddlColor2.SelectedValue != "0" && ddlColor3.SelectedValue != "0")
            //{
            //var valida1 = ValidaCantidad(txtCantidad1, int.Parse(ddlJugador1.SelectedValue));
            //if (valida1)
            //{
            //    args.IsValid = true;
            //    ShowMensajes("Correcto!!");
            //}
            //else
            //{

            //    args.IsValid = false;
            //    ShowMensajes("Debe rellenar todos los campos");
            //}



        }

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

                Juego();

            }
        }

        private void Juego()
        {
            casinoEntities casino = new casinoEntities();
            var idJugador = int.Parse(ddlJugador1.SelectedValue);
            var Jugador = casino.jugadores.SingleOrDefault(j => j.id == idJugador);

            var rand = new Random();
            var ruleta = rand.Next(1, 100);
            var Colores = (Dictionary<int, Color>)Session["Colores"];
            var ColorRuleta = Colores[ruleta];

            lblColorRuleta.Text = ColorRuleta.ToString();
            lblNombreRes1.Text = ddlJugador1.SelectedItem.Text;
            lblColorRes1.Text = ddlColor1.SelectedItem.Text;

            decimal recuperado = 0;
            if (ColorRuleta == Color.Verde)
                recuperado = 15;
            else if (ColorRuleta == Color.Rojo || ColorRuleta == Color.Negro)
                recuperado = 2;

            var cantidadApuesta = int.Parse(txtCantidad1.Text);
            var ganancia = recuperado * cantidadApuesta;
            var mensajeGanancia = "";
            if (ddlColor1.SelectedItem.Text == ColorRuleta.ToString())
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
            lblGanancia.Text = mensajeGanancia;
            lblDineroActual.Text = $"${Math.Round(Jugador.cantidad, 1)}";
        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            //var valida2 = ValidaCantidad(txtCantidad2, int.Parse(ddlJugador2.SelectedValue));
            //if ( valida2 )
            //{
            //    args.IsValid = true;
            //    ShowMensajes("Correcto!!");
            //}
            //else
            //{

            //    args.IsValid = false;
            //    ShowMensajes("Debe rellenar todos los campos");
            //}
        }

        protected void CustomValidator1_ServerValidate1(object source, ServerValidateEventArgs args)
        {
            //var valida3 = ValidaCantidad(txtCantidad3, int.Parse(ddlJugador3.SelectedValue));
            //if ( valida3)
            //{
            //    args.IsValid = true;
            //    ShowMensajes("Correcto!!");
            //}
            //else
            //{

            //    args.IsValid = false;
            //    ShowMensajes("Debe rellenar todos los campos");
            //}
        }
    }

    public enum Color
    {
        Verde,
        Rojo,
        Negro
    }




}
