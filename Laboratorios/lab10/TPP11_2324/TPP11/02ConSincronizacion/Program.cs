using System;
using System.Threading;

namespace _02ConSincronizacion
{
    class Program
    {
        public static readonly ConsoleColor[] colores = {
                    ConsoleColor.DarkBlue,  ConsoleColor.DarkGreen,  ConsoleColor.DarkCyan,
                    ConsoleColor.DarkRed, ConsoleColor.DarkMagenta,  ConsoleColor.DarkYellow,  ConsoleColor.Gray,
                    ConsoleColor.DarkGray,  ConsoleColor.Blue,  ConsoleColor.Green,  ConsoleColor.Cyan,  ConsoleColor.Red,
                    ConsoleColor.Magenta,  ConsoleColor.Yellow, ConsoleColor.White
                    };

        static void Main()
        {
            // Cada hilo se encarga de un objeto Color.
            // Invocará al método Show de ese objeto.
            Thread[] hilos = new Thread[colores.Length];
            for (int i = 0; i < colores.Length; i++)
            {
                ColorSincro color = new ColorSincro(colores[i]);
                hilos[i] = new Thread(color.Show);
            }
            foreach (Thread hilo in hilos)
            {
                hilo.Start();
            }

        }
    }
}
