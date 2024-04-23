using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace _02Division.Datos.Paralelo
{
    class Program
    {
        static readonly object obj = new object();
        /// <summary>
        /// TPL - Task Parallel Library o Librería de Tareas en Paralelo
        /// Simplifica la paralelización de aplicaciones:
        /// 
        /// Escala dinámicamente el nº de hilos en función del nº de CPUs
        /// 
        /// Ofrece servicios para la paralelización mediante la división de datos (data parallelism).
        /// 
        /// Ofrece servicios para la paralelización mediante tareas independientes (task parallelism).
        ///
        /// En este caso se resuelve Division.Datos.Secuencial con TPL (data parallelism)
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            string[] ficheros = Directory.GetFiles(@"..\..\..\..\pics", "*.jpg");
            string nuevaCarpeta = @"..\..\..\..\pics\rotated";
            Directory.CreateDirectory(nuevaCarpeta);
            List<int>threadIds = new ();
            
            Stopwatch sw = new Stopwatch();
            int cont = 1;

            // La siguiente tarea se realiza potencialmente en paralelo
            // Potencialmente se crean tantas tareas como elementos tiene el IEnumerable.

            //ForEach crea potencialmente un hilo por cada elemento de un IEnumerable

            // Recibe la tarea (Action) a ejecutar.
            sw.Start();
            Parallel.ForEach(ficheros, fichero => {
                string nombreFichero = Path.GetFileName(fichero);
                using (Bitmap bitmap = new Bitmap(fichero))
                {
                    //¿Y si hubiera recursos compartidos?
                    Console.WriteLine("Procesando el fichero \"{0}\" con el hilo ID {1}.", nombreFichero, Thread.CurrentThread.ManagedThreadId);
                    bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    bitmap.Save(Path.Combine(nuevaCarpeta, nombreFichero));
                }
                lock(obj)
                {
                    if (!threadIds.Contains(Thread.CurrentThread.ManagedThreadId))
                        threadIds.Add(Thread.CurrentThread.ManagedThreadId);
                }
            });            // TPL Espera a que acaben todos los hilos
            sw.Stop();
            //EJERCICIO ¿Cómo podríamos mostrar por pantalla el contador de hilos utilizados en el bloque anterior?
            Console.WriteLine($"Número total de hilos: {threadIds.Count}");

            Console.WriteLine("Tiempo: {0} ms.", sw.ElapsedMilliseconds);


            //Existe también un Parallel.For:
            //https://docs.microsoft.com/es-es/dotnet/api/system.threading.tasks.parallel.for?view=net-6.0#system-threading-tasks-parallel-for(system-int32-system-int32-system-action((system-int32-system-threading-tasks-parallelloopstate)))
            //que recibiría el índice de inicio y el de final (no se incluiría).
            // For crea potencialmente un hilo a partir de un índice de de comienzo y final, no incluyendo el final


        }

    }
}
