using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;

namespace _01Division.Datos.Secuencial
{
    class Program
    {
        /// <summary>
        /// Versión secuencial de un programa que recupera todos los jpg de un directorio,
        /// los rota 180 grados y los almacena en un nuevo directorio.
        /// </summary>
        static void Main()
        {

            string[] ficheros = Directory.GetFiles(@"..\..\..\..\pics", "*.jpg");
            string nuevaCarpeta = @"..\..\..\..\pics\rotated";
            Directory.CreateDirectory(nuevaCarpeta);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            foreach (string fichero in ficheros)
            {
                string nombreFichero = Path.GetFileName(fichero);
                using (Bitmap bitmap = new Bitmap(fichero))
                {

                    Console.WriteLine("Procesando el fichero \"{0}\" con el hilo ID {1}.", nombreFichero, Thread.CurrentThread.ManagedThreadId);
                    bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    bitmap.Save(Path.Combine(nuevaCarpeta, nombreFichero));
                }
            }
            sw.Stop();
            Console.WriteLine("Tiempo: {0} ms.", sw.ElapsedMilliseconds);
        }
    }
}
