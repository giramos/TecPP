using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace TPL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //            Empleando el procedimiento más adecuado de TPL, impleméntese un programa que,
            //a partir de un IEnumerable de enteros, reste la suma de la primera mitad del array a la suma de
            //la segunda mitad del array, sin emplear LINQ(1, 75 puntos).Implemente un equivalente en
            //PLINQ(0, 75 puntos).Ambos apartados deben probarse con un array de 5000 elementos de
            //valores entre 1 y 5.

                    // DIFERENCIA DE LAS MITADES DE UN VECTOR

            int[] array = GenerarArray(5000, 1, 5);
            int[] v = { 1, 2, 3, 4 };
            Stopwatch sw = new Stopwatch();

            sw.Start();
            double diferencia = DiferenciaMitadesVector(v);
            sw.Stop();
            Console.WriteLine("\nLa diferencia de la primera mitad menos la segunda es {0}. Tiempo: {1} msg.", diferencia, sw.ElapsedMilliseconds);

            sw.Restart();

            // Diferencia / distancia entre dos vectores

                    // DIFERENCIA / DISTANCIA ENTRE DOS VECTORES

            double[] x = { 1.0, 2.0, 3.0 };
            double[] y = { 4.0, 5.0, 6.0 };
            sw.Start();
            double distancePLINQ = ComputeDistancePLINQ(x, y);
            sw.Stop();
            Console.WriteLine("Distancia calculada con PLINQ: {0}. Tiempo: {1}.", distancePLINQ, sw.ElapsedMilliseconds);
        }

        private static double ComputeDistancePLINQ(double[] x, double[] y)
        {
            return Math.Sqrt(x
           .Zip(y, (xi, yi) => (xi - yi) * (xi - yi))
           .AsParallel()
           .Sum());
        }

        // SUMA VECTORIAL PLINQ
        static int[] SumarVectoresPLINQ(int[] vectorA, int[] vectorB)
        {
            return vectorA.AsParallel()
                .Zip(vectorB, (a, b) => a + b)
                .ToArray();
        }

        private static double DiferenciaMitadesVector(int[] array)
        {
            int mitad = array.Length / 2;

            // Calcula la suma de la primera mitad y la suma de la segunda mitad en paralelo
            double sumaPrimeraMitad = array.AsParallel().Where((numero, indice) => indice < mitad).Sum();
            double sumaSegundaMitad = array.AsParallel().Where((numero, indice) => indice >= mitad).Sum();

            return sumaSegundaMitad - sumaPrimeraMitad;
        }

        static double VectorModulusLinq(IEnumerable<short> vector)
        {
            return Math.Sqrt(vector.Select(vi => (long)vi * vi)
                    .Aggregate((vi, vj) => vi + vj)
                );
        }

        static double VectorModulusPLinq_v1(IEnumerable<short> vector)
        {
            return Math.Sqrt(
                vector.AsParallel() // with just one aggregate
                    .Aggregate<short, long>(0, (acc, item) => acc + item * item)

                );
        }

        static double VectorModulusPLinq_v2(IEnumerable<short> vector)
        {
            return Math.Sqrt(
                vector.AsParallel()
                .Select(vi => (long)vi * vi)
                .Aggregate((acc, vi) => acc + vi)
                );
        }

        static double VectorModulusPLinqLocal_v1(IEnumerable<short> vector)
        {
            return Math.Sqrt(
                vector.AsParallel()
                    .Select(vi => (long)vi * vi)
                    .Aggregate<long, long, long>(() => 0,  //local init, also called seedFactory
                               (acc, vi) => acc + vi, //body: same as before
                               (acu1, acu2) => acu1 + acu2, //combine local acumulators
                               finalResult => finalResult) //final state


                );
        }
        static double VectorModulusPLinqLocal_v2(IEnumerable<short> vector)
        {
            return Math.Sqrt(
                vector.AsParallel()
                    // with just one aggregate with local acumulators
                    .Aggregate<short, long, long>(() => 0,  //local init, also called seedFactory
                               (acc, item) => acc + item * item, //body: same as before
                               (acu1, acu2) => acu1 + acu2, //combine local acumulators
                               finalResult => finalResult) //final state


                );
        }

        static int[] GenerarArray(int longitud, int min, int max)
        {
            Random random = new Random();
            int[] array = new int[longitud];
            for (int i = 0; i < longitud; i++)
            {
                array[i] = random.Next(min, max + 1);
            }
            return array;
        }
    }
}
