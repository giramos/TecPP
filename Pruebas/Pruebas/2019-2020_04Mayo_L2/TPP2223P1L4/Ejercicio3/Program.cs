using Modelo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ejercicio3
{
    internal static class Program
    {
        static void Main()
        {

            int[] a = { 1,2,3,4,5,6,7,8,9,10 };
            int[] arr = { 1, 1, 2, 2, 3, 2, 2, 3, 4};
             //“1 - 2”, “2 - 3”, “2 - 3”, “3 - 4”
            Console.WriteLine(Metodo(a));

            var res = Met(arr); 
            foreach ( var i in res )
            {
                Console.Write(i.Item1+"-"+i.Item2+"\t");
            }
        }
        /// <summary>
        /// Resta de la suma de la primera mitad de un array menos la suma de la segunda mitad 
        /// </summary>
        /// <param name="enteros"></param>
        /// <returns></returns>
        static int Metodo(IEnumerable<int> enteros)
        {
            int mitad = enteros.Count() / 2;
            int sumaPri = 0;
            int sumaSeg = 0;
            int contador = 0;
            foreach(var i in enteros)
            {
                if(contador<mitad)
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
            return sumaPri-sumaSeg;
        }

        /// Combinacion de pares de numeros consecutivos
        /// 
        static IList<(int, int)> Met(IEnumerable<int> enteros)
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
