using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MasterWorkerComplementoVector
{
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        int[] vector = { 4, 5, 6, 8, 9, 10, 13, 15, 16, 18 };
    //        int[] universo = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };

    //        // Inicializar y ejecutar Master
    //        Master master = new Master(vector, universo);
    //        int[] complemento = master.CalcularComplemento();

    //        // Mostrar el resultado
    //        Console.WriteLine("Complemento del conjunto en el universo:");
    //        Console.WriteLine(string.Join(", ", complemento));
    //    }
    //}

    //class Master
    //{
    //    private readonly int[] vector;
    //    private readonly int[] universo;

    //    public Master(int[] vector, int[] universo)
    //    {
    //        this.vector = vector;
    //        this.universo = universo;
    //    }

    //    public int[] CalcularComplemento()
    //    {
    //        int numWorkers = Environment.ProcessorCount; // Número de workers
    //        int chunkSize = universo.Length / numWorkers; // Tamaño de cada segmento del universo para cada worker

    //        // Iniciar tasks para cada worker
    //        Task<int[]>[] tasks = new Task<int[]>[numWorkers];
    //        for (int i = 0; i < numWorkers; i++)
    //        {
    //            int start = i * chunkSize;
    //            int end = (i == numWorkers - 1) ? universo.Length : (i + 1) * chunkSize;
    //            tasks[i] = Task.Factory.StartNew(() => Worker(start, end));
    //        }

    //        // Recopilar resultados de cada worker
    //        List<int> complemento = new List<int>();
    //        foreach (var task in tasks)
    //        {
    //            complemento.AddRange(task.Result);
    //        }

    //        return complemento.ToArray();
    //    }

    //    private int[] Worker(int start, int end)
    //    {
    //        List<int> complemento = new List<int>();

    //        for (int i = start; i < end; i++)
    //        {
    //            if (!vector.Contains(universo[i]))
    //            {
    //                complemento.Add(universo[i]);
    //            }
    //        }

    //        return complemento.ToArray();
    //    }
    //}

    class Program
    {
        static void Main(string[] args)
        {
            //int[] vector = { 4, 5, 6, 8, 9, 10, 13, 15, 16, 18 };
            int[] vector = { 2, 1, 6 };
            //int[] universe = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 };
            int[] universe = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            //RESULTADOS:
            //vectorComplement ={ 1,2,3,7,11,12,14,17,19,20}
            //MasterWorkerComplementoVector: 0,3,4,5,7,8,9,}

            const int maximoHilos = 20;
            MostrarLinea(Console.Out, "Num Hilos", "Ticks", "Resultado", "Vector Complemento");
            for (int numeroHilos = 1; numeroHilos <= maximoHilos; numeroHilos++)
            {
                Master master = new Master(vector, universe, numeroHilos);
                DateTime antes = DateTime.Now;
                int[] resultado = master.CalcularComplemento();
                DateTime despues = DateTime.Now;
                MostrarLinea(Console.Out, numeroHilos, (despues - antes).Ticks, resultado.Length, resultado);

                // Entre ejecuciones, limpiamos y esperamos.
                GC.Collect();
                GC.WaitForFullGCComplete();
            }
        }

        static void MostrarLinea(TextWriter stream, string numHilosCabecera, string ticksCabecera, string resultadoCabecera, string complementoCabecera)
        {
            stream.WriteLine("{0,-10} {1,-15} {2,-15} {3,-20}", numHilosCabecera, ticksCabecera, resultadoCabecera, complementoCabecera);
        }

        static void MostrarLinea(TextWriter stream, int numHilos, long ticks, int resultado, int[] complemento)
        {
            stream.Write("{0,-10} {1,-15:N0} {2,-15:N0} ", numHilos, ticks, resultado);
            foreach (var elemento in complemento)
            {
                stream.Write(elemento + " ");
            }
            stream.WriteLine();
        }
    }

    public class Master
    {
        private int[] vector;
        private int[] universe;
        private int numeroHilos;

        public Master(int[] vector, int[] universe, int numeroHilos)
        {
            if (numeroHilos < 1)
                throw new ArgumentException("El número de hilos debe ser menor o igual al tamaño del vector");
            this.vector = vector;
            this.universe = universe;
            this.numeroHilos = numeroHilos;
        }

        public int[] CalcularComplemento()
        {
            ConcurrentBag<int> complemento = new ConcurrentBag<int>();
            Worker[] workers = new Worker[this.numeroHilos];
            int numElementosPorHilo = this.universe.Length / numeroHilos; // Cambiamos a universe.Length

            for (int i = 0; i < this.numeroHilos; i++)
            {
                int indiceDesde = i * numElementosPorHilo;
                int indiceHasta = (i + 1) * numElementosPorHilo - 1;
                if (i == this.numeroHilos - 1)
                {
                    indiceHasta = this.universe.Length - 1;
                }
                workers[i] = new Worker(this.vector, this.universe, complemento, indiceDesde, indiceHasta);
            }

            Thread[] hilos = new Thread[workers.Length];
            for (int i = 0; i < workers.Length; i++)
            {
                hilos[i] = new Thread(workers[i].Calcular);
                hilos[i].Name = "Worker número: " + (i + 1);
                hilos[i].Priority = ThreadPriority.Normal;
                hilos[i].Start();
            }

            foreach (Thread thread in hilos)
                thread.Join();

            return complemento.ToArray();
        }
    }

    internal class Worker
    {
        private int[] vector;
        private int[] universe;
        private ConcurrentBag<int> complemento;
        private int indiceDesde, indiceHasta;

        internal Worker(int[] vector, int[] universe, ConcurrentBag<int> complemento, int indiceDesde, int indiceHasta)
        {
            this.vector = vector;
            this.universe = universe;
            this.complemento = complemento;
            this.indiceDesde = indiceDesde;
            this.indiceHasta = indiceHasta;
        }

        internal void Calcular()
        {
            for (int i = this.indiceDesde; i <= this.indiceHasta; i++)
            {
                if (!vector.Contains(universe[i]))
                {
                    complemento.Add(universe[i]);
                }
            }
        }
    }
}