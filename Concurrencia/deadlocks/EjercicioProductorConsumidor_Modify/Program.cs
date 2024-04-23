using System;
using System.Collections.Generic;
using System.Threading;

namespace EjercicioProductorConsumidor
{
    class Program
    {
        /// <summary>
        /// ¿Por qué se bloquea el programa después de un tiempo? ¿Puedes arreglar el problema?
        /// ¿Añadiendo varios Consumidores y Productores?
        /// Una vez funcione adáptase el código para que funcione con una ConcurrentQueue
        /// https://learn.microsoft.com/es-es/dotnet/api/system.collections.concurrent?view=net-7.0
        /// Pruébese también con la Cola de la tarea 11.
        /// </summary>
        static void Main()
        {
            Queue<Producto> cola = new Queue<Producto>();
            Productor productor = new Productor(cola);
            Consumidor consumidor = new Consumidor(cola);
            new Thread(productor.Run).Start();
            Thread.Sleep(2000);
            new Thread(consumidor.Run).Start();
        }
    }
}
