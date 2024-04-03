using System;

using System.Threading;

namespace HilosPOO
{
    class Expositor<T>
    {
        private T _objeto;
        private int _nVeces;

        public Expositor(T objeto, int nVeces)
        {
            this._objeto = objeto;
            this._nVeces = nVeces;
        }

        /// <summary>
        /// Este método será el que le pasemos al hilo para que lo invoque.
        /// ¿Qué hace?
        /// </summary>
        public void Run()
        {
            int i = 0;
            //¿Qué indica Thread.CurrentThread.ManagedThreadId?
            Console.WriteLine("Comienza el hilo ID={0} que escribirá {1} veces cada 2000ms. (mínimo)", Thread.CurrentThread.ManagedThreadId, _nVeces);

            while (i < _nVeces)
            {
                //Dormimos el proceso durante 2000 milisegundos
                //¿Para qué es útil? ¿Y para qué no debe utilizarse?
                Thread.Sleep(2000);
                Console.WriteLine("Ejecución {0} del hilo [ID={1}; NOMBRE={2}; PRIORIDAD={3}] con valor {4}",
                    i, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name, Thread.CurrentThread.Priority, _objeto);
                i++;
            }
            Console.WriteLine($"Fin del hilo ID = {Thread.CurrentThread.ManagedThreadId}.");
        }
    }
}
