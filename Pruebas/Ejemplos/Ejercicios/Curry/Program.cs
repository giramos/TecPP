using System;
using System.Collections.Generic;

namespace Curry
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Curry Metodo int");

            Func<int, int>[] funciones =
            {
               n => n + 1,
            n => n * 2,
            n => n - 3
            };
            var res = MetodoIntCurry(10);
            var res1 = res(funciones);

            Console.WriteLine(res1(15));

            Console.WriteLine();

            Console.WriteLine("Curry Contine divisor");
            var lista = new[] { 2, 3, 5 };
            var res2 = ContainsDivisorCurry(lista);
            Console.WriteLine(res2(10));//true
            Console.WriteLine(res2(11));//false
        }

        // Metodo extensor de INT32 que recibe un array de func<int,int> y una variable int maximo. Devuelve
        //       La función deberá devolver el resultado de ir aplicando cada una de las funciones al resultado
        //de haber aplicado la anterior(a la primera función se le pasará el propio número). Si durante el
        //proceso se supera el valor de la variable “máximo”, se devolverá el resultado acumulado hasta
        public static int MetodoInt(Int32 entero, Func<int, int>[] funciones, int maximo)
        {
            int acumulado = funciones[0](entero);
            for (int i = 1; i < funciones.Length; i++)
            {
                acumulado = funciones[i](acumulado);
                if (acumulado > maximo)
                {
                    break; // Terminar el bucle si el acumulado supera el máximo
                }
            }
            return acumulado;
        }

        public static Func<Func<int, int>[], Func<int, int>> MetodoIntCurry(Int32 entero)
        {
            return (funciones) => (maximo) =>
            {
                int acumulado = funciones[0](entero);
                for (int i = 1; i < funciones.Length; i++)
                {
                    acumulado = funciones[i](acumulado);
                    if (acumulado > maximo)
                    {
                        break; // Terminar el bucle si el acumulado supera el máximo
                    }
                }
                return acumulado;
            };
        }

        public static bool ContainsDivisor(IEnumerable<int> nums, int n)
        {
            foreach (var e in nums)
            {
                if (n % e == 0)
                    return true;
            }
            return false;
        }

        public static Func<int, bool> ContainsDivisorCurry(IEnumerable<int> nums)
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

        //Ejercicio 1..

        //Implementar de forma currificada el método Buscar entregado en la sesión anterior.
        //Demostrar el uso de su invocación y de la aplicación parcial.
        public static T Buscar<T>(IEnumerable<T> col, Predicate<T> cond)
        {
            foreach (var i in col)
            {
                if (cond(i))
                {
                    return i;
                }
            }
            return default(T);
        }
        public static Func<Predicate<T>, T> BuscarCurry<T>(IEnumerable<T> col)
        {
            return (cond) =>
            {
                foreach (var i in col)
                {
                    if (cond(i))
                    {
                        return i;
                    }
                }
                return default(T);
            };

        }

        //Ejercicio 2.

        // Si - > 5 / 3 = 1 ; Resto = 2

        // Entonces -> 3 * 1 + 2 = 5;

        //Currifíquese la función y compruébese mediante el uso de la aplicación parcial el siguiente ejemplo:

        // Se sabe que la división:  20 / 6 = 3. Se desconoce el valor del resto.
        // Partiendo del valor 0, e incrementalmente, obténgase el resto.

        public static bool ComprobarDivision(int divisor, int dividendo, int cociente, int resto)
        {
            return dividendo == cociente * divisor + resto;
        }
        public static Func<int, Func<int, Func<int, bool>>> ComprobarDivisionCurry(int divisor)
        {
            return (dividendo) => (cociente) => (resto) =>
            {
                return dividendo == cociente * divisor + resto;
            };

        }
    }
}
