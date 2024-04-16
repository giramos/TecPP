using System;
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
            sumatorio.Calcular();
            Console.WriteLine("Valor tras realizar la operación directamente : {0}.", sumatorio.Valor);

            SumatorioInterLocked sumatorioInter = new SumatorioInterLocked(valor, numHilos);
            sumatorioInter.Calcular();
            Console.WriteLine("Valor tras realizar la operación con InterLocked : {0}.", sumatorioInter.Valor);

            // EJERCICIO: Implementar una versión con lock (usando object)
            // Toma tiempos de la versión de lock y de la de Interlock, imprímelos por pantalla.
            // Cómo realizar tomas de tiempo lo tenéis en la práctica (y entrega) anterior.

            //EJERCICIO: Crear y entender un ejemplo para cada método:
            //Increment, Add, Exchange y CompareExchange.

        }
    }
}
