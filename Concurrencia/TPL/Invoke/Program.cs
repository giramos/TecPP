using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Invoke
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //            . Empleando el procedimiento más adecuado de TPL, impleméntese un programa que,
            //a partir de un IEnumerable de enteros, reste la suma de la primera mitad del array a la suma de
            //la segunda mitad del array, sin emplear LINQ(1, 75 puntos).Implemente un equivalente en
            //PLINQ(0, 75 puntos).Ambos apartados deben probarse con un array de 5000 elementos de
            //valores entre 1 y 5.

                            // RESTA DE LA SUMA DE LAS MITADES DE UN ARRAY

            int[] v = { 1, 2, 3, 4 };
            Stopwatch sw = new Stopwatch();

            sw.Start();
            int diferencia = DiferenciaMitadesVector(v);
            sw.Stop();
            Console.WriteLine("\nLa diferencia de la primera mitad menos la segunda es {0}. Tiempo: {1} msg.", diferencia, sw.ElapsedMilliseconds);

            sw.Restart();

            //            Empleando la paralelización de tareas en TPL y a partir de una lista de 10000 palabras
            //con longitudes entre 3 y 8 caracteres, obtener en dos variables de tipo long(2, 00 puntos):
            // Número de palabras de longitud impar que empiezan por consonante.
            // Número de palabras de longitud par que empiezan por vocal.
            //Imprímase por pantalla el valor de las variables anteriores.Imprímase por pantalla el número
            //de hilos utilizados por TPL para resolver el cálculo

                        // LISTA DE PALABRAS PAR QUE EMPIEZAN POR VOCAL Y LISTA DE PALABRAS IMPARES QUE EMPIEZAN POR CONSONANTE

            var palabras = GenerarPalabras(10000, 3, 8);
            int imparConsonante = 0;
            int parVocal = 0;
            List<int> threadIds = new();


            Parallel.Invoke(
                () => imparConsonante = ContarPalabrasImparConsonante(palabras),
                () => parVocal = ContarPalabrasParVocal(palabras)
                );

            // Imprime los resultados
            Console.WriteLine("Número de palabras de longitud impar que empiezan por consonante: " + imparConsonante);
            Console.WriteLine("Número de palabras de longitud par que empiezan por vocal: " + parVocal);



            sw.Restart();

            //            Calcular la suma vectorial de dos vectores usando la siguiente
            //estrategia: con Invoke lanzar dos tareas en paralelo de modo que una de ellas
            //procese las posiciones pares y otra las impares.

                        // SUMA VECTORIAL DE DOS VECTORES : 1 TAREA POSICIONES PARES 2 TAREA POSICIONES IMPARES

            int[] vector1 = { 1, 2, 3, 4, 5 };
            int[] vector2 = { 6, 7, 8, 9, 10 };
            int[] resultado = new int[vector1.Length];

            // Lanza dos tareas en paralelo para calcular la suma vectorial
            Parallel.Invoke(
                () => SumaPosicionesPares(vector1, vector2, resultado),
                () => SumaPosicionesImpares(vector1, vector2, resultado)
            );

            // Imprime el resultado
            Console.WriteLine("Resultado de la suma vectorial: [" + string.Join(", ", resultado) + "]");

            sw.Restart();

            //            Calcula la diferencia / distancia entre dos vectores siguiendo: una tarea procesa los
            //elementos en posiciones pares de A y otra las impares, cada una calcula un resultado parcial.Al acabar las
            //dos tareas se usan los resultados parciales para obtener el resultado final.Cuidar la implementación para
            //que no sea necesario testear si una posición determinada es par o impar.

                        // DIFERENCIA DISTANCIA ENTRE DOS VECTORES

            double[] arrayA = { 3.0, 2.0, 4.0, 5.0 };
            double[] arrayB = { 1.0, 3.0 };

            // Variables para almacenar los resultados parciales
            double resultadoParcialParesA = double.MaxValue;
            double resultadoParcialParesB = double.MaxValue;

            // Crear y ejecutar las tareas en paralelo
            Parallel.Invoke(
                () => resultadoParcialParesA = CalcularDiferenciaMinimaPares(arrayA, arrayB),
                () => resultadoParcialParesB = CalcularDiferenciaMinimaPares(arrayB, arrayA)
            );

            // Obtener el resultado final
            double resultadoFinal = Math.Max(resultadoParcialParesA, resultadoParcialParesB);

            // Imprimir el resultado final
            Console.WriteLine("La máxima diferencia mínima en valor absoluto es: {0}", resultadoFinal);

        }
        static double CalcularDiferenciaMinimaPares(double[] arrayPar, double[] arrayImpar)
        {
            double diferenciaMinima = double.MaxValue;

            foreach (double a in arrayPar)
            {
                foreach (double b in arrayImpar)
                {
                    double diferencia = Math.Abs(a - b);
                    if (diferencia < diferenciaMinima)
                    {
                        diferenciaMinima = diferencia;
                    }
                }
            }

            return diferenciaMinima;
        }



        static void SumaPosicionesPares(int[] vector1, int[] vector2, int[] resultado)
        {
            for (int i = 0; i < vector1.Length; i += 2)
            {
                resultado[i] = vector1[i] + vector2[i];
            }
        }

        static void SumaPosicionesImpares(int[] vector1, int[] vector2, int[] resultado)
        {
            for (int i = 1; i < vector1.Length; i += 2)
            {
                resultado[i] = vector1[i] + vector2[i];
            }
        }

        //private static List<int> NumHilos(List<int> threadIds)
        //{
        //    lock (threadIds)
        //    {
        //        if (!threadIds.Contains(Thread.CurrentThread.ManagedThreadId))
        //            threadIds.Add(Thread.CurrentThread.ManagedThreadId);
        //    }
        //    return threadIds;
        //}

        private static int ContarPalabrasParVocal(List<string> palabras)
        {
            return palabras.AsParallel().Count(p => p.Length % 2 == 0 && EsVocal(p[0]));
        }

        private static int ContarPalabrasImparConsonante(List<string> palabras)
        {
            return palabras.AsParallel().Count(p => p.Length % 2 != 0 && EsConsonante(p[0]));
        }

        private static bool EsVocal(char letra)
        {
            return "aeiou".Contains(char.ToLower(letra));
        }
        static bool EsConsonante(char letra)
        {
            return "bcdfghjklmnpqrstvwxyz".Contains(char.ToLower(letra));
        }

        //private static int DiferenciaMitadesVector(int[] universe)
        //{
        //    int mitad = universe.Length / 2;
        //    int sumaPri = 0;
        //    int sumaSeg = 0;
        //    for(int i = 0; i < mitad; i++)
        //    {
        //        sumaPri += universe[i];
        //    }
        //    for(int i = mitad; i < universe.Length; i++)
        //    {
        //        sumaSeg += universe[i];
        //    }
        //    return sumaPri - sumaSeg;
        //}
        private static int DiferenciaMitadesVector(int[] universe)
        {
            int mitad = universe.Length / 2;
            int sumaPri = 0;
            int sumaSeg = 0;
            List<int> threadIds = new List<int>();

            Parallel.Invoke(
                () =>
                {
                    for (int i = 0; i < mitad; i++)
                    {
                        sumaPri += universe[i];
                        lock (threadIds)
                        {
                            if (!threadIds.Contains(Thread.CurrentThread.ManagedThreadId))
                                threadIds.Add(Thread.CurrentThread.ManagedThreadId);
                        }
                    }
                },
                () =>
                {
                    for (int i = mitad; i < universe.Length; i++)
                    {
                        sumaSeg += universe[i];
                        lock (threadIds)
                        {
                            if (!threadIds.Contains(Thread.CurrentThread.ManagedThreadId))
                                threadIds.Add(Thread.CurrentThread.ManagedThreadId);
                        }
                    }
                }
            );

            Console.WriteLine($"Número total de hilos: {threadIds.Count}");

            return sumaPri - sumaSeg;
        }


        static int[] GenerarVectorAleatorio(int min, int max, int tam)
        {
            Random random = new Random();
            int[] vector = new int[tam];

            for (int i = 0; i < tam; i++)
                vector[i] = random.Next(min, max + 1);

            return vector;
        }

        static List<string> GenerarPalabras(int cantidad, int longitudMin, int longitudMax)
        {
            Random random = new Random();
            List<string> palabras = new List<string>();

            for (int i = 0; i < cantidad; i++)
            {
                int longitud = random.Next(longitudMin, longitudMax + 1);
                string palabra = new string(Enumerable.Range(0, longitud).Select(_ => (char)random.Next('a', 'z' + 1)).ToArray());
                palabras.Add(palabra);
            }

            return palabras;
        }
    }
}
