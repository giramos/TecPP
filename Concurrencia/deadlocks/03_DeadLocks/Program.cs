using System;
using System.Threading;

namespace _03_DeadLocks
{
    internal class Program
    {
        static SharedDataStructure global;

        public static void Worker1()
        {
            Console.WriteLine("Thread 1 has started");
            Thread.Sleep(1000); //Sleeps for 1 second to ensure that the main thread enters the infinite loop
            global.Add(10);
            Console.WriteLine("Thread 1 has ended");
        }
        public static void Worker2()
        {
            Console.WriteLine("Thread 2 has started");
            Thread.Sleep(1000); //Sleeps for 1 second to ensure that the main thread enters the infinite loop
                                //lock (global)
                                //{
                                //    global.Add(10);
                                //}
            global.Add(10);

            Console.WriteLine("Thread 2 has ended");
        }
        public static void Worker3()
        {
            Console.WriteLine("Thread 3 has started");
            Thread.Sleep(1000); //Sleeps for 1 second to ensure that the main thread enters the infinite loop
                                //lock (global.SharedData)
                                //{
                                //    global.SharedData.Add(10);
                                //}
            global.SharedData.Add(10);

            Console.WriteLine("Thread 3 has ended");
        }

        static void Main(string[] args)
        {

            global = new SharedDataStructure();
            new Thread(Worker1).Start(); //Thread 1
            new Thread(Worker2).Start(); //Thread 2
            new Thread(Worker3).Start(); //Thread 2

            int count = 0;
            bool exitLoop = false; // Bandera para indicar cuándo detener el bucle

            Console.WriteLine(global.ToString());
            Console.WriteLine("Infinite lock"); //lock is never released. Is this preventing Thread1, Thread2 & Thread3 from using 'global' object?
            lock (global)
            {
                while (!exitLoop)
                {
                    Thread.Sleep(100); //simulates processing. This loop is doing nothing. Program never ends
                    count++;
                    if (count % 20 == 0)
                    {
                        count -= 20;
                        Console.WriteLine(global.ToString(), "PIPI");
                    }
                    // Aquí puedes establecer una condición para detener el bucle y liberar el bloqueo
                    if (count >= 20) // Por ejemplo, después de 2000 iteraciones
                    {
                        exitLoop = true; // Establecer la bandera para salir del bucle
                    }
                }
            }
        }
    }
}
