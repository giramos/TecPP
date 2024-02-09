using Laboratorio01;
using System;
using System.Diagnostics.SymbolStore;

namespace Laboratorio02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Incrementa dos dobles por ref");
            double n1 = 2;
            double n2 = 3;
            IncrementoDosDobles(ref n1, ref n2, 1.5, 2.0);
            Console.WriteLine($"n1 ahora vale {n1} y n2 ahora vale {n2}");

            Console.WriteLine();

            Console.WriteLine("Generar trayectoria por parametros opcionales y se usa el metodo anterior de ref");
            var puntoA = GenerarTrayectoria();
            foreach(var x in puntoA)
            {
                Console.WriteLine($"Punto A: {x.ToString()}");
            }
            Console.WriteLine();

            Console.WriteLine("Mostrar trayectoria");
            MostrarTrayectoria(puntoA);

            Console.WriteLine();

            puntoA = GenerarTrayectoria(longitud:5, incX:10, incY:10);

            Console.WriteLine("Mostrar trayectoria");
            MostrarTrayectoria(puntoA);

            // Calcular la distancia total recorrida y los puntos de inicio y fin
            double distanciaRecorrida;
            Point2d puntoInicio, puntoFin;
            DistanceStartEnd(puntoA, out distanciaRecorrida, out puntoInicio, out puntoFin);

            // Mostrar los resultados
            Console.WriteLine($"Distancia recorrida: {distanciaRecorrida}");
            Console.WriteLine($"Punto de inicio: {puntoInicio}");
            Console.WriteLine($"Punto de fin: {puntoFin}");



            //>>>>>>> METODOS EXTENSORES <<<<<<<<<

            // Invertir un numero
            Console.WriteLine();
            Console.WriteLine("Invertir un numero");

            var entero = 123456.Reverse();
            Console.WriteLine(entero);
            entero = 789.Invertir();
            Console.WriteLine(entero);
        }

        static void IncrementoDosDobles(ref double n1, ref double n2, double n3, double n4)
        {
            n1 += n3;
            n2 += n4;
        }
        //        Devuelve una trayectoria, almacenada en un array Point2d con el número de elementos de
        //longitud, donde todos los puntos excepto el primero se obtienen sumando al punto anterior de la trayectoria
        //los incrementos indicados mediante la función IncrementTwoDouble previamente definida.Ver
        //proyecto de matrices en encapsulación de solución.
        static Point2d[] GenerarTrayectoria(int longitud = 10, double x0 = 0.0, double y0 = 0.0, double incX = 1.0, double incY = 1.0)
        {
            Point2d[] array = new Point2d[longitud];
            array[0] = new Point2d(x0, y0);
            for(int i=1 ; i<longitud; i++)
            {
                IncrementoDosDobles(ref x0, ref y0, incX, incY);
                array[i] = new Point2d(x0, y0);
            }
            return array;

        }

        static void MostrarTrayectoria(Point2d[] trayectoria, bool soloPrimerCuadrante = false)
        {
            foreach (var punto in trayectoria)
            {
                if (!soloPrimerCuadrante || (punto.X >= 0 && punto.Y >= 0))
                {
                    Console.WriteLine($"Punto: {punto.ToString()}");
                }
            }
        }

        static void DistanceStartEnd(Point2d[] trayectoria, out double distanciaRecorrida, out Point2d puntoInicio, out Point2d puntoFin)
        {
            distanciaRecorrida = 0;
            puntoInicio = trayectoria[0];
            puntoFin = trayectoria[trayectoria.Length - 1];

            for (int i = 0; i < trayectoria.Length - 1; i++)
            {
                distanciaRecorrida += trayectoria[i].DistanciaEuclidea(trayectoria[i + 1]);
            }
        }

    }
}
