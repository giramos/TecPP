using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace _09_Diccionario
{
    class Program
    {
        private static Random random = new Random();

        static void Main(string[] args)
        {
            int[] array = GenerateRandomVector(50);
            Console.WriteLine("\nThe vector is: ");
            Show(array);

            Console.WriteLine("\nCalculating the positions");
            Master<int> master = new Master<int>(array, 4);
            var dictionary = master.ComputeDictionary();

            int lengthSummation = 0;
            foreach (var keyValue in dictionary)
            {
                Console.Write($"[{keyValue.Key}] : ");
                Show(keyValue.Value);
                lengthSummation += keyValue.Value.Count();
            }

            Console.WriteLine($"\nLa suma de las longitudes es: {lengthSummation}");
        }

        private static void Show(IEnumerable<int> collection)
        {
            Console.Write("{ ");
            int counter = 0;
            foreach (var element in collection)
            {
                if (counter == collection.Count() - 1)
                {
                    Console.Write($"{element}");
                }
                else
                {
                    Console.Write($"{element}, ");
                }
                counter++;
            }
            Console.Write(" }\n");
        }

        public static int[] GenerateRandomVector(int length)
        {
            int[] array = new int[length];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(1, 50);
            }
            return array;
        }
    }

    internal class Master<T>
    {
        private T[] array;
        private int numberOfThreads;

        public Master(T[] array, int v)
        {
            this.array = array;
            this.numberOfThreads = v;
        }

        public Dictionary<T, IEnumerable<int>> ComputeDictionary()
        {
            Worker<T>[] workers = new Worker<T>[numberOfThreads];
            int elementsPerThread = array.Length / numberOfThreads;
            for (int i = 0; i < workers.Length; i++)
            {
                workers[i] = new Worker<T>(array,
                                        i * elementsPerThread,
                                        (i < numberOfThreads - 1) ? (i + 1) * elementsPerThread - 1 : this.array.Length - 1);
            }

            Thread[] threads = new Thread[workers.Length];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(workers[i].ComputeDict);
                threads[i].Name = $"Worker Thread {i}";
                threads[i].Priority = ThreadPriority.Normal;
                threads[i].Start();
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            var dictionary = new Dictionary<T, IEnumerable<int>>();
            foreach (Worker<T> worker in workers)
            {
                foreach (var key in worker.Dict.Keys)
                {
                    if (dictionary.ContainsKey(key))
                    {
                        dictionary[key] = worker.Dict[key];
                    }
                    else
                    {
                        dictionary.Add(key, worker.Dict[key]);
                    }
                }
            }
            return dictionary;
        }
    }

    internal class Worker<T>
    {
        private T[] array;

        private int from;

        private int to;

        public Dictionary<T, IEnumerable<int>> Dict;

        public Worker(T[] array, int v1, int v2)
        {
            this.array = array;
            this.from = v1;
            this.to = v2;
        }

        public void ComputeDict()
        {
            var positions = new List<int>();
            var dictionary = new Dictionary<T, IEnumerable<int>>();
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    if (array[i].Equals(array[j]))
                    {
                        positions.Add(j);
                    }
                }
                if (!dictionary.ContainsKey(array[i]))
                {
                    dictionary.Add(array[i], positions);
                }
                positions = new List<int>();
            }
            Dict = dictionary;
        }
    }
}
