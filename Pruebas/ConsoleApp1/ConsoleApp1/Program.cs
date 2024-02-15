using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 37, 888, 1 };
            Array.Sort(arr);
            Console.WriteLine(arr[0]);
            Console.WriteLine();

            var a = new Angulo[] { new(112), new(12), new(8), new(112), new(89898), new(1111111), new(3) };
            foreach (var i in a) Console.Write(i.Radianes+"-");
            Console.WriteLine();
            Sort(a);
            foreach (var i in a) Console.Write(i.Radianes+"-");

            Console.WriteLine();
            var p = new Persona[] { new("ger","igl"), new("ger", "ram"), new("geri", "igle"),
            new("ger","iglesias"), new("a","i"), new("g","i"),new("german","iglesias ramos"), new("ger","igl")};
            foreach (var i in p) Console.WriteLine($"Nombre:{i.Nombre} - Apellido:{i.Apellido}");
            Sort(p);
            foreach (var i in p) Console.WriteLine($"Nombre:{i.Nombre} - Apellido:{i.Apellido}");


            Console.WriteLine();
            var pala = "cucurellagermangavitorresmanahatan";
            var res = pala.Metodo();
            foreach(var i in res) Console.WriteLine(i.ToString());

            Console.WriteLine();
            string palabra = "Mensajero";
            List<int> distribucionVocales = palabra.Vocales();

            // Imprimimos la distribución de vocales
            Console.WriteLine("Distribución de vocales en la palabra \"" + palabra + "\":");
            Console.WriteLine("a: " + distribucionVocales[0]);
            Console.WriteLine("e: " + distribucionVocales[1]);
            Console.WriteLine("i: " + distribucionVocales[2]);
            Console.WriteLine("o: " + distribucionVocales[3]);
            Console.WriteLine("u: " + distribucionVocales[4]);

        }

        public static void Sort(ICompara[] vector) 
        {
            for (int i = 0; i < vector.Length - 1; i++)
                for (int j = vector.Length - 1; j > i; j--)
                    if (vector[i].CompareTo(vector[j])>0)
                    {
                        ICompara temp = vector[i];
                        vector[i] = vector[j];
                        vector[j] = temp;
                    }
        }

        public static List<string> Combinacion<T>(IEnumerable<T> list1, IEnumerable<T> list2)
        {
            List<string> res = new List<string>();

            IEnumerator<T> enumerator1 = list1.GetEnumerator();
            IEnumerator<T> enumerator2 = list2.GetEnumerator();

            while (enumerator1.MoveNext() && enumerator2.MoveNext())
            {
                string combinacion = enumerator1.Current.ToString() + ":" + enumerator2.Current.ToString();
                res.Add(combinacion);
            }

            return res;
        }


    }

    public static class Clase
    {
        public static IList Metodo(this string palabra)
        {
            IList lista = new ArrayList();
            int a = 0, e = 0, i = 0, o =0, u = 0;
            foreach(var p in palabra)
            {
                if (p is 'a') a++;
                else if (p is 'e') e++;
                else if (p is 'i') i++;
                else if (p is 'o') o++;
                else if (p is 'u') u++;
                else;
            }
            lista.Add(a);
            lista.Add(e);
            lista.Add(i);
            lista.Add(o);
            lista.Add(u);
            return lista;
        }

        public static List<int> Vocales(this string palabra)
        {
            // Creamos una lista con la cantidad de vocales inicializada en cero
            List<int> distribucionVocales = new List<int>(new int[5]); // [a, e, i, o, u]

            // Convertimos la cadena a minúsculas para hacer la búsqueda de vocales de forma insensible a mayúsculas
            string palabraMinusculas = palabra.ToLower();

            // Iteramos sobre cada carácter de la palabra
            foreach (char caracter in palabraMinusculas)
            {
                // Verificamos si el carácter es una vocal y lo contamos
                if ("aeiou".Contains(caracter))
                {
                    int indice = "aeiou".IndexOf(caracter);
                    distribucionVocales[indice]++;
                }
            }

            return distribucionVocales;
        }
    }
}