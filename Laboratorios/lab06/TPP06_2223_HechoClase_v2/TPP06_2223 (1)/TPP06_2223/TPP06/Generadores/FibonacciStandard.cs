
namespace Generadores
{
    public static class FibonacciStandard
    {
        /// <summary>
        /// Cálculo recursivo del término n en la sucesión de Fibonacci.
        /// </summary>
        public static int Fibonacci(int n)
        {
            return n <= 2 ? 1 : Fibonacci(n - 2) + Fibonacci(n - 1);
        }
    }
}
