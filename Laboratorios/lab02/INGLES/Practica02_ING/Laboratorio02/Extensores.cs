using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio02
{
    public static class Extensores
    {
        /// <summary>
        /// Metodo que invierte un numero entero
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int Invertir(this int number)
        {
            int reversedNumber = 0;
            while (number > 0)
            {
                int digit = number % 10;
                reversedNumber = (reversedNumber * 10) + digit;
                number /= 10;
            }
            return reversedNumber;
        }
        /// <summary>
        /// Metodo que invierte un entero. Usa conversion a string y a array de char para luego devolver un int
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int Reverse(this int number)
        {
            string numberString = number.ToString();
            char[] charArray = numberString.ToCharArray();
            Array.Reverse(charArray);
            string reversedString = new string(charArray);
            return int.Parse(reversedString);
        }
        /// <summary>
        /// Metodo que comprueba si un nº es primo
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsPrime(this int number)
        {
            if (number <= 1)
                return false;

            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Maximo comun divisor
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int MCD(this int a, int b)
        {
            // El MCD de dos números no puede ser mayor que el número más pequeño
            int min = Math.Min(a, b);

            // Iteramos desde el número más pequeño hacia abajo hasta 1
            for (int i = min; i >= 1; i--)
            {
                if (a % i == 0 && b % i == 0)
                    return i; // Encontramos el máximo común divisor
            }

            return 1; // Si no se encuentra otro MCD, regresamos 1
        }

        /// <summary>
        /// Minimo comun multiplo
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int MCM(this int a, int b)
        {
            return (a * b) / a.MCD(b); // Usamos la relación MCM * MCD = a * b
        }


        public static int Concatenate(this int a, int b)
        {
            // Si b es negativo, eliminamos el signo multiplicándolo por -1
            b = Math.Abs(b);

            // Obtenemos la cantidad de dígitos de b
            int digitsCount = (int)Math.Floor(Math.Log10(b)) + 1;

            // Concatenamos b a la derecha de a multiplicándolo por 10 elevado a la cantidad de dígitos de b
            return (a * (int)Math.Pow(10, digitsCount)) + b;
        }

        public static int Concatenate1(this int a, int b)
        {
            return int.Parse(a.ToString() + Math.Abs(b));
        }
    }
}
