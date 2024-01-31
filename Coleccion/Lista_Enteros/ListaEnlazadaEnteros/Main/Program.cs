using System;
using ListaEnlazada;
namespace Main

/// <summary>
/// Germán Iglesias Ramos
/// UO202549
/// Lista enlazada enteros
/// TPP2024
/// </summary>
/// 

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

            // Resultado console: -7 222 22 15 33 3 2 1 -23
            Console.WriteLine("Borrando el -7");
            l.BorrarInicio(-7);
            Console.WriteLine("\n" + l.ToString());
            Console.WriteLine("Nº elem: " + l.NumElementos);
            Console.WriteLine("Borrando el -23");
            l.Borrar(-23);
            Console.WriteLine("\n" + l.ToString());
            Console.WriteLine("Nº elem: " + l.NumElementos);
            Console.WriteLine("Borrando el 15");
            l.Borrar(15);
            Console.WriteLine("\n" + l.ToString());
            Console.WriteLine("Nº elem: " + l.NumElementos);
            Console.WriteLine("Borrando el 222");
            l.Borrar(222);
            Console.WriteLine("\n" + l.ToString());
            Console.WriteLine("Nº elem: " + l.NumElementos);

            Console.WriteLine("Borrar final");
            Console.WriteLine("Borrando el uno");
            l.BorrarFinal(1);
            Console.WriteLine("\n" + l.ToString());
            Console.WriteLine("Nº elem: " + l.NumElementos);
        }
    }
}
