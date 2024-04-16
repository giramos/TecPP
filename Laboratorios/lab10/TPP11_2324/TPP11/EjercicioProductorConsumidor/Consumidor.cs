using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EjercicioProductorConsumidor
{
    class Consumidor
    {

        private ConcurrentQueue<Producto> cola;

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
                lock (cola)
                {
                    //while (cola.Count == 0)
                    if (cola.Count != 0)
                        Thread.Sleep(100);

                    producto = cola.TryDequeue();
                }
                Console.WriteLine("- Producto sacado: {0}.", producto);
                Thread.Sleep(random.Next(300, 700));
            }
        }

    }
}
