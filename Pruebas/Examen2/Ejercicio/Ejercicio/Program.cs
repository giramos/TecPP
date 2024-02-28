using System;
using System.Collections.Generic;

namespace Ejercicio
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var a = Generador(100, 20);
            foreach(var i in a)
            {
                Console.WriteLine(i);
            }
            
        }

        static Func<int> Metodo(int valor)
        {
            Random r = new Random();
            int currentValue = valor; // Guarda el valor inicial

            return () =>
            {
                if (currentValue <= 0)
                {
                    currentValue = valor; // Reinicia al valor inicial cuando llega a cero
                }

                int aleatorio = r.Next(0, currentValue); // Genera el número aleatorio
                currentValue = aleatorio; // Actualiza el valor actual al aleatorio generado
                return aleatorio; // Devuelve el número aleatorio
            };
        }

        static IEnumerable<int> Generador(int valor, int term)
        {
            int cont = 1;
            var generador = Metodo(valor);
            while (true)
            {
                yield return generador();
                cont++;
                if (cont == term)
                    yield break;
            }
        }
    }
}
