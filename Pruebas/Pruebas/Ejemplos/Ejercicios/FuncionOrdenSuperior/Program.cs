using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace FuncionOrdenSuperior
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Funcion elementos no repetidos");
            var nombres = new string[] { "Juan", "Miguel", "Pepe", "Carlos", "Pepe", "Carlos", "Carlos", "María", "Miguel", "Pepe" };
            var res1 = nombres.Metodo((a, b) => a.Contains(b));
            foreach (var i in res1) { Console.WriteLine(i); }

            Console.WriteLine();

            Console.WriteLine("Funcion: recibe dos ienumerables, devuleve un dicc");
            var cadena = GenerarPalabras(100, 2, 4);

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
            foreach (var s in rPala)
            {
                Console.WriteLine($"Número de predicados incumplidos: {s.Key}");
                Console.WriteLine("Elementos:");
                foreach (var t in s.Value)
                {
                    Console.WriteLine(t);
                }
                Console.WriteLine();
            }

            Console.WriteLine();

            Console.WriteLine("Funcion oS: combina dos ienumerables segun criterio");
            string[] palabrass = { "uno", "dos", "tres", "cuatro" };
            int[] enteros = { 1, 2, 3, 4, 5 };
            var res11 = palabrass.Combina<string, int, string>(enteros, (a, b) =>
            {
                //return String.Concat(a + ":" + b.ToString());
                return $"{a}:{b}";
            });
            foreach (var item in res11) { Console.Write(item + ","); }

            Console.WriteLine();

            res11 = palabrass.Combina1<string, int, string>(enteros, (a, b) =>
            {
                return String.Concat(a + ":" + b.ToString());
            });
            foreach (var item in res11) { Console.Write(item + ","); }
        }

        public static string[] GenerarPalabras(int cantidad, int longitudMinima, int longitudMaxima)
        {
            Random random = new();
            const string caracteres = "abcdefghijklmnopqrstuvwxyz";
            string[] palabras = new string[cantidad];

            for (int i = 0; i < cantidad; i++)
            {
                int longitud = random.Next(longitudMinima, longitudMaxima + 1);
                char[] palabra = new char[longitud];
                for (int j = 0; j < longitud; j++)
                {
                    palabra[j] = caracteres[random.Next(caracteres.Length)];
                }
                palabras[i] = new string(palabra);
            }

            return palabras;
        }

        // Metodo que recibe un inumerable de predicados y otro de elementos, devuelve un dicc con clave el numero de predicados cumplidos
        // y como entradas los elementos que no lo han cumplido

        public static IDictionary<int, IList<T>> Metodo<T>(IEnumerable<Predicate<T>> cond, IEnumerable<T> col)
        {
            IDictionary<int, IList<T>> dicc = new Dictionary<int, IList<T>>();
            IList<T> list = new List<T>();
            int cont = 0;
            foreach (var i in col)
            {
                foreach (var t in cond)
                {
                    if (!t(i))
                    {
                        if (dicc.ContainsKey(cont))
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

    public static class FuncionOrdenSuperior
    {
        // Método que recibe un Enumerable y un criterio de comparación y
        // devuelve un IEnumerable sin elementos repetidos
        public static IEnumerable<T> Metodo<T>(this IEnumerable<T> col, Func<T, T, bool> cond)
        {
            IEnumerator<T> enumerator = col.GetEnumerator();
            List<T> lista = new List<T>();

            while (enumerator.MoveNext())
            {
                T currentItem = enumerator.Current;

                // Verificamos si el elemento actual cumple con la condición proporcionada
                bool found = false;
                foreach (var item in lista)
                {
                    if (cond(currentItem, item))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    lista.Add(currentItem);
                }
            }
            return lista;
        }

        //        Impleméntese una función de orden superior genérica que combine los elementos
        //que comparten posición de dos IEnumerables.La combinación se realizará mediante una
        //función que recibirá los dos elementos y devolverá el resultado combinado.Los resultados de
        //las combinaciones se irán insertando en la lista realizada por el alumno en prácticas.Al finalizar
        //el proceso, se devolverá la lista.
        //Pruébese la funcionalidad con un array de strings: [“uno”, “dos”, “tres”, “cuatro”, “cinco”]
        //        y un
        //array de enteros[1, 2, 3, 4] para obtener como resultado[“uno:1”, “dos:2”, “tres:3”, “cuatro:4”]. 
        public static IEnumerable<R> Combina<T, Q, R>(this IEnumerable<T> col1, IEnumerable<Q> col2, Func<T, Q, R> func)
        {
            IList<R> dicc = new List<R>();
            IEnumerator<T> enum1 = col1.GetEnumerator();
            IEnumerator<Q> enum2 = col2.GetEnumerator();
            while (enum1.MoveNext() && enum2.MoveNext())
            {
                dicc.Add(func(enum1.Current, enum2.Current));
            }
            return dicc;
        }

        public static IList<S> Combina1<T, R, S>(this IEnumerable<T> col1, IEnumerable<R> col2, Func<T, R, S> func)
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

        public static IEnumerable<TResult> DuplicateWithCondition<TElement, TResult>(IEnumerable<TElement> collection, Func<TElement, TResult> f, Predicate<TElement> cond)
        {
            List<TResult> list = new List<TResult>();
            foreach (TElement x in collection)
            {
                if (cond(x) == true)
                {
                    list.Add(f(x));
                }
            }
            return list;
        }

        public static Dictionary<T, IList<T>> GroupByCondicional<T>(this IEnumerable<T> coleccion, Func<T, T> GetClave, Predicate<T> EsValido)
        {
            if (!coleccion.Any())
            {
                throw new Exception("la colección está vacía");
            }

            Dictionary<T, IList<T>> diccionario = new Dictionary<T, IList<T>>();

            foreach (var elemento in coleccion)
            {
                IList<T> lista = new List<T>();
                if (EsValido(elemento))
                { // si cumple la condición para añadirlo al diccionario
                    T clave = GetClave(elemento);

                    if (diccionario.ContainsKey(clave))
                    { // si ya existía la clave
                        lista.Add(elemento);
                        diccionario[clave] = lista;
                    }
                    else
                    { // si no existía la clave
                        lista.Add(elemento);
                        diccionario.Add(clave, lista);
                    }
                }
            }

            return diccionario;

        }
    }
}

