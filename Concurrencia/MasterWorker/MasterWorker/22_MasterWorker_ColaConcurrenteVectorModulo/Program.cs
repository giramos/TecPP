using ColaConcurrente;
using System;
using System.Threading;

namespace _22_MasterWorker_ColaConcurrenteVectorModulo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            short[] vector = CrearVectorAleatorio(100000, -10, 10);
            Console.WriteLine("HOLA");
            // * Cálculo con un solo hilo y con cola de trabajos
            double resultado = 0;
            Master master1 = new Master(vector, ref resultado, 3, 1);
            DateTime antes1 = DateTime.Now;
            double resultado1 = master1.CalcularModulo();
            DateTime despues1 = DateTime.Now;
            Console.WriteLine("Resultado del cálculo con un hilo y con cola de trabajos: {0:N2}.", resultado1);
            Console.WriteLine("Tiempo transcurrido: {0:N0} ticks de reloj.",
                (despues1 - antes1).Ticks);

            //// * Computación con cuatro hilos y con cola de trabajos
            //master1 = new Master1(vector, 4);
            //antes1 = DateTime.Now;
            //resultado1 = master1.CalcularModulo();
            //despues1 = DateTime.Now;
            //Console.WriteLine("Resultado del cálculo con cuatro hilos y con cola de trabajos: {0:N2}.", resultado1);
            //Console.WriteLine("Tiempo transcurrido: {0:N0} ticks de reloj.",
            //    (despues1 - antes1).Ticks);
        }

        /// <summary>
        /// Crea un vector aleatorio de números cortos
        /// </summary>
        /// <param name="numeroElementos">El tamaño del vector</param>
        /// <param name="menor">El valor más bajo para ser utilizado en la generación de elementos de vectores</param>
        /// <param name="mayor">El mayor valor para ser utilizado en la generación de elementos de vectores</param>
        /// <returns>El vector aleatorio</returns>
        public static short[] CrearVectorAleatorio(int numeroElementos, short menor, short mayor)
        {
            short[] vector = new short[numeroElementos];
            Random random = new Random();
            for (int i = 0; i < numeroElementos; i++)
                vector[i] = (short)random.Next(menor, mayor + 1);
            return vector;
        }

    }

    class Master
    {
        private short[] vector;
        private double resultado;
        private int numHilos;
        private Cola<int> trabajos;

        public Master(short[] vector, ref double resultado, int posPorHilo, int numHilos)
        {
            this.numHilos = numHilos;
            this.vector = vector;
            this.resultado = resultado;
            trabajos = generaTrabajos(posPorHilo, vector);
        }

        /// <summary>
        ///  Generamos los trabajos.
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        public Cola<int> generaTrabajos(int posPorHilo, short[] vector)
        {
            Cola<int> trabajos = new Cola<int>();
            int particiones = vector.Length / posPorHilo;
            for (int i = 0; i < particiones; i++)
                trabajos.Añadir(i * posPorHilo);
            return trabajos;
        }

        public double CalcularModulo()
        {
            WorkerColaConcurrente[] workers = new WorkerColaConcurrente[numHilos];
            int elementosPorHilo = vector.Length / numHilos;
            for (int i = 0; i < numHilos; i++)
            {
                workers[i] = new WorkerColaConcurrente(vector, ref resultado, trabajos, elementosPorHilo);
            }

            Thread[] hilos = new Thread[numHilos];
            for (int i = 0; i < numHilos; i++)
            {
                hilos[i] = new Thread(workers[i].Espejo);
                hilos[i].Name = "Worker Vector Espejo " + (i + 1);
                hilos[i].Priority = ThreadPriority.BelowNormal;
                hilos[i].Start();
            }
            for (int i = 0; i < hilos.Length; i++)
            {
                hilos[i].Join();

            }

            foreach (WorkerColaConcurrente worker in workers)
                resultado += worker.Resultado;
            return Math.Sqrt(resultado);
        }
    }

    class WorkerColaConcurrente
    {
        short[] vector;
        double resultado;
        Cola<int> trabajos;
        int posPorHilos;
        internal double Resultado
        {
            get { return this.resultado; }
        }
        public WorkerColaConcurrente(short[] vector, ref double resultado, Cola<int> trabajos, int posPorHilos)
        {
            this.vector = vector;
            this.resultado = resultado;
            this.trabajos = trabajos;
            this.posPorHilos = posPorHilos;
        }

        internal void Espejo()
        {
            //Mientras haya trabajos en la cola
            while (trabajos.Count() > 0)
            {
                //Sacamos el trabajo siguiente de la cola en i
                int i;
                if (trabajos.Extraer() != 0)
                {
                    //Si quedaban trabajos invertimos el vector

                    try
                    {
                        for (int j = 0; j <= j + posPorHilos; j++)
                            this.resultado += this.vector[j] * this.vector[j];
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("{0} Exception caught.", e);
                    }
                }
            }
        }
    }
}
