using System;
using System.Threading;

namespace MasterWorkerClase
{
    internal class Master
    {
        short[] vector1;
        short[] vector2;
        int nHilos;

        public Master(short[] vector1, short[] vector2, int nHilos)
        {
            if (nHilos < 1 || nHilos > vector1.Length)
                throw new ArgumentException("El número de hilos debe ser menor o igual al tamaño del vector1 y vector2");
            if (vector2.Length > vector1.Length)
                throw new ArgumentException("El vector 2 no puede tener mas longitud que el vector 1");
            this.vector1 = vector1;
            this.vector2 = vector2;
            this.nHilos = nHilos;
        }

        public int Calcular()
        {
            // Creamos los workers
            Worker[] workers = new Worker[this.nHilos];
            int numElementosPorHilo = this.vector1.Length / nHilos;
            for (int i = 0; i < this.nHilos; i++)
            {
                int indiceDesde = i * numElementosPorHilo;
                int indiceHasta = (i + 1) * numElementosPorHilo - 1;
                if (i == this.nHilos - 1) //el último hilo, llega hasta el final del vector.
                {
                    indiceHasta = this.vector1.Length - 1;
                }
                workers[i] = new Worker(this.vector1, this.vector2, indiceDesde, indiceHasta);
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
}