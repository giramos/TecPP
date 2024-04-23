using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace _20_MasterWorker_Encode
{
    internal class Program
    {
        static void Main(string[] args)
        {

            const int maximoHilos = 50;
            string cadena = "BBBBBBBBBBBBNNNNNNNNAAAAAAAAAAFFFFFFFFFFFDDIIIIIIIII";
           
            MostrarLinea(Console.Out, "Num Hilos", "Ticks", "Resultado");
            for (int numeroHilos = 1; numeroHilos <= maximoHilos; numeroHilos++)
            {
                Master master = new Master(cadena, numeroHilos);
                DateTime antes = DateTime.Now;
                ConcurrentDictionary<char,int> resultado = master.CalcularModulo();
                DateTime despues = DateTime.Now;
                MostrarLinea(Console.Out, numeroHilos, (despues - antes).Ticks, resultado.Count);
                Console.WriteLine(ImprimirDiccionario(resultado));
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
        static string ImprimirDiccionario(ConcurrentDictionary<char, int> diccionario)
        {
            string resultado = "";
            foreach (var kvp in diccionario)
            {
                resultado += $"[{kvp.Key}: {kvp.Value}], ";
            }
            return resultado;
        }
    }

    public class Master
    {
        private string cadena;

        /// <summary>
        /// Número de trabajadores que se van a emplear en el cálculo.
        /// </summary>
        private int numeroHilos;


        public Master(string cad, int numeroHilos)
        {
            if (numeroHilos < 1 || numeroHilos > cad.Length)
                throw new ArgumentException("El número de hilos debe ser menor o igual al tamaño de la cadena");
            this.cadena = cad;
            this.numeroHilos = numeroHilos;
        }

        /// <summary>
        /// Este método crea y coordina el cálculo
        /// </summary>
        public ConcurrentDictionary<char,int> CalcularModulo()
        {
            ConcurrentDictionary<char, int> dicc = new();
            // Creamos los workers
            Worker[] workers = new Worker[this.numeroHilos]; // Se crea un array de objetos Worker,
                                                             //	donde cada Worker representa un hilo que realizará parte del cálculo de	l módulo del vector.
            int numElementosPorHilo = this.cadena.Length / numeroHilos; // Esta línea calcula cuántos elementos del vector serán procesados por cada hilo.
                                                                        // Divide la longitud total del vector entre el número de hilos (numeroHilos) para determinar cuántos elementos debe procesar cada hilo.
            for (int i = 0; i < this.numeroHilos; i++) // La variable i representa el índice del hilo actual.
            {
                int indiceDesde = i * numElementosPorHilo; // Calcula el índice de inicio para el segmento del vector que será procesado por el hilo actual. 
                                                           // Multiplica el número de elementos por hilo por el índice del hilo para obtener el índice de inicio del segmento.
                int indiceHasta = (i + 1) * numElementosPorHilo - 1; // Calcula el índice de finalización para el segmento del vector que será procesado por el hilo actual.
                                                                     // Multiplica el número de elementos por hilo por el siguiente índice de hilo y resta 1 para obtener el índice de finalización del segmento.
                if (i == this.numeroHilos - 1) //el último hilo, llega hasta el final del vector.
                {
                    indiceHasta = this.cadena.Length - 1;
                }
                workers[i] = new Worker(this.cadena, dicc, indiceDesde, indiceHasta);
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

            
            return dicc;
        }
    }

    internal class Worker
    {
        private string cadena;
        private int indiceDesde, indiceHasta;
        private ConcurrentDictionary<char, int> resultado;
        private readonly static object lockObject = new object(); // Objeto de bloqueo para sincronización

        internal Worker(string cad, ConcurrentDictionary<char, int> d, int indiceDesde, int indiceHasta)
        {
            this.cadena = cad;
            this.resultado = d;
            this.indiceDesde = indiceDesde;
            this.indiceHasta = indiceHasta;
        }

        internal void Calcular()
        {
            for (int i = this.indiceDesde; i <= this.indiceHasta; i++)
            {
                // Bloqueo para asegurar el acceso seguro al diccionario compartido
                lock (lockObject)
                {
                    if (!resultado.ContainsKey(cadena[i]))
                    {
                        resultado[cadena[i]] = 1;
                    }
                    else
                    {
                        resultado[cadena[i]]++;
                    }
                }
            }
        }
    }



}
