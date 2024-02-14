using System;

namespace Laboratorio03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("");
            var inter1 = new Intervalo();
            var inter2 = new Intervalo(1,25);
            var inter3 = new Intervalo(1,15);
            Console.WriteLine($"Intervalo 1: {inter1.ToString()}");
            Console.WriteLine($"Intervalo 2: {inter2.ToString()}");
            Console.WriteLine($"Intervalo 3: {inter3.ToString()}");
            Console.WriteLine($"Intervalo 1 Tamaño: {inter1.Tamaño()}");
            Console.WriteLine($"Intervalo 2 Tamaño: {inter2.Tamaño()}");
            Console.WriteLine($"Intervalo 3 Tamaño: {inter3.Tamaño()}");
            var inter4 = new Intervalo(1, 15);
            Console.WriteLine($"Son iguales?? {inter2.Equals(inter4)}");
            Console.WriteLine($"Son iguales?? {inter1.Equals(inter3)}");

            Console.WriteLine("");
            Barra barra = new();
            Barra barra1 = new(1, 2, 3);
            Barra barra2 = new(10 , 1 , 5);
            Console.WriteLine($"Barra: {barra.ToString()}");
            Console.WriteLine($"Barra 1: {barra1.ToString()}");
            Console.WriteLine($"Barra 2: {barra2.ToString()}");
            Console.WriteLine($"Barra Size: {barra.Size()}");
            Console.WriteLine($"Barra 1 Size: {barra1.Size()}");
            Console.WriteLine($"Barra 2 Size: {barra2.Size()}"); 
            Barra barra3 = new(10, 1, 5);
            Console.WriteLine($"Son iguales?? {barra2.Equals(barra3)}");

            Console.WriteLine("");
            Console.WriteLine($"Cual Es mayor inter 2 que inter 3? {Mayor(inter2, inter3)}");
            Console.WriteLine($"Cual Es mayor inter 3 que inter 2? {Mayor(inter3, inter2)}");
        }

        public static IComparable Mayor(IComparable a, IComparable b)
        {
            return a.Tamaño() > b.Tamaño() ? a : b;
        }
    }
}
