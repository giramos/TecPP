using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace _13_MasterWorker_MatrizPositivosNegativos
{
    internal class Program
    {
        //    {
        //        Dado una matriz de números enteros rectangular, calcular de forma paralela dos listas: una con los
        //valores positivos y otra con los valores negativos.
        //Las condiciones que se deberán cumplir son:
        //1. El criterio de división de la matriz para el cálculo paralelo queda a criterio del alumno y será válido
        //siempre el número de partes sea coherente e inferior al numero de posiciones de la matriz.
        //2. El número de partes deberá de ser un parámetro o parámetros de la solución.
        //3. El número de hilos deberá ser igual al número de partes en que se divida la matriz para el cálculo. 
        //4. Los valores de las listas deben estar ordenados de forma creciente (0.5 puntos).
        //Matriz A
        //-5 -8 -1 6 3
        //-1 -3 9 -8 3
        //-10 -5 -4 10 4
        //3 -5 2 4 4
        //[Salida esperada]
        //        Lista1 -> -10 -8 -8 -5 -5 -5 -4 -3 -1 -1
        //Lista2 -> 2 3 3 3 4 4 4 6 9 1
        static void Main(string[] args)
        {
            int[,] A = CrearMatrizAleatoria(4, 5, -10, 10);
            List<int> listaP = new(A.GetLength(0)), listaN = new(A.GetLength(0));
            int maximoHilos = 5;
            //System.Console.WriteLine("Matriz A");
            //mostrarMatriz(A);
            //MasterMatriz master = new MasterMatriz(A, listaP, listaN, maximoHilos);
            //master.Calcular();
            //Console.WriteLine("Lista Positivos:\n");
            //mostrar(listaP);
            //Console.WriteLine("Lista Negativos:\n");
            //mostrar(listaN);
            MostrarLinea(Console.Out, "Num Hilos", "Ticks");

            //Toma de tiempos.
            Stopwatch stopWatch = new Stopwatch();

            for (int numeroHilos = 1; numeroHilos <= maximoHilos; numeroHilos++)
            {
                // Limpiar las listas antes de cada iteración
                listaP.Clear();
                listaN.Clear();

                MasterMatriz master = new MasterMatriz(A, listaP, listaN, numeroHilos);
                stopWatch.Restart();
                master.Calcular();
                stopWatch.Stop();

                MostrarLinea(Console.Out, numeroHilos, stopWatch.ElapsedTicks);
            }

            // Imprimir las listas después de todas las iteraciones
            Console.Write("Lista Positivos:\n");
            mostrar(listaP);
            Console.Write("Lista Negativos:\n");
            mostrar(listaN);

        }

        public static int[,] CrearMatrizAleatoria(int filas, int columnas, int menor, int mayor)
        {
            int[,] vector = new int[filas, columnas];
            Random random = new Random(1);
            for (int i = 0; i < filas; i++)
                for (int j = 0; j < columnas; j++)
                    vector[i, j] = (int)random.Next(menor, mayor + 1);
            return vector;
        }

        public static void mostrarMatriz(int[,] matriz)
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                    System.Console.Write(matriz[i, j] + " ");
                System.Console.WriteLine();
            }
        }
        public static void mostrar<T>(IEnumerable<T> lista)
        {
            foreach (T x in lista)
            {
                Console.Write(x + " ");
            }
            Console.WriteLine();
        }

        static void MostrarLinea(TextWriter stream, string numHilosCabecera, string ticksCabecera)
        {
            stream.WriteLine("{0};{1}", numHilosCabecera, ticksCabecera);
        }

        static void MostrarLinea(TextWriter stream, int numHilos, long ticks)
        {
            stream.WriteLine("{0};{1:N0}", numHilos, ticks);
        }
    }

    class MasterMatriz
    {
        /// <summary>
        /// Matriz de numero
        /// </summary>
        private int[,] matriz;
        /// <summary>
        /// Lista de resultados que coincidan con el valor (criterio) dado
        /// En este caso que sean > valor
        /// </summary>
        private List<int> positivos;
        private List<int> negativos;
        /// <summary>
        /// Número de trabajadores que se van a emplear en el cálculo.
        /// </summary>
        private int numeroHilos;

        public MasterMatriz(int[,] matriz, List<int> positivos, List<int> negativos, int numeroHilos)
        {
            if (numeroHilos < 1 || numeroHilos > matriz.Length)
                throw new ArgumentException("El número de hilos debe ser menor o igual al tamaño de la matriz");
            this.matriz = matriz;
            this.positivos = positivos;
            this.negativos = negativos;
            this.numeroHilos = numHilos(matriz, numeroHilos);

        }
        int numHilos(int[,] matriz, int posPorHilo)
        {
            return (matriz.Length / posPorHilo) / 2;
        }

        /// <summary>
        /// Este método crea y coordina el cálculo
        /// </summary>
        public void Calcular()
        {
            // Creamos los workers
            WorkerMatriz[] workers = new WorkerMatriz[this.numeroHilos];
            int numElementosPorHilo = this.matriz.Length / numeroHilos; // Reparte el trabajo
            for (int i = 0; i < this.numeroHilos; i++)
            {
                int indiceDesde = i * numElementosPorHilo;
                int indiceHasta = (i + 1) * numElementosPorHilo - 1;
                if (i == this.numeroHilos - 1) //el último hilo, llega hasta el final del vector.
                {
                    indiceHasta = this.matriz.Length - 1;
                }
                workers[i] = new WorkerMatriz(this.matriz, this.positivos, this.negativos, indiceDesde, indiceHasta);
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

        }
    }
    internal class WorkerMatriz
    {
        private int[,] matriz;
        private List<int> positivos;
        private List<int> negativos;

        /// <summary>
        /// Índices que indican el rango de elementos del vector 
        /// con el que vamos a trabajar.
        /// En el intervalo se incluyen ambos índices.
        /// </summary>
        private int indiceDesde, indiceHasta;

        /// <summary>
        /// Objeto estatico que actua como bloqueador. Ya que se usa la misma lista por varios hilos
        /// </summary>
        private static readonly object objeto_locker = new object();

        internal WorkerMatriz(int[,] matriz, List<int> positivos, List<int> negativos, int indiceDesde, int indiceHasta)
        {
            this.matriz = matriz;
            this.indiceDesde = indiceDesde;
            this.indiceHasta = indiceHasta;
            this.positivos = positivos;
            this.negativos = negativos;
        }

        /// <summary>
        /// Método que realiza el cálculo 
        /// </summary>
        internal void Calcular()
        {
            int filas = matriz.GetLength(0);
            int columnas = matriz.GetLength(1);
            //for (int i = this.indiceDesde; i <= this.indiceHasta; i++) // Comprobar bien el += Los indices vaya!
            for (int j = this.indiceDesde; j < filas; j++)
            {
                for (int k = 0; k < columnas; k++)
                {
                    if (matriz[j, k] < 0)
                    {
                        lock (objeto_locker)
                        {
                            this.negativos.Add(matriz[j, k]);
                        }

                    }
                    else
                    {
                        lock (objeto_locker)
                        {
                            this.positivos.Add(matriz[j, k]);
                        }

                    }
                }
            }
        }
    }
}

