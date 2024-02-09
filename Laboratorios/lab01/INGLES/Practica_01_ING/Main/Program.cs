using Laboratorio01;
using System;

namespace Main
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Point2d puntoA = new Point2d(1.0, 1.0);
            Point2d puntoB = new Point2d(2.0, 3.5);
            Console.WriteLine($"distancia euclidea entre el punto: {puntoA.ToString()}" +
                $" y el punto: {puntoB.ToString()} es {puntoA.DistanciaEuclidea(puntoB)}");
            Console.WriteLine();
            Linea linea = new Linea(puntoA.X, puntoA.Y);
            Console.WriteLine("Linea A: " + linea.ToString());
            Linea linea1 = new Linea(puntoB.X, puntoB.Y);
            Console.WriteLine("Linea B: " + linea1.ToString());
            Linea linea2 = new Linea(puntoA.X, puntoB.Y);
            Console.WriteLine("Linea AB: " + linea2.ToString());
            var res = linea.Perpendicular(puntoA);
            Console.WriteLine($"La perpendicular entre la linea A {linea.ToString()} y el punto A :{puntoA.ToString()} es {res}");
            var res1 = linea1.DistanciaLineaPunto(puntoB);
            Console.WriteLine($"La distancia de la linea1 {linea1.ToString()} y el punto B {puntoB.ToString()} es {res1}");
            var res2 = linea2.Interseccion(res);
            Console.WriteLine($"La interseccion entre la linea2 {linea2.ToString()} y la linea res {res.ToString()} es {res2}");


        }
    }
}
