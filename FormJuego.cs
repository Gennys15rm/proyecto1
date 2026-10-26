using System;
using System.Drawing;
using System.Windows.Forms;

namespace JuegoMatematico
{
    public class FormJuego : Form
    {
        readonly Jugador j1, j2;
        Jugador actual;
        readonly bool dos;
        readonly Juego juego = new Juego();
        readonly FormInicio inicio;
        int respuesta;
        Label lblJugador = new Label(), lblNivel = new Label(), lblPuntos = new Label(), lblVidas = new Label();
        Label lblOperacion = new Label(), lblMensaje = new Label();
        TextBox txtRespuesta = new TextBox();
        Button btn = new Button();

        public FormJuego(Jugador a, Jugador b, bool modo, FormInicio f)
        {
            j1 = a; j2 = b; dos = modo; actual = j1; inicio = f;
            Text = "Juego Matemático - Partida";
            Size = new Size(650, 500);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;

            var titulo = new Label { Text = "PARTIDA", Font = new Font("Segoe UI", 22, FontStyle.Bold), AutoSize = true, Location = new Point(270, 35) };
            lblJugador.Location = new Point(70, 100); lblJugador.AutoSize = true;
            lblNivel.Location = new Point(250, 100); lblNivel.AutoSize = true;
            lblPuntos.Location = new Point(390, 100); lblPuntos.AutoSize = true;
            lblVidas.Location = new Point(520, 100); lblVidas.AutoSize = true;

            lblOperacion.Font = new Font("Segoe UI", 28, FontStyle.Bold);
            lblOperacion.AutoSize = true; lblOperacion.Location = new Point(250, 170);

            txtRespuesta.Font = new Font("Segoe UI", 18);
            txtRespuesta.Width = 160; txtRespuesta.Location = new Point(245, 240);
            btn.Text = "RESPONDER"; btn.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btn.Size = new Size(160, 45); btn.Location = new Point(245, 295); btn.Click += Responder;

            lblMensaje.Font = new Font("Segoe UI", 12);
            lblMensaje.AutoSize = true; lblMensaje.Location = new Point(180, 365);

            Controls.AddRange(new Control[] { titulo, lblJugador, lblNivel, lblPuntos, lblVidas, lblOperacion, txtRespuesta, btn, lblMensaje });
            FormClosed += (s, e) => inicio.Close();
            Generar();
        }

        void Generar()
        {
            actual.Nivel = juego.ObtenerNivel(actual.Aciertos);
            respuesta = juego.GenerarOperacion(actual.Nivel);
            lblJugador.Text = "Jugador: " + actual.Nombre;
            lblNivel.Text = "Nivel: " + actual.Nivel;
            lblPuntos.Text = "Puntos: " + actual.Puntaje;
            lblVidas.Text = "Vidas: " + actual.Vidas;
            lblOperacion.Text = $"{juego.Numero1} {juego.Operacion} {juego.Numero2} = ?";
            lblMensaje.Text = "";
            txtRespuesta.Clear();
            txtRespuesta.Focus();
        }

        void Responder(object sender, EventArgs e)
        {
            if (!int.TryParse(txtRespuesta.Text, out int r))
            {
                MessageBox.Show("Ingrese un número válido.");
                return;
            }

            if (r == respuesta)
            {
                actual.Aciertos++; actual.Puntaje += 10;
                lblMensaje.Text = "¡Correcto! +10 puntos";
            }
            else
            {
                actual.Errores++; actual.Vidas--;
                lblMensaje.Text = "Incorrecto. Respuesta: " + respuesta;
            }

            if (actual.Vidas <= 0)
            {
                if (dos && actual == j1 && j2.Vidas > 0)
                {
                    actual = j2;
                    MessageBox.Show("Turno de " + j2.Nombre);
                    Generar();
                    return;
                }
                MostrarResultados();
                return;
            }

            if (dos) actual = actual == j1 ? j2 : j1;
            Generar();
        }

        void MostrarResultados()
        {
            Hide();
            new FormResultados(j1, j2, dos, inicio).Show();
        }
    }
}