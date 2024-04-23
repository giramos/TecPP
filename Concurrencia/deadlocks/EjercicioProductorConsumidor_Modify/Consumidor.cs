using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Timers;

namespace EjercicioProductorConsumidor
{
    class Consumidor
    {

        private Queue<Producto> cola;

        public Consumidor(Queue<Producto> cola)
        {
            this.cola = cola;
        }

//        Sin embargo, debemos tener cuidado en los Consumers si la
//estructura de datos está vacía…

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
                    //    Thread.Sleep(100); //¡Nadie puede acceder a cola!

                    //producto = cola.Dequeue();

                    if (cola.Count != 0)    // Comprueba solo el nº de elementos de la estructura en vez de eso
                        producto = cola.Dequeue(); // Si no hay elementos, sal del lock y deja que un Producer pueda insertar alguno
                }
                Console.WriteLine("- Producto sacado: {0}.", producto);
                Thread.Sleep(random.Next(300, 700));
            }
        }

    }
}
