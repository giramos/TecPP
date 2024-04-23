using Microsoft.VisualBasic;
using System.Numerics;
using System;
using System.Threading;

namespace ObligatoriaSesion10
{
    internal class Master
    {
        BitcoinValueData[] array;
        int valor;
        int numHilos;

        public Master(BitcoinValueData[] array, int valor, int numHilos)
        {
            if (numHilos < 1 || numHilos > array.Length)
                throw new ArgumentException("El número de hilos debe ser menor o igual al tamaño del vector");
            this.array = array;
            this.valor = valor;
            this.numHilos = numHilos;
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
                workers[i] = new Worker(this.array, this.valor, indiceDesde, indiceHasta);
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