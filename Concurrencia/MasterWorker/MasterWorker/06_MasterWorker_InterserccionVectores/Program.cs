using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace _06_MasterWorker_InterserccionVectores
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] vectorA = { 15, 18, 6, 14, 1, 7, 8, 2, 3, 0 };
            int[] vectorB = { 1, 2, 6, 7, 8 };

            // Prueba con 1 hilo
            Master master1 = new Master(vectorA, vectorB, 1);
            Console.WriteLine("Resultado con 1 hilo: " + string.Join(", ", master1.Calcular()));

            // Prueba con 4 hilos
            Master master4 = new Master(vectorA, vectorB, 4);
            Console.WriteLine("Resultado con 4 hilos: " + string.Join(", ", master4.Calcular()));



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

        static void MostrarLinea(TextWriter stream, int numHilos, long ticks, int[] resultado)
        {
            stream.WriteLine("{0};{1:N0};{2:N2}", numHilos, ticks, Imprime(resultado));
        }

        static  string Imprime( int[]col)
        {
            string a = "";
            foreach (var item in col)
            {
                a += $"{item} ";
            }
            return a;

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

        private int[] res;

        /// <summary>
        /// Número de trabajadores que se van a emplear en el cálculo.
        /// </summary>
        private int numeroHilos;


        public Master(int[] vectora, int[]vectorb, int numeroHilos)
        {
            if (numeroHilos < 1 || numeroHilos > vectora.Length)
                throw new ArgumentException("El número de hilos debe ser menor o igual al tamaño del vector");
            if (vectorb.Length > vectora.Length)
                throw new ArgumentException("El vector 2 no puede ser mayor al vector 1");
            this.vectorA = vectora;
            this.vectorB = vectorb;
            this.numeroHilos = numeroHilos;
            this.res = new int[vectorA.Length];
        }

        /// <summary>
        /// Este método crea y coordina el cálculo
        /// </summary>
        public int[] Calcular()
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
                workers[i] = new Worker(this.vectorA, this.vectorB, this.res, indiceDesde, indiceHasta);
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
            //return res.Where(x => x != 0).ToArray(); // Filtramos los ceros y devolvemos solo los elementos válidos
            return res;
        }
    }

    internal class Worker
    {

        /// <summary>
        /// Vector del que vamos a obtener el módulo
        /// </summary>
        private int[] vectorA;
        private int[] vectorB;



        /// <summary>
        /// Índices que indican el rango de elementos del vector 
        /// con el que vamos a trabajar.
        /// En el intervalo se incluyen ambos índices.
        /// </summary>
        private int indiceDesde, indiceHasta;

        /// <summary>
        /// Resultado del cálculo
        /// </summary>
        private int[] resultado;

        private static readonly object obj = new();

        internal int[] Resultado
        {
            get { return this.resultado; }
        }

        internal Worker(int[] vectora, int[] vectorb, int[] r, int indiceDesde, int indiceHasta)
        {
            this.vectorA = vectora;
            this.vectorB = vectorb;
            this.resultado = r;
            this.indiceDesde = indiceDesde;
            this.indiceHasta = indiceHasta;
            resultado = new int[vectorA.Length];
        }

        /// <summary>
        /// Método que realiza el cálculo
        /// </summary>
        internal void Calcular()
        {
            lock (obj)
            {
                for(int i = indiceDesde; i < indiceHasta; i++)
                {
                    for (int j = 0; j < vectorB.Length; j++)
                    {
                        if (vectorA.Contains(vectorB[j]))
                        {
                            resultado[i] = vectorA[i];
                            break;
                        }
                    }
                    
                }
            }
        }
    }
}


//    class Master
//    {
//        private readonly int[] vectorA;
//        private readonly int[] vectorB;
//        private readonly int numeroHilos;
//        private readonly List<int> resultadoInterseccion;

//        public Master(int[] vectorA, int[] vectorB, int numeroHilos)
//        {
//            this.vectorA = vectorA;
//            this.vectorB = vectorB;
//            this.numeroHilos = numeroHilos;
//            this.resultadoInterseccion = new List<int>();
//        }

//        public int[] CalcularInterseccion()
//        {
//            // Dividir el trabajo entre los hilos
//            int elementosPorHilo = vectorA.Length / numeroHilos;
//            Thread[] hilos = new Thread[numeroHilos];
//            for (int i = 0; i < numeroHilos; i++)
//            {
//                int indiceInicio = i * elementosPorHilo;
//                int indiceFin = (i == numeroHilos - 1) ? vectorA.Length : (i + 1) * elementosPorHilo;
//                hilos[i] = new Thread(() => Worker(indiceInicio, indiceFin));
//                hilos[i].Start();
//            }

//            // Esperar a que todos los hilos terminen
//            foreach (var hilo in hilos)
//            {
//                hilo.Join();
//            }

//            return resultadoInterseccion.ToArray();
//        }

//        private void Worker(int indiceInicio, int indiceFin)
//        {
//            for (int i = indiceInicio; i < indiceFin; i++)
//            {
//                if (vectorB.Contains(vectorA[i]))
//                {
//                    lock (resultadoInterseccion)
//                    {
//                        resultadoInterseccion.Add(vectorA[i]);
//                    }
//                }
//            }
//        }
//    }
//}
