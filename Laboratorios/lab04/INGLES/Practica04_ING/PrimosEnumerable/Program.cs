using System;
using System.Collections.Generic;

namespace PrimosEnumerable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Primo primo = new Primo(10);
            Console.WriteLine("Foreach\n");
            ForEach(primo);
            Console.WriteLine("\nIterador\n");
            Iterador(primo);
        }

        static void ForEach<T>(IEnumerable<T> secuencia)
        {
            foreach(var item in secuencia)
            {
                Console.WriteLine(item);
            }
        }

        static void Iterador<T>(IEnumerable<T> secuencia) 
        {
            IEnumerator<T> enumerator = secuencia.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine($"{enumerator.Current}");
            }
        }
    }
}
