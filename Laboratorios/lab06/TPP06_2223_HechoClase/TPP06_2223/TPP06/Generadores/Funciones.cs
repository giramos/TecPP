using System;
using System.Collections.Generic;

using System.Linq;


namespace Generadores
{
    public static class Funciones
    {

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

        static internal IEnumerable<int> ImparesGeneradorEstricto(int desde, int cantidad)
        {
            ImprimirAlerta("Entra en el generador estricto");
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
            ImprimirAlerta("Entra en el generador perezoso");
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


        static void ImprimirAlerta(string texto)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(texto);
            Console.ResetColor();
        }


    }
}
