using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace _10_DiccionarioPorcentageVocales
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int maximoHilos = 15;
            var palabras = GetPalabras();
            MostrarLinea(Console.Out, "Num Hilos", "Ticks", "Resultado");
            for (int numeroHilos = 1; numeroHilos <= maximoHilos; numeroHilos++)
            {
                Master master = new Master(palabras, numeroHilos);
                DateTime antes = DateTime.Now;
                var resultado = master.Calcular();
                DateTime despues = DateTime.Now;
                MostrarLinea(Console.Out, numeroHilos, (despues - antes).Ticks, resultado);

                //Entre ejecuciones, limpiamos y esperamos.
                GC.Collect();
                GC.WaitForFullGCComplete();
            }
        }

        static IEnumerable<string> GetPalabras()
        {
            // Lista de palabras
            List<string> palabras = new List<string>
            {
                "hola",
                "mundo",
                "este",
                "es",
                "un",
                "ejemplo",
                "de",
                "enumerable",
                "de",
                "palabras"
            };

            // Devuelve las palabras una por una usando yield return
            foreach (string palabra in palabras)
            {
                yield return palabra;
            }
        }

        static void MostrarLinea(TextWriter stream, string numHilosCabecera, string ticksCabecera, string resultadoCabecera)
        {
            stream.WriteLine("{0,-10} {1,-20} {2,-30}", numHilosCabecera, ticksCabecera, resultadoCabecera);
            stream.WriteLine(new string('-', 60));
        }

        static void MostrarLinea(TextWriter stream, int numHilos, long ticks, Dictionary<string, double> resultado)
        {
            stream.WriteLine("{0,-10} {1,-20:N0}", numHilos, ticks);
            foreach (var kvp in resultado)
            {
                stream.WriteLine("{0,-10} {1,-20:N2}%", kvp.Key, kvp.Value);
            }
            stream.WriteLine();
        }
    }

    public class Master
    {
        private IEnumerable<string> palabras;
        /// <summary>
        /// Número de trabajadores que se van a emplear en el cálculo.
        /// </summary>
        private int numeroHilos;

        public Master(IEnumerable<string> pa, int numeroHilos)
        {
            if (numeroHilos < 1)
                throw new ArgumentException("El número de hilos debe ser al menos 1");
            this.palabras = pa;
            this.numeroHilos = numeroHilos;
        }

        /// <summary>
        /// Este método crea y coordina el cálculo
        /// </summary>
        public Dictionary<string, double> Calcular()
        {
            Dictionary<string, double> dicc = new Dictionary<string, double>();

            // Creamos los workers
            Worker[] workers = new Worker[this.numeroHilos];
            int numElementosPorHilo = this.palabras.ToArray().Length / numeroHilos;
            for (int i = 0; i < this.numeroHilos; i++)
            {
                int indiceDesde = i * numElementosPorHilo;
                int indiceHasta = (i < numeroHilos - 1) ? (i + 1) * numElementosPorHilo - 1 : this.palabras.ToArray().Length - 1;
                workers[i] = new Worker(this.palabras, indiceDesde, indiceHasta);
            }

            // Iniciamos los hilos.
            Thread[] hilos = new Thread[workers.Length];
            for (int i = 0; i < workers.Length; i++)
            {
                hilos[i] = new Thread(workers[i].Calcular);
                hilos[i].Start();
            }

            // Esperamos a que acaben para continuar.
            foreach (Thread thread in hilos)
                thread.Join();

            // Combinamos los resultados de los workers
            foreach (Worker worker in workers)
            {
                foreach (var kvp in worker.Resultado)
                {
                    dicc[kvp.Key] = kvp.Value;
                }
            }

            return dicc;
        }
    }

    internal class Worker
    {
        private IEnumerable<string> palabras;
        private Dictionary<string, double> resultado;

        /// <summary>
        /// Índices que indican el rango de elementos del vector 
        /// con el que vamos a trabajar.
        /// En el intervalo se incluyen ambos índices.
        /// </summary>
        private int indiceDesde, indiceHasta;

        internal Dictionary<string, double> Resultado
        {
            get { return this.resultado; }
        }

        internal Worker(IEnumerable<string> pa, int indiceDesde, int indiceHasta)
        {
            this.palabras = pa;
            this.resultado = new Dictionary<string, double>();
            this.indiceDesde = indiceDesde;
            this.indiceHasta = indiceHasta;
        }

        internal void Calcular()
        {
            for (int i = this.indiceDesde; i <= this.indiceHasta; i++)
            {
                string palabra = palabras.ToArray()[i];
                double porcentaje = PorcentajeVocales(palabra);
                resultado[palabra] = porcentaje;
            }
        }

        private double PorcentajeVocales(string palabra)
        {
            int totalLetras = palabra.Length;
            int vocales = 0;

            foreach (char letra in palabra)
            {
                if ("AEIOUaeiou".Contains(letra))
                {
                    vocales++;
                }
            }

            if (totalLetras == 0)
            {
                return 0;
            }

            return (double)vocales / totalLetras * 100;
        }
    }
}
