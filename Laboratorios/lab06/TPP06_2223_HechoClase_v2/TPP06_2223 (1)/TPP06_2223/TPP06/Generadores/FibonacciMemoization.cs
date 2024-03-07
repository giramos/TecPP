using System.Collections.Generic;

namespace Generadores
{
    internal class FibonacciMemoization
    {
        /// <summary>
        /// Caché
        /// </summary>
        private static IDictionary<int, int> _cache = new Dictionary<int, int>();

        /// <summary>
        /// Cálculo del término n de fibonacci con Memoization.
        /// </summary>
        public static int Fibonacci(int n)
        {
            if (_cache.Keys.Contains(n))
                // * Si está en la caché. Lo devolvemos.
                return _cache[n];

            // * En caso contrario, lo calculamos.
            int value = n <= 2 ? 1 : Fibonacci(n - 2) + Fibonacci(n - 1);

            _cache.Add(n, value);

            return value;
        }

    }
}

