using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace _23_MasterWorker_DiccionarioVocalesNumero
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int maximoHilos = 10;
            var palabras = GetPalabras();
            MostrarLinea(Console.Out, "Num Hilos", "Ticks", "Resultado");
            for (int numeroHilos = 1; numeroHilos <= maximoHilos; numeroHilos++)
            {
                Master master = new Master(palabras, numeroHilos);
                DateTime antes = DateTime.Now;
                Dictionary<string, int> resultado = master.Calcular();
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

        static void MostrarLinea(TextWriter stream, int numHilos, long ticks, Dictionary<string, int> resultado)
        {
            stream.WriteLine("Número de Hilos: {0}", numHilos);
            stream.WriteLine("Tiempo de ejecución (ticks): {0:N0}", ticks);
            stream.WriteLine("Resultado del diccionario:");
            foreach (var kvp in resultado)
            {
                stream.WriteLine("{0}: {1}", kvp.Key, kvp.Value);
            }
            stream.WriteLine();
        }

    }

    public class Master
    {
        /// <summary>
        /// Vector del que se obtendrá el módulo
        /// </summary>
        private IEnumerable<string> palabras;

        /// <summary>
        /// Número de trabajadores que se van a emplear en el cálculo.
        /// </summary>
        private int numeroHilos;


        public Master(IEnumerable<string> palabras, int numeroHilos)
        {
            if (numeroHilos < 1 || numeroHilos > palabras.Count())
                throw new ArgumentException("El número de hilos debe ser menor o igual al tamaño del vector");
            this.palabras = palabras;
            this.numeroHilos = numeroHilos;
        }

        /// <summary>
        /// Este método crea y coordina el cálculo
        /// </summary>
        public Dictionary<string,int> Calcular()
        {
            Dictionary<string, int> dicc = new Dictionary<string, int>();
            // Creamos los workers
            Worker[] workers = new Worker[this.numeroHilos]; // Se crea un array de objetos Worker,
                                                             //	donde cada Worker representa un hilo que realizará parte del cálculo de	l módulo del vector.
            int numElementosPorHilo = this.palabras.ToArray().Length / numeroHilos; // Esta línea calcula cuántos elementos del vector serán procesados por cada hilo.
                                                                                    // Divide la longitud total del vector entre el número de hilos (numeroHilos) para determinar cuántos elementos debe procesar cada hilo.
            for (int i = 0; i < this.numeroHilos; i++) // La variable i representa el índice del hilo actual.
            {
                int indiceDesde = i * numElementosPorHilo; // Calcula el índice de inicio para el segmento del vector que será procesado por el hilo actual. 
                                                           // Multiplica el número de elementos por hilo por el índice del hilo para obtener el índice de inicio del segmento.
                int indiceHasta = (i + 1) * numElementosPorHilo - 1; // Calcula el índice de finalización para el segmento del vector que será procesado por el hilo actual.
                                                                     // Multiplica el número de elementos por hilo por el siguiente índice de hilo y resta 1 para obtener el índice de finalización del segmento.
                if (i == this.numeroHilos - 1) //el último hilo, llega hasta el final del vector.
                {
                    indiceHasta = this.palabras.ToArray().Length - 1;
                }
                workers[i] = new Worker(this.palabras, dicc, indiceDesde, indiceHasta);
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

            //Combinamos los resultados de los workers
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

        /// <summary>
        /// Vector del que vamos a obtener el módulo
        /// </summary>
        private IEnumerable<string> palabras;

        /// <summary>
        /// Índices que indican el rango de elementos del vector 
        /// con el que vamos a trabajar.
        /// En el intervalo se incluyen ambos índices.
        /// </summary>
        private int indiceDesde, indiceHasta;

        /// <summary>
        /// Resultado del cálculo
        /// </summary>
        private Dictionary<string, int> resultado;

        internal Dictionary<string, int> Resultado
        {
            get { return this.resultado; }
        }

        internal Worker(IEnumerable<string> vector, Dictionary<string, int> di, int indiceDesde, int indiceHasta)
        {
            this.palabras = vector;
            this.resultado = di;
            this.indiceDesde = indiceDesde;
            this.indiceHasta = indiceHasta;
        }

        /// <summary>
        /// Método que realiza el cálculo
        /// </summary>
        internal void Calcular()
        {
            for (int i = this.indiceDesde; i <= this.indiceHasta; i++)
            {
                string palabra = palabras.ToArray()[i];
                int vocales = ContarVocales(palabra);

                // Verifica si la palabra ya existe en el diccionario
                if (resultado.ContainsKey(palabra))
                {
                    // Si existe, suma el recuento de vocales al valor existente
                    resultado[palabra] += vocales;
                }
                else
                {
                    // Si no existe, agrega la palabra al diccionario con su recuento de vocales
                    resultado[palabra] = vocales;
                }
            }
        }

        private int ContarVocales(string v)
        {
            int cont = 0;
            foreach (var i in v)
            {
                if ("AEIOUaeiou".Contains(i))
                {
                    cont++;
                }
            }
            return cont;
        }
    }
}
