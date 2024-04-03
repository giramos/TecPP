using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;

namespace _03_MasterWorker_ColaPalabrasConsonates
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConcurrentQueue<string> cola = new();
            cola.Enqueue("Jose");
            cola.Enqueue("aveces");
            cola.Enqueue("ahi");
            cola.Enqueue("main");
            cola.Enqueue("aun");
            cola.Enqueue("sacerdote");
            int maximoHilos = 20;
            MostrarLinea(Console.Out, "Num Hilos", "Ticks", "Resultado");
            for (int numeroHilos = 1; numeroHilos <= maximoHilos; numeroHilos++)
            {
                Master master = new Master(cola, numeroHilos);
                DateTime antes = DateTime.Now;
                ConcurrentQueue<string> palabrasConsonantes = master.Calcular();
                foreach(var i in palabrasConsonantes)
                {
                    Console.WriteLine(i);
                }
                DateTime despues = DateTime.Now;
                MostrarLinea(Console.Out, numeroHilos, (despues - antes).Ticks, palabrasConsonantes.Count);

                //Entre ejecuciones, limpiamos y esperamos.
                GC.Collect();
                GC.WaitForFullGCComplete();
            }
        }

        static void MostrarLinea(TextWriter stream, string numHilosCabecera, string ticksCabecera, string resultadoCabecera)
        {
            stream.WriteLine("{0};{1};{2}", numHilosCabecera, ticksCabecera, resultadoCabecera);
        }

        static void MostrarLinea(TextWriter stream, int numHilos, long ticks, int resultado)
        {
            stream.WriteLine("{0};{1:N0};{2:N0}", numHilos, ticks, resultado);
        }
    }

    public class Master
    {
        private ConcurrentQueue<string> cola;
        private int numeroHilos;

        public Master(ConcurrentQueue<string> cola, int numeroHilos)
        {
            if (numeroHilos < 1 || numeroHilos > cola.Count)
                throw new ArgumentException("El número de hilos debe ser menor o igual al tamaño de la cola");
            this.cola = cola;
            this.numeroHilos = numeroHilos;
        }

        public ConcurrentQueue<string> Calcular()
        {
            ConcurrentQueue<string> palabrasConsonantes = new ConcurrentQueue<string>();

            Worker[] workers = new Worker[this.numeroHilos];
            int numElementosPorHilo = this.cola.Count / numeroHilos;
            for (int i = 0; i < this.numeroHilos; i++)
            {
                int indiceDesde = i * numElementosPorHilo;
                int indiceHasta = (i == this.numeroHilos - 1) ? this.cola.Count - 1 : (i + 1) * numElementosPorHilo - 1;
                workers[i] = new Worker(this.cola, palabrasConsonantes, indiceDesde, indiceHasta);
            }
            // * Iniciamos los hilos.
            Thread[] hilos = new Thread[workers.Length];
            for (int i = 0; i < workers.Length; i++)
            {
                hilos[i] = new Thread(workers[i].Calcular); // Creamos el hilo
                hilos[i].Name = "Worker número: " + (i + 1); // le damos un nombre (opcional)
                hilos[i].Priority = ThreadPriority.Normal; // prioridad (opcional)
                hilos[i].Start();   // arrancamos el hilo
            }

            //¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡OJO!!!!!!!!!!!!!!!!
            //Esperamos a que acaben para continuar.
            foreach (Thread thread in hilos)
                thread.Join();

            return palabrasConsonantes;
        }
    }

    internal class Worker
    {
        private ConcurrentQueue<string> cola;

        private ConcurrentQueue<string> palabrasConsonantes;
        private int indiceDesde, indiceHasta;

        internal Worker(ConcurrentQueue<string> cola, ConcurrentQueue<string> palabrasConsonantes, int indiceDesde, int indiceHasta)
        {
            this.cola = cola;
            this.palabrasConsonantes = palabrasConsonantes;
            this.indiceDesde = indiceDesde;
            this.indiceHasta = indiceHasta;
        }

        internal void Calcular()
        {
            string palabra;
            while (cola.TryDequeue(out palabra))
            {
                if (TieneMasConsonantes(palabra))
                {
                    palabrasConsonantes.Enqueue(palabra);
                }
            }
        }

        private bool TieneMasConsonantes(string palabra)
        {
            int consonantes = 0;
            int vocales = 0;
            foreach (char c in palabra.ToLower())
            {
                if (char.IsLetter(c))
                {
                    if ("aeiou".Contains(c))
                        vocales++;
                    else
                        consonantes++;
                }
            }
            return consonantes > vocales;
        }
    }
}
