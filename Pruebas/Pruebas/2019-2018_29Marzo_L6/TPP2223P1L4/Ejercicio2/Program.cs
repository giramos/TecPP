using System;
using System.Collections.Generic;
using System.Linq;


namespace Ejercicio2
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var cadena = Modelo.Model.GenerarPalabras(100, 2, 4);

            var res = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Predicate<int>[] cond =
            {
                a => a % 2 == 0,
                a => a.ToString().EndsWith('0') == false
            };
            var res2 = EvaluarPredicados(cond, res);
            foreach (var i in res2)
            {
                Console.WriteLine("Nº incumplidos: " + i.Key);
                Console.WriteLine("Numeros que los incumplen");
                foreach (var j in i.Value)
                {
                    Console.WriteLine(j);
                }
                Console.WriteLine();
            }



            /// con string
            /// 
            var palabras = new string[] { "casa", "perro", "gato", "mesa", "silla", "coche", "sol", "luna" };
            var predicados = new Predicate<string>[] 
            { palabra => palabra.Length % 2 != 0
            , palabra => palabra.Length == 4 };

            var resultado = EvaluarPredicados(predicados, palabras);

            // Imprimir resultados
            foreach (var kvp in resultado)
            {
                Console.WriteLine($"Número de predicados incumplidos: {kvp.Key}");
                Console.WriteLine("Elementos:");
                foreach (var elemento in kvp.Value)
                {
                    Console.WriteLine(elemento);
                }
                Console.WriteLine();
            }


            // Con random palabras
            Console.WriteLine();
            var rPala = EvaluarPredicados(predicados, cadena);
            foreach(var s in rPala)
            {
                Console.WriteLine($"Número de predicados incumplidos: {s.Key}");
                Console.WriteLine("Elementos:");
                foreach (var t in s.Value)
                {
                    Console.WriteLine(t);
                }
                Console.WriteLine();
            }
        }
        public static IDictionary<int,IList<T>> Metodo<T>(IEnumerable<Predicate<T>> cond, IEnumerable<T> col)
        {
            IDictionary<int, IList<T>> dicc = new Dictionary<int, IList<T>>();
            IList<T> list = new List<T>();
            int cont = 0;
            foreach (var i in col) 
            {
                foreach(var t in cond)
                {
                    if (!t(i))
                    {
                        if(dicc.ContainsKey(cont))
                            dicc[cont++].Add(i);
                        dicc[cont] = list;
                    }
                }
            }
            return dicc;
        }

        public static IDictionary<int, IList<T>> EvaluarPredicados<T>(IEnumerable<Predicate<T>> predicados, IEnumerable<T> elementos)
        {
            IDictionary<int, IList<T>> resultado = new Dictionary<int, IList<T>>();

            foreach (var elemento in elementos)
            {
                int predicadosIncumplidos = 0;

                foreach (var predicado in predicados)
                {
                    if (!predicado(elemento))
                    {
                        predicadosIncumplidos++;
                    }
                }

                if (!resultado.ContainsKey(predicadosIncumplidos))
                {
                    resultado[predicadosIncumplidos] = new List<T>();
                }

                resultado[predicadosIncumplidos].Add(elemento);
            }

            return resultado;
        }

    }

}
