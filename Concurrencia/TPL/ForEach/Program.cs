using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ForEach
{
    internal class Program
    {
        private static List<int> threadIds = new();
        private static readonly object obj = new();
        static void Main(string[] args)
        {

            //var vector = GenerarVectorAleatorio(-10, 10, 1000000);
            int[] vector = { 2, 3, 4 };

            // SUMATORIO VECTOR

            int resultadoTotal = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            resultadoTotal = Sumatorio(vector, resultadoTotal);
            sw.Stop();
            Console.WriteLine("El sumatorio total es {0}. Tiempo: {1}.", resultadoTotal, sw.ElapsedMilliseconds);

            sw.Restart();

            //int resultadoMinimoTotal = int.MaxValue;
            //sw.Start();
            //resultadoMinimoTotal = Minimo(vector, resultadoMinimoTotal);
            //sw.Stop();
            //Console.WriteLine("El minimo total es {0}. Tiempo: {1}.", resultadoMinimoTotal, sw.ElapsedMilliseconds);

            //sw.Restart();

            // VALOR MINIMO DE UN VECTOR

            int resultadoMinimoTotal = int.MaxValue;
            sw.Start();
            resultadoMinimoTotal = MinimoSimple(vector, resultadoMinimoTotal);
            sw.Stop();
            Console.WriteLine("El minimo total es {0}. Tiempo: {1}.", resultadoMinimoTotal, sw.ElapsedMilliseconds);

            sw.Restart();

            // VECTOR COMPLEMENTO DADOS DOS VECTORES

            int[] vector1 = { 4, 5, 6, 8, 9, 10, 13, 15, 16, 18 };
            int[] universe = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            //int[] universe = Enumerable.Range(1, 20).ToArray(); // Generar el universo del 1 al 20
            //vectorComplement ={ 1,2,3,7,11,12,14,17,19,20}

            sw.Start();
            int[] complemento = ComplementoVector(vector1, universe);
            sw.Stop();
            Console.WriteLine("El complemento de un vector se calcula en. Tiempo: {0} msg.", sw.ElapsedMilliseconds);
            foreach (var i in complemento)
            {
                Console.Write(i + " ");
            }
            sw.Restart();

            // MAXIMA DIFERENCIA MINIMA ENTRE DOS VECTORES

            //            Usando el bucle paralelo adecuado, método que calcule la máxima diferencia mínima en valor absoluto
            //entre dos arrays de reales.Por ejemplo, si los arrays son A = { 3.0, 2.0, 4.0, 5.0 } y B = { 1.0, 3.0 } las mínimas
            //diferencias en valor absoluto entre cada elemento de A y todos los de B son { 0.0, 1.0, 1.0, 2.0}. El resultado
            //final es el máximo de esas diferencias, es decir 2.0.Requiere menos memoria no almacenar todas las
            //diferencias mínimas y en lugar de esto actualizar el máximo cada vez que se calcula una de ellas, hacerlo así.
            double[] A = { 3.0, 2.0, 4.0, 5.0 };
            double[] B = { 1.0, 3.0 };
            sw.Start();
            double maxDiferenciaMinima = CalcularMaximaDiferenciaMinima(A, B);
            sw.Stop();
            Console.WriteLine("\nLa máxima diferencia mínima en valor absoluto es: {0}. Tiempo: {1} msg.", maxDiferenciaMinima, sw.ElapsedMilliseconds);

            sw.Restart();

            // DOS LISTAS DE PALABRAS MEDIANTE UN CRITERIO

            //calcula mediante TPL dos listas de palabras a 
            //    partir de un IEnumerable de palabras. La primera conendra a todas las palabras de tamaño inferior a 5 y la segunda el resto.
            IEnumerable<string> palabras = new List<string>
            {
                "hola",
                "mundo",
                "este",
                "es",
                "un",
                "ejemplo",
                "de",
                "enumerable",
                "de",
                "palabras"
            };

            List<string> palabrasCortas = new List<string>();
            List<string> palabrasLargas = new List<string>();

            // Dividir las palabras en dos listas utilizando Parallel.ForEach
            Parallel.ForEach(palabras, palabra =>
            {
                if (palabra.Length < 5)
                {
                    lock (palabrasCortas)
                    {
                        palabrasCortas.Add(palabra);
                    }
                }
                else
                {
                    lock (palabrasLargas)
                    {
                        palabrasLargas.Add(palabra);
                    }
                }
                lock (obj)
                {
                    if (!threadIds.Contains(Thread.CurrentThread.ManagedThreadId))
                    {
                        Console.WriteLine("Identificador hilo: " + Thread.CurrentThread.ManagedThreadId); // Lo ponemos por pantalla
                        threadIds.Add(Thread.CurrentThread.ManagedThreadId);
                    }
                }
            });
            Console.WriteLine($"Número total de hilos: {threadIds.Count}");

            Console.WriteLine("Palabras cortas:");
            foreach (string word in palabrasCortas)
            {
                Console.WriteLine(word);
            }

            Console.WriteLine("\nPalabras largas:");
            foreach (string word in palabrasLargas)
            {
                Console.WriteLine(word);
            }

            // DOS LISTAS DE PALABRAS MEDIANTE UN CRITERIO RESULTADOS PARCIALES


            // Dividir las palabras en dos listas utilizando Parallel.ForEach
            Parallel.ForEach(palabras, () => (new List<string>(), new List<string>()), // Inicializa las listas locales
                (palabra, state, localLists) =>
                {
                    var palabrasCortasLocal = localLists.Item1;
                    var palabrasLargasLocal = localLists.Item2;

                    if (palabra.Length < 5)
                    {
                        palabrasCortasLocal.Add(palabra);
                    }
                    else
                    {
                        palabrasLargasLocal.Add(palabra);
                    }

                    return localLists; // Devuelve las listas locales modificadas
                },
                localListsFinal =>
                {
                    lock (palabrasCortas)
                    {
                        palabrasCortas.AddRange(localListsFinal.Item1); // Agrega las palabras cortas al resultado total
                    }
                    lock (palabrasLargas)
                    {
                        palabrasLargas.AddRange(localListsFinal.Item2); // Agrega las palabras largas al resultado total
                    }
                }
            );

            Console.WriteLine("Palabras cortas:");
            foreach (string word in palabrasCortas)
            {
                Console.WriteLine(word);
            }

            Console.WriteLine("\nPalabras largas:");
            foreach (string word in palabrasLargas)
            {
                Console.WriteLine(word);
            }

            // VECTOR ESPEJO

            int[] vectora = { 1, 2, 3, 4, 5 };
            int[] vectorEspejo = new int[vectora.Length];

            //Parallel.ForEach(Partitioner.Create(0, vectora.Length), range =>
            //{
            //    for (int i = range.Item1; i < range.Item2; i++)
            //    {
            //        vectorEspejo[vectora.Length - 1 - i] = vectora[i];
            //    }
            //});
            Parallel.ForEach(Enumerable.Range(0, vectora.Length), i =>
            {
                vectorEspejo[vectora.Length - 1 - i] = vectora[i];
                lock (obj)
                {
                    if (!threadIds.Contains(Thread.CurrentThread.ManagedThreadId))
                    {
                        Console.WriteLine("Identificador hilo: " + Thread.CurrentThread.ManagedThreadId); // Lo ponemos por pantalla
                        threadIds.Add(Thread.CurrentThread.ManagedThreadId);
                    }
                }
            });
            Console.WriteLine($"Número total de hilos: {threadIds.Count}"); ;

            Console.WriteLine("Vector original:");
            ImprimirVector(vectora);

            Console.WriteLine("\nVector espejo:");
            ImprimirVector(vectorEspejo);


            // MODULO  DE UN VECTOR CON RESULTADOS PARCIALES

            var vectorModulo = GenerarVectorAleatorio(-100, 100, 100000);
            long result = 0;
            Parallel.ForEach(vectorModulo,
            () => 0, // Method to initialize the local variable
            (v, loopState, subtotal) => subtotal += v * v,
            // Method to be executed when each partition has completed.
            // finalResult is the final value of subtotal for a particular partition.
            finalResult => Interlocked.Add(ref result, finalResult));
            Console.WriteLine("The result obtained is: {0:N2}.", Math.Sqrt(result));
        }







        // Calcula la maxima diferencia minima entre dos vectores
        private static double CalcularMaximaDiferenciaMinima(double[] A, double[] B)
        {
            double maxDiferenciaMinima = double.MinValue;

            // Iteramos sobre los elementos de A
            Parallel.ForEach(A, a =>
            {
                double minDiferencia = double.MaxValue;

                // Calculamos la diferencia mínima en valor absoluto entre el elemento de A y los elementos de B
                foreach (double b in B)
                {
                    double diferencia = Math.Abs(a - b);
                    if (diferencia < minDiferencia)
                    {
                        minDiferencia = diferencia;
                    }
                }

                // Actualizamos el máximo de las diferencias mínimas si es necesario
                if (minDiferencia > maxDiferenciaMinima)
                {
                    maxDiferenciaMinima = minDiferencia;
                }
                lock (obj)
                {
                    if (!threadIds.Contains(Thread.CurrentThread.ManagedThreadId))
                    {
                        Console.WriteLine("Identificador hilo: " + Thread.CurrentThread.ManagedThreadId); // Lo ponemos por pantalla
                        threadIds.Add(Thread.CurrentThread.ManagedThreadId);
                    }
                }
            });
            Console.WriteLine($"Número total de hilos: {threadIds.Count}");

            return maxDiferenciaMinima;
        }

        // Calcula la maxima diferencia minima entre dos vectores con resultados parciales
        private static double CalcularMaximaDiferenciaMinima2(double[] A, double[] B)
        {
            double maximaDiferenciaMinima = double.MinValue;

            // Iteramos sobre los elementos de A
            Parallel.ForEach(A, () => double.MaxValue, // Inicializa el resultado local en el máximo valor de double
                (a, state, minDiferenciaLocal) =>
                {
                    // Calculamos la diferencia mínima en valor absoluto entre el elemento de A y los elementos de B
                    foreach (double b in B)
                    {
                        double diferencia = Math.Abs(a - b);
                        if (diferencia < minDiferenciaLocal)
                        {
                            minDiferenciaLocal = diferencia;
                        }
                    }
                    return minDiferenciaLocal; // Devuelve la diferencia mínima local
                },
                minDiferenciaFinal =>
                {
                    // Actualizamos el máximo de las diferencias mínimas si es necesario
                    lock (A)
                    {
                        if (minDiferenciaFinal > maximaDiferenciaMinima)
                        {
                            maximaDiferenciaMinima = minDiferenciaFinal;
                        }
                    }
                }
            );

            return maximaDiferenciaMinima;
        }

        // Complemento de un vector
        private static int[] ComplementoVector(int[] vector1, int[] universe)
        {
            List<int> res = new();
            Parallel.ForEach(universe, num =>
            {
                lock (res)
                {
                    if (!vector1.Contains(num))
                    {
                        res.Add(num);
                    }
                }
                lock (obj)
                {
                    if (!threadIds.Contains(Thread.CurrentThread.ManagedThreadId))
                    {
                        Console.WriteLine("Identificador hilo: " + Thread.CurrentThread.ManagedThreadId); // Lo ponemos por pantalla
                        threadIds.Add(Thread.CurrentThread.ManagedThreadId);
                    }
                }
            });
            Console.WriteLine($"Número total de hilos: {threadIds.Count}");

            return res.ToArray();
        }

        // Complemento de dos vectores usando resultados parciales
        private static int[] ComplementoVector2(int[] vector1, int[] universe)
        {
            // Resultado total
            List<int> res = new List<int>();

            // Ejecutar Parallel.ForEach con resultados locales
            Parallel.ForEach<int, List<int>>(
                universe,
                () => new List<int>(), // Inicializa la lista local para cada hilo
                (num, loopState, localList) =>
                {
                    if (!vector1.Contains(num))
                    {
                        localList.Add(num); // Agrega el número al resultado local si no está en vector1
                    }
                    return localList; // Devuelve la lista local modificada
                },
                (localListFinal) =>
                {
                    lock (res)
                    {
                        res.AddRange(localListFinal); // Agrega el resultado local al resultado total
                    }
                }
            );

            return res.ToArray(); // Convierte el resultado total a un array y lo devuelve
        }

        // Valor minimo de un vector

        private static int MinimoSimple(int[] vector, int resultadoMinimoTotal)
        {
            Parallel.ForEach(vector, actual =>
            {
                if (actual < resultadoMinimoTotal)
                {
                    resultadoMinimoTotal = actual;
                }
                lock (obj)
                {
                    if (!threadIds.Contains(Thread.CurrentThread.ManagedThreadId))
                    {
                        Console.WriteLine("Identificador hilo: " + Thread.CurrentThread.ManagedThreadId); // Lo ponemos por pantalla
                        threadIds.Add(Thread.CurrentThread.ManagedThreadId);
                    }
                }
            });
            Console.WriteLine($"Número total de hilos: {threadIds.Count}");
            return resultadoMinimoTotal;
        }

        // Valor minimo de un vector. Resultados parciales
        private static int Minimo(int[] vector, int resultadoMinimoTotal)
        {
            //<T,S> donde T es el tipo de origen y S es el tipo del resultado local.
            Parallel.ForEach<int, int>(
                //IEnumerable<T> a recorrer. Potencialmente un hilo por elemento.
                vector,

                // Func<S> que inicializa la variable de resultado local para la partición.
                () => int.MaxValue, // Inicializa con el valor máximo de int

                // Func<T,ParallelLoopState, S> que representa el algoritmo a ejecutar.
                (actual, loopState, resultadoLocal) =>
                {
                    if (actual < resultadoLocal)
                        return actual; // Devuelve el nuevo mínimo encontrado
                    return resultadoLocal; // Devuelve el mínimo actual
                },

                //Action<Q> una vez finalizada la partición, ¿qué hacer con el resultado local?
                resultadoLocalFinal =>
                {
                    if (resultadoLocalFinal < resultadoMinimoTotal)
                        Interlocked.Exchange(ref resultadoMinimoTotal, resultadoLocalFinal);
                }
            );
            return resultadoMinimoTotal;
        }


        // Sumatorio con resultados parciales/locales
        private static int Sumatorio(int[] vector, int resultadoTotal)
        {
            //<T,S> donde T es el tipo de origen y S es el tipo del resultado local.
            Parallel.ForEach<int, int>(
                //IEnumerable<T> a recorrer. Potencialmente un hilo por elemento.
                vector,

                // Func<S> que inicializa la variable de resultado local para la partición.
                () => 0, // Inicializa el resultado local en 0

                //Func<T,ParallelLoopState, S> que representa el algoritmo a ejecutar.
                (actual, loopState, resultadoLocal) =>
                {
                    resultadoLocal += actual; // Suma el valor actual al resultado local
                    return resultadoLocal; // Devuelve el resultado local modificado
                },

                //Action<Q> una vez finalizada la partición, ¿qué hacer con el resultado local?
                resultadoLocalFinal => Interlocked.Add(ref resultadoTotal, resultadoLocalFinal) // Agrega el resultado local al resultado total de forma segura para subprocesos
            );
            return resultadoTotal; // Devuelve el resultado total
        }




        static int[] GenerarVectorAleatorio(int min, int max, int tam)
        {
            Random random = new Random();
            int[] vector = new int[tam];

            for (int i = 0; i < tam; i++)
                vector[i] = random.Next(min, max + 1);

            return vector;
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
