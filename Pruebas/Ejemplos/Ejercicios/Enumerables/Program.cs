using System;
using System.Collections.Generic;
using System.Linq;

namespace Enumerables
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enumerable mas vocales que consonantes");

            var palabras = new string[] { "casa", "perro", "gato", "mesa", "silla", "coche", "sol", "luna",
            "ahora", "huida", "quieto"};
            var res = MasVocalesQueConsonantes(palabras);
            foreach (var item in res) { Console.WriteLine(item); }

            Console.WriteLine();

            int[] a = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int[] arr = { 1, 1, 2, 2, 3, 2, 2, 3, 4 };
            //“1 - 2”, “2 - 3”, “2 - 3”, “3 - 4”


            Console.WriteLine("\nResta primera mitad del array a la segunda\n" + RestaMitadPrimeraMenosMitadSegunda(a));

            Console.WriteLine();
            Console.WriteLine("Combinacion de numeros consecutivos");
            var res1 = CombinaNumerosConsecutivos(arr);
            foreach (var i in res1)
            {
                Console.Write(i.Item1 + "-" + i.Item2 + "\t");
            }
        }
        /// <summary>
        /// Mas vocales que consonantes
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>

        public static IEnumerable<string> MasVocalesQueConsonantes(IEnumerable<string> col)
        {
            IList<string> list = new List<string>();
            string vocales = "AEIOUaeiou";

            foreach (var item in col)
            {
                int contVocales = 0;
                int contConsonantes = 0;
                foreach (var i in item)
                {

                    if (vocales.Contains(i))
                    {
                        contVocales++;
                    }
                    else
                    {
                        contConsonantes++;
                    }

                }
                if (contVocales > contConsonantes) { list.Add(item); }

            }
            return list;
        }

        /// <summary>
        /// Resta de la suma de la primera mitad de un array menos la suma de la segunda mitad 
        /// </summary>
        /// <param name="enteros"></param>
        /// <returns></returns>
        static int RestaMitadPrimeraMenosMitadSegunda(IEnumerable<int> enteros)
        {
            int mitad = enteros.Count() / 2;
            int sumaPri = 0;
            int sumaSeg = 0;
            int contador = 0;
            foreach (var i in enteros)
            {
                if (contador < mitad)
                {
                    sumaPri += i;
                    contador++;
                }
                else
                {
                    sumaSeg += i;
                    contador++;
                }
            }
            return sumaPri - sumaSeg;
        }

        /// Combinacion de pares de numeros consecutivos
        /// 
        static IList<(int, int)> CombinaNumerosConsecutivos(IEnumerable<int> enteros)
        {
            var arr = enteros.ToArray();
            var lista = new List<(int, int)>();

            for (int i = 0; i < arr.Length - 1; i++)
            {
                if (arr[i] + 1 == arr[i + 1])
                {
                    lista.Add((arr[i], arr[i + 1]));
                }
            }

            return lista;
        }
    }
}
