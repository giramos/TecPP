using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using System.Threading;
using System.Threading.Tasks;

namespace For
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // POSICION DEL VALOR MINIMO DE UN VECTOR

            int[] vector = { 4, 5, 6, 1, 4, 5, 6, 1 };

            int valorMinimo = vector[0];
            int posicionMinima = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            posicionMinima = PosicionValorMinimo(vector, valorMinimo, posicionMinima);
            sw.Stop();
            Console.WriteLine("La posicion del valor minimo es {0} . Tiempo: {1}.", posicionMinima, sw.ElapsedMilliseconds);

            sw.Restart();

            // OCURRENCIA PRIMERA DE UN VALOR DADO EN UN VECTOR

            int valor = 6;
            sw.Start();
            int posicionOcurrencia = OcurrenciaValor(vector, valor);
            sw.Stop();
            Console.WriteLine("La primera posicion en la que aparece el valor {0} es la posicion {1} . Tiempo: {2}.", valor, posicionOcurrencia, sw.ElapsedMilliseconds);

            sw.Restart();

            //            . Empleando el procedimiento más adecuado de TPL, impleméntese un programa que,
            //a partir de un IEnumerable de enteros, reste la suma de la primera mitad del array a la suma de
            //la segunda mitad del array, sin emplear LINQ(1, 75 puntos).Implemente un equivalente en
            //PLINQ(0, 75 puntos).Ambos apartados deben probarse con un array de 5000 elementos de
            //valores entre 1 y 5.

            // RESTA DE LAS MITADES DE UN VECTOR

            int[] v = { 1, 2, 3, 4 };

            sw.Start();
            int diferencia = DiferenciaMitadesVector(v);
            sw.Stop();
            Console.WriteLine("\nLa diferencia de la primera mitad menos la segunda es {0}. Tiempo: {1} msg.", diferencia, sw.ElapsedMilliseconds);

            sw.Restart();


            //            Calcular la suma de los cuadrados de los elementos de valor impar
            //de un vector, usando Parallel.For o Parallel.ForEach más el método
            //adecuado de Interlocked.

            // SUMA DE LOS CUADRADOS DE LOS ELEMENTOS DE UN VECTOR DE VALOR IMPAR

            int[] vec = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            sw.Start();
            long sumaCuadradosImpares = CalcularSumaCuadradosImpares(vec);
            sw.Stop();
            Console.WriteLine("La suma de los cuadrados de los elementos de valor impar es: {0}. Tiempo : {1}", sumaCuadradosImpares, sw.ElapsedMilliseconds);

            sw.Reset();


            //            Dado una matriz de números enteros rectangular, calcular de forma paralela dos listas: una con los
            //valores positivos y otra con los valores negativos.
            //
            //Matriz A
            //-5 - 8 - 1 6 3
            //            - 1 - 3 9 - 8 3
            //- 10 - 5 - 4 10 4
            //3 - 5 2 4 4
            //[Salida esperada]
            //Lista1-> - 10 - 8 - 8 - 5 - 5 - 5 - 4 - 3 - 1 - 1
            //Lista2-> 2 3 3 3 4 4 4 6 9 10
            //
            //Realizar el ejercicio anterior usando TPL. El número de hilos lo determinará TPL

            // DADA UNA MATRIZ CALCULAR LISTA DE POSITIVOCS Y NEGATIVOS

            int[,] matriz = {
            {-5, -8, -1, 6, 3},
            {-1, -3, 9, -8, 3},
            {-10, -5, -4, 10, 4},
            {3, -5, 2, 4, 4}
                };

            var listaNegativos = new ConcurrentBag<int>();
            var listaPositivos = new ConcurrentBag<int>();

            CalcularPositivosNegativosMatriz(matriz, listaPositivos, listaNegativos);

            Console.WriteLine("Lista1 (negativos): " + string.Join(" ", listaNegativos));
            Console.WriteLine("Lista2 (positivos): " + string.Join(" ", listaPositivos));

            sw.Restart();


            // DIFERENCIA / DISTANCIA ENTRE DOS VECTORES

            //Calcular la distancia entre vectores
            double[] x = { 1.0, 2.0, 3.0 };
            double[] y = { 4.0, 5.0, 6.0 };

            sw.Start();
            double distanceTPL = ComputeDistanceF(x, y);
            sw.Stop();
            Console.WriteLine("Distancia calculada con TPL {0}: . Tiempo: {1} msg", distanceTPL, sw.ElapsedMilliseconds);

            // VECTOR ESPEJO

            int[] vectorq = { 1, 2, 3, 4, 5 };
            int[] vectorEspejo = new int[vectorq.Length];

            Parallel.For(0, vectorq.Length, i =>
            {
                vectorEspejo[vectorq.Length - 1 - i] = vectorq[i];
            });

            Console.WriteLine("Vector original:");
            ImprimirVector(vectorq);

            Console.WriteLine("\nVector espejo:");
            ImprimirVector(vectorEspejo);
        }

        // Disctancia entre dos vectores. MasterWorker
        private static double ComputeDistanceF(double[] x, double[] y)
        {
            int n = x.Length;
            double sumOfSquares = 0;

            Parallel.For(0, n, i =>
            {
                double diff = x[i] - y[i];
                sumOfSquares += diff * diff;
            });

            return Math.Sqrt(sumOfSquares);
        }

        // Diferencia / distancia entre do vectores con resultados parciales
        private static double ComputeDistanceF2(double[] x, double[] y)
        {
            int n = x.Length;
            double sumOfSquares = 0;

            // Calcula la suma de los cuadrados de las diferencias de cada elemento de los vectores
            Parallel.For(0, n, () => 0.0, (i, state, subtotal) =>
            {
                double diff = x[i] - y[i];
                subtotal += diff * diff;
                return subtotal;
            },
            (subtotal) =>
            {
                lock (x) // Bloquea el acceso al vector para evitar escrituras simultáneas
                {
                    sumOfSquares += subtotal; // Agrega el subtotal al total
                }
            });

            return Math.Sqrt(sumOfSquares); // Devuelve la raíz cuadrada de la suma de los cuadrados
        }

        //Matriz => lista positivos / negativos
        private static void CalcularPositivosNegativosMatriz(int[,] matriz, ConcurrentBag<int> listaPositivos, ConcurrentBag<int> listaNegativos)
        {
            Parallel.For(0, matriz.GetLength(0), i =>
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    int elemento = matriz[i, j];
                    if (elemento < 0)
                    {
                        listaNegativos.Add(elemento);
                    }
                    else if (elemento > 0)
                    {
                        listaPositivos.Add(elemento);
                    }
                }
            });
        }

        // Suma cuadrados impares de los elementos de un vector
        private static long CalcularSumaCuadradosImpares(int[] vector)
        {
            long sumaCuadradosImpares = 0;

            // Iterar sobre los elementos del vector en paralelo
            Parallel.For(0, vector.Length, i =>
            {
                int elemento = vector[i];

                // Verificar si el elemento es impar
                if (elemento % 2 != 0)
                {
                    long cuadrado = elemento * elemento;

                    // Agregar el cuadrado del elemento a la suma utilizando Interlocked.Add para seguridad en la concurrencia
                    Interlocked.Add(ref sumaCuadradosImpares, cuadrado);
                }
            });

            return sumaCuadradosImpares;
        }

        // Suma cuadrados impares de los elementos de un vector con resultados parciales
        private static long CalcularSumaCuadradosImpares2(int[] vector)
        {
            long sumaCuadradosImpares = 0;

            // Iterar sobre los elementos del vector en paralelo
            Parallel.For<long>(0, vector.Length, () => 0, (i, state, subtotal) =>
            {
                int elemento = vector[i];

                // Verificar si el elemento es impar
                if (elemento % 2 != 0)
                {
                    long cuadrado = elemento * elemento;
                    subtotal += cuadrado;
                }
                return subtotal;
            },
            (subtotal) =>
            {
                Interlocked.Add(ref sumaCuadradosImpares, subtotal);
            });

            return sumaCuadradosImpares;
        }

        // SUMA VECTORIAL

        static int[] SumarVectores(int[] vectorA, int[] vectorB)
        {
            int[] resultado = new int[vectorA.Length];

            Parallel.For(0, vectorA.Length, i =>
            {
                resultado[i] = vectorA[i] + vectorB[i];
            });

            return resultado;
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
            Parallel.For(0, mitad, i =>
            {
                sumaPri += universe[i];
            });
            Parallel.For(mitad, universe.Length, i =>
            {
                sumaSeg += universe[i];
            });
            return sumaPri - sumaSeg;
        }

        // Diferencia 1º mitad - 2º mitad con resultados parciales
        private static int DiferenciaMitadesVector2(int[] universe)
        {
            int mitad = universe.Length / 2;
            int sumaPri = 0;
            int sumaSeg = 0;

            // Suma de la primera mitad del vector
            Parallel.For(0, mitad, () => 0, (i, state, subtotal) =>
            {
                subtotal += universe[i];
                return subtotal;
            },
            (subtotal) =>
            {
                lock (universe) // Bloquea el acceso al vector para evitar escrituras simultáneas
                {
                    sumaPri += subtotal; // Agrega el subtotal al total de la primera mitad
                }
            });

            // Suma de la segunda mitad del vector
            Parallel.For(mitad, universe.Length, () => 0, (i, state, subtotal) =>
            {
                subtotal += universe[i];
                return subtotal;
            },
            (subtotal) =>
            {
                lock (universe) // Bloquea el acceso al vector para evitar escrituras simultáneas
                {
                    sumaSeg += subtotal; // Agrega el subtotal al total de la segunda mitad
                }
            });

            return sumaPri - sumaSeg; // Devuelve la diferencia entre las sumas de las dos mitades
        }

        // Usando Parallel.For, implementar un método reciba un vector de
        //int y un int, devuelve la posición de la primera ocurrencia del valor en el vector o
        //-1 si no está.
        private static int OcurrenciaValor(int[] vector, int valor)
        {
            int posicion = -1; // Inicializa la posición como -1, indicando que aún no se ha encontrado la ocurrencia

            Parallel.For(0, vector.Length, i =>
            {
                if (vector[i] == valor) // Si encontramos una ocurrencia del valor en el índice actual
                {
                    // Usamos Interlocked para actualizar la posición de manera segura en un entorno de subprocesos
                    Interlocked.CompareExchange(ref posicion, i, -1);
                }
            });

            return posicion; // Devuelve la posición encontrada, que podría ser -1 si no se encontró ninguna ocurrencia
        }

        private static int OcurrenciaValor2(int[] vector, int valor)
        {
            int posicion = -1; // Inicializa la posición como -1, indicando que aún no se ha encontrado la ocurrencia
            object lockObject = new object(); // Objeto para sincronizar el acceso a la variable posición

            Parallel.For(0, vector.Length, i =>
            {
                if (vector[i] == valor) // Si encontramos una ocurrencia del valor en el índice actual
                {
                    lock (lockObject) // Bloquea el acceso a la variable posición para evitar condiciones de carrera
                    {
                        // Actualiza la posición solo si aún no se ha encontrado ninguna ocurrencia
                        if (posicion == -1)
                        {
                            posicion = i;
                        }
                    }
                }
            });

            return posicion; // Devuelve la posición encontrada, que podría ser -1 si no se encontró ninguna ocurrencia
        }

        // Ocurrencia de un valor dado con resultados parciales
        private static int OcurrenciaValor3(int[] vector, int valor)
        {
            int posicion = -1; // Inicializa la posición como -1, indicando que aún no se ha encontrado la ocurrencia

            // Utilizamos Parallel.For con una variable local de partición para buscar la ocurrencia del valor
            Parallel.For(0, vector.Length, () => -1, // Inicializa la posición local como -1
                (i, state, posicionLocal) =>
                {
                    if (vector[i] == valor && posicionLocal == -1) // Si encontramos una ocurrencia del valor y aún no se ha encontrado otra
                    {
                        posicionLocal = i; // Asigna la posición de la ocurrencia a la variable local
                        state.Break(); // Detiene la iteración
                    }
                    return posicionLocal; // Devuelve la posición local
                },
                posicionFinal =>
                {
                    // Si se encontró una ocurrencia y su posición es menor que la actual, actualiza la posición final
                    if (posicionFinal != -1 && (posicion == -1 || posicionFinal < posicion))
                    {
                        posicion = posicionFinal;
                    }
                }
            );

            return posicion; // Devuelve la primera posición encontrada del valor en el vector, o -1 si no se encontró ninguna ocurrencia
        }

        //EJERCICIO: FOR -> INDICES Y POSICIONES
        //Impleméntese el ejercicio anterior empleando el For, almacenando la POSICIÓN del valor mínimo.
        //Debe almacenarse la posición más cercana al inicio del vector que contenga el valor mínimo:
        //      {4, 5, 6, 1, 4, 5, 6, 1} -> Resultado esperado: 3
        private static int PosicionValorMinimo(int[] vector, int valorMinimo, int posicionMinima)
        {
            Parallel.For(0, vector.Length, i =>
            {
                lock (vector)
                {
                    if (vector[i] < valorMinimo)
                    {
                        valorMinimo = vector[i];
                        posicionMinima = i;
                    }
                }
            });
            return posicionMinima;
        }

        // Posicion valor minimo de un vector con resultados parciales
        private static int PosicionValorMinimo2(int[] vector, int valorMinimo, int posicionMinima)
        {
            // Utilizamos Parallel.For con resultados locales
            Parallel.For(0, vector.Length, () => int.MaxValue, // Inicializa el valor mínimo local en el máximo valor de int
                (i, state, minLocal) =>
                {
                    if (vector[i] < minLocal)
                    {
                        minLocal = vector[i];
                        posicionMinima = i;
                    }
                    return minLocal; // Devuelve el mínimo local
                },
                minFinal =>
                {
                    // Si el mínimo local encontrado es menor que el valor mínimo global, actualizamos el valor mínimo global
                    lock (vector)
                    {
                        if (minFinal < valorMinimo)
                        {
                            valorMinimo = minFinal;
                        }
                    }
                }
            );

            return posicionMinima;
        }


        static void ImprimirVector(int[] vector)
        {
            foreach (var elemento in vector)
            {
                Console.Write(elemento + " ");
            }
            Console.WriteLine();
        }


    }
}
