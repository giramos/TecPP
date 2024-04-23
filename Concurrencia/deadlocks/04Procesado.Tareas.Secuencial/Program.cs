using System;
using System.Diagnostics;

namespace _04Procesado.Tareas.Secuencial
{

    /// <summary>
    /// Ejemplo secuencial de un programa que procesa un fichero
    /// con diferentes tareas.
    /// </summary>
    class Program
    {
        static void Main()
        {
            String texto = ProcesadorTextos.LeerFicheroTexto(@"..\..\..\..\clarin.txt");
            string[] palabras = ProcesadorTextos.DividirEnPalabras(texto);

            Stopwatch sw = new Stopwatch();
            sw.Start();

            int signosPuntuacion = ProcesadorTextos.ContarSignosPuntuacion(texto);
            var palabrasMasLargas = ProcesadorTextos.PalabrasMasLargas(palabras);
            var palabrasMasCortas = ProcesadorTextos.PalabrasMasCortas(palabras);
            int masRepeticiones, menosRepeticiones;
            var palabrasAparecenMasVeces = ProcesadorTextos.PalabrasAparecenMasVeces(palabras, out masRepeticiones);
            var palabrasAparecenMenosVeces = ProcesadorTextos.PalabrasAparecenMenosVeces(palabras, out menosRepeticiones);
            sw.Stop();

            ProcesadorTextos.MostrarResultados(signosPuntuacion, palabrasMasCortas, palabrasMasLargas, palabrasAparecenMenosVeces, menosRepeticiones,
                palabrasAparecenMasVeces, masRepeticiones);

            Console.WriteLine("Tiempo: {0} ms.", sw.ElapsedMilliseconds);
        }
    }
}
