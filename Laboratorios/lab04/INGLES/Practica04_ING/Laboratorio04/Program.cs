using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Laboratorio04
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enteros aleatorios \n");

            // Pares de enteros aleatorios
            Pair<int>[] paresEnteros = new Pair<int>[10];
            Random r = new Random();
            for (int i = 0; i < paresEnteros.Length; i++)
            {
                paresEnteros[i] = new Pair<int>(r.Next(20), r.Next(20));
            }
            Show(paresEnteros);
            
            // Comprobar Max(Algorithms.cs) con pares de enteros aleatorios


            Console.WriteLine("El maximo es: " + Algorithms.Max(paresEnteros));

            Console.WriteLine("\nChar aleatorios \n");

            // Pares de char aleatorios ("Mayusculas")
            Pair<char>[] pares = new Pair<char>[10];
            for (int i = 0; i < pares.Length; i++)
            {
                int primerElemento = r.Next(65, 90);
                int segundoElemento = r.Next(65, 90);
                pares[i] = new Pair<char>((char)primerElemento, (char)segundoElemento);
            }
            Show(pares);

            Console.WriteLine("\nMetodo sort \n");

            // Comprobar Sort(Algorithms.cs) con pares de char aleatorios
            Algorithms.Sort(pares);
            Show(pares);

            Console.WriteLine("\nMetodo max \n");

            // Comprobar Max(Algorithms.cs) con pares de char aleatorios
            
            
            Console.WriteLine("El maximo es: " + Algorithms.Max(pares));

        }

        static void Show<T>(T[] array)
        {
            foreach (var i in array)
                Console.WriteLine(i);
        } 
    }
}
