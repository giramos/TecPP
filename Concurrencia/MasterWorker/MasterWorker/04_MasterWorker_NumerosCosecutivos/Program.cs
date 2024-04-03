using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace _04_MasterWorker_NumerosCosecutivos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int maximoHilos = 30;
            int[] vector = { 1, 1, 2, 2, 3, 2, 2, 3, 4 };

            TimeSpan tiempoMasRapido = TimeSpan.MaxValue;
            int hilosMasRapido = 0;
            Dictionary<string, int> resultadoMasRapido = null;

            MostrarLinea(Console.Out, "Num Hilos", "Ticks", "Resultado");
            for (int numeroHilos = 1; numeroHilos <= maximoHilos; numeroHilos++)
            {
                Master master = new Master(vector, numeroHilos);
                DateTime antes = DateTime.Now;
                Dictionary<string, int> resultado = master.Calcular();
                DateTime despues = DateTime.Now;
                TimeSpan tiempoEjecucion = despues - antes;
                MostrarLinea(Console.Out, numeroHilos, tiempoEjecucion.Ticks, resultado.Count);

                if (tiempoEjecucion < tiempoMasRapido)
                {
                    tiempoMasRapido = tiempoEjecucion;
                    hilosMasRapido = numeroHilos;
                    resultadoMasRapido = resultado;
                }

                //Entre ejecuciones, limpiamos y esperamos.
                GC.Collect();
                GC.WaitForFullGCComplete();
            }

            Console.WriteLine($"La ejecución más rápida fue con {hilosMasRapido} hilos, que tardó {tiempoMasRapido.TotalMilliseconds} milisegundos.");
            Console.WriteLine("Resultado:");
            foreach (var kvp in resultadoMasRapido)
            {
                Console.WriteLine($"Pareja: {kvp.Key}, Repeticiones: {kvp.Value}");
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
        private int[] vector;
        private int numeroHilos;

        public Master(int[] vector, int numeroHilos)
        {
            if (numeroHilos < 1 || numeroHilos > 30) // Limitamos el número máximo de hilos a 30
                throw new ArgumentException("El número de hilos debe ser mayor que cero y menor o igual a 30");
            this.vector = vector;
            this.numeroHilos = numeroHilos; ;
            this.vector = vector;
            this.numeroHilos = numeroHilos;
        }

        public Dictionary<string, int> Calcular()
        {
            Dictionary<string, int> parejasConsecutivas = new Dictionary<string, int>();

            Worker[] workers = new Worker[this.numeroHilos];
            int numElementosPorHilo = this.vector.Length / numeroHilos;
            for (int i = 0; i < this.numeroHilos; i++)
            {
                int indiceDesde = i * numElementosPorHilo;
                int indiceHasta = (i + 1) * numElementosPorHilo - 1;
                if (i == this.numeroHilos - 1) //el último hilo, llega hasta el final del vector.
                {
                    indiceHasta = this.vector.Length - 1;
                }
                workers[i] = new Worker(this.vector, indiceDesde, indiceHasta);
            }

            // Iniciamos los hilos.
            Thread[] hilos = new Thread[workers.Length];
            for (int i = 0; i < workers.Length; i++)
            {
                hilos[i] = new Thread(workers[i].Calcular);
                hilos[i].Start();
            }

            // Esperamos a que acaben los hilos.
            foreach (Thread thread in hilos)
                thread.Join();

            // Consolidamos los resultados de los workers.
            foreach (Worker worker in workers)
            {
                foreach (var pareja in worker.Resultado)
                {
                    if (parejasConsecutivas.ContainsKey(pareja))
                        parejasConsecutivas[pareja]++;
                    else
                        parejasConsecutivas[pareja] = 1;
                }
            }

            return parejasConsecutivas;
        }
    }

    internal class Worker
    {
        private int[] vector;
        private List<string> resultado;
        private int indiceDesde, indiceHasta;

        internal Worker(int[] vector, int indiceDesde, int indiceHasta)
        {
            this.vector = vector;
            this.indiceDesde = indiceDesde;
            this.indiceHasta = indiceHasta;
            this.resultado = new List<string>();
        }

        internal void Calcular()
        {
            for (int i = this.indiceDesde; i < this.indiceHasta; i++)
            {
                if (vector[i] + 1 == vector[i + 1])
                {
                    string pareja = vector[i].ToString() + "-" + vector[i + 1].ToString();
                    resultado.Add(pareja);
                }
                
            }
        }



        internal List<string> Resultado
        {
            get { return this.resultado; }
        }
    }
}
