using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Hilos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread hiloParametrizado = new Thread(ObtenerDatos);

            //En el método Start pasamos el parámetro si es necesario.
            hiloParametrizado.Start("wwww.google.es");
            hiloParametrizado.Join();


            //También podríamos utilizar directamente una expresión lambda:
            Thread hiloSuelto = new Thread(
                     valor =>
                     {
                         Console.WriteLine("El hilo suelto recibe " + valor);
                     }
             );
            hiloSuelto.Start("Exprensión lambda");
            hiloSuelto.Join();


            //¿Y si necesitamos más parámetros? Variables libres.

            //Vamos a crear 4 hilos.
            //Cada hilo debería imprimir un par de valores: 40 y 41, 41 y 42, 42 y 43, 43 y 44...

            //¿Cómo arreglamos esto?

            Thread[] hilos = new Thread[4];
            int numero = 40;

            for (int i = 0; i < 4; i++)
            {
                int copia = numero; // OPCION 1
                //Sin parámetro                
                hilos[i] = new Thread(
                    () =>
                    {
                        //Console.WriteLine($"{numero} {numero + 1} ");
                        //Console.WriteLine($"{Interlocked.Add(ref numero, 0)} {Interlocked.Add(ref numero, 1)} "); // OPCION 2
                        Console.WriteLine($"{copia} {copia + 1} "); //OPCION 1
                    }
                    );
                hilos[i].Start();
                numero++; // OPCION 1
            }

            foreach (var hilo in hilos)
            {
                hilo.Join();
            }

            //Ejercicio: Empleando un enfoque funcional, impleméntese el ejercicio Expositor de HilosPOO.
            Console.WriteLine("Ejercicio Expositores con un enfoque funcional");
            Thread[] HilosExpositor = new Thread[4];
            for(int i = 0; i<HilosExpositor.Length; i++)
            {
                HilosExpositor[i] = new Thread(() => EjecutaRun(i + 1, i));
                HilosExpositor[i].Start();
            }

            foreach (var hilo in HilosExpositor)
            {
                hilo.Join();
            }

            // Ejercicio: //Empleando el ienumerable de palabras clasifica, mediante la creacion manual de hilos, 
            //las palabras en tres colecciones distintas: 
            //    1)Palabras con todas sus letras mayusculas 
            //    2)PAlabras con todas sus letras minusculas 
            //    3)Palabras con letras tanto mayusculas como minusculas.
            //Finalmente comprueba mediante PLinq que cada solucion posee las palabras adecuadas
            var palabras = new string[] { "Hola", "adios", "BUENAS", "ok", "OKI", "Doki", "PEPE", "el", "hijo" };
            List<string> may = new List<string>();
            List<string> min = new List<string>();
            List<string> mix = new List<string>();
            Thread hiloMay = new Thread(() => Clasifica(palabras, may, Tipo.Mayusculas));
            Thread hiloMin = new Thread(() => Clasifica(palabras, min, Tipo.Minusculas));
            Thread hiloMix = new Thread(() => Clasifica(palabras, mix, Tipo.Mixto));
            hiloMay.Start();
            hiloMin.Start();
            hiloMix.Start();
            hiloMay.Join();
            hiloMin.Join();
            hiloMix.Join();
            //Comprobacion con PLINQ
            Console.WriteLine("Lista de mayusculas");
            may.ForEach(Console.WriteLine);
            Console.WriteLine("Lista de minusculas");
            min.ForEach(Console.WriteLine);
            Console.WriteLine("Lista de mayusculas y minusculas");
            mix.ForEach(Console.WriteLine);

            // Ejercicio:  crear dos hilosmanualmente que trabajen con el ienumerable de palabras
            // proporcionado. Ambos hilos haran uso de la misma variable compartida que hará de contador.
            // El primer hilo recorrera el ienumerable y restara 1 al contador por cada palabra de tamaño par.
            // El segundo hilo hara una operación similar, pero sumará 1 por cada palabra de tamaño impar que encuentre.
            // Imprimir mediante este procedimiento la diferencia entre el nº de palabras para y el impar
            int cont = 0;
            Thread hilo1 = new Thread(() => Operacion(palabras, ref cont, true));
            Thread hilo2 = new Thread(() => Operacion(palabras, ref cont, false));
            hilo1.Start();
            hilo2.Start();
            hilo1.Join();
            hilo2.Join();
            Console.WriteLine("La diferencia es: "+ cont);

            //Ejercicio: igual que el anterior pero que sea apartir de un array de hilos

            // Crear un array de 4 hilos
            Thread[] hilosClasifica = new Thread[4];

            // Iniciar cada hilo en un bucle
            for (int i = 0; i < hilosClasifica.Length; i++)
            {
                hilosClasifica[i] = new Thread(() => ClasificarPalabras(palabras, i));
                hilosClasifica[i].Start();
            }

            // Esperar a que todos los hilos terminen
            foreach (var hilo in hilosClasifica)
            {
                hilo.Join();
            }

            //Ejercicio: Igual Igual que el de minusculas y mayus

            //string[] palabras = { "Hola", "adios", "BUENAS", "ok", "OKI", "Doki", "PEPE", "el", "hijo" };

            //// Crear listas para almacenar las palabras clasificadas
            //List<string> may = new List<string>();
            //List<string> min = new List<string>();
            //List<string> mix = new List<string>();

            //// Crear un array de hilos para clasificar las palabras
            //Thread[] hilos = new Thread[3];

            //// Iniciar cada hilo en un bucle
            //hilos[0] = new Thread(() => Clasifica(palabras, may, Tipo.Mayusculas));
            //hilos[1] = new Thread(() => Clasifica(palabras, min, Tipo.Minusculas));
            //hilos[2] = new Thread(() => Clasifica(palabras, mix, Tipo.Mixto));

            //foreach (var hilo in hilos)
            //{
            //    hilo.Start();
            //}

            //// Esperar a que todos los hilos terminen
            //foreach (var hilo in hilos)
            //{
            //    hilo.Join();
            //}

            //// Imprimir las palabras clasificadas
            //Console.WriteLine("Lista de mayúsculas:");
            //may.ForEach(Console.WriteLine);
            //Console.WriteLine("Lista de minúsculas:");
            //min.ForEach(Console.WriteLine);
            //Console.WriteLine("Lista de mayúsculas y minúsculas:");
            //mix.ForEach(Console.WriteLine);
        }

        static void ClasificarPalabras(string[] palabras, int indice)
        {
            // Determinar si el índice es par o impar
            bool esPar = indice % 2 == 0;

            // Clasificar las palabras según si son pares o no
            foreach (var palabra in palabras)
            {
                if ((palabra.Length % 2 == 0 && esPar) || (palabra.Length % 2 != 0 && !esPar))
                {
                    Console.WriteLine($"Hilo {Thread.CurrentThread.ManagedThreadId}: {palabra} es {(esPar ? "par" : "impar")}");
                }
            }
        }

        private static void Operacion(string[] palabras, ref int cont, bool esPar)
        {
            foreach(var i in palabras)
            {
                if(i.Length % 2 == 0 && esPar)
                {
                    cont--;
                }
                else if(i.Length %2 != 0 && !esPar)
                {
                    cont++;
                }
            }
        }

        private static void Clasifica(string[] palabras, List<string> lista, Tipo tipo)
        {
            foreach(var i in palabras)
            {
                switch (tipo)
                {
                    case Tipo.Mayusculas:
                        if (i.All(Char.IsUpper))
                        {
                            lista.Add(i);
                        }
                        break;
                    case Tipo.Minusculas:
                        if (i.All(Char.IsLower))
                        {
                            lista.Add(i);
                        }
                        break;
                    case Tipo.Mixto:
                        if(!i.All(Char.IsUpper) && !i.All(Char.IsLower))
                        {
                            lista.Add(i);
                        }
                        break;
                }
            }
        }

        public enum Tipo { Mayusculas, Minusculas, Mixto }
        public static void ObtenerDatos(object valor)
        {
            Console.WriteLine("Obteniendo datos del destino {0}", valor);
            //Simulamos carga de trabajo, fines demostrativos.
            Thread.Sleep(2000);
            Console.WriteLine("Datos obtenidos y almacenados");

        }

        public static void EjecutaRun<T>(int _nVeces, T _objeto)
        {
            int i = 0;
            //¿Qué indica Thread.CurrentThread.ManagedThreadId? El identificador del hilo
            Console.WriteLine("Comienza el hilo ID={0} que escribirá {1} veces cada 2000ms.", Thread.CurrentThread.ManagedThreadId, _nVeces);

            while (i < _nVeces)
            {
                //Dormimos el proceso durante 2000 milisegundos
                //¿Para qué es útil? No para sincronizar. simular si. Nunca para esperar que otro hilo haga algo
                Thread.Sleep(2000);
                Console.WriteLine("Ejecución {0} del hilo [ID={1}; NOMBRE={2}; PRIORIDAD={3}] con valor {4}",
                    i, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.Name, Thread.CurrentThread.Priority, _objeto);
                i++;
            }
        }
    }

}
