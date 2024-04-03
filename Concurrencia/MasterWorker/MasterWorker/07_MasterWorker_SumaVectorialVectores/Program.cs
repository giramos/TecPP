using System;
using System.Threading;

namespace _07_MasterWorker_SumaVectorialVectores
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] vectorA = { 1, 2, 3, 4, 5 };
            int[] vectorB = { 6, 7, 8, 9, 10 };
            int[] resultado = new int[vectorA.Length];

            Master master = new Master(vectorA, vectorB, resultado, 4);
            master.CalcularSumaVectorial();

            Console.WriteLine("Resultado de la suma vectorial: " + string.Join(", ", resultado));
        }
    }

    class Master
    {
        private readonly int[] vectorA;
        private readonly int[] vectorB;
        private readonly int[] resultado;
        private readonly int numeroHilos;

        public Master(int[] vectorA, int[] vectorB, int[] resultado, int numeroHilos)
        {
            this.vectorA = vectorA;
            this.vectorB = vectorB;
            this.resultado = resultado;
            this.numeroHilos = numeroHilos;
        }

        public void CalcularSumaVectorial()
        {
            Thread[] hilos = new Thread[numeroHilos];
            for (int i = 0; i < numeroHilos; i++)
            {
                int threadId = i;
                hilos[i] = new Thread(() => Worker(threadId));
                hilos[i].Start();
            }

            foreach (var hilo in hilos)
            {
                hilo.Join();
            }
        }

        private void Worker(int threadId)
        {
            int nThread = numeroHilos;
            for (int i = threadId; i < vectorA.Length; i += nThread)
            {
                resultado[i] = vectorA[i] + vectorB[i];
            }
        }
    }
}

