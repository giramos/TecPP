using ListaGenerica;
using System;

namespace PruebaTuLista
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Lista<int> enteros = new();
            enteros.Añadir(1);
            enteros.Añadir(1);
            enteros.Añadir(1);
            enteros.Añadir(1);
            Console.WriteLine($"Lista enteros {enteros.ToString()}");
            
            Lista<double> doubles = new();
            doubles.Añadir(1.5);
            doubles.Añadir(1.3);
            doubles.Añadir(1.2);
            doubles.Añadir(1.4);
            Console.WriteLine($"Lista enteros {doubles.ToString()}");

            Lista<char> chars = new();
            chars.Añadir('t');
            chars.Añadir('A');
            chars.Añadir('1');
            chars.Añadir('?');
            Console.WriteLine($"Lista enteros {chars.ToString()}");

            Lista<string> strings = new();
            strings.Añadir("avebruoifda");
            strings.Añadir(" ");
            strings.Añadir("null");
            strings.Añadir(null);
            Console.WriteLine($"Lista enteros {strings.ToString()}");
            Console.WriteLine();

            foreach (var i in enteros)
            {
                Console.Write(i);
            }
            Console.WriteLine();
            foreach (var i in chars)
            {
                Console.Write(i);
            }
            Console.WriteLine();

            foreach (var i in doubles)
            {
                Console.Write(i);
            }
            Console.WriteLine();

            foreach (var i in strings)
            {
                Console.Write(i);
            }

        }
    }
}
