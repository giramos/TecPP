using Modelo;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Ejercicio3
{
    internal static class Program
    {
        static void Main()
        {
            var palabras = new string[] { "casa", "perro", "gato", "mesa", "silla", "coche", "sol", "luna",
            "ahora", "huida", "quieto"};
            var res = palabras.Metodo();
            foreach (var item in res) { Console.WriteLine(item); }

        }

        /// <summary>
        /// Mas vocales que consonantes
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>

        public static IEnumerable<string> Metodo(this IEnumerable<string> col)
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
    }
}
