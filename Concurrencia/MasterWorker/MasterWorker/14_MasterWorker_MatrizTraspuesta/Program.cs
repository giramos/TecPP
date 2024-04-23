using System;
using System.Threading;

namespace _14_MasterWorker_MatrizTraspuesta
{
    internal class Program
    {
        static void Main(string[] args)
        {
            short nfilas = 10, ncolumnas = 6;
            int numeroHilos = 5;

            short[,] A = CrearMatrizAleatoria(nfilas, ncolumnas, 0, 10);

            System.Console.WriteLine("Matriz A sin cola concurrente"); mostrarMatriz(A);

            short[,] C1 = ejercicio2(A, numeroHilos);
            System.Console.WriteLine("\nMatriz C sin cola concurrente");
            mostrarMatriz(C1);

        }

        //
        // 
        // Versión paralela del cálculo de la transpuesta de una matriz sin TPL con un número de hilos arbitrario.
        // sin una cola de trabajos.
        public static short[,] ejercicio2(short[,] A, int nhilos)
        {
            Master m1 = new Master(A, nhilos);
            return m1.CalcularTraspuesta2();

        }

        public static short[,] CrearMatrizAleatoria(int filas, int columnas, int menor, int mayor)
        {
            short[,] vector = new short[filas, columnas];
            Random random = new Random(1);
            for (int i = 0; i < filas; i++)
                for (int j = 0; j < columnas; j++)
                    vector[i, j] = (short)random.Next(menor, mayor + 1);
            return vector;
        }
        public static void mostrarMatriz(short[,] matriz)
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                    System.Console.Write(matriz[i, j] + " ");
                System.Console.WriteLine();
            }
        }

        public static void mostrarVector(int[] matriz)
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                System.Console.Write(matriz[i] + " ");
                System.Console.WriteLine();
            }
        }
    }

    //
    // Calculo de C = A*B
    //
    public class Master
    {

        private short[,] matrizA;
        private short[,] matrizC;
        private int nHilos;
        //private ConcurrentQueue<int> trabajos;

        public Master(short[,] matrizA, int nHilos)
        {
            this.matrizA = matrizA;
            this.nHilos = nHilos;
            //Se inicia MatrizC con las dimensiones invertidas respecto a matrizA.
            this.matrizC = new short[this.matrizA.GetLength(1), this.matrizA.GetLength(0)];
            // this.trabajos = generaTrabajos(matrizA);


        }

        public short[,] CalcularTraspuesta2()
        {
            //REservamos espacio para el array de trabajadores
            Worker[] workers = new Worker[nHilos];
            int elementosPorHilo = this.matrizC.Length / nHilos;
            for (short i = 0; i < nHilos; i++)
                workers[i] = new Worker(this.matrizA, this.matrizC,
                    i * elementosPorHilo,
                    (i < this.nHilos - 1) ? (i + 1) * elementosPorHilo - 1 : this.matrizC.Length - 1);

            //Creamos y lanzamos los hilos trabajadores
            Thread[] hilos = new Thread[workers.Length];
            for (short i = 0; i < hilos.GetLength(0); i++)
            {
                hilos[i] = new Thread(workers[i].Calcular2);
                hilos[i].Name = "Worker  " + (i);
                hilos[i].Priority = ThreadPriority.BelowNormal;
                hilos[i].Start();
            }
            //Esperamos por la finalización de los hilos trabajadores.
            foreach (Thread hilo in hilos)
                hilo.Join();


            return this.matrizC;
        }
    }

    internal class Worker
    {
        private short[,] A, C;
        //private ConcurrentQueue<int> trabajos;
        private int índiceDesde, índiceHasta;

        /// <summary>
        ///  Trabajor encargado de trasponer filas de la matriz A dejándolas sobre la C.
        ///  Extraerá trabajos (el número de fila a trasponer) de la cola trabajos hasta
        ///  que se acaben.
        /// </summary>
        /// <param name="A"> Matriz a trasponer</param>
        /// <param name="C"> Matriz resultado</param>
        /// <param name="trabajos" Cola concurrente con los trabajos. trabajo></param>
        internal Worker(short[,] A, short[,] C, int índiceDesde, int índiceHasta)
        {
            this.A = A; this.C = C;
            // this.trabajos = trabajos;
            this.índiceDesde = índiceDesde;
            this.índiceHasta = índiceHasta;
        }

        internal void Calcular2()
        {
            int matACols = A.GetLength(1);
            int matCCols = C.GetLength(0);
            int matARows = A.GetLength(0);

            for (int i = 0; i < matARows; i++)
            {
                for (int j = 0; j < matCCols; j++)
                {
                    for (int k = 0; k < matACols; k++)
                    {
                        C[j, i] = A[i, j];
                    }

                }
            }

        }

    }

}
