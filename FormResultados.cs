using System;
using System.Drawing;
using System.Windows.Forms;

namespace JuegoMatematico
{
    public class FormResultados : Form
    {
        readonly Jugador j1, j2;
        readonly bool dos;
        readonly FormInicio inicio;
        readonly string[] nombres = new string[20];
        readonly int[] puntajes = new int[20];
        int cantidad;

        public FormResultados(Jugador a, Jugador b, bool modo, FormInicio f)
        {
            j1 = a; j2 = b; dos = modo; inicio = f;
            Text = "Juego Matemático - Resultados";
            Size = new Size(650, 520);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;

            var titulo = new Label { Text = "RESULTADOS", Font = new Font("Segoe UI", 22, FontStyle.Bold), AutoSize = true, Location = new Point(230, 35) };
            var datos = new Label { Font = new Font("Segoe UI", 12), AutoSize = true, Location = new Point(90, 100) };
            var ganador = new Label { Font = new Font("Segoe UI", 14, FontStyle.Bold), AutoSize = true, Location = new Point(90, 180) };
            var lista = new ListBox { Location = new Point(90, 245), Size = new Size(450, 130) };
            var cerrar = new Button { Text = "VOLVER AL INICIO", Location = new Point(220, 405), Size = new Size(200, 45) };

            Guardar(j1.Nombre, j1.Puntaje);
            string texto = $"{j1.Nombre}: {j1.Puntaje} puntos";
            if (dos)
            {
                Guardar(j2.Nombre, j2.Puntaje);
                texto += $"\r\n{j2.Nombre}: {j2.Puntaje} puntos";
                ganador.Text = j1.Puntaje == j2.Puntaje ? "🤝 ¡Empate!" :
                    "🏆 Ganador: " + (j1.Puntaje > j2.Puntaje ? j1.Nombre : j2.Nombre);
            }
            else ganador.Text = "Puntaje final: " + j1.Puntaje;

            datos.Text = texto;
            Ordenar();
            for (int i = 0; i < cantidad; i++) lista.Items.Add($"{i + 1}. {nombres[i]} - {puntajes[i]} puntos");

            cerrar.Click += (s, e) => { Close(); inicio.Show(); };
            FormClosed += (s, e) => inicio.Close();
            Controls.AddRange(new Control[] { titulo, datos, ganador, lista, cerrar });
        }

        void Guardar(string nombre, int puntaje)
        {
            if (cantidad < nombres.Length)
            {
                nombres[cantidad] = nombre;
                puntajes[cantidad] = puntaje;
                cantidad++;
            }
        }

        void Ordenar()
        {
            for (int i = 0; i < cantidad - 1; i++)
                for (int j = i + 1; j < cantidad; j++)
                    if (puntajes[j] > puntajes[i])
                    {
                        (puntajes[i], puntajes[j]) = (puntajes[j], puntajes[i]);
                        (nombres[i], nombres[j]) = (nombres[j], nombres[i]);
                    }
        }
    }
}