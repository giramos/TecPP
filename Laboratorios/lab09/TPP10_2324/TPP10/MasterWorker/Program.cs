using System;
using System.Diagnostics;
using System.IO;

namespace MasterWorker
{
    class Program
    {
        static void Main(string[] args)
        {

            const int maximoHilos = 50;
            short[] vector = CrearVectorAleatorio(1000000, -10, 10);
            MostrarLinea(Console.Out, "Num Hilos", "Ticks", "Resultado");

            //Toma de tiempos.
            Stopwatch stopWatch = new Stopwatch();

            for (int numeroHilos = 1; numeroHilos <= maximoHilos; numeroHilos++)
            {
                Master master = new Master(vector, numeroHilos);
                stopWatch.Restart();
                double resultado = master.CalcularModulo();
                stopWatch.Stop();

                MostrarLinea(Console.Out, numeroHilos, stopWatch.ElapsedTicks, resultado);

                //Entre ejecuciones, limpiamos y esperamos.
                GC.Collect();
                GC.WaitForFullGCComplete();
            }


        }
        public static short[] CrearVectorAleatorio(int numElementos, short menor, short mayor)
        {
            short[] vector = new short[numElementos];
            Random random = new Random();
            for (int i = 0; i < numElementos; i++)
                vector[i] = (short)random.Next(menor, mayor + 1);
            return vector;
        }

        static void MostrarLinea(TextWriter stream, string numHilosCabecera, string ticksCabecera, string resultadoCabecera)
        {
            stream.WriteLine("{0};{1};{2}", numHilosCabecera, ticksCabecera, resultadoCabecera);
        }

        static void MostrarLinea(TextWriter stream, int numHilos, long ticks, double resultado)
        {
            stream.WriteLine("{0};{1:N0};{2:N2}", numHilos, ticks, resultado);
        }

    }
}

