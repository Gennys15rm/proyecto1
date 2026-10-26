using System;

namespace JuegoMatematico
{
    public class Juego
    {
        private readonly Random random = new Random();
        public int Numero1 { get; private set; }
        public int Numero2 { get; private set; }
        public char Operacion { get; private set; }

        public int GenerarOperacion(int nivel)
        {
            int max = nivel switch { 1 => 10, 2 => 20, 3 => 50, _ => 100 };
            Numero1 = random.Next(1, max + 1);
            Numero2 = random.Next(1, max + 1);

            int op = nivel == 1 ? 0 : random.Next(nivel == 2 ? 2 : nivel == 3 ? 3 : 4);
            Operacion = op switch { 0 => '+', 1 => '-', 2 => '*', _ => '/' };

            if (Operacion == '/')
            {
                Numero2 = random.Next(1, 10);
                Numero1 = Numero2 * random.Next(1, 10);
            }
            return CalcularRespuesta();
        }

        public int CalcularRespuesta() => Operacion switch
        {
            '+' => Numero1 + Numero2,
            '-' => Numero1 - Numero2,
            '*' => Numero1 * Numero2,
            '/' => Numero1 / Numero2,
            _ => 0
        };

        public int ObtenerNivel(int aciertos) =>
            aciertos < 5 ? 1 : aciertos < 10 ? 2 : aciertos < 15 ? 3 : 4;
    }
}