using _04Procesado.Tareas.Secuencial;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ActividadObligatoria
{
    internal class Program
    {
        static void Main(string[] args)
        {
            String texto = ProcesadorTextos.LeerFicheroTexto("clarin.txt"); // Cargamos el texto
            String[] palabras = ProcesadorTextos.DividirEnPalabras(texto); // Separamos el texto en palabras


            // ---------------------------------------- VERSION PARALELO

            Stopwatch cronometro = new Stopwatch(); // Donde vamos a medir el tiempo

            IDictionary<String, int> contadorPalabras = new Dictionary<String, int>();

            IDictionary<String, int> contadorPalarasP(String[] vector) // contador de palabras en paralelo
            {
                IDictionary<String, int> solucion = new Dictionary<String, int>();

                IList<int> threads = new List<int>(); // Los hilos

                // misma tarea pra muchos datos -> paralelizacion de datos
                // procesamos todos los datos -> foreach
                Parallel.ForEach(
                    vector, (e) =>
                    {
                        // para mostrar los hilos por pantalla
                        if (!threads.Contains(Thread.CurrentThread.ManagedThreadId)) // Si aún no está en la lista
                        {
                            Console.WriteLine("Identificador hilo: " + Thread.CurrentThread.ManagedThreadId); // Lo ponemos por pantalla
                            threads.Add(Thread.CurrentThread.ManagedThreadId); // Lo añadimos a nuestra lista de hilos
                        }


                        lock (solucion) // lock del dictionary
                        {
                            if (solucion.ContainsKey(e)) // si está en el diccionario
                            {
                                solucion[e] = solucion[e] + 1; // Lo contamos
                            }
                            else // Si no está en el diccionario lo añadimos
                            {
                                solucion.Add(e, 1); // al añadirlo está a 1
                            }
                        }
                    }
                    );

                return solucion; // devolvemos el diccionario
            }

            cronometro.Start(); // empezamos a medir
            contadorPalabras = contadorPalarasP(palabras);
            cronometro.Stop(); // terminamos de medir
            long millisParalelo = cronometro.ElapsedMilliseconds; // calculamos cuánto ha tardado

            //cronometro.Start(); // empezamos a medir
            var resultado = contadorPalabras.AsParallel().OrderByDescending((arg) => arg.Value).ThenBy((arg) => arg.Key); // los resultados en paralelo ordenados
            //cronometro.Stop(); // terminamos de medir
            //long millisParalelo = cronometro.ElapsedMilliseconds; // calculamos cuánto ha tardado






            // -------------------------------------------------- VERSION SECUENCIAL

            IDictionary<String, int> contadorPalabrasS(String[] v) // contador de palabras en secuencial
            {
                IDictionary<String, int> solucion = new Dictionary<String, int>();
                for (int i = 0; i < v.Length; i++)
                {
                    if (solucion.ContainsKey(v[i])) // si ya está en el diccionario
                    {
                        solucion[v[i]] = solucion[v[i]] + 1;
                    }
                    else // Si no está la creamos y añadimos
                    {
                        solucion.Add(v[i], 1);
                    }
                }

                return solucion;
            }

            cronometro.Restart(); // volvemos a poner el cronometro
            IDictionary<String, int> resultadoS = contadorPalabrasS(palabras);
            cronometro.Stop(); // terminamos de medir
            long millisSecuencial = cronometro.ElapsedMilliseconds;


            //cronometro.Restart(); // volvemos a poner el cronometro
            var orderedResultSequential = resultadoS.OrderByDescending((arg) => arg.Value).ThenBy((arg) => arg.Key);
            //cronometro.Stop(); // terminamos de medir
            //long millisSecuencial = cronometro.ElapsedMilliseconds; // calculamos cuánto ha tardado

            // Tiempos
            Console.WriteLine("Tiempo en paralelo: ", millisParalelo);
            Console.WriteLine("Tiempo en secuencial: ", millisSecuencial);
            Console.WriteLine("Beneficio del rendimiento: ", (millisSecuencial / (double)millisParalelo - 1) * 100);
        }
    }
}
