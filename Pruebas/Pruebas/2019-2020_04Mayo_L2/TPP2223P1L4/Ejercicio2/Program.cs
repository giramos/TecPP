using System;
using System.Collections;
using System.Collections.Generic;


namespace Ejercicio2
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            string[] palabras = { "uno", "dos", "tres", "cuatro"};
            int[] enteros = { 1, 2, 3, 4, 5 };
            var res = Metodo<string, int, string>(palabras, enteros, (a, b) =>
            {
                //return String.Concat(a + ":" + b.ToString());
                 return $"{a}:{b}";
            });
            foreach (var item in res) { Console.Write(item + ","); }

            Console.WriteLine();

             res = Metodo1<string, int, string>(palabras, enteros, (a, b) =>
            {
                return String.Concat(a + ":" + b.ToString());
            });
            foreach (var item in res) { Console.Write(item + ","); }
        }

        public static IList<S> Metodo<T, R, S>(IEnumerable<T> col1, IEnumerable<R> col2, Func<T, R, S> func)
        {
            IList<S> lista = new List<S>();
            IEnumerator ie1 = col1.GetEnumerator();
            IEnumerator ie2 = col2.GetEnumerator();
            while (ie1.MoveNext() && ie2.MoveNext())
            {
                lista.Add(func((T)ie1.Current, (R)ie2.Current));
            }
            return lista;

        }

        public static IList<S> Metodo1<T, R, S>(IEnumerable<T> col1, IEnumerable<R> col2, Func<T, R, S> func)
        {
            IList<S> lista = new List<S>();

            int i = 0;
            foreach (T item1 in col1)
            {
                int j = 0;
                foreach (R item2 in col2)
                {
                    if (i == j) // Solo combina elementos cuando los índices coinciden
                    {
                        lista.Add(func(item1, item2));
                        break; // Rompe el bucle para avanzar al siguiente elemento de col1
                    }
                    j++;
                }
                i++;
            }

            return lista;
        }


    }

}
