using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading;

namespace _06_MasterWorker_InterserccionVectores
{
    public class Program
    {
        static void Main(string[] args)
        {
            int[] vectorA = { 15, 18, 6, 14, 1, 7, 8, 2, 3, 0 };
            int[] vectorB = { 1, 2, 6, 7, 8 };

            //RESULTADO:
            //6, 1, 7, 8, 2}.

            // Prueba con 1 hilo
            //Master master1 = new Master(vectorA, vectorB, 1);
            //Console.WriteLine("Resultado con 1 hilo: " + string.Join(", ", master1.Calcular()));

            //// Prueba con 4 hilos
            //Master master4 = new Master(vectorA, vectorB, 4);
            //Console.WriteLine("Resultado con 4 hilos: " + string.Join(", ", master4.Calcular()));

            const int maximoHilos = 50;
            MostrarLinea(Console.Out, "Num Hilos", "Ticks", "Resultado", "Vector Complemento");
            for (int numeroHilos = 1; numeroHilos <= maximoHilos; numeroHilos++)
            {
                Master master = new Master(vectorA, vectorB, numeroHilos);
                DateTime antes = DateTime.Now;
                int[] resultado = master.Calcular();
                DateTime despues = DateTime.Now;
                MostrarLinea(Console.Out, numeroHilos, (despues - antes).Ticks, resultado.Length, resultado);

                // Entre ejecuciones, limpiamos y esperamos.
                GC.Collect();
                GC.WaitForFullGCComplete();
            }
        }

        static void MostrarLinea(TextWriter stream, string numHilosCabecera, string ticksCabecera, string resultadoCabecera, string complementoCabecera)
        {
            stream.WriteLine("{0,-10} {1,-15} {2,-15} {3,-20}", numHilosCabecera, ticksCabecera, resultadoCabecera, complementoCabecera);
        }

        static void MostrarLinea(TextWriter stream, int numHilos, long ticks, int resultado, int[] complemento)
        {
            stream.Write("{0,-10} {1,-15:N0} {2,-15:N0} ", numHilos, ticks, resultado);
            stream.Write("vectorComplement = {");
            foreach (var elemento in complemento)
            {
                stream.Write(elemento + ",");
            }
            stream.Write("}\n");
        }
    }

    public class Master
    {
        private int[] vectorA;
        private int[] vectorB;
        private int numeroHilos;

        public Master(int[] vectora, int[] vectorb, int numeroHilos)
        {
            if (numeroHilos < 1 )
                throw new ArgumentException("El número de hilos debe ser menor o igual al tamaño del vector");
            if (vectorb.Length > vectora.Length)
                throw new ArgumentException("El vector 2 no puede ser mayor al vector 1");
            this.vectorA = vectora;
            this.vectorB = vectorb;
            this.numeroHilos = numeroHilos;
        }

        public int[] Calcular()
        {
            List<int> resultado = new List<int>();
            Worker[] workers = new Worker[this.numeroHilos];
            int numElementosPorHilo = this.vectorA.Length / numeroHilos;

            for (int i = 0; i < this.numeroHilos; i++)
            {
                int indiceDesde = i * numElementosPorHilo;
                int indiceHasta = (i + 1) * numElementosPorHilo - 1;
                if (i == this.numeroHilos - 1)
                {
                    indiceHasta = this.vectorA.Length - 1;
                }
                workers[i] = new Worker(this.vectorA, this.vectorB, resultado, indiceDesde, indiceHasta);
            }

            Thread[] hilos = new Thread[workers.Length];
            for (int i = 0; i < workers.Length; i++)
            {
                hilos[i] = new Thread(workers[i].Calcular);
                hilos[i].Name = "Worker número: " + (i + 1);
                hilos[i].Priority = ThreadPriority.Normal;
                hilos[i].Start();
            }

            foreach (Thread thread in hilos)
                thread.Join();

            return resultado.ToArray();
        }
    }

    internal class Worker
    {
        private int[] vectorA;
        private int[] vectorB;
        private int indiceDesde, indiceHasta;
        private List<int> resultado;
        private static readonly object obj = new();

        internal Worker(int[] vectora, int[] vectorb, List<int> r, int indiceDesde, int indiceHasta)
        {
            this.vectorA = vectora;
            this.vectorB = vectorb;
            this.resultado = r;
            this.indiceDesde = indiceDesde;
            this.indiceHasta = indiceHasta;
        }

        internal void Calcular()
        {
            for (int i = indiceDesde; i <= indiceHasta; i++) // Change here
            {
                if (vectorB.Contains(vectorA[i]))
                {
                    lock(obj)
                        resultado.Add(vectorA[i]);
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
