using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Laboratorio05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Delegados");

            Operacion asinDelegado = Asinh;
            Operacion acosDelegado = Acosh;
            Operacion atanDelegado = Atanh;
            double num = 45;

            Console.WriteLine($"Asin de num:{num} = {Asinh(num)}");
            Console.WriteLine($"Acos de num:{num} = {Acosh(num)}");
            Console.WriteLine($"Atan de num:{num} = {Atanh(num)}");

            Console.WriteLine("");

            Console.WriteLine("Funciones lambda con parametros");

            Func<double, double> funAsin = (double x) => Math.Log(x + Math.Sqrt(Math.Pow(x, 2) + 1));
            Func<double, double> funAcos = (double x) => Math.Log(x + Math.Sqrt(Math.Pow(x, 2) - 1));
            Func<double, double> funAtan = (double x) => (Math.Log(1 + x) - Math.Log(1 - x)) / 2;

            Console.WriteLine($"Funcion asin: {funAsin(num)}");
            Console.WriteLine($"Funcion acos: {funAcos(num)}");
            Console.WriteLine($"Funcion atan: {funAtan(num)}");

            Console.WriteLine("");

            //Console.WriteLine("Funciones lambda sin parametros");

            //var fAsin = () => Math.Log(num + Math.Sqrt(Math.Pow(num, 2) + 1));
            //var fAcos = () => Math.Log(num + Math.Sqrt(Math.Pow(num, 2) - 1));
            //var fAtan = () => (Math.Log(1 + num) - Math.Log(1 - num)) / 2;

            //Console.WriteLine($"Funcion asin: {fAsin(num)}");
            //Console.WriteLine($"Funcion acos: {fAcos(num)}");
            //Console.WriteLine($"Funcion atan: {fAtan(num)}");

            Console.WriteLine("Aplicar una accion");
            var e = Enumerable.Range(0, 10);
            ApplyAction(e, x => Console.Write(x + " "));
            Console.WriteLine("");
            Console.WriteLine("Aplicar una accion (multiplicar cada elemento por si mismo)");
            ApplyAction(e, x => Console.Write(x * x + " "));

            Console.WriteLine("");
            Console.WriteLine("Aplicar numeros pares a la lista de enumerables");
            var isEven = Count(e, x => x % 2 == 0);
            Console.WriteLine($"¿Cuantos pares tiene la lista? {isEven}");
        }

        public delegate double Operacion(double x);

        public static double Asinh(double x)
        {
            return Math.Log(x + Math.Sqrt(Math.Pow(x,2)+1));
        }

        public static double Acosh(double x)
        {
            return Math.Log(x + Math.Sqrt(Math.Pow(x, 2) - 1));
        }

        public static double Atanh(double x)
        {
            return (Math.Log(1 + x) - Math.Log(1 - x)) / 2;
        }

        static void ApplyAction<T>(IEnumerable<T> col, Action<T> a)
        {
            foreach (T t in col)
            {
                a(t);
            }
        }

        static int Count<T>(IEnumerable<T> col, Predicate<T> predicate)
        {
            int count = 0;
            foreach(var i in col)
            {
                if (predicate(i)) { count++; }
            }
            return count;
        }
    }
}
