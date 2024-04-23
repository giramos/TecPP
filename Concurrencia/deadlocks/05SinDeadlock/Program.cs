using System;
using System.Threading;

namespace _05SinDeadlock
{
    class Program
    {

        /// <summary>
        /// El Interbloqueo (o deadlock) se produce cuando varias tareas (procesos o hilos)
        /// están esperando a un evento que sólo otra puede iniciar, por tanto
        /// todas las tareas se bloquean de forma permanente.
        /// En este caso, si de forma concurrente se hace una transferencia de A a B y de B a A,
        /// estaremos en un caso de interbloqueo.
        /// </summary>
        public static void Main()
        {
            decimal cantidadInicial = 30000;
            Cuenta cuentaA = new Cuenta("A", cantidadInicial), cuentaB = new Cuenta("B", cantidadInicial);

            Random random = new Random();
            int iteraciones = 10;
            Thread[] hilos = new Thread[iteraciones];

            //Mitad de transferencias (una por hilo) serán A->B y la otra mitad B->A con cantidades aleatorias.
            for (int i = 0; i < iteraciones; i++)
            {
                decimal cantidad = (decimal)random.NextDouble() * random.Next(1, 600);
                if (i % 2 == 0)
                    hilos[i] = new Thread(() => Transferir(cuentaA, cuentaB, cantidad));
                else
                    hilos[i] = new Thread(() => Transferir(cuentaB, cuentaA, cantidad));
                hilos[i].Name = "Num. transferencia: " + i;
            }

            foreach (Thread hilo in hilos)
                hilo.Start();

        }

        private static void Transferir(Cuenta cuentaA, Cuenta cuentaB, decimal cantidad)
        {
            Console.WriteLine("Transfiriendo {0:N} euros de la cuenta {1} a la cuenta {2}...",
                cantidad, cuentaA.NumCuenta, cuentaB.NumCuenta);
            if (cuentaA.Transferir(cuentaB, cantidad))
                Console.WriteLine("\tTrasferencia con éxito, hilo: {0}.", Thread.CurrentThread.Name);
            else
                Console.WriteLine("\tTransferencia errónea, hilo: {0}.", Thread.CurrentThread.Name);
        }
    }
}
