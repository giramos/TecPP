using ColaConcurrente;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace _21_MasterWorker_ColaTrabajosVectorEspejo
{
    //internal class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        //Ejemplo1
    //        short TamA = 12, PosPorHiloA = 3; //Salen 2 hilos para Ejercicio1
    //        short[] A = new short[TamA];
    //        for (short i = 0; i < TamA; i++)
    //            A[i] = (short)(i + 1);

    //        //Ejemplo2
    //        short TamB = 21, PosPorHiloB = 2; //Salen 5 hilos para Ejercicio1
    //        short[] B = new short[TamB];
    //        for (short i = 0; i < TamB; i++)
    //            B[i] = (short)(i + 1);

    //        //Ejemplo3
    //        short TamC = 31, PosPorHiloC = 5; //Salen 3 hilos para Ejercicio1
    //        short[] C = new short[TamC];
    //        for (short i = 0; i < TamC; i++)
    //            C[i] = (short)(i + 1);

    //        int tamPoolHilos = 4;
    //        System.Console.WriteLine("\nEjercicio3. Espejo de un vector sin TPL con esquema cola de trabajos\n");
    //        ejercicio3(A, PosPorHiloA, B, PosPorHiloB, C, PosPorHiloC, tamPoolHilos);
    //    }

    //    //
    //    // Ejercicio3
    //    // Versión paralela del cálculo del espejo de un vector sin TPL con cola de trabajos.
    //    //
    //    public static void ejercicio3(short[] A, int PosPorHiloA, short[] B, int PosPorHiloB, short[] C, int PosPorHiloC, int tamPoolHilos)
    //    {
    //        ejercicio3tucodigo(A, PosPorHiloA, tamPoolHilos);
    //        ejercicio3tucodigo(B, PosPorHiloB, tamPoolHilos);
    //        ejercicio3tucodigo(C, PosPorHiloC, tamPoolHilos);

    //    }
    //    //
    //    public static void ejercicio3tucodigo(short[] vector, int PosPorHilo, int tamPoolHilos)
    //    {
    //        short[] resultado = null;
    //        resultado = new short[vector.Length];
    //        System.Console.WriteLine("\nVector Original");
    //        mostrarVector<short>(vector);

    //        //Aqui va tu codigo
    //        MasterColaTrabajos mct = new MasterColaTrabajos(vector, ref resultado, PosPorHilo, tamPoolHilos);
    //        mct.CalcularEspejo();

    //        System.Console.WriteLine("\nVector resultado");
    //        mostrarVector<short>(resultado);
    //    }

    //    public static void mostrarVector<T>(T[] matriz)
    //    {
    //        for (int i = 0; i < matriz.GetLength(0); i++)
    //        {
    //            System.Console.Write(matriz[i] + " ");

    //        }
    //    }
    //}

    //class MasterColaTrabajos
    //{
    //    private short[] vector;
    //    private short[] resultado;
    //    private int numHilos;
    //    private ConcurrentQueue<int> trabajos;

    //    public MasterColaTrabajos(short[] vector, ref short[] resultado, int posPorHilo, int numHilos)
    //    {
    //        this.numHilos = numHilos;
    //        this.vector = vector;
    //        this.resultado = resultado;
    //        trabajos = generaTrabajos(posPorHilo, vector);
    //    }

    //    /// <summary>
    //    ///  Generamos los trabajos.
    //    /// </summary>
    //    /// <param name="A"></param>
    //    /// <returns></returns>
    //    public ConcurrentQueue<int> generaTrabajos(int posPorHilo, short[] vector)
    //    {
    //        ConcurrentQueue<int> trabajos = new ConcurrentQueue<int>();
    //        int particiones = vector.Length / posPorHilo;
    //        for (int i = 0; i < particiones; i++)
    //            trabajos.Enqueue(i * posPorHilo);
    //        return trabajos;
    //    }

    //    public void CalcularEspejo()
    //    {
    //        WorkerColaConcurrente[] workers = new WorkerColaConcurrente[numHilos];
    //        int elementosPorHilo = vector.Length / numHilos;
    //        for (int i = 0; i < numHilos; i++)
    //        {
    //            workers[i] = new WorkerColaConcurrente(vector, ref resultado, trabajos, elementosPorHilo);
    //        }

    //        Thread[] hilos = new Thread[numHilos];
    //        for (int i = 0; i < numHilos; i++)
    //        {
    //            hilos[i] = new Thread(workers[i].Espejo);
    //            hilos[i].Name = "Worker Vector Espejo " + (i + 1);
    //            hilos[i].Priority = ThreadPriority.BelowNormal;
    //            hilos[i].Start();
    //        }
    //        for (int i = 0; i < hilos.Length; i++)
    //        {
    //            hilos[i].Join();

    //        }
    //    }
    //}

    //class WorkerColaConcurrente
    //{
    //    short[] vector;
    //    short[] resultado;
    //    ConcurrentQueue<int> trabajos;
    //    int posPorHilos;

    //    public WorkerColaConcurrente(short[] vector, ref short[] resultado, ConcurrentQueue<int> trabajos, int posPorHilos)
    //    {
    //        this.vector = vector;
    //        this.resultado = resultado;
    //        this.trabajos = trabajos;
    //        this.posPorHilos = posPorHilos;
    //    }

    //    internal void Espejo()
    //    {
    //        //Mientras haya trabajos en la cola
    //        while (trabajos.Count > 0)
    //        {
    //            //Sacamos el trabajo siguiente de la cola en i
    //            int i;
    //            if (trabajos.TryDequeue(out i))
    //            {
    //                //Si quedaban trabajos invertimos el vector

    //                try
    //                {
    //                    for (int j = i; j <= i + posPorHilos; j++)
    //                        resultado[resultado.Length - 1 - j] = vector[j];
    //                }
    //                catch (Exception e)
    //                {
    //                    Console.WriteLine("{0} Exception caught.", e);
    //                }
    //            }
    //        }
    //    }
    //}

    internal class Program
    {
        static void Main(string[] args)
        {
            //Ejemplo1
            short TamA = 12, PosPorHiloA = 3; //Salen 2 hilos para Ejercicio1
            short[] A = new short[TamA];
            for (short i = 0; i < TamA; i++)
                A[i] = (short)(i + 1);

            //Ejemplo2
            short TamB = 21, PosPorHiloB = 2; //Salen 5 hilos para Ejercicio1
            short[] B = new short[TamB];
            for (short i = 0; i < TamB; i++)
                B[i] = (short)(i + 1);

            //Ejemplo3
            short TamC = 31, PosPorHiloC = 5; //Salen 3 hilos para Ejercicio1
            short[] C = new short[TamC];
            for (short i = 0; i < TamC; i++)
                C[i] = (short)(i + 1);

            int tamPoolHilos = 4;
            System.Console.WriteLine("\nEjercicio3. Espejo de un vector sin TPL con esquema cola de trabajos\n");
            ejercicio3(A, PosPorHiloA, B, PosPorHiloB, C, PosPorHiloC, tamPoolHilos);
        }

        //
        // Ejercicio3
        // Versión paralela del cálculo del espejo de un vector sin TPL con cola de trabajos.
        //
        public static void ejercicio3(short[] A, int PosPorHiloA, short[] B, int PosPorHiloB, short[] C, int PosPorHiloC, int tamPoolHilos)
        {
            ejercicio3tucodigo(A, PosPorHiloA, tamPoolHilos);
            ejercicio3tucodigo(B, PosPorHiloB, tamPoolHilos);
            ejercicio3tucodigo(C, PosPorHiloC, tamPoolHilos);

        }
        //
        public static void ejercicio3tucodigo(short[] vector, int PosPorHilo, int tamPoolHilos)
        {
            short[] resultado = null;
            resultado = new short[vector.Length];
            System.Console.WriteLine("\nVector Original");
            mostrarVector<short>(vector);

            //Aqui va tu codigo
            MasterColaTrabajos mct = new MasterColaTrabajos(vector, ref resultado, PosPorHilo, tamPoolHilos);
            mct.CalcularEspejo();

            System.Console.WriteLine("\nVector resultado");
            mostrarVector<short>(resultado);
        }

        public static void mostrarVector<T>(T[] matriz)
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                System.Console.Write(matriz[i] + " ");

            }
        }
    }

    class MasterColaTrabajos
    {
        private short[] vector;
        private short[] resultado;
        private int numHilos;
        private Cola<int> trabajos;

        public MasterColaTrabajos(short[] vector, ref short[] resultado, int posPorHilo, int numHilos)
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

        public void CalcularEspejo()
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
        }
    }

    class WorkerColaConcurrente
    {
        short[] vector;
        short[] resultado;
        Cola<int> trabajos;
        int posPorHilos;

        public WorkerColaConcurrente(short[] vector, ref short[] resultado, Cola<int> trabajos, int posPorHilos)
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
                            resultado[resultado.Length - 1 - j] = vector[j];
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
