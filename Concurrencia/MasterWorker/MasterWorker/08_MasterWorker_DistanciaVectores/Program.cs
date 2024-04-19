using System;
using System.IO;
using System.Numerics;
using System.Threading;

namespace _08_MasterWorker_DistanciaVectores
{
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        int[] vectorA = { 1, 2, 3, 4, 5 };
    //        int[] vectorB = { 6, 7, 8, 9, 10 };
    //        int[] v1 = CrearVectorAleatorio(100000, -10, 10);
    //        int[] v2 = CrearVectorAleatorio(100000, -10, 10); 


    //        //Master master = new Master(vectorA, vectorB, 4);
    //        Master master = new Master(v1, v2, 4);
    //        double distancia = master.CalcularDistancia();

    //        Console.WriteLine("Distancia entre los vectores: " + distancia);
    //    }

    //    public static int[] CrearVectorAleatorio(int numElementos, int menor, int mayor)
    //    {
    //        int[] vector = new int[numElementos];
    //        Random random = new Random();
    //        for (int i = 0; i < numElementos; i++)
    //            vector[i] = random.Next(menor, mayor + 1);
    //        return vector;
    //    }
    //}

    //class Master
    //{
    //    private readonly int[] vectorA;
    //    private readonly int[] vectorB;
    //    private readonly int numeroHilos;

    //    public Master(int[] vectorA, int[] vectorB, int numeroHilos)
    //    {
    //        this.vectorA = vectorA;
    //        this.vectorB = vectorB;
    //        this.numeroHilos = numeroHilos;
    //    }

    //    public double CalcularDistancia()
    //    {
    //        Thread[] hilos = new Thread[numeroHilos];
    //        double[] resultadosParciales = new double[numeroHilos];
    //        for (int i = 0; i < numeroHilos; i++)
    //        {
    //            int threadId = i;
    //            hilos[i] = new Thread(() => resultadosParciales[threadId] = Worker(threadId));
    //            hilos[i].Start();
    //        }

    //        foreach (var hilo in hilos)
    //        {
    //            hilo.Join();
    //        }

    //        double distanciaTotal = 0;
    //        foreach (var distanciaParcial in resultadosParciales)
    //        {
    //            distanciaTotal += distanciaParcial;
    //        }

    //        return Math.Sqrt(distanciaTotal);
    //    }

    //    private double Worker(int threadId)
    //    {
    //        int nThread = numeroHilos;
    //        double distanciaParcial = 0;
    //        for (int i = threadId; i < vectorA.Length; i += nThread)
    //        {
    //            double diferencia = vectorA[i] - vectorB[i];
    //            distanciaParcial += diferencia * diferencia;
    //        }
    //        return distanciaParcial;
    //    }
    //}

    internal class Program
    {
        static void Main(string[] args)
        {

            const int maximoHilos = 50;
            short[] vectorA = { 1, 2, 3, 4, 5 };
            //short[] vectorA = CrearVectorAleatorio(100000, -10, 10);
            short[] vectorB = { 6, 7, 8, 9, 10 };
            //short[] vectorB = CrearVectorAleatorio(100000, -10, 10);
            MostrarLinea(Console.Out, "Num Hilos", "Ticks", "Resultado");
            for (int numeroHilos = 1; numeroHilos <= maximoHilos; numeroHilos++)
            {
                Master master = new Master(vectorA, vectorB, numeroHilos);
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
        private short[] vectorA;
        private short[] vectorB;

        /// <summary>
        /// Número de trabajadores que se van a emplear en el cálculo.
        /// </summary>
        private int numeroHilos;


        public Master(short[] vectora, short[] vectorb, int numeroHilos)
        {
            if (numeroHilos < 1 )
                throw new ArgumentException("El número de hilos debe ser menor o igual al tamaño del vector");
            this.vectorA = vectora;
            this.vectorB = vectorb;
            this.numeroHilos = numeroHilos;
        }

        /// <summary>
        /// Este método crea y coordina el cálculo
        /// </summary>
        public double CalcularModulo()
        {
            // Creamos los workers
            Worker[] workers = new Worker[this.numeroHilos]; // Se crea un array de objetos Worker,
                                                             //	donde cada Worker representa un hilo que realizará parte del cálculo de	l módulo del vector.
            int numElementosPorHilo = this.vectorA.Length / numeroHilos; // Esta línea calcula cuántos elementos del vector serán procesados por cada hilo.
                                                                         // Divide la longitud total del vector entre el número de hilos (numeroHilos) para determinar cuántos elementos debe procesar cada hilo.
            for (int i = 0; i < this.numeroHilos; i++) // La variable i representa el índice del hilo actual.
            {
                int indiceDesde = i * numElementosPorHilo; // Calcula el índice de inicio para el segmento del vector que será procesado por el hilo actual. 
                                                           // Multiplica el número de elementos por hilo por el índice del hilo para obtener el índice de inicio del segmento.
                int indiceHasta = (i + 1) * numElementosPorHilo - 1; // Calcula el índice de finalización para el segmento del vector que será procesado por el hilo actual.
                                                                     // Multiplica el número de elementos por hilo por el siguiente índice de hilo y resta 1 para obtener el índice de finalización del segmento.
                if (i == this.numeroHilos - 1) //el último hilo, llega hasta el final del vector.
                {
                    indiceHasta = this.vectorA.Length - 1;
                }
                workers[i] = new Worker(this.vectorA, this.vectorB, indiceDesde, indiceHasta);
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
        private short[] vectorA;
        private short[] vectorB;

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

        internal Worker(short[] vectora, short[] vectorb, int indiceDesde, int indiceHasta)
        {
            this.vectorA = vectora;
            this.vectorB = vectorb;
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
                this.resultado += (long)Math.Pow(this.vectorA[i] - this.vectorB[i], 2);
        }

    }
}