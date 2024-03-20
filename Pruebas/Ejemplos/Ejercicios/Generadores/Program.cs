using System;
using System.Collections.Generic;
using System.Linq;

namespace Generadores
{
    public static class Program
    {
        static void Main(string[] args)
        {
            const int numeroDeTerminos = 10;

            // 10 primeros términos de una sucesion de numeros primos

            int i = 1;

            foreach (int valor in PrimoInfinito())
            {
                Console.WriteLine("Término {0}: {1}.", i, valor);
                if (i++ == numeroDeTerminos)
                    break;
            }

            Console.WriteLine();

            // Empleando iterador (IEnumerator) 

            var iterador = PrimoInfinito().GetEnumerator();
            iterador.MoveNext();

            Console.WriteLine("Término 1: {0}.", iterador.Current);

            // iterador.Reset();  <- ¡OJO! El reset no está soportado.

            Console.WriteLine();

            // * Especificando el número de términos que necesitamos
            i = 1;
            foreach (int valor in PrimoFinito(numeroDeTerminos))
                Console.WriteLine("Término {0}: {1}.", i++, valor);

            Console.WriteLine();

            // El primer primo despues del 100

            int desde = 100; int cantidad = 1; int mostrarNElementos = 1;

            IEnumerable<int> primosLazy = PrimoLazy(desde, cantidad);

            Console.Write("{0} elementos tras el término {1} (perezosa/lazy):\n\t", mostrarNElementos, desde);
            //Una vez obtenida TODA la colección, solamente consumimos 10.            
            primosLazy.ForEach(item => Console.Write("{0} ", item), mostrarNElementos); // Aqui hasta que no se utiliza la secuencia no se genera y se genera cuando se llega al desde 

            Console.WriteLine();

            // Mostrar el primer primo mayor que 100 usando InfinitePrime()
            int primerPrimoMayorQue100 = PrimoInfinito().First(primo => primo > 100);
            Console.WriteLine($"Primer primo mayor que 100: {primerPrimoMayorQue100}");

            Console.WriteLine();

            // Mostrar los 10 primos siguientes después de los primeros 10 primos con el mismo generador
            IEnumerable<int> primeros10Primos = PrimoFinito(10);
            int[] siguientes10Primos = PrimoFinito(20).Skip(10).Take(10).ToArray();

            Console.WriteLine("Primeros 10 primos:");
            foreach (int primo in primeros10Primos)
            {
                Console.Write($"{primo} ");
            }
            Console.WriteLine();
            Console.WriteLine("Siguientes 10 primos:");
            foreach (int primo in siguientes10Primos)
            {
                Console.Write($"{primo} ");
            }
            Console.WriteLine();

            Console.WriteLine();

            // Mostrar los primeros 20 números primos usando FinitePrime(20)
            Console.WriteLine("Primeros 20 números primos:");
            foreach (int primo in PrimoFinito(20))
            {
                Console.Write($"{primo} ");
            }

            // LIBRO

            const int numDeTerminos = 3;
            int ii = 1;
            foreach (Libro valor in GeneradorInfinito(5))
            {
                Console.WriteLine("Titulo: " + valor.Titulo + " pags: " + valor.NumPaginas);
                if (ii++ == numDeTerminos)
                    break;
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Generador infinito de los términos de la sucesión de Fibonacci
        /// </summary>
        static internal IEnumerable<int> FibonacciInfinito()
        {
            int primero = 1, segundo = 1;
            while (true)
            {
                // Devolvemos el primer valor.
                // ¡yield almacena el estado de la ejecución!
                // Cuando se vuelva a invocar
                // Se recupera el estado y continúa en la línea posterior al yield
                yield return primero;
                int suma = primero + segundo;
                primero = segundo;
                segundo = suma;
            }
        }

        internal static IEnumerable<int> FactorialInfinito()
        {
            int n = 1;
            int factorial = 1;
            while (true)
            {
                yield return factorial;
                n++;
                factorial *= n;
            }
        }

        /// <summary>
        /// Generador infinito de los términos de la sucesión de números Primos
        /// </summary>
        public static IEnumerable<int> PrimoInfinito()
        {
            int num = 0;
            while (true)
            {
                if (IsPrime(num))
                {
                    yield return num;
                }
                num++;
            }
        }

        static internal IEnumerable<int> PrimoLazy(int desde, int cuantos)
        {
            //using System.Linq métodos extensores
            //Skip salta N elementos.
            //Take selecciona N elementos a partir del actual.
            return PrimoInfinito().Skip(desde).Take(cuantos);
        }
        internal static IEnumerable<int> ParesInfinito()
        {
            int n = 2;
            while (true)
            {
                yield return n;
                n += 2;
            }
        }

        internal static IEnumerable<int> ImparesInfinito()
        {
            int n = 1;
            while (true)
            {
                yield return n;
                n += 2;
            }
        }

        internal static IEnumerable<double> AleatoriosInfinito()
        {
            Random rand = new Random();
            while (true)
            {
                yield return rand.NextDouble();
            }
        }

        public static IEnumerable<char> LetrasAleatoriasInfinito()
        {
            Random rand = new Random();
            while (true)
            {
                yield return (char)('a' + rand.Next(26));
            }
        }

        /// <summary>
        /// Generador finito de términos de la sucesión de Fibonacci.
        /// </summary>
        static internal IEnumerable<int> FibonacciFinito(int maxTermino)
        {
            int primero = 1, segundo = 1, termino = 1;
            while (true)
            {
                yield return primero;
                int suma = primero + segundo;
                primero = segundo;
                segundo = suma;
                if (termino++ == maxTermino)
                    // No hay más elementos, hacemos break con yield
                    yield break;
            }
        }
        /// <summary>
        /// Generador finito de términos de la sucesión de números Primos.
        /// </summary>
        public static IEnumerable<int> PrimoFinito_v1(int maxTermino)
        {
            int num = 0, termino = 1;
            while (true)
            {
                if (IsPrime(num))
                {
                    yield return num;
                }
                num++;
                if (termino++ == maxTermino)
                    // No hay más elementos, hacemos break con yield
                    yield break;

            }
        }

        public static IEnumerable<int> PrimoFinito(int count)
        {
            int num = 2;
            int found = 0;
            while (found < count)
            {
                if (IsPrime(num))
                {
                    yield return num;
                    found++;
                }
                num++;
            }
        }


        static internal IEnumerable<int> ImparesGeneradorEstricto(int desde, int cantidad)
        {
            int n = 1, contador = 0;
            //Propósito de simulación, el cálculo sería mucho más simple sin usar while.
            //Avanzamos hasta llegar al término inicial.
            while (contador < desde)
            {
                n += 2;
                contador++;
            }
            IList<int> resultado = new List<int>();
            contador = 0;
            while (contador < cantidad)
            {
                contador++;
                resultado.Add(n);
                n += 2;
            }
            return resultado;
        }

        /// <summary>
        /// Secuencia infinita de impares implementada de manera perezosa (o lazy).
        /// </summary>
        static private IEnumerable<int> ImparesGeneradorLazy()
        {
            int n = 1;
            while (true)
            {
                if (n % 2 != 0)
                    yield return n;
                n += 2;
            }
        }
        static internal IEnumerable<int> NumerosImparesLazy(int desde, int cuantos)
        {
            //using System.Linq métodos extensores
            //Skip salta N elementos.
            //Take selecciona N elementos a partir del actual.
            return ImparesGeneradorLazy().Skip(desde).Take(cuantos);
        }
        internal static IEnumerable<int> FactorialFinito(int maxTermino)
        {
            int n = 1;
            int factorial = 1;
            int count = 0;
            while (count < maxTermino)
            {
                yield return factorial;
                n++;
                factorial *= n;
                count++;
            }
        }

        internal static IEnumerable<int> ImparesFinito(int max)
        {
            int n = 1;
            while (n < max)
            {
                yield return n;
                n += 2;
            }
        }

        internal static IEnumerable<double> AleatoriosFinito(int max)
        {
            int n = 0;
            Random rand = new Random();
            while (n < max)
            {
                yield return rand.NextDouble();
                n++;
            }
        }
        public static bool IsPrime(int n)
        {
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
            return true;
        }

        // parametro opcional maximo
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action, int? maximoElementos = null)
        {
            int contador = 0;
            foreach (T item in enumerable)
            {
                if (maximoElementos.HasValue && maximoElementos.Value < contador++)
                    break;
                action(item);
            }
        }

        public static IEnumerable<Libro> GeneradorInfinito(int longitud, int numPags = 1) // parámetro opcional
        {

            while (true)
            {
                String tituloLibro = "";
                while (longitud > 0)
                {
                    // genera 1 caracter
                    Random r = new Random();
                    //char c = (char)r.Next(97, 123);

                    tituloLibro += (char)r.Next(97, 123);
                }


                Libro elLibro = new Libro(tituloLibro, numPags);

                yield return elLibro;

            }
        }
    }

    public class Libro
    {
        public string Titulo { get; }
        public int NumPaginas { get; }

        public Libro(string titulo, int numPaginas)
        {
            this.Titulo = titulo;
            this.NumPaginas = numPaginas;
        }

        public override string ToString()
        {
            return $"Título: {Titulo} Número de páginas: {NumPaginas}";
        }

    }
}
