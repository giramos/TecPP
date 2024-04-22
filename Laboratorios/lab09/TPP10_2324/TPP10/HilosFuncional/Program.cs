using System;

using System.Threading;

namespace HilosFuncional
{
    class Program
    {
        static void Main(string[] args)
        {

            // En el ejemplo HilosPOO, el método Run de cada Expositor no recibía parámetros.
            // De hecho, utilizábamos el propio objeto Expositor para encapsular los datos (valor y nVeces).
            // El/Los parámetro/s lo encapsulábamos como atributo/s de una clase.

            // A continuación veremos los casos desde un enfoque más funcional.

            //Thread recibe en su constructor cualquier Action con 0 o 1 parámetros de tipo object.

            Thread hiloParametrizado = new Thread(ObtenerDatos);

            //En el método Start pasamos un argumento (si es necesario).
            hiloParametrizado.Start("wwww.google.es");


            //También podríamos utilizar directamente una expresión lambda:
            Thread hiloSuelto = new Thread(
                     valor =>
                     {
                         Console.WriteLine("El hilo suelto recibe " + valor);
                     }
             );
            hiloSuelto.Start("Declarando el action directamente");


            //¿Y si necesitamos más parámetros? 

            //¿Qué concepto se está aplicando aquí?

            //Vamos a crear 4 hilos.
            //Cada hilo debería imprimir un par de valores: 40 y 41, 41 y 42, 42 y 43, 43 y 44... En cualquier orden.

            //EJERCICIO: ¿Cómo arreglamos esto?

            Thread[] hilos = new Thread[4];
            int numero = 40;
            for (int i = 0; i < 4; i++)
            {
                int copia = numero;
                //Sin parámetro                
                hilos[i] = new Thread(
                    () =>
                        { 
                            Console.WriteLine($"{copia} {copia+1}");
                        }
                    );
                hilos[i].Start();
                numero++;
            }

            //EJERCICIO: Empleando un enfoque funcional, impleméntese la funcionalidad del ejercicio Expositor de HilosPOO.
            
            //Thread[] hilosExpositor = new Thread[4];
            //for (int i = 0;i < 4; i++)
            //{
            //    hilosExpositor[i] = new Thread(() =>
            //    {
            //        EjecutarRun(1 + i, i);
            //    });
            //    hilosExpositor[i].Name = "Hilo número: " + i; //Nombre del hilo.
            //    hilosExpositor[i].Priority = ThreadPriority.Normal; //Prioridad
            //    hilosExpositor[i].Start();
            //}

            //// Ejercicio fun
            //Thread hiloFuncional = new Thread(Run);

            ////En el método Start pasamos un argumento (si es necesario).
            //hiloFuncional.Start(1);


            //Thread[] hilosfunc = new Thread[4];

            //for (int i = 0; i < 4; i++)
            //{
            //    //¿Qué estamos enviando en el construtor de cada Thread?
            //    hilosfunc[i] = new Thread(Run);

            //    //Parámetros opcionales.
            //    hilosfunc[i].Name = "Hilo número: " + i; //Nombre del hilo.
            //    hilosfunc[i].Priority = ThreadPriority.Normal; //Prioridad
            //}

            //foreach (Thread hilo in hilosfunc)
            //{
            //    //Iniciamos el hilo con el método Start de la clase Thread.
            //    hilo.Start(1);
            //    //hilo.Join();
            //}
            
            Thread[] hilosfunc = new Thread[4];

            for (int i = 0; i < 4; i++)
            {
                //¿Qué estamos enviando en el construtor de cada Thread?
                //hilosfunc[i] = new Thread(Run);
                hilosfunc[i] = new Thread(
                     valor =>
                     {
                         Run(valor);
                     }
             );

                //Parámetros opcionales.
                hilosfunc[i].Name = "Hilo número: " + i; //Nombre del hilo.
                hilosfunc[i].Priority = ThreadPriority.Normal; //Prioridad
            }

            foreach (Thread hilo in hilosfunc)
            {
                //Iniciamos el hilo con el método Start de la clase Thread.
                hilo.Start(1);
                //hilo.Join();
            }
        }

        private static void EjecutarRun<T>(int _nVeces, T _objeto)
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

        public static void ObtenerDatos(object valor)
        {
            Console.WriteLine("Obteniendo datos del destino {0}", valor);
            //Simulamos carga de trabajo, fines demostrativos.
            Thread.Sleep(2000);
            Console.WriteLine("Datos obtenidos y almacenados");

        }

        static int _nVeces = 4;
        public static void Run<T>(T _objeto)
        {
            int i = 0;
            int? ob = _objeto as int?;
            //¿Qué indica Thread.CurrentThread.ManagedThreadId?
            Console.WriteLine("Comienza el hilo ID={0} que escribirá {1} veces cada 2000ms. (mínimo)", Thread.CurrentThread.ManagedThreadId, _nVeces);

            while (i < _nVeces)
            {
                //Dormimos el proceso durante 2000 milisegundos
                //¿Para qué es útil? ¿Y para qué no debe utilizarse?
                Thread.Sleep(2000);
                Console.WriteLine("Ejecución {0} del hilo [ID={1}; NOMBRE={2}; PRIORIDAD={3}] con valor {4}",
                    i, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name, Thread.CurrentThread.Priority, ob);
                i++;
                ob++;
                
            }
            Console.WriteLine($"Fin del hilo ID = {Thread.CurrentThread.ManagedThreadId}.");
        }
    }
}
