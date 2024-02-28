
using System;
using System.Collections.Generic;

namespace Currificacion
{
    public static class Ejercicio
    {
        //Ejercicio 1..

        //Implementar de forma currificada el método Buscar entregado en la sesión anterior.
        //Demostrar el uso de su invocación y de la aplicación parcial.
        public static T Buscar<T>(this IEnumerable<T> col, Predicate<T> con)
        {
            foreach (T t in col)
            {
                if (con(t))
                {
                    return t;
                }
            }
            return default(T);
        }

        public static Func<Predicate<T>, T> BuscarCurry<T>(IEnumerable<T> col)
        {
            return con =>
            {
                foreach (T t in col)
                {
                    if (con(t))
                    {
                        return t;
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

        public static Func<int, Func<int, Func<int, bool>>> ComprobarDivision(int divisor)
        {
            return dividendo => cociente => resto => dividendo == cociente * divisor + resto;
        }


        //Ejercicio 3. Convierte un metodo Suma con tres parametros a una version currificada
        public static int Suma(int a, int b, int c)
        {
            return a + b + c;
        }
        
        public static Func<int,Func<int,int>> SumaCurry(int a)
        {
            return b => c => a + b + c;
        }

    }
}
