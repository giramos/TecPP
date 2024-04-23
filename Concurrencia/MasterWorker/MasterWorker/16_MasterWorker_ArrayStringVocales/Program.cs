using System;
using System.Threading;

namespace _16_MasterWorker_ArrayStringVocales
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Vocales");
            char[] array = { 'j', 'P', 'a', 'B', 'e', 'p', 'x', 'O', 'i' };
            string palabra = "comsaoiehfswiujbefAFESUIFHSNFVIESAFBNAJWEHIFsnaskjdfniuwefnsiuwbehgbas";
            char[] res = new char[palabra.Length];
            Console.WriteLine("\nVector original:");
            mostrarVector<char>(palabra.ToCharArray());

            int numHilo = 33;
            _02MasterVocales master = new _02MasterVocales(palabra.ToCharArray(), numHilo, ref res);
            master.Calcular();

            Console.WriteLine("\nVector resultado con {0} hilo:", numHilo);
            mostrarVector<char>(res);

        }

        public static void mostrarVector<T>(T[] matriz)
        {
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                System.Console.Write(matriz[i] + " ");

            }
        }
    }

    internal class _02MasterVocales
    {
        /// <summary>
        /// Vector del que se obtendrá el módulo
        /// </summary>
        private char[] vector;

        /// <summary>
        /// Número de trabajadores que se van a emplear en el cálculo.
        /// </summary>
        private int numeroHilos;

        private char[] resultado;


        public _02MasterVocales(char[] vector, int numeroHilos, ref char[] res)
        {
            if (numeroHilos < 1 || numeroHilos > vector.Length)
                throw new ArgumentException("El número de hilos debe ser menor o igual al tamaño del vector");
            this.vector = vector;
            this.numeroHilos = numeroHilos;
            this.resultado = res;
        }

        /// <summary>
        /// Este método crea y coordina el cálculo
        /// </summary>
        public void Calcular()
        {
            // Creamos los workers
            _02WorkerVocales[] workers = new _02WorkerVocales[this.numeroHilos];
            int numElementosPorHilo = this.vector.Length / numeroHilos; // Reparte el trabajo
            for (int i = 0; i < this.numeroHilos; i++)
            {
                int indiceDesde = i * numElementosPorHilo;
                int indiceHasta = (i + 1) * numElementosPorHilo - 1;
                if (i == this.numeroHilos - 1) //el último hilo, llega hasta el final del vector.
                {
                    indiceHasta = this.vector.Length - 1;
                }
                workers[i] = new _02WorkerVocales(this.vector, indiceDesde, indiceHasta, ref resultado);
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
            //char[] resultado; 
            //foreach (Worker worker in workers)
            //    lock(obj_lock)
            //        resultado = worker.Resultado;
            //return resultado;
        }
    }

    internal class _02WorkerVocales
    {
        /// <summary>
        /// Vector del que vamos a obtener el módulo
        /// </summary>
        private char[] vector;
        private static readonly object obj_lock = new object();


        /// <summary>
        /// Índices que indican el rango de elementos del vector 
        /// con el que vamos a trabajar.
        /// En el intervalo se incluyen ambos índices.
        /// </summary>
        private int indiceDesde, indiceHasta;

        /// <summary>
        /// Resultado del cálculo
        /// </summary>
        private char[] resultado;

        internal char[] Resultado
        {
            get { return this.resultado; }
        }

        internal _02WorkerVocales(char[] vector, int indiceDesde, int indiceHasta, ref char[] res)
        {
            this.vector = vector;
            this.indiceDesde = indiceDesde;
            this.indiceHasta = indiceHasta;
            this.resultado = res;
        }

        /// <summary>
        /// Método que realiza el cálculo 
        /// </summary>
        internal void Calcular() // ES UN ACTIONNNNNNNNNNNNNNNNNNNNN
        {

            for (int i = this.indiceDesde; i <= this.indiceHasta; i++) // Comprobar bien el += Los indices vaya!

                if ("aeiouAEIOU".Contains(vector[i]))
                {
                    lock (obj_lock)
                    {
                        resultado[i] = vector[i];
                    }

                }
        }
    }
}
