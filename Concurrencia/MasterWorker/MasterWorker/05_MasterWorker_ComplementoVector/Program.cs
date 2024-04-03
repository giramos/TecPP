using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterWorkerComplementoVector
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] vector = { 4, 5, 6, 8, 9, 10, 13, 15, 16, 18 };
            int[] universo = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

            // Inicializar y ejecutar Master
            Master master = new Master(vector, universo);
            int[] complemento = master.CalcularComplemento();

            // Mostrar el resultado
            Console.WriteLine("Complemento del conjunto en el universo:");
            Console.WriteLine(string.Join(", ", complemento));
        }
    }

    class Master
    {
        private readonly int[] vector;
        private readonly int[] universo;

        public Master(int[] vector, int[] universo)
        {
            this.vector = vector;
            this.universo = universo;
        }

        public int[] CalcularComplemento()
        {
            int numWorkers = Environment.ProcessorCount; // Número de workers
            int chunkSize = universo.Length / numWorkers; // Tamaño de cada segmento del universo para cada worker

            // Iniciar tasks para cada worker
            Task<int[]>[] tasks = new Task<int[]>[numWorkers];
            for (int i = 0; i < numWorkers; i++)
            {
                int start = i * chunkSize;
                int end = (i == numWorkers - 1) ? universo.Length : (i + 1) * chunkSize;
                tasks[i] = Task.Factory.StartNew(() => Worker(start, end));
            }

            // Recopilar resultados de cada worker
            List<int> complemento = new List<int>();
            foreach (var task in tasks)
            {
                complemento.AddRange(task.Result);
            }

            return complemento.ToArray();
        }

        private int[] Worker(int start, int end)
        {
            List<int> complemento = new List<int>();

            for (int i = start; i < end; i++)
            {
                if (!vector.Contains(universo[i]))
                {
                    complemento.Add(universo[i]);
                }
            }

            return complemento.ToArray();
        }
    }
}
