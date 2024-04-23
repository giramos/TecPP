using ColaConcurrente;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EjercicioProductorConsumidor
{
    class Consumidor
    {

        private Cola<Producto> cola;

        public Consumidor(Cola<Producto> cola)
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
                    //while (cola.Count() == 0)
                    //    Thread.Sleep(100);

                    //producto = cola.Extraer();
                    if (cola.Count() != 0)
                        producto = cola.Extraer();
                }
                Console.WriteLine("- Producto sacado: {0}.", producto);
                Thread.Sleep(random.Next(300, 700));
            }
        }

    }
}
