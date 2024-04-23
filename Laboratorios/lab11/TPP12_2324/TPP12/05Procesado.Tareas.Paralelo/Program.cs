using _04Procesado.Tareas.Secuencial;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace _05Procesado.Tareas.Paralelo
{
    /// <summary>
    /// Versión Paralelizada de Procesado.Tareas.Secuencial
    /// Paralelización de Tareas
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            String texto = ProcesadorTextos.LeerFicheroTexto(@"..\..\..\..\clarin.txt");
            string[] palabras = ProcesadorTextos.DividirEnPalabras(texto);



            //Variables que serán utilizadas por las tareas:

            string[] palabrasMasLargas = null, palabrasMasCortas = null, palabrasAparecenMasVeces = null, palabrasAparecenMenosVeces = null;

            int masRepeticiones = 0, menosRepeticiones = 0, signosPuntuacion = 0;

            // El método Invoke de la clase Parallel se emplea en la paralelización de tareas independientes:
            // Recibe Actions - Haciendo uso de la palabra clave params : 
            //Esto nos permite ir pasando N parámetros separados por comas. En este caso Actions y, cada uno, es una tarea.

            // Crea potencialmente un hilo por cada tarea
            // Espera a que acaben todos los hilos

            //Nótese que al ser Actions no devuelven nada, por eso están almacenando los resultados en las variables definidas más arriba.

            Stopwatch sw = new Stopwatch();
            sw.Start();

            Parallel.Invoke(

                //!Ojo! ¿Y si X tareas utilizasen un recurso compartido?
                () => signosPuntuacion = ProcesadorTextos.ContarSignosPuntuacion(texto),
                () => palabrasMasLargas = ProcesadorTextos.PalabrasMasLargas(palabras),
                () => palabrasMasCortas = ProcesadorTextos.PalabrasMasCortas(palabras),
                () => palabrasAparecenMasVeces = ProcesadorTextos.PalabrasAparecenMasVeces(palabras, out masRepeticiones),
                () => palabrasAparecenMenosVeces = ProcesadorTextos.PalabrasAparecenMenosVeces(palabras, out menosRepeticiones)

            );

            sw.Stop();

            ProcesadorTextos.MostrarResultados(signosPuntuacion, palabrasMasCortas, palabrasMasLargas, palabrasAparecenMenosVeces, menosRepeticiones,
                palabrasAparecenMasVeces, masRepeticiones);

            Console.WriteLine("Tiempo: {0} ms.", sw.ElapsedMilliseconds);
        }
    }
}
