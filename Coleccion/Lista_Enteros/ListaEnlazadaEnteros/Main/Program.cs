using System;
using ListaEnlazada;
namespace Main
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pruebas Listas Enlazada de enteros");

            Lista l = new Lista();
            l.AñadirFinal(3);
            l.AñadirFinal(2);
            l.AñadirFinal(1);
            Console.WriteLine(l.ToString());
            Console.WriteLine("Nº elem: " + l.NumElementos);

            l.AñadirInicio(22);
            Console.WriteLine("\n" + l.ToString());
            Console.WriteLine("Nº elem: " + l.NumElementos);

            l.AñadirInicio(222);
            Console.WriteLine("\n" + l.ToString());
            Console.WriteLine("Nº elem: " + l.NumElementos);

            l.AñadirFinal(-23);
            l.AñadirInicio(-7);
            Console.WriteLine("\n" + l.ToString());
            Console.WriteLine("Nº elem: " + l.NumElementos);

            l.Añadir(15, 3);
            Console.WriteLine("\n" + l.ToString());
            Console.WriteLine("Nº elem: " + l.NumElementos);

            l.Añadir(33, 4);
            Console.WriteLine("\n" + l.ToString());
            Console.WriteLine("Nº elem: " + l.NumElementos);
        }
    }
}
