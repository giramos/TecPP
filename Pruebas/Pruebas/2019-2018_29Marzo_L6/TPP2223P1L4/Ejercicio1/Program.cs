﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


namespace Ejercicio1
{
    public class Program
    {
        static void Main(string[] args)
        {
            string palabra = "AvecesMeRioDoyMilVolteretas";
            Console.WriteLine("Lista con un array de dimension 5:");
            var res = palabra.Vocales();
            foreach (var i in res)
                Console.WriteLine(i);

            Console.WriteLine("\nLista:");
            res = palabra.Vocales1();
            foreach (var i in res)
                Console.WriteLine(i);

            Console.WriteLine("\n5Parametros:");
            var res1 = palabra.Vocales2();
            Console.WriteLine(res1.Item1);
            Console.WriteLine(res1.Item2);
            Console.WriteLine(res1.Item3);
            Console.WriteLine(res1.Item4);
            Console.WriteLine(res1.Item5);

            Console.WriteLine("\nTuplas:");
            var res2 = palabra.Vocales3();
            Console.WriteLine(res2.Item1);
            Console.WriteLine(res2.Item2);
            Console.WriteLine(res2.Item3);
            Console.WriteLine(res2.Item4);
            Console.WriteLine(res2.Item5);

            Console.WriteLine("\nDiccionario:");
            var res3 = palabra.Vocales4();
            foreach (var i in res3)
            {
                Console.WriteLine($"Clave:{i.Key} - Valor:{i.Value}");
            }

            //CLAUSULAS************************************


            Console.WriteLine("\nClasula");
            var res4 = ClausulaVocales(palabra);
            Console.WriteLine(res4());
            Console.WriteLine(res4());
            Console.WriteLine(res4());
            Console.WriteLine(res4());
            Console.WriteLine(res4());
            Console.WriteLine(res4());
            Console.WriteLine(res4());
            Console.WriteLine(res4());

            Console.WriteLine("\nClasula2");
            res4 = ClausulaVocales1(palabra);
            Console.WriteLine(res4());
            Console.WriteLine(res4());
            Console.WriteLine(res4());
            Console.WriteLine(res4());
            Console.WriteLine(res4());
            Console.WriteLine(res4());
            Console.WriteLine(res4());
            Console.WriteLine(res4());

            Console.WriteLine("\nClasula diccionario");
            var res5 = ClausulaVocales2(palabra);
            Console.WriteLine(res5());
            Console.WriteLine(res5());
            Console.WriteLine(res5());
            Console.WriteLine(res5());
            Console.WriteLine(res5());
            Console.WriteLine(res5());
            Console.WriteLine(res5());
            Console.WriteLine(res5());
        }
        //Parte b) del ejercicio => Cierre
        public static Func<int> ClausulaVocales(string cadena)
        {
            IList<int> list = new List<int>(new int[5]);
            string cadena1 = cadena.ToLower();

            // Calcular los recuentos de vocales
            foreach (var item in cadena1)
            {
                if ("aeiou".Contains(item))
                {
                    int indice = "aeiou".IndexOf(item);
                    list[indice]++;
                }
            }

            // Crear la clausura
            int indexCounter = 0;

            return () =>
            {
                // Devolver el recuento correspondiente y luego reiniciar si se alcanza el final
                int count = list[indexCounter];
                indexCounter = (indexCounter + 1) % 5; // Para volver al inicio cuando se alcanza la última vocal
                return count;
            };
        }

        public static Func<int> ClausulaVocales1(string cadena)
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
            int indice = 0;
            return () =>
            {
                int cont = list[indice];
                indice = (indice + 1) % 5; // condicion cuando se llegue al ultimo indice, el 5, el de la u
                return cont;

            };
        }

        //Diccionario
        public static Func<int> ClausulaVocales2(string cadena)
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
            // Crear la clausura
            List<char> vowels = new List<char>("aeiou");
            int indexCounter = 0;

            return () =>
            {
                // Devolver el recuento correspondiente y luego reiniciar si se alcanza el final
                char vowel = vowels[indexCounter];
                int count = dicc.ContainsKey(vowel) ? dicc[vowel] : 0;
                indexCounter = (indexCounter + 1) % 5; // Para volver al inicio cuando se alcanza la última vocal
                return count;
            };
        }


    }
    /// <summary>
    /// Parte a) del ejercicio => 5 formas de realizarlo
    /// </summary>
    public static class Extensor
    {
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
    }


}
