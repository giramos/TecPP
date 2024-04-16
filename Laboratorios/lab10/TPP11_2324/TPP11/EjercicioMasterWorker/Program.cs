using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;

namespace EjercicioMasterWorker
{
    class Program
    {
        /// <summary>
        /// A partir del Master Worker de la entrega obligatoria, prueba las siguientes modificaciones:
        /// -Los workers almacenarán en una lista "Resultado", los valores que sean superiores a la cantidad buscada.
        ///     - No admitirá duplicados.
        ///     - La lista Resultado la pasa el Master a los workers y DEBE SER LA MISMA PARA TODOS LOS WORKERS.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var data = Utils.GetBitcoinData();
            int valor = 7000;
            const int maximoHilos = 50;


            //Console.WriteLine(CultureInfo.CurrentCulture);
            //foreach (var d in data)
            //    Console.WriteLine(d);

            MostrarLinea(Console.Out, "Num Hilos", "Ticks", "Resultado");
            for (int numeroHilos = 1; numeroHilos <= maximoHilos; numeroHilos++)
            {
                Master master = new Master(data, valor, numeroHilos);
                DateTime antes = DateTime.Now;
                var resultado = master.Calcula();
                DateTime despues = DateTime.Now;
                MostrarLinea(Console.Out, numeroHilos, (despues - antes).Ticks, resultado);

                //Entre ejecuciones, limpiamos y esperamos.
                GC.Collect();
                GC.WaitForFullGCComplete();
            }
            Console.ReadLine();
        }

        static void MostrarLinea(TextWriter stream, string numHilosCabecera, string ticksCabecera, string resultadoCabecera)
        {
            stream.WriteLine("{0};{1};{2}", numHilosCabecera, ticksCabecera, resultadoCabecera);
        }

        static void MostrarLinea(TextWriter stream, int numHilos, long ticks, int resultado)
        {
            stream.WriteLine("{0};{1:N0};{2:N2}", numHilos, ticks, resultado);
        }
    }

    internal class Master
    {
        BitcoinValueData[] array;
        int valor;
        int numHilos;
        List<double> valores;

        public Master(BitcoinValueData[] array, int valor, int numHilos)
        {
            if (numHilos < 1 || numHilos > array.Length)
                throw new ArgumentException("El número de hilos debe ser menor o igual al tamaño del vector");
            this.array = array;
            this.valor = valor;
            this.numHilos = numHilos;
            this.valores = new List<double>();
        }

        public int Calcula()
        {
            // Creamos los workers
            Worker[] workers = new Worker[this.numHilos];
            int numElementosPorHilo = this.array.Length / numHilos;
            for (int i = 0; i < this.numHilos; i++)
            {
                int indiceDesde = i * numElementosPorHilo;
                int indiceHasta = (i + 1) * numElementosPorHilo - 1;
                if (i == this.numHilos - 1) //el último hilo, llega hasta el final del vector.
                {
                    indiceHasta = this.array.Length - 1;
                }
                workers[i] = new Worker(this.array, this.valor, this.valores, indiceDesde, indiceHasta);
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

            //Por último, sumamos todos los resultados de los trabajadores.
            //y devolvemos la raiz cuadrada.
            int resultado = 0;
            foreach (Worker worker in workers)
                resultado += worker.Resultado;
            return resultado;
        }
    }

    internal class Worker
    {
        /// <summary>
        /// Vector del que vamos a obtener el módulo
        /// </summary>
        private BitcoinValueData[] vector;
        private int valor;
        private int indiceDesde, indiceHasta;
        private List<double> valores;

        private static readonly object obj = new();

        /// <summary>
        /// Resultado del cálculo
        /// </summary>
        private int resultado;

        internal int Resultado
        {
            get { return this.resultado; }
        }

        internal Worker(BitcoinValueData[] vector, int valor, List<double> valores, int indiceDesde, int indiceHasta)
        {
            this.vector = vector;
            this.valor = valor;
            this.indiceDesde = indiceDesde;
            this.indiceHasta = indiceHasta;
            this.valores = valores;
        }

        /// <summary>
        /// Método que realiza el cálculo
        /// </summary>
        internal void Calcular()
        {
            this.resultado = 0;
            for (int i = this.indiceDesde; i <= this.indiceHasta; i++)
                if (vector[i].Value >= valor)
                {
                    if (!valores.Contains(vector[i].Value))
                    {
                        lock (obj)
                        {
                            valores.Add(vector[i].Value);
                        }
                        
                        this.resultado += 1;
                    }
                }

        }
    }

    class Utils
    {
        public static bool Verbose = false;

        /// <summary>
        /// Converts a timestamp to a proper date and time
        /// </summary>
        /// <param name="unixTimeStamp">Timestamp in Unix format</param>
        /// <returns>Equivalent DateTime object</returns>
        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        /// <summary>
        /// From the context of a file with Bitcoin values data, build the corresponding objects holding these values
        /// </summary>
        /// <param name="contents">File contents</param>
        /// <returns>Enumerable of BitcoinValueData with the parsed contents</returns>
        private static IEnumerable<BitcoinValueData> GetFileContents(string contents)
        {
            var lines = contents.Split('\n');
            foreach (var line in lines)
            {
                //Clear data and ignore blank lines.
                var lineTemp = line.Trim();
                if (lineTemp.Length == 0) continue;
                var parts = lineTemp.Split(',');
                yield return new BitcoinValueData
                {
                    Timestamp = UnixTimeStampToDateTime(Double.Parse(parts[0])),
                    Value = CultureInfo.CurrentCulture.ToString().Contains("es-") ? Double.Parse(parts[1].Replace('.', ',')) : Double.Parse(parts[1])

                };
            }
        }
        /// <summary>
        /// Read all the data files tied to this program, placed in the /data directory of the C# project.
        /// </summary>
        /// <returns>Enumerable with the data of all files placed in BitcoinValueData objects </returns>
        private static IEnumerable<BitcoinValueData> ReadDataFiles()
        {
            List<BitcoinValueData> listToReturn = new List<BitcoinValueData>();

            foreach (string file in Directory.EnumerateFiles("C: \\Users\\UO202549\\Downloads\\TPP11_2324\\TPP11\\EjercicioProductorConsumidor\\data\\", "*.txt")) 
            {
                if (Verbose)
                    Console.WriteLine("Reading file '" + file + "' ...");
                listToReturn.AddRange(GetFileContents(File.ReadAllText(file)));
            }

            return listToReturn;
        }

        /// <summary>
        /// Read all the data files tied to this program, placed in the /data directory of the C# project.
        /// </summary>
        /// <returns>Array with the data of all files placed in BitcoinValueData objects </returns>
        public static BitcoinValueData[] GetBitcoinData()
        {
            return ReadDataFiles().ToArray();
        }
    }

    public class BitcoinValueData
    {
        public DateTime Timestamp { get; set; }
        public double Value { get; set; }

        public override string ToString()
        {
            return Timestamp + ": " + Value;
        }
    }
}
