using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace _02_MasterWorker_VectorContieneVector
{
    internal class Program
    {
        // A través de 2 arrays de enteros (el tamaño del 2º es <= al del 1º)
        // Calcular el número de veces que ses repite el 2º array en el primero.
        // Suponer que tendrá un máximo de longitudV1/longitudV2 hilos
        // Ejemplo:
        // { 2, 2, 1, 3, 2, 2, 1, 2, 1, 2, 2, 1 } y { 2, 2, 1}
        // Resultado: 3

        //Probarlo posteriormente con el RandomVector.
        //short[] v1 = CreateRandomVector(1000, 0, 4);
        //short[] v2 = CreateRandomVector(2, 0, 4);
        static void Main(string[] args)
        {
            int[] vector1 = { 2, 2, 1, 3, 2, 2, 1, 2, 1, 2, 2, 1 };
            int[] vector2 = { 2, 2, 1 };
            //int[] vector1 = CrearVectorAleatorio(1000, 0, 4);
            //int[] vector2 = CrearVectorAleatorio(2, 0, 4);
            int maximoHilos = vector1.Length / vector2.Length; // Maximo de hilos permitidos
            MostrarLinea(Console.Out, "Num Hilos", "Ticks", "Resultado");
            for (int numeroHilos = 1; numeroHilos <= maximoHilos; numeroHilos++)
            {
                Master master = new Master(vector1, vector2, numeroHilos);
                DateTime antes = DateTime.Now;
                int resultado = master.Calcular();
                DateTime despues = DateTime.Now;
                MostrarLinea(Console.Out, numeroHilos, (despues - antes).Ticks, resultado);

                //Entre ejecuciones, limpiamos y esperamos.
                GC.Collect();
                GC.WaitForFullGCComplete();
            }
        }
        public static int[] CrearVectorAleatorio(int numElementos, int menor, int mayor)
        {
            int[] vector = new int[numElementos];
            Random random = new Random();
            for (int i = 0; i < numElementos; i++)
                vector[i] = (int)random.Next(menor, mayor + 1);
            return vector;
        }

        static void MostrarLinea(TextWriter stream, string numHilosCabecera, string ticksCabecera, string resultadoCabecera)
        {
            stream.WriteLine("{0};{1};{2}", numHilosCabecera, ticksCabecera, resultadoCabecera);
        }

        static void MostrarLinea(TextWriter stream, int numHilos, long ticks, int resultado)
        {
            stream.WriteLine("{0};{1:N0};{2:N2}", numHilos, ticks, resultado);
        }
    }

    public class Master
    {
        // vector origen
        private int[] vector1;

        private int[] vector2;

        /// <summary>
        /// Número de trabajadores que se van a emplear en el cálculo.
        /// </summary>
        private int numeroHilos;

        public Master(int[] vector1, int[] vector2, int numeroHilos)
        {
            if (numeroHilos < 1 || numeroHilos > vector1.Length)
                throw new ArgumentException("El número de hilos debe ser menor o igual al tamaño del vector1 y vector2");
            if (vector2.Length > vector1.Length)
                throw new ArgumentException("El vector 2 no puede tener mas longitud que el vector 1");
            this.vector1 = vector1;
            this.vector2 = vector2;
            this.numeroHilos = numeroHilos;
        }

        /// <summary>
        /// Este método crea y coordina el cálculo
        /// </summary>
        public int Calcular()
        {
            // Creamos los workers
            Worker[] workers = new Worker[this.numeroHilos];
            for (int i = 0; i < this.numeroHilos; i++)
            {
                int indiceDesde = i * this.vector1.Length / numeroHilos;
                int indiceHasta = (i + 1) * (this.vector1.Length / numeroHilos) - 1;
                if (i == this.numeroHilos - 1) //el último hilo, llega hasta el final del vector.
                {
                    indiceHasta = this.vector1.Length - 1;
                }
                workers[i] = new Worker(this.vector1, this.vector2, indiceDesde, indiceHasta);
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

            int resultado = 0;
            foreach (Worker worker in workers)
                resultado += worker.Resultado;
            return resultado;
        }
    }

    internal class Worker
    {
        private int[] vector1;
        private int[] vector2;

        private int indiceDesde, indiceHasta;

        private int resultado;

        internal int Resultado
        {
            get { return this.resultado; }
        }

        internal Worker(int[] vector1, int[] vector2, int indiceDesde, int indiceHasta)
        {
            this.vector1 = vector1;
            this.vector2 = vector2;
            this.indiceDesde = indiceDesde;
            this.indiceHasta = indiceHasta;
        }

        internal void Calcular()
        {
            this.resultado = 0;
            for (int i = this.indiceDesde; i <= this.indiceHasta; i++)
            {
                bool coincide = true;
                for (int j = 0; j < this.vector2.Length; j++)
                {
                    if (i + j >= this.vector1.Length || this.vector1[i + j] != this.vector2[j])
                    {
                        coincide = false;
                        break;
                    }
                }
                if (coincide)
                {
                    resultado++;
                }
            }
        }

    }
}
