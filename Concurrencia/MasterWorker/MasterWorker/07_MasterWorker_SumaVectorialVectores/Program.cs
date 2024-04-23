using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace _07_MasterWorker_SumaVectorialVectores
{
//    ) Modificar el proyecto de modo que calcule la suma vectorial de dos
//vectores de int, añadiendo dos atributos más de tipo vector, representando tanto
//los datos como el resultado.Realizar las modificaciones oportunas.Modificar el
//particionado de datos del siguiente modo: 
//Cada worker se crea con un atributo de tipo int: thread.Por ejemplo, si se crean
//cuatro los valores de ese atributo para cada thread serán 0, 1, 2, 3. Otro también
//int: nThread, representando el número de workers, en este ejemplo 4. Cada
//worker procesa los datos en las posiciones thread, thread+nThread,
//thread+2*nThread, etc.Entonces, si se usan 4 threads, el thread 0 procesa los
//elementos en 0,4,8,12,... el 1 en 1,5,9,13,... el 2 en 2,6,10,14,... el 3 en 3,7,11,15,...
    internal class Program
    {
        static void Main(string[] args)
        {

            const int maximoHilos = 4;
            int[] vectorA = { 1, 2, 3, 4, 5 };
            int[] vectorB = { 6, 7, 8, 9, 10 };
            MostrarLinea(Console.Out, "Num Hilos", "Ticks", "Resultado");
            for (int numeroHilos = 1; numeroHilos <= maximoHilos; numeroHilos++)
            {
                Master master = new Master(vectorA, vectorB, numeroHilos);
                DateTime antes = DateTime.Now;
                var resultado = master.CalcularModulo();
                DateTime despues = DateTime.Now;
                MostrarLinea(Console.Out, numeroHilos, (despues - antes).Ticks, resultado.Count());
                Console.WriteLine();
                mostrarVector(resultado);
                Console.WriteLine();
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

    /// <summary>
    /// Calcula el módulo de un vector de manera concurrente.
    /// ///Raiz_Cuadrada(x1^2 + x2^2 + x3^2 + ... + xn^2);
    /// Genera y cordina un conjunto de hilos trabajadores (Workers)
    /// que realizarán el cálculo.
    /// 
    /// </summary>
    public class Master
    {
        private int[] vectorA;
        private int[] vectorB;

        /// <summary>
        /// Número de trabajadores que se van a emplear en el cálculo.
        /// </summary>
        private int numeroHilos;


        public Master(int[] vectora, int[] vectorb, int numeroHilos)
        {
            if (numeroHilos < 1 || numeroHilos > vectora.Length)
                throw new ArgumentException("El número de hilos debe ser menor o igual al tamaño del vector");
            this.vectorA = vectora;
            this.vectorB = vectorb;
            this.numeroHilos = numeroHilos;
        }

        /// <summary>
        /// Este método crea y coordina el cálculo
        /// </summary>
        public int[] CalcularModulo()
        {
            int[] vectorResultado = new int[vectorA.Length];
            // Creamos los workers
            Worker[] workers = new Worker[this.numeroHilos]; // Se crea un array de objetos Worker,
                                                             //	donde cada Worker representa un hilo que realizará parte del cálculo de	l módulo del vector.
            //int numElementosPorHilo = this.vectorA.Length / numeroHilos; // Esta línea calcula cuántos elementos del vector serán procesados por cada hilo.
                                                                        // Divide la longitud total del vector entre el número de hilos (numeroHilos) para determinar cuántos elementos debe procesar cada hilo.
            for (int i = 0; i < this.numeroHilos; i++) // La variable i representa el índice del hilo actual.
            {
                workers[i] = new Worker(this.vectorA, this.vectorB, vectorResultado, i, this.numeroHilos);

                //int indiceDesde = i * numElementosPorHilo; // Calcula el índice de inicio para el segmento del vector que será procesado por el hilo actual. 
                //                                           // Multiplica el número de elementos por hilo por el índice del hilo para obtener el índice de inicio del segmento.
                //int indiceHasta = (i + 1) * numElementosPorHilo - 1; // Calcula el índice de finalización para el segmento del vector que será procesado por el hilo actual.
                //                                                     // Multiplica el número de elementos por hilo por el siguiente índice de hilo y resta 1 para obtener el índice de finalización del segmento.
                //if (i == this.numeroHilos - 1) //el último hilo, llega hasta el final del vector.
                //{
                //    indiceHasta = this.vectorA.Length - 1;
                //}
                //workers[i] = new Worker(this.vectorA, this.vectorB, vectorResultado, indiceDesde, indiceHasta);
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
            
            return vectorResultado;
        }
    }

    internal class Worker
    {

        /// <summary>
        /// Vector del que vamos a obtener el módulo
        /// </summary>
        private int[] vectora;
        private int[] vectorb;
        //private int indiceDesde, indiceHasta;
        private int thread;
        private int nThread;

        /// <summary>
        /// Resultado del cálculo
        /// </summary>
        private int[] resultado;

        internal int[] Resultado
        {
            get { return this.resultado; }
        }

        //internal Worker(int[] vector1, int[] vector2, int[] res, int indiceDesde, int indiceHasta)
        //{
        //    this.vectora = vector1;
        //    this.vectorb = vector2;
        //    this.indiceDesde = indiceDesde;
        //    this.indiceHasta = indiceHasta;
        //    this.resultado = res;
        //}

        internal Worker(int[] vectorA, int[] vectorB, int[] vectorResultado, int thread, int nThread)
        {
            this.vectora = vectorA;
            this.vectorb = vectorB;
            this.resultado = vectorResultado;
            this.thread = thread;
            this.nThread = nThread;
        }

        /// <summary>
        /// Método que realiza el cálculo
        /// </summary>
        internal void Calcular()
        {
            //for (int i = this.indiceDesde; i <= this.indiceHasta; i++)
            for (int i = thread; i < vectora.Length; i += nThread)
                resultado[i] = this.vectora[i] + this.vectorb[i];
        }

    }
}