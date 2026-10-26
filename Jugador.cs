namespace JuegoMatematico
{
    public class Jugador
    {
        public string Nombre { get; set; }
        public int Puntaje { get; set; }
        public int Aciertos { get; set; }
        public int Errores { get; set; }
        public int Vidas { get; set; }
        public int Nivel { get; set; }

        public Jugador(string nombre)
        {
            Nombre = nombre;
            Vidas = 3;
            Nivel = 1;
        }
    }
}