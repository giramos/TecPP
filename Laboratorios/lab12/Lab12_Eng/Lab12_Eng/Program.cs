using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Linq;

namespace Lab12_Eng
{
    internal class Program
    {
        private static readonly object o = new();
         
        static void Main(string[] args)
        {
            //            Implementar dos
            //métodos que calculen cuantas veces se repite cada entero en un array de este tipo, uno
            //usando Parallel.For y otro usando Parallel.ForEach.
            
            Stopwatch sw = new();
            var array = vectorAleatorio(1000, 0, 20);

            sw.Start();
            var res = MetodoFor(array);
            sw.Stop();
            Console.WriteLine("\nFOR");
            Console.WriteLine("Tiempo para el for {0}", sw.ElapsedMilliseconds);
        
            Show(res);

            sw.Restart();
            res = MetodoForeach(array);
            sw.Stop();
            Console.WriteLine("FOREACH");
            Console.WriteLine("Tiempo para el foreach {0}", sw.ElapsedMilliseconds);

            Show(res);

            sw.Restart();
            res = MetodoInvoke(array);
            sw.Stop();
            Console.WriteLine("INVOKE");
            Console.WriteLine("Tiempo para el invoke {0}", sw.ElapsedMilliseconds);

            Show(res);

            // Varianza

            sw.Restart();
            var varianza = varLinq(array);
            sw.Stop();
            Console.WriteLine("Varianza");
            Console.WriteLine("Tiempo para la varianza {0}. Resultado: {1}", sw.ElapsedMilliseconds, varianza);

            sw.Restart();
            var varianzaP = varPLinq(array);
            sw.Stop();
            Console.WriteLine("Varianza Plinq");
            Console.WriteLine("Tiempo para la varianza con PLINQ {0}. Resultado: {1}", sw.ElapsedMilliseconds, varianzaP);

            //            En este caso es posible usarlos para implementar a su vez un método que los invoque en
            //paralelo(usando Parallel.Invoke) para calcular esas magnitudes para un parámetro
            //de tipo short[]. Al terminar la invocación, calcular su varianza usando la fórmula E(x^2) -
            //(E(x)) ^ 2.Comprobar si en este caso existe alguna ganancia en eficiencia.

            sw.Restart();
            double resulVarianza = CalcularVarianza(array);
            sw.Stop();
            Console.WriteLine("Varianza con Invoke");
            Console.WriteLine("Tiempo para la varianza con Invoke {0}. Resultado: {1}", sw.ElapsedMilliseconds, resulVarianza);

            //            Ejercicio 4.Generar una colección con enteros aleatorios ordenados de menor a mayor
            //(por ejemplo, 100 números entre 2 y 10000), producir una lista con los valores de esa
            //colección que sean primos. Implementar de tres formas distintas, usando
            //Parallel.For, Parallel.ForEach y PLINQ. ¿Alguna particularidad en como se
            //almacenan / producen los resultados?

            var col = CreateRandomVector(100, 2, 10000);
            List<int> primos = new();

            sw.Restart();
            primos = CalcularPrimosFor(col);
            sw.Stop();
            Console.WriteLine("Primos con for");
            Console.WriteLine("Tiempo para los primos con for {0}. Resultado: {1}", sw.ElapsedMilliseconds, primos.Count());
            Show(primos);

            sw.Restart();
            primos = CalcularPrimosForeach(col);
            sw.Stop();
            Console.WriteLine("Primos con foreach");
            Console.WriteLine("Tiempo para los primos con foreach {0}. Resultado: {1}", sw.ElapsedMilliseconds, primos.Count());
            Show(primos);

            sw.Restart();
            primos = CalcularPrimosPLinq(col);
            sw.Stop();
            Console.WriteLine("Primos con PLinq");
            Console.WriteLine("Tiempo para los primos con PLinq {0}. Resultado: {1}", sw.ElapsedMilliseconds, primos.Count());
            Show(primos);

            //            vEjercicio 5.Calcular el módulo de un vector usando Parallel.For o
            //Parallel.ForEach ¿es necesario usar algo más?.


            var vectorModulo = CreateRandomVector(100000, -100, 100);
            long result = 0;
            Parallel.ForEach(vectorModulo,
            () => 0, // Method to initialize the local variable
            (v, loopState, subtotal) => subtotal += v * v,
            // Method to be executed when each partition has completed.
            // finalResult is the final value of subtotal for a particular partition.
            finalResult => Interlocked.Add(ref result, finalResult));
            Console.WriteLine("The result obtained is: {0:N2}.", Math.Sqrt(result));

            // MODULO CON PLINQ

            static double ModulusPlinq(IEnumerable<double> col)
            {
                return Math.Sqrt(col.AsParallel().Aggregate(0.0, (acc, e) => acc += e * e));
            }

            //            Ejercicio 6.Sumar vectorialmente dos vectores, escoger la alternativa más adecuada
            //(Parallel.For, Parallel.ForEach, PLINQ)


        }

        static IEnumerable<int> PrimesParallelForLocal(int[] data)
        {
            List<int> result = new List<int>();
            Parallel.For(0, data.Length,
            () => new List<int>(),
            (i, loop, partialResult) =>
            {
                if (IsPrime(data[i]))
                    partialResult.Add(data[i]);
                return partialResult;
            },
            (x) =>
            {
                lock (result)
                    result.AddRange(x);
            });
            return result;
        }

        static IEnumerable<int> PrimesParallelForEachLocal(int[] data)
        {
            List<int> result = new List<int>();
            Parallel.ForEach(data,
            () => new List<int>(),
            (element, loop, partialResult) =>
            {
                if (IsPrime(element))
                    partialResult.Add(element);
                return partialResult;
            },
            (x) =>
            {
                lock (result)
                    result.AddRange(x);
            }
            );
            return result;
        }

        private static List<int> CalcularPrimosFor(int[] col)
        {
            List<int> threadIds = new();
            List<int> res = new();
            Parallel.For(0, col.Length, i =>
            {
                if (IsPrime(col[i]))
                {
                    lock(res)
                        res.Add(col[i]);
                }
                lock (o)
                {
                    if (!threadIds.Contains(Thread.CurrentThread.ManagedThreadId))
                    {
                        Console.WriteLine("PrimosFor Thread ID = " +
                        Thread.CurrentThread.ManagedThreadId);
                        threadIds.Add(Thread.CurrentThread.ManagedThreadId);
                    }
                }
            });
            return res;
        }

        private static List<int> CalcularPrimosForeach(int[] col)
        {
            List<int> threadIds = new();
            List<int> res = new();
            Parallel.ForEach(col, i =>
            {
                if (IsPrime(i))
                {
                    lock (res)
                        res.Add(i);
                }
                lock (o)
                {
                    if (!threadIds.Contains(Thread.CurrentThread.ManagedThreadId))
                    {
                        Console.WriteLine("PrimosForeach Thread ID = " +
                        Thread.CurrentThread.ManagedThreadId);
                        threadIds.Add(Thread.CurrentThread.ManagedThreadId);
                    }
                }
            });
            return res;
        }
        private static List<int> CalcularPrimosPLinq(int[] col)
        {
            List<int> threadIds = new();
            var res = col.AsParallel().Where(num => IsPrime(num));
            lock (o)
            {
                if (!threadIds.Contains(Thread.CurrentThread.ManagedThreadId))
                {
                    Console.WriteLine("Primos PLINQ Thread ID = " +
                    Thread.CurrentThread.ManagedThreadId);
                    threadIds.Add(Thread.CurrentThread.ManagedThreadId);
                }
            }
            return res.ToList();

        }

        static bool IsPrime(int n)
        {
            if (n < 2)
                return false;
            for (int i = 2; i <= n / 2; i++)
                if (n % i == 0)
                    return false;
            return true;
        }

        private static double CalcularVarianza(int[] array)
        {
            double e = 0.0;
            double e2 = 0.0;
            Parallel.Invoke(
                () => e = mean(array),
                () => e2 = meanX2(array)
            );
            return e2 - e * e;
        }

        private static Dictionary<int, int> MetodoForeach(int[] array)
        {
            List<int> threadIds = new();
            Dictionary<int, int> dicc = new();

            Parallel.ForEach(array, i =>
            {
                lock (dicc)
                {
                    if (dicc.ContainsKey(i))
                    {
                        dicc[i]++;
                    }
                    else
                    {
                        dicc[i] = 1;
                    }
                }
                lock (o)
                {
                    if (!threadIds.Contains(Thread.CurrentThread.ManagedThreadId))
                    {
                        Console.WriteLine("FOREACH Thread ID = " +
                        Thread.CurrentThread.ManagedThreadId);
                        threadIds.Add(Thread.CurrentThread.ManagedThreadId);
                    }
                }
            });
            return dicc;
        }

        private static Dictionary<int, int> MetodoFor(int[] array)
        {
            List<int> threadIds = new();
            Dictionary<int, int> dicc = new();

            Parallel.For(0, array.Length, i =>
            {
                lock (dicc)
                {
                    if (dicc.ContainsKey(array[i]))
                    {
                        dicc[array[i]]++;
                    }
                    else
                    {
                        dicc[array[i]] = 1;
                        //dicc.Add(array[i], i);
                    }
                }
                lock (o)
                {
                    if (!threadIds.Contains(Thread.CurrentThread.ManagedThreadId))
                    {
                        Console.WriteLine("FOR Thread ID = " +
                        Thread.CurrentThread.ManagedThreadId);
                        threadIds.Add(Thread.CurrentThread.ManagedThreadId);
                    }
                }

            });
            return dicc;
        }

        private static Dictionary<int, int> MetodoInvoke(int[] array)
        {
            List<int> threadIds = new();
            Dictionary<int, int> dicc1 = new();
            Dictionary<int, int> dicc2 = new();

            Parallel.Invoke(
                () =>
                {
                    for (int i = 0; i < array.Length; i = i + 2)
                    {
                        lock (dicc1)
                        {
                            if (dicc1.ContainsKey(array[i]))
                            {
                                dicc1[array[i]]++;
                            }
                            else
                            {
                                dicc1[array[i]] = 1;
                            }
                        }

                    }
                    lock (o)
                    {
                        if (!threadIds.Contains(Thread.CurrentThread.ManagedThreadId))
                        {
                            Console.WriteLine("INVOKE Thread ID = " +
                            Thread.CurrentThread.ManagedThreadId);
                            threadIds.Add(Thread.CurrentThread.ManagedThreadId);
                        }
                    }
                },
                () =>
                {
                    for (int i = 1; i < array.Length; i = i + 2)
                    {
                        lock (dicc2)
                        {
                            if (dicc2.ContainsKey(array[i]))
                            {
                                dicc2[array[i]]++;
                            }
                            else
                            {
                                dicc2[array[i]] = 1;
                            }
                        }

                    }
                    lock (o)
                    {
                        if (!threadIds.Contains(Thread.CurrentThread.ManagedThreadId))
                        {
                            Console.WriteLine("INVOKE Thread ID = " +
                            Thread.CurrentThread.ManagedThreadId);
                            threadIds.Add(Thread.CurrentThread.ManagedThreadId);
                        }
                    }
                }
                );
            Dictionary<int, int> d = new Dictionary<int, int>();
            foreach (var x in dicc1.Keys)
            {
                if (dicc2.Keys.Contains(x))
                {
                    d[x] = dicc1[x] + dicc2[x];
                }
                else
                {
                    d.Add(x, dicc1[x]);
                }
            }
            foreach (var x in dicc2.Keys)
            {
                if (!d.Keys.Contains(x))
                {
                    d.Add(x, dicc2[x]);
                }
            }
            return d;

        }
        /// <summary>
        /// Calcula la a varianza de un vector de short usando la fórmula E((x-E(x))^2), E es la media y 
        //        x representa la muestra, es por lo tanto la media del cuadrado de la diferencia entre cada
        //elemento y la media de todos los elementos: se necesita conocer en primer lugar E(x).
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        static double varLinq(IEnumerable<int> vector)
        {
            var mean = vector.Aggregate(0.0, (acc, el) => acc + el) /
            vector.Count();
            return vector.Aggregate(0.0, (acc, el) => acc + Math.Pow((el­ - mean), 2.0)) / vector.Count();
        }

        static double varPLinq(IEnumerable<int> vector)
        {
            var mean = vector.AsParallel().Aggregate(0.0, (acc, el) => acc + el) /
            vector.Count();
            return vector.AsParallel().Aggregate(0.0, (acc, el) => acc + Math.Pow((el­ - mean), 2.0)) / vector.Count();
        }

        //  E(x) 
        static double mean(IEnumerable<int> vector)
        {
            return vector.Aggregate(0.0, (acc, el) => acc + el) / (double)
            vector.Count();
        }

        // E(x^2)
        static double meanX2(IEnumerable<int> vector)
        {
            return vector.Aggregate(0.0, (acc, el) => acc + el * el) /
            (double)vector.Count();
        }

        static int[] CreateRandomVector(int numberOfElements, short lowest, short greatest)
        {
            int[] vector = new int[numberOfElements];
            Random random = new Random();
            for (int i = 0; i < numberOfElements; i++)
                vector[i] = (short)random.Next(lowest, greatest + 1);
            return vector;
        }

        static int[] vectorAleatorio(int n, int min, int max)
        {
            Random r = new Random();  //RANDOM NO ES THREAD SAVE
            int[] res = new int[n];
            for (int i = 0; i < n; i++)
            {
                res[i] = r.Next(min, max);
            }
            return res;
        }

        static void Show<T>(IEnumerable<T> c, char sep = '\n', char end = '\n')
        {
            foreach (var e in c)
                Console.Write("{0}{1}", e, sep);
            Console.WriteLine(end);
        }
    }
}
