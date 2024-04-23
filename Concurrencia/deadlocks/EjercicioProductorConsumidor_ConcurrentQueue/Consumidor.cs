using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EjercicioProductorConsumidor
{
    class Consumidor
    {

        //private Queue<Producto> cola;
        private ConcurrentQueue<Producto> cola;

        //public Consumidor(Queue<Producto> cola)
        public Consumidor(ConcurrentQueue<Producto> cola)
        {
            this.cola = cola;
        }

        public void Run()
        {
            Random random = new Random();
            while (true)
            {
                Console.WriteLine("- Sacando producto...");
                Producto producto = null;
                //Producto producto;
                lock (cola)
                {
                    //while (cola.Count == 0)
                    //    Thread.Sleep(100);

                    //cola.TryDequeue(out producto);
                    if (cola.Count != 0)
                        cola.TryDequeue(out producto);
                }
                Console.WriteLine("- Producto sacado: {0}.", producto);
                Thread.Sleep(random.Next(300, 700));
            }
        }

    }
}
