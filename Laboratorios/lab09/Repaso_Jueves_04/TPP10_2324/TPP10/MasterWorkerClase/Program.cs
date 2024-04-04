using System;
using System.IO;
using System.Runtime.Intrinsics;

namespace MasterWorkerClase
{
    class Program
    {
        // A través de 2 arrays de enteros (el tamaño del 2º es <= al del 1º)
        // Calcular el número de veces que ses repite el 2º array en el primero.
        // Suponer que tendrá un máximo de longitudV1/longitudV2 hilos
        // Ejemplo:
        // { 2, 2, 1, 3, 2, 2, 1, 2, 1, 2, 2, 1 } y { 2, 2, 1}
        // Resultado: 3
        static void Main(string[] args)
        {
            //short[] v1 = new short[] { 2, 2, 1, 3, 2, 2, 1, 2, 1, 2, 2, 1 };
            //short[] v2 = new short[] { 2, 2, 1 };
            short[] v1 = CrearVectorAleatorio(1000, 0, 4);
            short[] v2 = CrearVectorAleatorio(2, 0, 4);
            int maximoHilos = v1.Length / v2.Length; // Maximo de hilos permitidos
            MostrarLinea(Console.Out, "Num Hilos", "Ticks", "Resultado");
            for (int numeroHilos = 1; numeroHilos <= maximoHilos; numeroHilos++)
            {
                Master master = new Master(v1, v2, numeroHilos);
                DateTime antes = DateTime.Now;
                double resultado = master.Calcular();
                DateTime despues = DateTime.Now;
                MostrarLinea(Console.Out, numeroHilos, (despues - antes).Ticks, resultado);

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
