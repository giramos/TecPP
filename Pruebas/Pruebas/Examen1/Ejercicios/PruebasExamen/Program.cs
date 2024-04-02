using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace PruebasExamen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var res = "comotatucucuucu".Vocales();
            foreach (var i in res) { Console.WriteLine(i); }
            Console.WriteLine();
            string palabra = "AAAEEEeeeiiiO";
            var r = Vocales(palabra);
            for (int i = 0; i < 5; i++)
                Console.WriteLine(r());
            Console.WriteLine();
            Func<int, int>[] fn =
            {
                (a) => a + 3,
                (a) => a + 2,
                (a) => a + 1,
            };
            var resAp = 40.Aplicacion(fn, 100);
            Console.WriteLine(resAp);
            var curry = AplicacionCurrificada1(40)(fn)(100);
            Console.WriteLine(curry);
            string[] cadena = { "uno", "dos", "tres", "cuatro", "cinco" };
            int[] enteros = { 1, 2, 3, 4 };
            var ff = Combi<string, int>(cadena, enteros);
            Console.Write("[");
            foreach (var i in ff)
            {
                Console.Write(i);
            }
            Console.WriteLine("]");

            Console.WriteLine();
            foreach (var i in Rand(100))
            {
                Console.WriteLine(i);
            }
            Console.WriteLine("\n Clausulas");
            var nume = RandClausula(100);
            Console.WriteLine(nume());
            Console.WriteLine(nume());
            Console.WriteLine(nume());
            Console.WriteLine(nume());

        }
        static List<int> Rand(int limite)
        {
            List<int> numeros = new List<int>();
            Random rand = new Random();

            while (limite > 0)
            {
                int numeroAleatorio = rand.Next(0, limite);
                numeros.Add(numeroAleatorio);
                limite = numeroAleatorio;
            }

            return numeros;
        }

        static Func<int> RandClausula(int limite)
        {
            Random rand = new Random();
            return () =>
            {
                int numeroAleatorio = rand.Next(0, limite);
                limite = numeroAleatorio;
                return limite;
            };

        }
        public static IEnumerable<string> Combi<T, R>(IEnumerable<T> col1, IEnumerable<R> col2)
        {
            IList<string> lista = new List<string>(col2.Count());
            IEnumerator<T> enu1 = col1.GetEnumerator();
            IEnumerator<R> enu2 = col2.GetEnumerator();
            int cont = 0;
            while (enu1.MoveNext() && enu2.MoveNext())
            {
                lista.Add($"{enu1.Current}{enu2.Current}");
            }
            return lista;

        }

        public static Func<int> Vocales(string cadena)
        {
            int cont = -1;
            var palabra = cadena.ToLower();
            return () =>
            {
                IList<int> list = new List<int>(new int[5]);
                foreach (var i in palabra)
                {
                    if ("aeiou".Contains(i))
                    {
                        int indice = "aeiou".IndexOf(i);
                        list[indice]++;
                    }
                }
                cont++;
                return list[cont];
            };
        }

        public static Func<Func<int, int>[], int, int> AplicacionCurrificada(Int32 entero)
        {
            return (array, maximo) =>
            {
                int resultado = 0;
                foreach (var i in array)
                {
                    if (resultado <= maximo)
                    {
                        resultado += i(entero);
                    }
                }
                return resultado;
            };

        }

        public static Func<Func<int, int>[], Func<int, int>> AplicacionCurrificada1(Int32 entero)
        {
            return (array) => (maximo) =>
            {
                int resultado = 0;
                foreach (var i in array)
                {
                    if (resultado <= maximo)
                    {
                        resultado += i(entero);
                    }
                }
                return resultado;
            };

        }

    }

    public static class Extensor
    {
        public static IList<int> Vocales(this String palabra)
        {
            IList<int> list = new List<int>(new int[5]);
            foreach (var i in palabra)
            {
                if ("aeiou".Contains(i))
                {
                    int indice = "aeiou".IndexOf(i);
                    list[indice]++;
                }
            }
            return list;
        }

        public static int Aplicacion(this Int32 entero, Func<int, int>[] array, int maximo)
        {
            //int contador = 0;
            int resultado = 0;
            foreach (var i in array)
            {
                if (resultado <= maximo)
                {
                    resultado += i(entero);
                    //contador++;
                }
            }
            return resultado;
        }
    }
}
