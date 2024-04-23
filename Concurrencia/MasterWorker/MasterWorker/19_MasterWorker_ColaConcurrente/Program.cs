using ColaConcurrente;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;

namespace _19_MasterWorker_ColaConcurrente
{
    class Program
    {
        static void Main(string[] args)
        {
            // Crear una lista de palabras para procesar
            List<string> palabras = new List<string>
            {
                "hola",
                "mundo",
                "programación",
                "informática",
                "trabajo",
                "computadora",
                "manzana",
                "perro",
                "gato",
                "casa"
            };

            // Mostrar las palabras originales
            Console.WriteLine("Palabras originales:");
            Console.WriteLine(string.Join(", ", palabras));

            int maximoHilos = 20;

            // Realizar múltiples iteraciones con diferentes números de hilos
            for (int numeroHilos = 1; numeroHilos <= maximoHilos; numeroHilos++)
            {
                // Crear una instancia de la cola concurrente para cada iteración
                Cola<string> cola = new Cola<string>();

                // Crear el Master y los Workers para la iteración actual
                Master master = new Master(palabras, cola, numeroHilos);

                // Medir el tiempo de ejecución
                Stopwatch stopwatch = Stopwatch.StartNew();

                // Iniciar el procesamiento en paralelo
                master.Procesar();

                stopwatch.Stop();

                // Mostrar el tiempo de ejecución
                Console.WriteLine($"\nIteración con {numeroHilos} hilo(s): {stopwatch.ElapsedMilliseconds} ms");

                // Mostrar el contenido final de la cola
                Console.WriteLine("Palabras con más consonantes que vocales:");
                Console.WriteLine(cola);
            }

            MostrarLinea(Console.Out, "Num Hilos", "Ticks", "Resultado");
            for (int numeroHilos = 1; numeroHilos <= maximoHilos; numeroHilos++)
            {
                Cola<string> cola = new Cola<string>();
                Master master = new Master(palabras, cola, numeroHilos);
                DateTime antes = DateTime.Now;
                master.Procesar();

                Console.WriteLine(cola.ToString());

                DateTime despues = DateTime.Now;
                MostrarLinea(Console.Out, numeroHilos, (despues - antes).Ticks, cola.Count());

                //Entre ejecuciones, limpiamos y esperamos.
                GC.Collect();
                GC.WaitForFullGCComplete();
            }
        }

        static void MostrarLinea(TextWriter stream, string numHilosCabecera, string ticksCabecera, string resultadoCabecera)
        {
            stream.WriteLine("{0};{1};{2}", numHilosCabecera, ticksCabecera, resultadoCabecera);
        }

        static void MostrarLinea(TextWriter stream, int numHilos, long ticks, int resultado)
        {
            stream.WriteLine("{0};{1:N0};{2:N0}", numHilos, ticks, resultado);
        }
    }
}

public class Master
{
    private List<string> palabras;
    private Cola<string> cola;
    private int numWorkers;

    public Master(List<string> palabras, Cola<string> cola, int numWorkers)
    {
        this.palabras = palabras;
        this.cola = cola;
        this.numWorkers = numWorkers;
    }

    public void Procesar()
    {
        // Crear los Workers
        Worker[] workers = new Worker[numWorkers];
        int numElementosPorHilo = this.palabras.ToArray().Length / numWorkers;
        for (int i = 0; i < this.numWorkers; i++)
        {
            int indiceDesde = i * numElementosPorHilo;
            int indiceHasta = (i + 1) * numElementosPorHilo - 1;
            if (i == this.numWorkers - 1)
            {
                indiceHasta = this.palabras.ToArray().Length - 1;
            }
            workers[i] = new Worker(this.palabras, cola, indiceDesde, indiceHasta);
        }

        // Iniciar los Workers en hilos separados
        Thread[] threads = new Thread[numWorkers];
        for (int i = 0; i < numWorkers; i++)
        {
            threads[i] = new Thread(workers[i].Trabajar);
            threads[i].Start();
        }

        // Esperar a que todos los hilos terminen
        foreach (Thread thread in threads)
        {
            thread.Join();
        }
    }
}

public class Worker
{
    private List<string> palabras;
    private Cola<string> cola;
    private int indiceDesde, indiceHasta;
    private static object lockObj = new object(); // Objeto de bloqueo

    public Worker(List<string> palabras, Cola<string> cola, int indiceDesde, int indiceHasta)
    {
        this.palabras = palabras;
        this.cola = cola;
        this.indiceDesde = indiceDesde;
        this.indiceHasta = indiceHasta;
    }

    public void Trabajar()
    {
        // Procesar palabras en la lista
        for (int i = this.indiceDesde; i <= this.indiceHasta; i++)
        {
            if (ContarConsonantes(palabras[i]) > ContarVocales(palabras[i]))
            {
                // Añadir la palabra a la cola dentro de un bloqueo
                lock (lockObj)
                {
                    cola.Añadir(palabras[i]);
                }
            }
        }
    }

    private int ContarConsonantes(string palabra)
    {
        // Contar consonantes en la palabra
        return palabra.Count(c => "bcdfghjklmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ".Contains(c));
    }

    private int ContarVocales(string palabra)
    {
        // Contar vocales en la palabra
        return palabra.Count(c => "aeiouAEIOU".Contains(c));
    }
}
