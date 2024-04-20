using System;
using System.Collections.Generic;
using System.Threading;

namespace _04_MasterWorker_NumerosCosecutivos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int maximoHilos = 30;
            int[] vector = { 1, 1, 2, 2, 3, 2, 2, 3, 4 };

            TimeSpan tiempoMasRapido = TimeSpan.MaxValue;
            int hilosMasRapido = 0;
            Dictionary<string, int> resultadoMasRapido = null;

            MostrarLinea(Console.Out, "Num Hilos", "Ticks", "Resultado");
            for (int numeroHilos = 1; numeroHilos <= maximoHilos; numeroHilos++)
            {
                // Crear el Master con el vector y el número de hilos actual
                Master master = new Master(vector, numeroHilos);

                // Registrar el tiempo de inicio
                DateTime antes = DateTime.Now;

                // Ejecutar el programa y obtener el resultado
                Dictionary<string, int> resultado = master.Calcular();

                // Registrar el tiempo de fin
                DateTime despues = DateTime.Now;

                // Calcular el tiempo de ejecución
                TimeSpan tiempoEjecucion = despues - antes;

                // Mostrar la línea de resultados
                MostrarLinea(Console.Out, numeroHilos, tiempoEjecucion.Ticks, resultado.Count);

                // Actualizar el resultado más rápido si el tiempo actual es menor
                if (tiempoEjecucion < tiempoMasRapido)
                {
                    tiempoMasRapido = tiempoEjecucion;
                    hilosMasRapido = numeroHilos;
                    resultadoMasRapido = resultado;
                }

                // Entre ejecuciones, limpiamos y esperamos.
                GC.Collect();
                GC.WaitForFullGCComplete();

                // Mostrar los resultados de cada hilo
                Console.WriteLine($"Resultados del hilo para {numeroHilos} hilos:");
                foreach (var kvp in resultado)
                {
                    Console.WriteLine($"Pareja: {kvp.Key}, Repeticiones: {kvp.Value}");
                }
            }

            // Imprimir el resultado más rápido junto con el número de hilos empleados
            Console.WriteLine($"La ejecución más rápida fue con {hilosMasRapido} hilos, que tardó {tiempoMasRapido.TotalMilliseconds} milisegundos.");
            Console.WriteLine("Resultado:");
            foreach (var kvp in resultadoMasRapido)
            {
                Console.WriteLine($"Pareja: {kvp.Key}, Repeticiones: {kvp.Value}");
            }
        }

        // Método para mostrar una línea de resultados en la consola
        static void MostrarLinea(System.IO.TextWriter stream, string numHilosCabecera, string ticksCabecera, string resultadoCabecera)
        {
            stream.WriteLine("{0};{1};{2}", numHilosCabecera, ticksCabecera, resultadoCabecera);
        }

        // Método para mostrar una línea de resultados en la consola
        static void MostrarLinea(System.IO.TextWriter stream, int numHilos, long ticks, int resultado)
        {
            stream.WriteLine("{0};{1:N0};{2:N0}", numHilos, ticks, resultado);
        }
    }

    // Clase Master que coordina el trabajo de los Workers
    public class Master
    {
        private int[] vector;
        private int numeroHilos;

        public Master(int[] vector, int numeroHilos)
        {
            if (numeroHilos < 1 || numeroHilos > 30) // Limitamos el número máximo de hilos a 30
                throw new ArgumentException("El número de hilos debe ser mayor que cero y menor o igual a 30");
            this.vector = vector;
            this.numeroHilos = numeroHilos;
        }

        // Método para calcular las parejas consecutivas y contarlas
        public Dictionary<string, int> Calcular()
        {
            Dictionary<string, int> parejasConsecutivas = new Dictionary<string, int>();
            List<string> listaComun = new List<string>();

            Worker[] workers = new Worker[this.numeroHilos];
            int numElementosPorHilo = this.vector.Length / numeroHilos;
            int indiceInicio = 0;

            // Crear y ejecutar los Workers
            for (int i = 0; i < this.numeroHilos; i++)
            {
                int indiceFin = (i == this.numeroHilos - 1) ? this.vector.Length : indiceInicio + numElementosPorHilo;
                workers[i] = new Worker(this.vector, listaComun, indiceInicio, indiceFin);
                indiceInicio = indiceFin;
            }

            // Iniciar los hilos
            Thread[] hilos = new Thread[workers.Length];
            for (int i = 0; i < workers.Length; i++)
            {
                hilos[i] = new Thread(workers[i].Calcular);
                hilos[i].Start();
            }

            // Esperar a que acaben los hilos
            foreach (Thread thread in hilos)
                thread.Join();

            

            // Contar las parejas consecutivas
            foreach (string pareja in listaComun)
            {
                if (parejasConsecutivas.ContainsKey(pareja))
                {
                    parejasConsecutivas[pareja]++;
                }
                else
                {
                    parejasConsecutivas[pareja] = 1;
                }
            }

            return parejasConsecutivas;
        }
    }

    // Clase Worker que realiza el procesamiento de la lista de números
    internal class Worker
    {
        private int[] vector;
        private List<string> listaComun;
        private int indiceInicio, indiceFin;
        private static readonly object lockObject = new object(); // Objeto de bloqueo para sincronización

        internal Worker(int[] vector, List<string> listaComun, int indiceInicio, int indiceFin)
        {
            this.vector = vector;
            this.listaComun = listaComun;
            this.indiceInicio = indiceInicio;
            this.indiceFin = indiceFin;
        }

        // Método para encontrar parejas consecutivas
        internal void Calcular()
        {
            for (int i = this.indiceInicio; i < this.indiceFin - 1; i++)
            {
                if (vector[i] + 1 == vector[i + 1])
                {
                    string pareja = vector[i].ToString() + "-" + vector[i + 1].ToString();
                    lock (lockObject) // Bloqueo para asegurar el acceso seguro a la lista compartida
                    {
                        listaComun.Add(pareja);
                    }
                }
            }
        }
    }

}
