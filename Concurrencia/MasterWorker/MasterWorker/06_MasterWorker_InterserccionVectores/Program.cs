using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace _06_MasterWorker_InterserccionVectores
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] vectorA = { 15, 18, 6, 14, 1, 7, 8, 2, 3, 0 };
            int[] vectorB = { 1, 2, 6, 7, 8 };

            // Prueba con 1 hilo
            Master master1 = new Master(vectorA, vectorB, 1);
            Console.WriteLine("Resultado con 1 hilo: " + string.Join(", ", master1.CalcularInterseccion()));

            // Prueba con 4 hilos
            Master master4 = new Master(vectorA, vectorB, 4);
            Console.WriteLine("Resultado con 4 hilos: " + string.Join(", ", master4.CalcularInterseccion()));
        }
    }

    class Master
    {
        private readonly int[] vectorA;
        private readonly int[] vectorB;
        private readonly int numeroHilos;
        private readonly List<int> resultadoInterseccion;

        public Master(int[] vectorA, int[] vectorB, int numeroHilos)
        {
            this.vectorA = vectorA;
            this.vectorB = vectorB;
            this.numeroHilos = numeroHilos;
            this.resultadoInterseccion = new List<int>();
        }

        public int[] CalcularInterseccion()
        {
            // Dividir el trabajo entre los hilos
            int elementosPorHilo = vectorA.Length / numeroHilos;
            Thread[] hilos = new Thread[numeroHilos];
            for (int i = 0; i < numeroHilos; i++)
            {
                int indiceInicio = i * elementosPorHilo;
                int indiceFin = (i == numeroHilos - 1) ? vectorA.Length : (i + 1) * elementosPorHilo;
                hilos[i] = new Thread(() => Worker(indiceInicio, indiceFin));
                hilos[i].Start();
            }

            // Esperar a que todos los hilos terminen
            foreach (var hilo in hilos)
            {
                hilo.Join();
            }

            return resultadoInterseccion.ToArray();
        }

        private void Worker(int indiceInicio, int indiceFin)
        {
            for (int i = indiceInicio; i < indiceFin; i++)
            {
                if (vectorB.Contains(vectorA[i]))
                {
                    lock (resultadoInterseccion)
                    {
                        resultadoInterseccion.Add(vectorA[i]);
                    }
                }
            }
        }
    }
}
