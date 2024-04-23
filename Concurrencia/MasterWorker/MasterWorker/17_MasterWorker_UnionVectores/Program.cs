using System;
using System.IO;
using System.Threading;

namespace MasterWorkerUnionVectores
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] vectorA = { 15, 18, 6, 14, 1, 7, 8, 2, 3, 0 };
            int[] vectorB = { 1, 2, 6, 7, 8 };

            const int maximoHilos = 50;
            MostrarLinea(Console.Out, "Num Hilos", "Ticks", "Resultado", "Vector Union");
            for (int numeroHilos = 1; numeroHilos <= maximoHilos; numeroHilos++)
            {
                Master master = new Master(vectorA, vectorB, numeroHilos);
                DateTime antes = DateTime.Now;
                int[] resultado = master.CalcularUnion();
                DateTime despues = DateTime.Now;
                MostrarLinea(Console.Out, numeroHilos, (despues - antes).Ticks, resultado.Length, resultado);

                // Entre ejecuciones, limpiamos y esperamos.
                GC.Collect();
                GC.WaitForFullGCComplete();
            }
        }

        static void MostrarLinea(TextWriter stream, string numHilosCabecera, string ticksCabecera, string resultadoCabecera, string unionCabecera)
        {
            stream.WriteLine("{0,-10} {1,-15} {2,-15} {3,-20}", numHilosCabecera, ticksCabecera, resultadoCabecera, unionCabecera);
        }

        static void MostrarLinea(TextWriter stream, int numHilos, long ticks, int resultado, int[] union)
        {
            stream.Write("{0,-10} {1,-15:N0} {2,-15:N0} ", numHilos, ticks, resultado);
            stream.Write("Vector Union = {");
            foreach (var elemento in union)
            {
                stream.Write(elemento + ",");
            }
            stream.Write("}\n");
        }
    }

    public class Master
    {
        private int[] vectorA;
        private int[] vectorB;
        private int numeroHilos;

        public Master(int[] vectorA, int[] vectorB, int numeroHilos)
        {
            if (numeroHilos < 1)
                throw new ArgumentException("El número de hilos debe ser al menos 1");
            this.vectorA = vectorA;
            this.vectorB = vectorB;
            this.numeroHilos = numeroHilos;
        }

        public int[] CalcularUnion()
        {
            int[] resultado = new int[vectorA.Length + vectorB.Length];
            Worker[] workers = new Worker[this.numeroHilos]; // Array de objetos Worker, donde cada Worker representa un hilo que procesará parte de la unión de los vectores.

            int numElementosPorHilo = (this.vectorA.Length + this.vectorB.Length) / this.numeroHilos; // Calcula cuántos elementos serán procesados por cada hilo.

            for (int i = 0; i < this.numeroHilos; i++) // Iteramos sobre cada hilo
            {
                int indiceDesde = i * numElementosPorHilo; // Calcula el índice de inicio para el segmento de los vectores que será procesado por el hilo actual.
                int indiceHasta = (i + 1) * numElementosPorHilo - 1; // Calcula el índice de fin para el segmento de los vectores que será procesado por el hilo actual.

                // Si es el último hilo, aseguramos que procese hasta el final de los vectores.
                if (i == this.numeroHilos - 1)
                {
                    indiceHasta = this.vectorA.Length + this.vectorB.Length - 1;
                }

                // Creamos un nuevo Worker con los índices calculados.
                workers[i] = new Worker(this.vectorA, this.vectorB, resultado, indiceDesde, indiceHasta);
            }

            // Iniciamos los hilos.
            Thread[] hilos = new Thread[workers.Length];
            for (int i = 0; i < workers.Length; i++)
            {
                hilos[i] = new Thread(workers[i].Calcular); // Creamos el hilo
                hilos[i].Name = "Worker número: " + (i + 1); // Opcional: le damos un nombre
                hilos[i].Priority = ThreadPriority.Normal; // Opcional: definimos una prioridad
                hilos[i].Start(); // Arrancamos el hilo
            }

            foreach (Thread thread in hilos)
                thread.Join();

            return resultado;
        }
    }

    internal class Worker
    {
        private int[] vectorA;
        private int[] vectorB;
        private int[] resultado;
        private int indiceDesde, indiceHasta;
        static readonly object ob = new();

        internal Worker(int[] vectorA, int[] vectorB, int[] resultado, int indiceDesde, int indiceHasta)
        {
            this.vectorA = vectorA;
            this.vectorB = vectorB;
            this.resultado = resultado;
            this.indiceDesde = indiceDesde;
            this.indiceHasta = indiceHasta;
        }

        internal void Calcular()
        {
            int indiceResultado = indiceDesde;
            for (int i = indiceDesde; i <= indiceHasta; i++)
            {

                if (i < vectorA.Length)
                {
                    lock (ob)
                        resultado[indiceResultado++] = vectorA[i];
                }

                else
                {
                    lock (ob)
                        resultado[indiceResultado++] = vectorB[i - vectorA.Length];

                }


            }
        }
    }
}
