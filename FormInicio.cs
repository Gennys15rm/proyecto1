using System;
using System.Drawing;
using System.Windows.Forms;

namespace JuegoMatematico
{
    public class FormInicio : Form
    {
        TextBox txtJugador1 = new TextBox();
        TextBox txtJugador2 = new TextBox();
        ComboBox cmbModo = new ComboBox();
        Button btnIniciar = new Button();

        public FormInicio()
        {
            Text = "Juego Matemático - Inicio";
            Size = new Size(600, 430);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;

            var titulo = new Label { Text = "JUEGO MATEMÁTICO", Font = new Font("Segoe UI", 22, FontStyle.Bold), AutoSize = true, Location = new Point(155, 45) };
            var l1 = new Label { Text = "Nombre jugador 1:", AutoSize = true, Location = new Point(100, 125) };
            var l2 = new Label { Text = "Nombre jugador 2:", AutoSize = true, Location = new Point(100, 185) };
            var lm = new Label { Text = "Modalidad:", AutoSize = true, Location = new Point(100, 245) };

            txtJugador1.Location = new Point(250, 120); txtJugador1.Width = 230;
            txtJugador2.Location = new Point(250, 180); txtJugador2.Width = 230;
            cmbModo.Location = new Point(250, 240); cmbModo.Width = 230;
            cmbModo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbModo.Items.AddRange(new object[] { "Individual", "Dos jugadores" });
            cmbModo.SelectedIndex = 0;
            txtJugador2.Enabled = false;

            cmbModo.SelectedIndexChanged += (s, e) =>
            {
                txtJugador2.Enabled = cmbModo.SelectedIndex == 1;
                if (!txtJugador2.Enabled) txtJugador2.Clear();
            };

            btnIniciar.Text = "INICIAR PARTIDA";
            btnIniciar.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnIniciar.Size = new Size(230, 48);
            btnIniciar.Location = new Point(250, 300);
            btnIniciar.Click += Iniciar;

            Controls.AddRange(new Control[] { titulo, l1, l2, lm, txtJugador1, txtJugador2, cmbModo, btnIniciar });
        }

        void Iniciar(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtJugador1.Text))
            {
                MessageBox.Show("Ingrese el nombre del jugador 1.");
                return;
            }

            bool dos = cmbModo.SelectedIndex == 1;
            if (dos && string.IsNullOrWhiteSpace(txtJugador2.Text))
            {
                MessageBox.Show("Ingrese el nombre del jugador 2.");
                return;
            }

            var j1 = new Jugador(txtJugador1.Text.Trim());
            var j2 = dos ? new Jugador(txtJugador2.Text.Trim()) : null;
            Hide();
            new FormJuego(j1, j2, dos, this).Show();
        }
    }
}