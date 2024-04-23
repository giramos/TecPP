using System;
using System.IO;
using System.Threading;
namespace _03Interlocked
{
    class Program
    {

        /// <summary>
        /// Una alternativa más eficiente a lock es el uso de la clase Interlocked (System.Threading)
        /// Realiza asignaciones de forma primitiva.
        /// Métodos más utilizados: Increment, Decrement, CompareExchange, Add.
        /// https://learn.microsoft.com/en-us/dotnet/api/system.threading.interlocked?view=net-7.0
        /// Observa bien con qué tipos pueden utilizarse.
        /// </summary>
        static void Main()
        {
            const long valor = 100000000;
            const int numHilos = 10;
            Console.WriteLine("Valor esperado como resultado: 0.");
            Sumatorio sumatorio = new Sumatorio(valor, numHilos);
            DateTime antes = DateTime.Now;
            sumatorio.Calcular();
            DateTime despues = DateTime.Now;
            Console.WriteLine("Valor tras realizar la operación directamente : {0}.", sumatorio.Valor);
            MostrarLinea(Console.Out, (despues - antes).Ticks, sumatorio.Valor);

            SumatorioInterLocked sumatorioInter = new SumatorioInterLocked(valor, numHilos);
            antes = DateTime.Now;
            sumatorioInter.Calcular();
            despues = DateTime.Now;
            Console.WriteLine("Valor tras realizar la operación con InterLocked : {0}.", sumatorioInter.Valor);
            MostrarLinea(Console.Out, (despues - antes).Ticks, sumatorioInter.Valor);

            // EJERCICIO: Implementar una versión con lock (usando object)
            // Toma tiempos de la versión de lock y de la de Interlock, imprímelos por pantalla.
            // Cómo realizar tomas de tiempo lo tenéis en la práctica (y entrega) anterior.

            SumatorioLock sumatorioLock = new SumatorioLock(valor, numHilos);
            antes = DateTime.Now;
            sumatorioLock.Calcular();
            despues = DateTime.Now;
            Console.WriteLine("Valor tras realizar la operación con InterLocked : {0}.", sumatorioLock.Valor);
            MostrarLinea(Console.Out, (despues - antes).Ticks, sumatorioLock.Valor);

            //EJERCICIO: Crear y entender un ejemplo para cada método:
            //Increment, Add, Exchange y CompareExchange.

        }

        private static void MostrarLinea(TextWriter stream, long ticks, long valor)
        {
            stream.WriteLine("Tiempo de ejecución (ticks): {0:N0}", ticks);
            stream.WriteLine("Resultado del sumatorio: {0}", valor);
            stream.WriteLine();
        }
    }
}
