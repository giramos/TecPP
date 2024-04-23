using System;
using System.Threading;

namespace Interlocked_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Valores enteros para demostración:
            int num1 = 1111;
            int num2 = 3553;

            // Valores iniciales de los dos números enteros:
            Console.WriteLine("\nValor de `num1`: {0}", num1.ToString());
            Console.WriteLine("\nValor de `num2`: {0}", num2.ToString());

            // Uso de `Interlocked.Decrement`:
            // Equivalencia: num1 = num1 - 1;
            Interlocked.Decrement(ref num1);
            

            Console.WriteLine("\nNuevo valor de `num1` después de Decrement: {0}", num1.ToString());

            // Uso de `Interlocked.Increment`:
            // Equivalencia: num2 = num2 + 1;
            Interlocked.Increment(ref num2);

            Console.WriteLine("\nNuevo valor de `num2` después de Increment: {0}", num2.ToString());

            // Uso de `Interlocked.Add`:
            // Equivalencia: num1 = num1 + num2;
            Interlocked.Add(ref num1, num2);

            // Valores actualizados:
            Console.WriteLine("\nValor de `num1` después de Add: {0}", num1.ToString());
            Console.WriteLine("Valor de `num2` después de Add: {0}", num2.ToString());

            // Uso de `Interlocked.Exchange`:
            // Equivalencia: num2 = num1;
            Interlocked.Exchange(ref num2, num1);

            Console.WriteLine("\nValor de `num1` después de Exchange: {0}", num1.ToString());
            Console.WriteLine("Valor de `num2` después de Exchange: {0}", num2.ToString());

            // Uso de `Interlocked.CompareExchange`:
            // Equivalencia: if (num1 == num2) num1 = 3;
            Interlocked.CompareExchange(ref num1, 3, num2);

            // Valores actualizados:
            Console.WriteLine("\nValor de `num1` después de CompareExchange: {0}", num1.ToString());
            //Console.WriteLine("\nValor de `num1` después de CompareExchange: {0}", valor);
            Console.WriteLine("Valor de `num2` después de CompareExchange: {0}\n", num2.ToString());
        }
    }
}
