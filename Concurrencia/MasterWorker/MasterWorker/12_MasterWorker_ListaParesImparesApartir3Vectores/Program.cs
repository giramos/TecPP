using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace _12_MasterWorker_ListaParesImparesApartir3Vectores
{
    internal class Program
    {
        static void Main(string[] args)
        {
            short[] u = CreateRandomVector(100000, 0, 100);
            short[] v = CreateRandomVector(100000, 0, 100);
            short[] w = CreateRandomVector(100000, 0, 100);

            List<short> pares = new List<short>();
            List<short> impares = new List<short>();


            // Do the part of showing the evolution of execution times relative to the number of threads here
            Master master;
            ShowLine(Console.Out, "Numer of Threads", "Ticks", "Result");
            for (int i = 1; i < 10; i++)
            {
                master = new Master(u, v, w, pares, impares, i);
                DateTime before = DateTime.Now;
                double result = master.ComputeMean();
                DateTime after = DateTime.Now;
                ShowLine(Console.Out, i, (after - before).Ticks, result);
                GC.Collect(); // The garbage collector is run 
                GC.WaitForFullGCComplete();//the thread is more precise due to the thread collector
            }
            Console.ReadLine();
        }


        public static short[] CreateRandomVector(int numberOfElements, short lowest, short greatest)
        {
            short[] vector = new short[numberOfElements];
            Random random = new Random();
            for (int i = 0; i < numberOfElements; i++)
                vector[i] = (short)random.Next(lowest, greatest + 1);
            return vector;
        }


        private const string CSV_SEPARATOR = ";";

        static void ShowLine(TextWriter stream, string numberOfThreadsTitle, string ticksTitle, string resultTitle)
        {
            stream.WriteLine("{0}{3} {1}{3} {2}{3}", numberOfThreadsTitle, ticksTitle, resultTitle, CSV_SEPARATOR);
        }

        static void ShowLine(TextWriter stream, int numberOfThreads, long ticks, double result)
        {
            stream.WriteLine("{0}{3} {1:N0}{3} {2:N2}{3}", numberOfThreads, ticks, result, CSV_SEPARATOR);
        }
    }

    public class Master
    {
        private short[] u;
        private short[] v;
        private short[] w;

        private List<short> pairs = new List<short>();
        private List<short> odds = new List<short>();

        private int numberOfThreads;

        public Master(short[] u, short[] v, short[] w, List<short> pairs, List<short> odds, int numberOfThreads)
        {
            if (numberOfThreads < 1 || numberOfThreads > u.Length)
                throw new ArgumentException("The number of threads must be lower or equal to the elements of the vector");

            this.u = u;
            this.v = v;
            this.w = w;
            this.pairs = pairs;
            this.odds = odds;

            this.numberOfThreads = numberOfThreads;
        }

        public double ComputeMean()
        {
            Worker[] workers = new Worker[this.numberOfThreads];
            int elementsPerThread = this.u.Length / numberOfThreads;
            for (int i = 0; i < this.numberOfThreads; i++)
                workers[i] = new Worker(this.u, this.v, this.w, this.pairs, this.odds,
                    i * elementsPerThread,//From
                    (i < this.numberOfThreads - 1) ? (i + 1) * elementsPerThread - 1 : this.u.Length - 1); // to

            Thread[] threads = new Thread[workers.Length];
            for (int i = 0; i < workers.Length; i++)
            {
                threads[i] = new Thread(workers[i].Compute);
                threads[i].Name = "Vector mean u+v-w " + (i + 1); // we name then (optional)
                threads[i].Priority = ThreadPriority.Normal; // we assign them a priority (optional)
                threads[i].Start();   // we start their execution
            }

            foreach (Thread thread in threads)
                thread.Join();

            //Computing final solution
            long result = 0;
            for (int i = 0; i < workers.Length; i++)
            {
                result += workers[i].Result;
            }

            return result / u.Length;
        }

    }

    internal class Worker
    {
        private short[] u;
        private short[] v;
        private short[] w;

        private List<short> pairs = new List<short>();
        private List<short> odds = new List<short>();

        private int fromIndex, toIndex;

        private long result;
        //locking the access to the list
        private static object _lockerUEven = new object();
        private static object _lockerVEven = new object();
        private static object _lockerWEven = new object();
        private static object _lockerUOdd = new object();
        private static object _lockerVOdd = new object();
        private static object _lockerWOdd = new object();

        internal long Result
        {
            get { return this.result; }
        }

        internal Worker(short[] u, short[] v, short[] w, List<short> pairs, List<short> odds, int fromIndex, int toIndex)
        {
            this.u = u;
            this.v = v;
            this.w = w;

            this.pairs = pairs;
            this.odds = odds;

            this.fromIndex = fromIndex;
            this.toIndex = toIndex;
        }

        internal void Compute()
        {
            for (int i = this.fromIndex; i <= this.toIndex; i++)
            {
                if (u[i] % 2 == 0)
                {
                    lock (_lockerUEven) { pairs.Add(u[i]); }
                }
                if (v[i] % 2 == 0)
                {
                    lock (_lockerVEven) { pairs.Add(v[i]); }
                }
                if (w[i] % 2 == 0)
                {
                    lock (_lockerWEven) { pairs.Add(w[i]); }
                }

                if (u[i] % 2 != 0)
                {
                    lock (_lockerUOdd) { odds.Add(u[i]); }
                }
                if (v[i] % 2 != 0)
                {
                    lock (_lockerVOdd) { odds.Add(v[i]); }
                }
                if (v[i] % 2 != 0)
                {
                    lock (_lockerWOdd) { odds.Add(w[i]); }
                }

                this.result += this.u[i] + this.v[i] - this.w[i];
            }
        }
    }
}
