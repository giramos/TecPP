

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

   namespace ObligatoriaSesion10
{
        /// <summary>
        ///Cuenta el numero de veces que el valor del BitCoin sobrepasa o iguala cierto valor limite. 
        /// 
        /// </summary>
        public class Master
        {
            private int limite; //Valor limite 

            private BitcoinValueData[] vector; /// Vector del que se contará

            private int numeroHilos; /// Número de trabajadores que se van a emplear en el cálculo.

            public Master(BitcoinValueData[] vector, int numberOfThreads, int limite)
            {
                if (numberOfThreads < 1 || numberOfThreads > vector.Length)
                    throw new ArgumentException("El número de hilos debe ser menor o igual al tamaño del vector");
                this.vector = vector;
                this.numeroHilos = numberOfThreads;
                this.limite = limite;
            }

            /// <summary>
            /// Este método crea y coordina el cálculo
            /// </summary>
            public double Contar()
            {
                // Creamos los workers
                Worker[] workers = new Worker[this.numeroHilos];
                int numeroElementosPorHilo = this.vector.Length / numeroHilos;
                for (int i = 0; i < this.numeroHilos; i++)
                    workers[i] = new Worker(this.vector, // Inicia lod objetos Workers
                        i * numeroElementosPorHilo,
                        (i < this.numeroHilos - 1) ? (i + 1) * numeroElementosPorHilo - 1 : this.vector.Length - 1, this.limite
                        );

                Thread[] threads = new Thread[workers.Length];
                for (int i = 0; i < workers.Length; i++)
                {
                    threads[i] = new Thread(workers[i].Contar);// Creamos el hilo
                    threads[i].Name = "Worker Vector Modulus " + (i + 1);// le damos un nombre (opcional)
                    threads[i].Priority = ThreadPriority.BelowNormal;// prioridad (opcional)
                    threads[i].Start();  // arranca un hilo asociado con cada Worker // hilo secun
                }

                //¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡¡OJO!!!!!!!!!!!!!!!!
                //Esperamos a que acaben
                foreach (Thread thread in threads)
                    thread.Join();


                //Por último, sumamos todos los resultados parciales
                long result = 0;
                foreach (Worker worker in workers)
                {
                    result += worker.Result;
                }
                //Devolvemos el resultado. 
                return result;
            }

        }

    }