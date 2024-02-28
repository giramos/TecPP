using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Lab06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Currificar");
            int[] lista = { 2, 3, 5 };
            Console.WriteLine($"Es 10 divisible por algun elemento de la lista? {ContainsDivisorCurry(lista)(10)}");
            Console.WriteLine($"Es 11 divisible por algun elemento de la lista? {ContainsDivisorCurry(lista)(11)}");

            Console.WriteLine("Cierre");
            // * Modeling loops by means of closures
            int i = 0; // The i variable is used in both closures (condition and body)
            RepeatUntilLoop(() => i < 10, () => { Console.Write(i + " "); i++; });
            Console.WriteLine();

            Console.WriteLine("While");
            int[,] a = new int[3, 4] {
                            {0, 1, 2, 3} ,
                            {4, 5, 6, 7} ,
                            {8, 9, 10, 11} ,
                        };
            int row = 0;
            int column = 0;
            while (row < 3)
            {
                while (column < 4)
                {
                    Console.Write("{0} ", a[row, column]);
                    column++;
                }
                Console.WriteLine();
                column = 0;
                row++;
            }
            Console.WriteLine("WhileCierre");
            int row1 = 0;
            int column1 = 0;
            WhileLoop(() => row1 < 3, () =>
            {
                WhileLoop(() => column1 < 4, () =>
                {
                    Console.Write("{0} ", a[row1, column1]);
                    column1++;
                });
                Console.WriteLine();
                column1 = 0;
                row1++;
            });


        }

        //        1. Curificar este método
        //```csharp
        static bool ContainsDivisor(IEnumerable<int> nums, int n)
        {
            foreach (var e in nums)
            {
                if (n % e == 0)
                    return true;
            }
            return false;
        }
        ////```
        //para implementar `static Predicate<int> ContainsDivisor(IEnumerable<int> e)`. 

        static Predicate<int> ContainsDivisorCurry(IEnumerable<int> nums)
        {
            return (n) =>
            {
                foreach (var e in nums)
                {
                    if (n % e == 0)
                        return true;
                }
                return false;

            };
        }

        static void RepeatUntilLoop(Func<bool> condicion, Action cuerpo)
        {
            cuerpo();
            if (condicion()) { RepeatUntilLoop(condicion, cuerpo); }
        }

        static void WhileLoop(Func<bool> condition, Action body)
        {
            if (condition())
            {
                body();
                WhileLoop(condition, body); // recursion to iterate
            }
        }
    }
}
