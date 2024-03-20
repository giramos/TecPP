using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Extensores
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Metodo que imprime nombres no repetidos");
            var nombres = new string[] { "Juan", "Miguel", "Pepe", "Carlos", "Pepe", "Carlos", "Carlos", "María", "Miguel", "Pepe" };
            var res = nombres.NoRepetidos();
            foreach (var i in res) { Console.WriteLine(i); }

            Console.WriteLine();

            Console.WriteLine("Vocales");
            var palabra = "aasfkjhfweEESIDFSINWOUASNDJHRUWBSDuusdusiwoo";
            var res1 = palabra.Vocales4();
            foreach (var i in res1) { Console.WriteLine(i); }

            Console.WriteLine();

            Console.WriteLine("Extensor de int");
            Func<int, int>[] funciones =
            {
               n => n + 1,
            n => n * 2,
            n => n - 3
            };

            var resInt = 10.MetodoInt(funciones, 15);
            Console.WriteLine(resInt);
            Console.WriteLine(resInt);

            Console.WriteLine();

            Console.WriteLine("reduceRange EXTENSOR");

            var col = new[] { 3, 4, 2, 4, 5, 6 };
            var reduce = col.ReduceRange(1, 3);
            foreach (var i in reduce) { Console.WriteLine(i); }

            Console.WriteLine();

            Console.WriteLine("Mayusculas Minusculas extensor");
            var mayMin = "GeRMaN";
            var resultado = mayMin.MayusculasMinusculas();
            foreach(var i in resultado) { Console.WriteLine(i); }

            Console.WriteLine();

            Console.WriteLine("Encore");
            var str = "BBBBBBBBBBBBNBBBBBBBBBBBBNNNBBBBBBBBBBBBBBBBBBBBBBBBNBBBBBBBBBBBBBB";
            var encore = str.EncodeRLE();
            foreach(var i in encore) { Console.WriteLine(i); }
            var decore = encore.DecodeRLE();
            foreach (var i in decore) { Console.Write(i); }

            Console.WriteLine();

            // Ejemplo de uso
            List<int> numbers = new List<int> { 10, 20, 30, 40, 50, 60, 70, 80, 90 };

            // Obtener los elementos que están en una posición cuyo valor es par
            var result = numbers.ElementsAtWhere((index, value) => index % 2 == 0 && value % 2 == 0);

            // Imprimir el resultado
            Console.WriteLine("Elementos en posición par y valor par:");
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();

            Console.WriteLine("Suma vectorial");
            Console.WriteLine("--Exercise4--");
            int[] a1 = { 2, 2, 3, 4, 5 };
            int[] a2 = { -1, 0, 2, 1 };
            Func<int, int, int> function = (x, y) => x + y;
            var resulta = a1.MapTwo(a2, function);
            //resulta.Last();
            foreach (var e in resulta)
            {
                Console.WriteLine(e);
            }
                
        }
    }

    public static class Extensores
    {
        // Metodo que recibe un Enumerable y devuelve otro sin elementos repetidos
        public static IEnumerable NoRepetidos(this IEnumerable col)
        {
            IEnumerator enum1 = col.GetEnumerator();
            IList lista = new List<object>();
            while (enum1.MoveNext())
            {
                if (!lista.Contains(enum1.Current))
                {
                    lista.Add(enum1.Current);
                }
            }
            return lista;

        }

        public static IList<int> Vocales(this string cadena)
        {
            IList<int> list = new List<int>(new int[5]);
            string cadena1 = cadena.ToLower();
            foreach (var item in cadena1)
            {
                if ("aeiou".Contains(item))
                {
                    int indice = "aeiou".IndexOf(item);
                    list[indice]++;
                }
            }
            return list;
        }

        public static IList<int> Vocales1(this string cadena)
        {
            IList<int> list = new List<int>();
            string cadena1 = cadena.ToLower();
            int contadorA = 0;
            int contadorE = 0;
            int contadorI = 0;
            int contadorO = 0;
            int contadorU = 0;
            foreach (var i in cadena1)
            {
                if (i.Equals('a')) { contadorA++; }
                else if (i.Equals('e')) { contadorE++; }
                else if (i.Equals('i')) { contadorI++; }
                else if (i.Equals('o')) { contadorO++; }
                else if (i.Equals('u')) { contadorU++; }
                else { }
            }
            list.Add(contadorA);
            list.Add(contadorE);
            list.Add(contadorI);
            list.Add(contadorO);
            list.Add(contadorU);
            return list;
        }

        public static (int, int, int, int, int) Vocales2(this string cadena)
        {
            string cadena1 = cadena.ToLower();
            int contadorA = 0;
            int contadorE = 0;
            int contadorI = 0;
            int contadorO = 0;
            int contadorU = 0;
            foreach (var i in cadena1)
            {
                if (i.Equals('a')) { contadorA++; }
                else if (i.Equals('e')) { contadorE++; }
                else if (i.Equals('i')) { contadorI++; }
                else if (i.Equals('o')) { contadorO++; }
                else if (i.Equals('u')) { contadorU++; }
                else { }
            }
            return (contadorA, contadorE, contadorI, contadorO, contadorU);
        }
        public static Tuple<int, int, int, int, int> Vocales3(this string cadena)
        {
            string cadena1 = cadena.ToLower();
            int contadorA = 0;
            int contadorE = 0;
            int contadorI = 0;
            int contadorO = 0;
            int contadorU = 0;
            foreach (var i in cadena1)
            {
                if (i.Equals('a')) { contadorA++; }
                else if (i.Equals('e')) { contadorE++; }
                else if (i.Equals('i')) { contadorI++; }
                else if (i.Equals('o')) { contadorO++; }
                else if (i.Equals('u')) { contadorU++; }
                else { }
            }
            return new Tuple<int, int, int, int, int>(contadorA, contadorE, contadorI, contadorO, contadorU);
        }

        public static IDictionary<char, int> Vocales4(this string cadena)
        {
            IDictionary<char, int> dicc = new Dictionary<char, int>();
            string cadena1 = cadena.ToLower();
            foreach (var i in cadena1)
            {
                if ("aeiou".Contains(i))
                {

                    if (!dicc.ContainsKey(i))
                    {
                        dicc[i] = 0;
                    }
                    dicc[i]++;
                }
            }
            return dicc;
        }

        public static IDictionary<string, int> MayusculasMinusculas(this string palabra)
        {
            IDictionary<string,int>dicc = new Dictionary<string,int>();
            foreach(var i in palabra)
            {
                if (char.IsLower(i))
                {
                    if (dicc.ContainsKey("min"))
                    {
                        dicc["min"]++;
                    }
                    else
                    {
                        dicc.Add("min", 1);
                    }
                }
                else
                {
                    if (dicc.ContainsKey("may"))
                    {
                        dicc["may"]++;
                    }
                    else
                    {
                        dicc.Add("may", 1);
                    }
                }
            }

            return dicc;
        }


        // Metodo extensor de INT32 que recibe un array de func<int,int> y una variable int maximo. Devuelve
        //       La función deberá devolver el resultado de ir aplicando cada una de las funciones al resultado
        //de haber aplicado la anterior(a la primera función se le pasará el propio número). Si durante el
        //proceso se supera el valor de la variable “máximo”, se devolverá el resultado acumulado hasta

        public static int MetodoInt(this Int32 entero, Func<int, int>[] funciones, int maximo)
        {
            int acumulado = funciones[0](entero);
            for (int i = 1; i < funciones.Length; i++)
            {
                acumulado = funciones[i](acumulado);
                if (acumulado > maximo)
                {
                    break; // Terminar el bucle si el acumulado supera el máximo
                }
            }
            return acumulado;
        }

        // Metodo que devuelve un ienumerable entre dos limites pasados por parametros
        public static IEnumerable<T> ReduceRange<T>(this IEnumerable<T> collection, int i, int? n = default(int))
        {
            if (i < 0 || i >= n || n >= collection.Count() || i >= collection.Count()) throw new IndexOutOfRangeException();
            List<T> result = new List<T>();

            if (n == default(int)) n = collection.Count();
            for (int k = i; k <= n; k++)
                result.Add(collection.ToArray()[k]);
            return result;
        }

        public static IEnumerable<(int, T)> EncodeRLE<T>(this IEnumerable<T> collection)
        {
            var list = new List<(int, T)>();
            int counter = 0, iter = 0; ;
            T previousElement = default;
            foreach (var item in collection)
            {
                if (iter != 0)
                {
                    if (item.Equals(previousElement))
                    {
                        counter++;
                    }
                    else
                    {
                        counter++;
                        list.Add((counter, previousElement));
                        counter = 0;
                    }
                }
                previousElement = item;
                iter++;
            }
            counter++;
            list.Add((counter, previousElement));
            return list;
        }

        public static IEnumerable<T> DecodeRLE<T>(this IEnumerable<(int, T)> collection)
        {
            var list = new List<T>();
            int counter = 0;
            foreach (var item in collection)
            {
                counter = item.Item1;
                while (counter > 0)
                {
                    list.Add(item.Item2);
                    counter--;
                }
            }
            return list;
        }

        //        Crear un método extensor para el interfaz IEnumerable<T> que
        //devuelva los elementos de la colección que están en una posición cuyo valor
        //cumple un predicado que se pasa como parámetro. Probar este método con
        //cualquier colección.
        public static IEnumerable<T> ElementsAtWhere<T>(this IEnumerable<T> source, Func<int, T, bool> predicate)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            int index = 0;
            foreach (var item in source)
            {
                if (predicate(index, item))
                    yield return item;
                index++;
            }
        }
        // Metodo por ejemplo para hacer un select de dos colecciones
        public static IEnumerable<TOutput> MapTwo<TInput1, TInput2, TOutput>(
         this IEnumerable<TInput1> list1,
         IEnumerable<TInput2> list2,
         Func<TInput1, TInput2, TOutput> function)
        {
            if (list1 == null)
                throw new ArgumentNullException(nameof(list1));
            if (list2 == null)
                throw new ArgumentNullException(nameof(list2));
            if (function == null)
                throw new ArgumentNullException(nameof(function));

            using var enumerator1 = list1.GetEnumerator();
            using var enumerator2 = list2.GetEnumerator();

            while (enumerator1.MoveNext() && enumerator2.MoveNext())
            {
                yield return function(enumerator1.Current, enumerator2.Current);
            }
        }
    }
}
