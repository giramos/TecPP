using ColaConcurrente;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace _03_MasterWorker_ColaPalabrasConsonates
{
    //internal class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        string[] palabras = {
    //            "Jose", "aveces", "ahi", "main", "aun", "sacerdote", "Hola",
    //            "OpenAI", "Amigo", "Test", "Consonantes", "Vocales", "Programación"
    //        };

    //        int maximoHilos = 20;
    //        MostrarLinea(Console.Out, "Num Hilos", "Ticks", "Resultado");
    //        for (int numeroHilos = 1; numeroHilos <= maximoHilos; numeroHilos++)
    //        {
    //            Master master = new Master(palabras, numeroHilos);
    //            DateTime antes = DateTime.Now;
    //            ConcurrentQueue<string> palabrasConsonantes = master.Calcular();
    //            foreach (var i in palabrasConsonantes)
    //            {
    //                Console.WriteLine(i);
    //            }
    //            DateTime despues = DateTime.Now;
    //            MostrarLinea(Console.Out, numeroHilos, (despues - antes).Ticks, palabrasConsonantes.Count);

    //            //Entre ejecuciones, limpiamos y esperamos.
    //            GC.Collect();
    //            GC.WaitForFullGCComplete();
    //        }
    //    }

    //    static void MostrarLinea(TextWriter stream, string numHilosCabecera, string ticksCabecera, string resultadoCabecera)
    //    {
    //        stream.WriteLine("{0};{1};{2}", numHilosCabecera, ticksCabecera, resultadoCabecera);
    //    }

    //    static void MostrarLinea(TextWriter stream, int numHilos, long ticks, int resultado)
    //    {
    //        stream.WriteLine("{0};{1:N0};{2:N0}", numHilos, ticks, resultado);
    //    }
    //}

    //public class Master
    //{
    //    private string[] palabras;
    //    private int numeroHilos;

    //    public Master(string[] palabras, int numeroHilos)
    //    {
    //        this.palabras = palabras;
    //        this.numeroHilos = numeroHilos;
    //    }

    //    public ConcurrentQueue<string> Calcular()
    //    {
    //        ConcurrentQueue<string> palabrasConsonantes = new ConcurrentQueue<string>();

    //        // Creamos los workers
    //        Worker[] workers = new Worker[this.numeroHilos];
    //        int numElementosPorHilo = this.palabras.Length / numeroHilos;
    //        for (int i = 0; i < this.numeroHilos; i++)
    //        {
    //            int indiceDesde = i * numElementosPorHilo;
    //            int indiceHasta = (i + 1) * numElementosPorHilo - 1;
    //            if (i == this.numeroHilos - 1)
    //            {
    //                indiceHasta = this.palabras.Length - 1;
    //            }
    //            workers[i] = new Worker(this.palabras, palabrasConsonantes, indiceDesde, indiceHasta);
    //        }
    //        // Iniciamos los hilos
    //        Thread[] hilos = new Thread[workers.Length];
    //        for (int i = 0; i < workers.Length; i++)
    //        {
    //            hilos[i] = new Thread(workers[i].Calcular);
    //            hilos[i].Name = "Worker número: " + (i + 1);
    //            hilos[i].Priority = ThreadPriority.Normal;
    //            hilos[i].Start();
    //        }

    //        // Esperamos a que acaben para continuar
    //        foreach (Thread thread in hilos)
    //            thread.Join();

    //        return palabrasConsonantes;
    //    }
    //}

    //internal class Worker
    //{
    //    private string[] palabras;
    //    private ConcurrentQueue<string> palabrasConsonantes;
    //    private int indiceDesde, indiceHasta;

    //    internal Worker(string[] palabras, ConcurrentQueue<string> palabrasConsonantes, int indiceDesde, int indiceHasta)
    //    {
    //        this.palabras = palabras;
    //        this.palabrasConsonantes = palabrasConsonantes;
    //        this.indiceDesde = indiceDesde;
    //        this.indiceHasta = indiceHasta;
    //    }

    //    internal void Calcular()
    //    {
    //        for (int i = this.indiceDesde; i <= this.indiceHasta; i++)
    //        {
    //            if (TieneMasConsonantes(this.palabras[i]))
    //            {
    //                palabrasConsonantes.Enqueue(this.palabras[i]);
    //            }
    //        }
    //    }

    //    private bool TieneMasConsonantes(string palabra)
    //    {
    //        int consonantes = 0;
    //        int vocales = 0;
    //        foreach (char c in palabra.ToLower())
    //        {
    //            if (char.IsLetter(c))
    //            {
    //                if ("aeiou".Contains(c))
    //                    vocales++;
    //                else
    //                    consonantes++;
    //            }
    //        }
    //        return consonantes > vocales;
    //    }
    //}

    //internal class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Queue<string> palabras = new Queue<string>();
    //        palabras.Enqueue("Hola");
    //        palabras.Enqueue("OpenAI");
    //        palabras.Enqueue("Amigo");
    //        palabras.Enqueue("Test");
    //        palabras.Enqueue("Consonantes");
    //        palabras.Enqueue("Vocales");
    //        palabras.Enqueue("Programación");
    //        palabras.Enqueue("Jose");
    //        palabras.Enqueue("aveces");
    //        palabras.Enqueue("ahi");
    //        palabras.Enqueue("main");
    //        palabras.Enqueue("aun");
    //        palabras.Enqueue("sacerdote");

    //        int maximoHilos = 20;
    //        MostrarLinea(Console.Out, "Num Hilos", "Ticks", "Resultado");
    //        for (int numeroHilos = 1; numeroHilos <= maximoHilos; numeroHilos++)
    //        {
    //            Master master = new Master(palabras, numeroHilos);
    //            DateTime antes = DateTime.Now;
    //            ConcurrentQueue<string> palabrasConsonantes = master.Calcular();
    //            foreach (var i in palabrasConsonantes)
    //            {
    //                Console.WriteLine(i);
    //            }
    //            DateTime despues = DateTime.Now;
    //            MostrarLinea(Console.Out, numeroHilos, (despues - antes).Ticks, palabrasConsonantes.Count);

    //            //Entre ejecuciones, limpiamos y esperamos.
    //            GC.Collect();
    //            GC.WaitForFullGCComplete();
    //        }
    //    }

    //    static void MostrarLinea(TextWriter stream, string numHilosCabecera, string ticksCabecera, string resultadoCabecera)
    //    {
    //        stream.WriteLine("{0};{1};{2}", numHilosCabecera, ticksCabecera, resultadoCabecera);
    //    }

    //    static void MostrarLinea(TextWriter stream, int numHilos, long ticks, int resultado)
    //    {
    //        stream.WriteLine("{0};{1:N0};{2:N0}", numHilos, ticks, resultado);
    //    }
    //}

    //public class Master
    //{
    //    private Queue<string> palabras;
    //    private int numeroHilos;

    //    public Master(Queue<string> palabras, int numeroHilos)
    //    {
    //        this.palabras = palabras;
    //        this.numeroHilos = numeroHilos;
    //    }

    //    public ConcurrentQueue<string> Calcular()
    //    {
    //        ConcurrentQueue<string> palabrasConsonantes = new ConcurrentQueue<string>();

    //        // Creamos los workers
    //        Worker[] workers = new Worker[this.numeroHilos];
    //        int numElementosPorHilo = this.palabras.ToArray().Length / numeroHilos;
    //        for (int i = 0; i < this.numeroHilos; i++)
    //        {
    //            int indiceDesde = i * numElementosPorHilo;
    //            int indiceHasta = (i + 1) * numElementosPorHilo - 1;
    //            if (i == this.numeroHilos - 1)
    //            {
    //                indiceHasta = this.palabras.ToArray().Length - 1;
    //            }
    //            workers[i] = new Worker(this.palabras, palabrasConsonantes, indiceDesde, indiceHasta);
    //        }
    //        // Iniciamos los hilos
    //        Thread[] hilos = new Thread[workers.Length];
    //        for (int i = 0; i < workers.Length; i++)
    //        {
    //            hilos[i] = new Thread(workers[i].Calcular);
    //            hilos[i].Name = "Worker número: " + (i + 1);
    //            hilos[i].Priority = ThreadPriority.Normal;
    //            hilos[i].Start();
    //        }

    //        // Esperamos a que acaben para continuar
    //        foreach (Thread thread in hilos)
    //            thread.Join();

    //        return palabrasConsonantes;
    //    }
    //}

    //internal class Worker
    //{
    //    private Queue<string> palabras;
    //    private ConcurrentQueue<string> palabrasConsonantes;
    //    private int indiceDesde, indiceHasta;

    //    internal Worker(Queue<string> palabras, ConcurrentQueue<string> palabrasConsonantes, int indiceDesde, int indiceHasta)
    //    {
    //        this.palabras = palabras;
    //        this.palabrasConsonantes = palabrasConsonantes;
    //        this.indiceDesde = indiceDesde;
    //        this.indiceHasta = indiceHasta;
    //    }

    //    internal void Calcular()
    //    {
    //        for (int i = this.indiceDesde; i <= this.indiceHasta; i++)
    //        {
    //            if (TieneMasConsonantes(this.palabras.ToArray()[i]))
    //            {
    //                palabrasConsonantes.Enqueue(this.palabras.ToArray()[i]);
    //            }
    //        }
    //    }

    //    private bool TieneMasConsonantes(string palabra)
    //    {
    //        int consonantes = 0;
    //        int vocales = 0;
    //        foreach (char c in palabra.ToLower())
    //        {
    //            if (char.IsLetter(c))
    //            {
    //                if ("aeiou".Contains(c))
    //                    vocales++;
    //                else
    //                    consonantes++;
    //            }
    //        }
    //        return consonantes > vocales;
    //    }
    //}

    //internal class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Queue<string> palabras = new Queue<string>();
    //        palabras.Enqueue("Hola");
    //        palabras.Enqueue("OpenAI");
    //        palabras.Enqueue("Amigo");
    //        palabras.Enqueue("Test");
    //        palabras.Enqueue("Consonantes");
    //        palabras.Enqueue("Vocales");
    //        palabras.Enqueue("Programación");
    //        palabras.Enqueue("Jose");
    //        palabras.Enqueue("aveces");
    //        palabras.Enqueue("ahi");
    //        palabras.Enqueue("main");
    //        palabras.Enqueue("aun");
    //        palabras.Enqueue("sacerdote");

    //        int maximoHilos = 20;
    //        MostrarLinea(Console.Out, "Num Hilos", "Ticks", "Resultado");
    //        for (int numeroHilos = 1; numeroHilos <= maximoHilos; numeroHilos++)
    //        {
    //            Master master = new Master(palabras, numeroHilos);
    //            DateTime antes = DateTime.Now;
    //            ConcurrentQueue<string> palabrasConsonantes = master.Calcular();
    //            DateTime despues = DateTime.Now;
    //            MostrarLinea(Console.Out, numeroHilos, (despues - antes).Ticks, palabrasConsonantes.Count);
    //        }
    //    }

    //    static void MostrarLinea(TextWriter stream, string numHilosCabecera, string ticksCabecera, string resultadoCabecera)
    //    {
    //        stream.WriteLine("{0,-10} {1,-10} {2,-10}", numHilosCabecera, ticksCabecera, resultadoCabecera);
    //    }

    //    static void MostrarLinea(TextWriter stream, int numHilos, long ticks, int resultado)
    //    {
    //        stream.WriteLine("{0,-10} {1,-10:N0} {2,-10:N0}", numHilos, ticks, resultado);
    //    }
    //}

    //public class Master
    //{
    //    private Queue<string> palabras;
    //    private int numeroHilos;

    //    public Master(Queue<string> palabras, int numeroHilos)
    //    {
    //        this.palabras = palabras;
    //        this.numeroHilos = numeroHilos;
    //    }

    //    public ConcurrentQueue<string> Calcular()
    //    {
    //        ConcurrentQueue<string> palabrasConsonantes = new ConcurrentQueue<string>();

    //        // Calculamos cuántas palabras procesará cada hilo
    //        int palabrasPorHilo = palabras.Count / numeroHilos;

    //        // Creamos los workers
    //        Worker[] workers = new Worker[numeroHilos];
    //        for (int i = 0; i < numeroHilos; i++)
    //        {
    //            // Para cada hilo, se le asigna su porción de palabras para procesar
    //            Queue<string> palabrasParaHilo = new Queue<string>(palabras.Skip(i * palabrasPorHilo).Take(palabrasPorHilo));
    //            workers[i] = new Worker(palabrasParaHilo, palabrasConsonantes);
    //        }

    //        // Iniciamos los hilos
    //        Thread[] hilos = new Thread[numeroHilos];
    //        for (int i = 0; i < numeroHilos; i++)
    //        {
    //            hilos[i] = new Thread(workers[i].Calcular);
    //            hilos[i].Start();
    //        }

    //        // Esperamos a que acaben para continuar
    //        foreach (Thread thread in hilos)
    //            thread.Join();

    //        return palabrasConsonantes;
    //    }
    //}

    //internal class Worker
    //{
    //    private Queue<string> palabras;
    //    private ConcurrentQueue<string> palabrasConsonantes;

    //    internal Worker(Queue<string> palabras, ConcurrentQueue<string> palabrasConsonantes)
    //    {
    //        this.palabras = palabras;
    //        this.palabrasConsonantes = palabrasConsonantes;
    //    }

    //    internal void Calcular()
    //    {
    //        string palabra;
    //        while (palabras.TryDequeue(out palabra))
    //        {
    //            if (TieneMasConsonantes(palabra))
    //            {
    //                palabrasConsonantes.Enqueue(palabra);
    //            }
    //        }
    //    }

    //    private bool TieneMasConsonantes(string palabra)
    //    {
    //        int consonantes = 0;
    //        int vocales = 0;
    //        foreach (char c in palabra.ToLower())
    //        {
    //            if (char.IsLetter(c))
    //            {
    //                if ("aeiou".Contains(c))
    //                    vocales++;
    //                else
    //                    consonantes++;
    //            }
    //        }
    //        return consonantes > vocales;
    //    }
    //}

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Cola<string> cola = new Cola<string>();
    //        cola.Añadir("Hola");
    //        cola.Añadir("OpenAI");
    //        cola.Añadir("Amigo");
    //        cola.Añadir("Test");
    //        cola.Añadir("Consonantes");
    //        cola.Añadir("Vocales");
    //        cola.Añadir("Programación");
    //        cola.Añadir("Jose");
    //        cola.Añadir("aveces");
    //        cola.Añadir("ahi");
    //        cola.Añadir("main");
    //        cola.Añadir("aun");
    //        cola.Añadir("sacerdote");

    //        int maximoHilos = 20;
    //        MostrarLinea(Console.Out, "Num Hilos", "Ticks", "Resultado");
    //        for (int numeroHilos = 1; numeroHilos <= maximoHilos; numeroHilos++)
    //        {
    //            Master<string> master = new Master<string>(ref cola, numeroHilos);
    //            DateTime antes = DateTime.Now;
    //            ConcurrentQueue<string> palabrasConsonantes = master.Calcular();
    //            foreach (var i in palabrasConsonantes)
    //            {
    //                Console.WriteLine(i);
    //            }
    //            DateTime despues = DateTime.Now;
    //            MostrarLinea(Console.Out, numeroHilos, (despues - antes).Ticks, palabrasConsonantes.Count);

    //            // Entre ejecuciones, limpiamos y esperamos.
    //            GC.Collect();
    //            GC.WaitForFullGCComplete();
    //        }
    //    }

    //    static void MostrarLinea(System.IO.TextWriter stream, string numHilosCabecera, string ticksCabecera, string resultadoCabecera)
    //    {
    //        stream.WriteLine("{0};{1};{2}", numHilosCabecera, ticksCabecera, resultadoCabecera);
    //    }

    //    static void MostrarLinea(System.IO.TextWriter stream, int numHilos, long ticks, int resultado)
    //    {
    //        stream.WriteLine("{0};{1:N0};{2:N0}", numHilos, ticks, resultado);
    //    }
    //}

    //public class Master<T>
    //{
    //    private Cola<T> cola;
    //    private int numeroHilos;

    //    public Master(ref Cola<T> cola, int numeroHilos)
    //    {
    //        this.cola = cola;
    //        this.numeroHilos = numeroHilos;
    //    }

    //    public ConcurrentQueue<T> Calcular()
    //    {
    //        ConcurrentQueue<T> palabrasConsonantes = new ConcurrentQueue<T>();

    //        // Creamos los workers
    //        Worker<T>[] workers = new Worker<T>[this.numeroHilos];
    //        int numElementosPorHilo = this.cola.Count() / numeroHilos;
    //        for (int i = 0; i < this.numeroHilos; i++)
    //        {
    //            int indiceDesde = i * numElementosPorHilo;
    //            int indiceHasta = (i + 1) * numElementosPorHilo - 1;
    //            if (i == this.numeroHilos - 1)
    //            {
    //                indiceHasta = this.cola.Count() - 1;
    //            }
    //            workers[i] = new Worker<T>(ref this.cola, palabrasConsonantes, indiceDesde, indiceHasta);
    //        }

    //        // Iniciamos los hilos
    //        Thread[] hilos = new Thread[workers.Length];
    //        for (int i = 0; i < workers.Length; i++)
    //        {
    //            hilos[i] = new Thread(workers[i].Calcular);
    //            hilos[i].Name = "Worker número: " + (i + 1);
    //            hilos[i].Priority = ThreadPriority.Normal;
    //            hilos[i].Start();
    //        }

    //        // Esperamos a que acaben para continuar
    //        foreach (Thread thread in hilos)
    //            thread.Join();

    //        return palabrasConsonantes;
    //    }
    //}

    //internal class Worker<T>
    //{
    //    private Cola<T> cola;
    //    private ConcurrentQueue<T> palabrasConsonantes;
    //    private int indiceDesde, indiceHasta;
    //    private static readonly object obj = new();

    //    internal Worker(ref Cola<T> cola, ConcurrentQueue<T> palabrasConsonantes, int indiceDesde, int indiceHasta)
    //    {
    //        this.cola = cola;
    //        this.palabrasConsonantes = palabrasConsonantes;
    //        this.indiceDesde = indiceDesde;
    //        this.indiceHasta = indiceHasta;
    //    }

    //    internal void Calcular()
    //    {
    //        while (cola.Count()>0)
    //        {
    //            T palabra = cola.Extraer();
    //            if (TieneMasConsonantes(palabra))
    //            {
    //                palabrasConsonantes.Enqueue(palabra);
    //            }
    //        }
    //    }

    //    private bool TieneMasConsonantes(T palabra)
    //    {
    //        int consonantes = 0;
    //        int vocales = 0;
    //        foreach (char c in palabra.ToString().ToLower())
    //        {
    //            if (char.IsLetter(c))
    //            {
    //                if ("aeiou".Contains(c))
    //                    vocales++;
    //                else
    //                    consonantes++;
    //            }
    //        }
    //        return consonantes > vocales;
    //    }
    //}

    internal class Program
    {
        static void Main(string[] args)
        {
            Queue<string> palabras = new Queue<string>();
            palabras.Enqueue("Hola");
            palabras.Enqueue("OpenAI");
            palabras.Enqueue("Amigo");
            palabras.Enqueue("Test");
            palabras.Enqueue("Consonantes");
            palabras.Enqueue("Vocales");
            palabras.Enqueue("Programación");
            palabras.Enqueue("Jose");
            palabras.Enqueue("aveces");
            palabras.Enqueue("ahi");
            palabras.Enqueue("main");
            palabras.Enqueue("aun");
            palabras.Enqueue("sacerdote");

            int maximoHilos = 20;
            MostrarLinea(Console.Out, "Num Hilos", "Ticks", "Resultado");
            for (int numeroHilos = 1; numeroHilos <= maximoHilos; numeroHilos++)
            {
                Master master = new Master(palabras, numeroHilos);
                DateTime antes = DateTime.Now;
                Cola<string> palabrasConsonantes = master.Calcular();

                Console.WriteLine(palabrasConsonantes.ToString());

                DateTime despues = DateTime.Now;
                MostrarLinea(Console.Out, numeroHilos, (despues - antes).Ticks, palabrasConsonantes.Count());

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

    public class Master
    {
        private Queue<string> palabras;
        private int numeroHilos;

        public Master(Queue<string> palabras, int numeroHilos)
        {
            this.palabras = palabras;
            this.numeroHilos = numeroHilos;
        }

        public Cola<string> Calcular()
        {
            Cola<string> palabrasConsonantes = new Cola<string>();

            // Creamos los workers
            Worker[] workers = new Worker[this.numeroHilos];
            int numElementosPorHilo = this.palabras.ToArray().Length / numeroHilos;
            for (int i = 0; i < this.numeroHilos; i++)
            {
                int indiceDesde = i * numElementosPorHilo;
                int indiceHasta = (i + 1) * numElementosPorHilo - 1;
                if (i == this.numeroHilos - 1)
                {
                    indiceHasta = this.palabras.ToArray().Length - 1;
                }
                workers[i] = new Worker(this.palabras, palabrasConsonantes, indiceDesde, indiceHasta);
            }
            // Iniciamos los hilos
            Thread[] hilos = new Thread[workers.Length];
            for (int i = 0; i < workers.Length; i++)
            {
                hilos[i] = new Thread(workers[i].Calcular);
                hilos[i].Name = "Worker número: " + (i + 1);
                hilos[i].Priority = ThreadPriority.Normal;
                hilos[i].Start();
            }

            // Esperamos a que acaben para continuar
            foreach (Thread thread in hilos)
                thread.Join();

            return palabrasConsonantes;
        }
    }

    internal class Worker
    {
        private Queue<string> palabras;
        private Cola<string> palabrasConsonantes;
        private int indiceDesde, indiceHasta;
        static readonly object obj = new();

        internal Worker(Queue<string> palabras, Cola<string> palabrasConsonantes, int indiceDesde, int indiceHasta)
        {
            this.palabras = palabras;
            this.palabrasConsonantes = palabrasConsonantes;
            this.indiceDesde = indiceDesde;
            this.indiceHasta = indiceHasta;
        }

        internal void Calcular()
        {
            for (int i = this.indiceDesde; i <= this.indiceHasta; i++)
            {
                if (TieneMasConsonantes(this.palabras.ToArray()[i]))
                {
                    palabrasConsonantes.Añadir(this.palabras.ToArray()[i]);
                }

            }
        }

        private bool TieneMasConsonantes(string palabra)
        {
            int consonantes = 0;
            int vocales = 0;
            foreach (char c in palabra.ToLower())
            {
                if (char.IsLetter(c))
                {
                    if ("aeiou".Contains(c))
                        vocales++;
                    else
                        consonantes++;
                }
            }
            return consonantes > vocales;
        }
    }

}