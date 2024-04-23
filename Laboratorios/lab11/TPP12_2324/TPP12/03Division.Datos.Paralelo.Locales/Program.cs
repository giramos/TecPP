using System;
using System.Diagnostics;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace _03Procesado.Tareas.Secuencial.Locales
{
    internal class Program
    {
        /// <summary>
        /// Se presenta una sobrecarga del método ForEach en la que cada partición genera un resultado local.
        /// 
        /// </summary>
        static void Main()
        {
            var vector = GenerarVectorAleatorio(-10, 10, 1000000);
            int resultadoTotal = 0;
            Stopwatch sw = new Stopwatch();

            sw.Start();

            //<T,S> donde T es el tipo de origen y S es el tipo del resultado local.
            Parallel.ForEach<int, int>(
                //IEnumerable<T> a recorrer. Potencialmente un hilo por elemento.
                vector,

                // Func<S> que inicializa la variable de resultado local para la partición.
                () => 0, // () => new List<int>() 

                //Func<T,ParallelLoopState, S> que representa el algoritmo a ejecutar.
                (actual, loopState, resultadoLocal) =>
                {
                    resultadoLocal += actual;
                    return resultadoLocal;
                },

                //Action<Q> una vez finalizada la partición, ¿qué hacer con el resultado local?
                resultadoLocalFinal => Interlocked.Add(ref resultadoTotal, resultadoLocalFinal)
            );
            sw.Stop();

            Console.WriteLine("El sumatorio total es {0}. Tiempo: {1}.", resultadoTotal, sw.ElapsedMilliseconds);

            //EJERCICIO: FOREACH _> elementos
            //Impleméntese el cálculo del valor mínimo en el vector anterior, siguiendo los dos enfoques posibles:
            //  - Empleando resultados parciales/locales.
            //  - No empleando resultados parciales/locales (téngase en cuenta los posibles recursos compartidos).
            // Imprímase el tiempo empleado para cada uno de los enfoques.

            var vector1 = GenerarVectorAleatorio(-10, 10, 1000000);
            int resultadoMinimo = int.MaxValue;
            Stopwatch sw1 = new Stopwatch();

            sw1.Start();

            foreach (var actual in vector1)
            {
                if (actual < resultadoMinimo)
                    resultadoMinimo = actual;
            }

            sw1.Stop();

            Console.WriteLine("FOREACH SIN PARALELIZAR: El valor mínimo es {0}. Tiempo: {1} ms.", resultadoMinimo, sw1.ElapsedMilliseconds);

            var vector2 = GenerarVectorAleatorio(-10, 10, 1000000);
            int resultadoMinimo1 = int.MaxValue;
            Stopwatch sw2 = new Stopwatch();

            sw2.Start();

            Parallel.ForEach(vector2, actual =>
            {
                if (actual < resultadoMinimo1)
                    resultadoMinimo1 = actual;
            });

            sw2.Stop();

            Console.WriteLine("FOREACH NORMAL: El valor mínimo es {0}. Tiempo: {1} ms.", resultadoMinimo1, sw2.ElapsedMilliseconds);

            var vector3 = GenerarVectorAleatorio(-10, 10, 1000000);
            int resultadoMinimo3 = int.MaxValue;
            Stopwatch sw3 = new Stopwatch();

            sw3.Start();

            Parallel.ForEach<int, int>(
                vector3,
                () => 0,
                (actual, loopState, resultadoLocal) =>
                {
                    if (actual < resultadoLocal)
                        resultadoLocal = actual;
                    return resultadoLocal;
                },
                resultadoLocalFinal =>
                {
                    if (resultadoLocalFinal < resultadoMinimo3)
                        Interlocked.Exchange(ref resultadoMinimo3, resultadoLocalFinal);
                }
            );

            sw3.Stop();

            Console.WriteLine("RESULTADOS LOCALES: El valor mínimo es {0}. Tiempo: {1} ms.", resultadoMinimo3, sw3.ElapsedMilliseconds);



            //EJERCICIO: FOR -> INDICES Y POSICIONES
            //Impleméntese el ejercicio anterior empleando el For, almacenando la POSICIÓN del valor mínimo.
            //Debe almacenarse la posición más cercana al inicio del vector que contenga el valor mínimo:
            //      {4, 5, 6, 1, 4, 5, 6, 1} -> Resultado esperado: 3

            int[] vector4 = { 4, 5, 6, 1, 4, 5, 6, 1 };

            int posMin = 0;
            int valorMin = vector4[0];

            Parallel.For(1, vector4.Length, i =>
            {
                lock (vector4)
                {
                    if (vector4[i] < valorMin)
                    {
                        valorMin = vector4[i];
                        posMin = i;
                    }
                }

            });

            Console.WriteLine("FOR PARALELO: La posición del valor mínimo es: " + posMin);
        }

        static int[] GenerarVectorAleatorio(int min, int max, int tam)
        {
            Random random = new Random();
            int[] vector = new int[tam];

            for (int i = 0; i < tam; i++)
                vector[i] = random.Next(min, max + 1);

            return vector;
        }
    }
}
