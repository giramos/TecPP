using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Generadores
{
    public static class Program
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action, int? maximoElementos = null)
        {
            int contador = 0;
            foreach (T item in enumerable)
            {
                if (maximoElementos.HasValue && maximoElementos.Value < contador++)
                    break;
                action(item);
            }
        }

        static void Main(string[] args)
        {

            const int numeroDeTerminos = 10;
            
            // 10 primeros términos de la sucesión de Fibonacci

            int i = 1;

            foreach (int valor in Funciones.FibonacciInfinito() )
            {
                Console.WriteLine("Término {0}: {1}.", i, valor);
                if (i++ == numeroDeTerminos)
                    break;
            }

            Console.WriteLine();

            // Empleando iterador (IEnumerator) 
            
            var iterador = Funciones.FibonacciInfinito().GetEnumerator();
            iterador.MoveNext();

            Console.WriteLine("Término 1: {0}.", iterador.Current);
            // iterador.Reset();  <- ¡OJO! El reset no está soportado.

            Console.WriteLine();

            // * Especificando el número de términos que necesitamos
            i = 1;
            foreach (int valor in Funciones.FibonacciFinito(numeroDeTerminos))
                Console.WriteLine("Término {0}: {1}.", i++, valor);

            
            LimpiarPantalla();

            // eager vs lazy.

            
            const int desde = 1000, cantidad = 10000000, mostrarNElementos = 10;

            //Estricta (eager): Para trabajar con una colección tenemos que tener la colección COMPLETA.
            
            var chrono = new Stopwatch();
            chrono.Start();

            var imparesEstricto = Funciones.ImparesGeneradorEstricto(desde, cantidad);
            Console.Write("{0} elementos tras el término {1} (estricta/eager):\n\t", mostrarNElementos, desde);

            //Una vez obtenida TODA la colección, solamente consumimos 10.            
            imparesEstricto.ForEach( item => Console.Write("{0} ", item) , mostrarNElementos);


            Console.WriteLine();
            chrono.Stop();
            long ticksEstricto = chrono.ElapsedTicks;
            chrono.Reset();
            chrono.Start();



            //Perezosa (lazy): Generamos la colección (potencialmente infinita) bajo demanda.
            //Fíjate en el uso del Skip y el Take en la función NumerosImparesLazy.

            IEnumerable<int> imparesLazy = Funciones.NumerosImparesLazy(desde, cantidad);
            
            Console.Write("{0} elementos tras el término {1} (perezosa/lazy):\n\t", mostrarNElementos, desde);

            imparesLazy.ForEach( item => Console.Write("{0} ", item) , mostrarNElementos);
            
            Console.WriteLine();
            chrono.Stop();
            long ticksPerezosa = chrono.ElapsedTicks;

            Console.WriteLine("Tiempo consumido por la versión estricta: {0:N} ticks.", ticksEstricto);
            Console.WriteLine("Tiempo consumido pr la versión perezosa: {0:N} ticks.", ticksPerezosa);
            Console.WriteLine("La versión perezosa es {0:N} veces más rápida.", (double)ticksEstricto / ticksPerezosa - 1);


            LimpiarPantalla();

            //Uso de Memorización.

            const int nTermino = 40;
            int resultado;

            var crono = new Stopwatch();
            crono.Start();
            resultado = FibonacciStandard.Fibonacci(nTermino);
            crono.Stop();
            long ticksPrimeraSinMem = crono.ElapsedTicks;
            Console.WriteLine("Sin Memorización, 1ª llamada: {0:N} ticks. Resultado: {1}.", ticksPrimeraSinMem, resultado);

            crono.Restart();
            resultado = FibonacciStandard.Fibonacci(nTermino);
            crono.Stop();
            long ticksSegundaSinMem = crono.ElapsedTicks;
            Console.WriteLine("Sin Memorización, 2ª llamada: {0:N} ticks. Resultado: {1}.", ticksSegundaSinMem, resultado);

            crono.Restart();
            resultado = FibonacciMemoization.Fibonacci(nTermino);
            crono.Stop();
            long ticksPrimeraConMem = crono.ElapsedTicks;
            Console.WriteLine("Con Memorización, 1ª llamada: {0:N} ticks. Resultado: {1}.", ticksPrimeraConMem, resultado);

            crono.Restart();
            resultado = FibonacciMemoization.Fibonacci(nTermino);
            crono.Stop();
            long ticksSegundaConMem = crono.ElapsedTicks;
            Console.WriteLine("Con Memorización, 2ª llamada: {0:N} ticks. Resultado: {1}.", ticksSegundaConMem, resultado);

            //C# no permite el paso perezoso de parámetros.
            //Como trabajo para casa:
            //En los ejemplos del campus debéis analizar, modificar y comprender
            //El proyecto lazy.simulated de la solución continuations

        }

        static void LimpiarPantalla()
        {
            Console.WriteLine("\nPulse una tecla para continuar al siguiente ejemplo...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}

