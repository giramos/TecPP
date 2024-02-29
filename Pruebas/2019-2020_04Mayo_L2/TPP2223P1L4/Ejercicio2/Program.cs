using System;
using System.Collections.Generic;


namespace Ejercicio2
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            string[] palabras = { "uno", "dos", "tres", "cuatro" };
            int[] enteros = { 1, 2, 3, 4 };
            (string,int) r = Combi(palabras, enteros);
            
                Console.WriteLine(r.Item1 + r.Item2);
                Console.WriteLine(r.Item1 + r.Item2);
                Console.WriteLine(r.Item1 + r.Item2);
                Console.WriteLine(r.Item1 + r.Item2);
            
          
        }

        public static Tuple<R,T> Combi<R,T>(IEnumerable<R> col1, IEnumerable<T> col2)
        {
            Tuple<R, T> lista;
            IEnumerator<R> enumerator1 = col1.GetEnumerator();
            IEnumerator<T> enumerator2 = col2.GetEnumerator();
            while(enumerator1.MoveNext() && enumerator2.MoveNext())
            {
                lista.Item1[];= enumerator1.Current;
                lista.Item2 = enumerator2.Current;
            }
            return lista;
        }

    }

}
