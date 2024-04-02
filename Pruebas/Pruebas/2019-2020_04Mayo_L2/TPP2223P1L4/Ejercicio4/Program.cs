using System;
using System.Collections.Generic;
using System.Linq;
using Modelo;

namespace Ejercicio4
{
    internal class Program
    {
        private static Model modelo = new Model();
        static void Main()
        {
            ConsultaA();
            ConsultaB();
        }

        private static void ConsultaA()
        {


        }

        private static void ConsultaB()
        {


        }

        private static void Show<T>(IEnumerable<T> colección)
        {
            foreach (var item in colección)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Elementos en la colección: {0}.", colección.Count());
        }
    }


}
