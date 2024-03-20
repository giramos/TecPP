using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Memorizacion
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            const int nTermino = 11;
            bool resultado;
            var crono = new Stopwatch();

            crono.Restart();
            resultado = PrimoMemorizacion.IsPrime(nTermino);
            crono.Stop();
            long ticksPrimeraConMem = crono.ElapsedTicks;
            Console.WriteLine("Con Memorización, 1ª llamada: {0:N} ticks. Resultado: {1}.", ticksPrimeraConMem, resultado);

            crono.Restart();
            resultado = PrimoMemorizacion.IsPrime(nTermino);
            crono.Stop();
            long ticksSegundaConMem = crono.ElapsedTicks;
            Console.WriteLine("Con Memorización, 2ª llamada: {0:N} ticks. Resultado: {1}.", ticksSegundaConMem, resultado);


        }
    }

    public class PrimoMemorizacion
    {
        /// <summary>
        /// Caché
        /// </summary>
        private static IDictionary<int, bool> _cache = new Dictionary<int, bool>();

        /// <summary>
       

        public static bool IsPrime(int n)
        {


            // Si el número está en la caché, devolvemos el resultado guardado
            if (_cache.ContainsKey(n)) return _cache[n];

            // Si no está en la caché, lo calculamos como antes

            // Si el número es menor que 2, no es primo
            if (n < 2) return false;

            // Si el número es divisible por 2, solo es primo si es igual a 2
            if (n % 2 == 0) return n == 2;

            // Comprobar si el número es divisible por algún entero entre 3 y la raíz cuadrada del número
            int limit = (int)Math.Sqrt(n);
            for (int i = 3; i <= limit; i += 2)
            {
                if (n % i == 0) return false;
            }

            // Si no se encuentra ningún divisor, el número es primo
            bool result = true;

            _cache.Add(n, result);

            // Guardamos el resultado en la caché con el número como clave
            return result;
        }

       
    }
}
