using System;
using System.IO;
using System.Threading;

namespace _01_MasterWorker
{
    internal class Program
    {
        static void Main(string[] args)
        {

            const int maximoHilos = 50;
            short[] vector = CrearVectorAleatorio(100000, -10, 10);
            MostrarLinea(Console.Out, "Num Hilos", "Ticks", "Resultado");
            for (int numeroHilos = 1; numeroHilos <= maximoHilos; numeroHilos++)
            {
                Master master = new Master(vector, numeroHilos);
                DateTime antes = DateTime.Now;
                double resultado = master.CalcularModulo();
                DateTime despues = DateTime.Now;
                MostrarLinea(Console.Out, numeroHilos, (despues - antes).Ticks, resultado);

                //Entre ejecuciones, limpiamos y esperamos.
                GC.Collect();
                GC.WaitForFullGCComplete();
            }


        }
        public static short[] CrearVectorAleatorio(int numElementos, short menor, short mayor)
        {
            short[] vector = new short[numElementos];
            Random random = new Random();
            for (int i = 0; i < numElementos; i++)
                vector[i] = (short)random.Next(menor, mayor + 1);
            return vector;
        }

        static void MostrarLinea(TextWriter stream, string numHilosCabecera, string ticksCabecera, string resultadoCabecera)
        {
            stream.WriteLine("{0};{1};{2}", numHilosCabecera, ticksCabecera, resultadoCabecera);
        }

        static void MostrarLinea(TextWriter stream, int numHilos, long ticks, double resultado)
        {
            stream.WriteLine("{0};{1:N0};{2:N2}", numHilos, ticks, resultado);
        }
    }

    /// <summary>
    /// Calcula el módulo de un vector de manera concurrente.
    /// ///Raiz_Cuadrada(x1^2 + x2^2 + x3^2 + ... + xn^2);
    /// Genera y cordina un conjunto de hilos trabajadores (Workers)
    /// que realizarán el cálculo.
    /// 
    /// </summary>
    public class Master
    {
        /// <summary>
        /// Vector del que se obtendrá el módulo
        /// </summary>
        private short[] vector;

        /// <summary>
        /// Número de trabajadores que se van a emplear en el cálculo.
        /// </summary>
        private int numeroHilos;


        public Master(short[] vector, int numeroHilos)
        {
            if (numeroHilos < 1 || numeroHilos > vector.Length)
                throw new ArgumentException("El número de hilos debe ser menor o igual al tamaño del vector");
            this.vector = vector;
            this.numeroHilos = numeroHilos;
        }

        /// <summary>
        /// Este método crea y coordina el cálculo
        /// </summary>
        public double CalcularModulo()
        {
            // Creamos los workers
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

            //Por último, sumamos todos los resultados de los trabajadores.
            //y devolvemos la raiz cuadrada.
            long resultado = 0;
            foreach (Worker worker in workers)
                resultado += worker.Resultado;
            return Math.Sqrt(resultado);
        }
    }

    internal class Worker
    {

        /// <summary>
        /// Vector del que vamos a obtener el módulo
        /// </summary>
        private short[] vector;

        /// <summary>
        /// Índices que indican el rango de elementos del vector 
        /// con el que vamos a trabajar.
        /// En el intervalo se incluyen ambos índices.
        /// </summary>
        private int indiceDesde, indiceHasta;

        /// <summary>
        /// Resultado del cálculo
        /// </summary>
        private long resultado;

        internal long Resultado
        {
            get { return this.resultado; }
        }

        internal Worker(short[] vector, int indiceDesde, int indiceHasta)
        {
            this.vector = vector;
            this.indiceDesde = indiceDesde;
            this.indiceHasta = indiceHasta;
        }

        /// <summary>
        /// Método que realiza el cálculo
        /// </summary>
        internal void Calcular()
        {
            this.resultado = 0;
            for (int i = this.indiceDesde; i <= this.indiceHasta; i++)
                this.resultado += this.vector[i] * this.vector[i];
        }

    }
}
