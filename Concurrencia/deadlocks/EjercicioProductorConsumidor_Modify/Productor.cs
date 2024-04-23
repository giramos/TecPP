using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EjercicioProductorConsumidor
{
    class Productor
    {

        private Queue<Producto> cola;
        private int numeroProductosCreados;


        public void Run()
        {
            Random random = new Random();
            while (true)
            {
                Producto producto = new Producto(++numeroProductosCreados);
                Console.WriteLine("+ Insertando {0}...", producto);
                lock (cola)
                    cola.Enqueue(producto);
                Console.WriteLine("+ {0} insertado.", producto);
                Thread.Sleep(random.Next(500, 1000));
            }
        }


        public Productor(Queue<Producto> cola)
        {
            this.cola = cola;
        }
    }
}
