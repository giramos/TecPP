using System;
using System.IO;
using System.Numerics;
using System.Threading;

namespace _15_MasterWorker_VectoriEspejo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //short[] vector = CrearVectorAleatorio(100000, -10, 10);
            short TamA = 12;
            short[] A = new short[TamA];
            for (short i = 0; i < TamA; i++)
                A[i] = (short)(i + 1);

            MostrarLinea(Console.Out, "Num Hilos", "Ticks", "Resultado");
            for (int numeroHilos = 1; numeroHilos <= TamA; numeroHilos++)
            {
                Console.WriteLine("\nVector original");
                mostrarVector<short>(A);
                Master master = new Master(A, numeroHilos);
                DateTime antes = DateTime.Now;
                short[] resultado = master.Calcular();
                DateTime despues = DateTime.Now;
                MostrarLinea(Console.Out, numeroHilos, (despues - antes).Ticks, resultado.Length);
                Console.WriteLine("\nVector espejo");
                mostrarVector<short>(resultado);
                //Entre ejecuciones, limpiamos y esperamos.
                GC.Collect();
                GC.WaitForFullGCComplete();
            }


        }
        public static void mostrarVector<T>(T[] matriz)
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                System.Console.Write(matriz[i] + " ");

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
        public short[] Calcular()
        {
            short[] res = new short[vector.Length];
            // Creamos los workers
            Worker[] workers = new Worker[this.numeroHilos]; // Se crea un array de objetos Worker,
                                                             //	donde cada Worker representa un hilo que realizará parte del cálculo de	l módulo del vector.
            int numElementosPorHilo = this.vector.Length / numeroHilos; // Esta línea calcula cuántos elementos del vector serán procesados por cada hilo.
                                                                        // Divide la longitud total del vector entre el número de hilos (numeroHilos) para determinar cuántos elementos debe procesar cada hilo.
            for (int i = 0; i < this.numeroHilos; i++) // La variable i representa el índice del hilo actual.
            {
                int indiceDesde = i * numElementosPorHilo; // Calcula el índice de inicio para el segmento del vector que será procesado por el hilo actual. 
                                                           // Multiplica el número de elementos por hilo por el índice del hilo para obtener el índice de inicio del segmento.
                int indiceHasta = (i + 1) * numElementosPorHilo - 1; // Calcula el índice de finalización para el segmento del vector que será procesado por el hilo actual.
                                                                     // Multiplica el número de elementos por hilo por el siguiente índice de hilo y resta 1 para obtener el índice de finalización del segmento.
                if (i == this.numeroHilos - 1) //el último hilo, llega hasta el final del vector.
                {
                    indiceHasta = this.vector.Length - 1;
                }
                workers[i] = new Worker(this.vector, res, indiceDesde, indiceHasta);
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

            return res;
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
        private short[] resultado;

        internal short[] Resultado
        {
            get { return this.resultado; }
        }

        internal Worker(short[] vector, short[] resul, int indiceDesde, int indiceHasta)
        {
            this.vector = vector;
            this.indiceDesde = indiceDesde;
            this.indiceHasta = indiceHasta;
            this.resultado = resul;
        }

        /// <summary>
        /// Método que realiza el cálculo
        /// </summary>
        internal void Calcular()
        {
            for (int i = this.indiceDesde; i <= this.indiceHasta; i++)
                this.resultado[resultado.Length - 1 - i] = this.vector[i];
        }

    }
}
