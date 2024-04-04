using System;

using System.Diagnostics;

namespace Procesos
{
    class Program
    {
        static void Main(string[] args)
        {
            //Obtenemos los procesos del sistema -> Process[]
            var procesos = Process.GetProcesses();
            //Para cada proceso
            foreach (var proceso in procesos)
            {
                Console.WriteLine("PID: {0}\tNombre: {1}\tMemoria: {2:N} MB", proceso.Id, proceso.ProcessName, proceso.VirtualMemorySize64 / 1024.0 / 1024);

                //Cada hilo del proceso
                ProcessThreadCollection hilosProceso = proceso.Threads;
                foreach (ProcessThread hilo in hilosProceso)
                    Console.WriteLine("\t HiloID: {0}\tPrioridad: {1}\tEstado: {2}", hilo.Id, hilo.CurrentPriority, hilo.ThreadState);
            }
        }
    }
}
