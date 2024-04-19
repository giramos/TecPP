using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace _11_DiccionarioPalabrasRepetidas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ejercicio Dicc Maria");
            List<string> palabras = new List<string>() { "Maria", "Jose", "Lucas", "Jose", "Pili", "Maria", "Pedro", "Rosa", "Jose", "Pablo", "Concha", "Concha" };

            const int maximoHilos = 12;
            MostrarLinea(Console.Out, "Num Hilos", "Ticks", "Resultado");

            //Toma de tiempos.
            Stopwatch stopWatch = new Stopwatch();

            for (int numeroHilos = 1; numeroHilos <= maximoHilos; numeroHilos++)
            {
                Master master = new Master(palabras, numeroHilos);
                stopWatch.Restart();
                var resultado = master.Calcular();
                stopWatch.Stop();

                MostrarLinea(Console.Out, numeroHilos, stopWatch.ElapsedTicks, resultado);

                //Entre ejecuciones, limpiamos y esperamos.
                GC.Collect();
                GC.WaitForFullGCComplete();
            }

        }

        static void MostrarLinea(TextWriter stream, string numHilosCabecera, string ticksCabecera, string resultadoCabecera)
        {
            stream.WriteLine("{0};{1};{2}", numHilosCabecera, ticksCabecera, resultadoCabecera);
        }

        static void MostrarLinea(TextWriter stream, int numHilos, long ticks, Dictionary<string, int> resultado)
        {
            stream.WriteLine("{0};{1:N0}", numHilos, ticks);
            foreach (var i in resultado)
            {
                Console.Write(i + " ");
            }
        }
    }

    internal class Master
    {
        /// <summary>
        /// Lista de nombres (origen)
        /// </summary>
        private List<string> nombres;
        /// <summary>
        /// Lista de resultados que coincidan con el valor (criterio) dado
        /// En este caso que sean > valor
        /// </summary>
        //private Dictionary<string, int> resultado = new Dictionary<string, int>();
        /// <summary>
        /// Número de trabajadores que se van a emplear en el cálculo.
        /// </summary>
        private int numeroHilos;


        public Master(List<string> lista, int numeroHilos)
        {
            if (numeroHilos < 1 || numeroHilos > lista.Count)
                throw new ArgumentException("El número de hilos debe ser menor o igual al tamaño de la lista");
            this.nombres = lista;
            this.numeroHilos = numeroHilos;

        }


        /// <summary>
        /// Este método crea y coordina el cálculo
        /// </summary>
        public Dictionary<string, int> Calcular()
        {
            Dictionary<string, int> dicc = new Dictionary<string, int>();

            // Creamos los workers
            Worker[] workers = new Worker[this.numeroHilos];
            int numElementosPorHilo = this.nombres.Count / numeroHilos; // Reparte el trabajo
            for (int i = 0; i < this.numeroHilos; i++)
            {
                int indiceDesde = i * numElementosPorHilo;
                int indiceHasta = (i + 1) * numElementosPorHilo - 1;
                if (i == this.numeroHilos - 1) //el último hilo, llega hasta el final del vector.
                {
                    indiceHasta = this.nombres.Count - 1;
                }
                workers[i] = new Worker(this.nombres, dicc, indiceDesde, indiceHasta);
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
            // Es fundamental entender cómo y cuándo usar el Join.
            foreach (Thread thread in hilos)
            {
                thread.Join();
            }

            // Combinamos los resultados de los workers
            foreach (Worker worker in workers)
            {
                foreach (var kvp in worker.Resultado)
                {
                    dicc[kvp.Key] = kvp.Value;
                }
            }

            return dicc ;
        }
    }

    internal class Worker
    {
        /// <summary>
        /// Lista del que
        /// </summary>
        private List<string> nombres;

        private int indiceDesde, indiceHasta;

        private Dictionary<string, int> resultado;

        public Dictionary<string, int> Resultado { get { return resultado; } }
        /// <summary>
        /// Objeto estatico que actua como bloqueador. Ya que se usa la misma lista por varios hilos
        /// </summary>
        private static readonly object objeto_locker = new object();

        internal Worker(List<string> lista, Dictionary<string, int> resultado, int indiceDesde, int indiceHasta)
        {
            this.nombres = lista;
            this.indiceDesde = indiceDesde;
            this.indiceHasta = indiceHasta;
            this.resultado = resultado;
        }

        /// <summary>
        /// Método que realiza el cálculo 
        /// </summary>
        internal void Calcular() // ES UN ACTIONNNNNNNNNNNNNNNNNNNNN
        {
            for (int i = this.indiceDesde; i <= this.indiceHasta; i++) // Comprobar bien el += Los indices vaya!
                lock (objeto_locker)
                {
                    if (!resultado.ContainsKey(nombres[i]))
                    {
                        resultado[nombres[i]] = 0;
                    }
                    resultado[nombres[i]]++;
                }
        }
    }
}
