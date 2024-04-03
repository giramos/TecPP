using System;
using System.Threading;

namespace _08_MasterWorker_DistanciaVectores
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] vectorA = { 1, 2, 3, 4, 5 };
            int[] vectorB = { 6, 7, 8, 9, 10 };

            Master master = new Master(vectorA, vectorB, 4);
            double distancia = master.CalcularDistancia();

            Console.WriteLine("Distancia entre los vectores: " + distancia);
        }
    }

    class Master
    {
        private readonly int[] vectorA;
        private readonly int[] vectorB;
        private readonly int numeroHilos;

        public Master(int[] vectorA, int[] vectorB, int numeroHilos)
        {
            this.vectorA = vectorA;
            this.vectorB = vectorB;
            this.numeroHilos = numeroHilos;
        }

        public double CalcularDistancia()
        {
            Thread[] hilos = new Thread[numeroHilos];
            double[] resultadosParciales = new double[numeroHilos];
            for (int i = 0; i < numeroHilos; i++)
            {
                int threadId = i;
                hilos[i] = new Thread(() => resultadosParciales[threadId] = Worker(threadId));
                hilos[i].Start();
            }

            foreach (var hilo in hilos)
            {
                hilo.Join();
            }

            double distanciaTotal = 0;
            foreach (var distanciaParcial in resultadosParciales)
            {
                distanciaTotal += distanciaParcial;
            }

            return Math.Sqrt(distanciaTotal);
        }

        private double Worker(int threadId)
        {
            int nThread = numeroHilos;
            double distanciaParcial = 0;
            for (int i = threadId; i < vectorA.Length; i += nThread)
            {
                double diferencia = vectorA[i] - vectorB[i];
                distanciaParcial += diferencia * diferencia;
            }
            return distanciaParcial;
        }
    }
}