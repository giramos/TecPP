using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DelPredefinidosGenericos
{
    public static class Extensores
    {
        /// <summary>
        /// ¿Qué hace este método?
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="coleccion"></param>
        /// <param name="accion"></param>
        public static void ForEach<T>(this IEnumerable<T> coleccion, Action<T> accion)
        {
            foreach (T item in coleccion)
                accion(item);
        }

        //        Contar: Recibe un IEnumerable y un Delegado genérico.Devuelve el número de elementos del
        //IEnumerable que cumplen la condición del delegado.Probar para: 
        // Para obtener cuántos libros tienen menos de N páginas 
        // Para obtener cuántos libros se publicaron más tarde del año N.
        public static int Contar<T>(this IEnumerable<T> col, Predicate<T> cond)
        {
            int count = 0;
            foreach (T item in col)
            {
                if (cond(item))
                {
                    count++;
                }
            }
            return count;
        }

        //        Contiene: Recibe un IEnumerable y un Delegado genérico.Devuelve la posición del primer
        //elemento que cumple las condiciones.Devuelve -1 si no encuentra elementos. 

        public static int Contiene<T>(this IEnumerable<T> col, Predicate<T> cond)
        {
            int pos = -1;
            foreach (T item in col)
            {
                pos++;
                if (cond(item))
                {
                    return pos;
                }
            }
            return pos;
        }

        //        Filtrar: Recibe un IEnumerable y un Delegado genérico.Devuelve un IEnumerable con los
        //elementos que cumplen la condición.

        public static IEnumerable<T> Filtrar<T>(this IEnumerable<T> col, Predicate<T> cond)
        {
            IList<T> list = new List<T>();
            foreach (T item in col)
            {
                if (cond(item))
                {
                    list.Add(item);
                }
            }
            return list;
        }

        //        Mostrar: Recibe un IEnumerable y un Delegado genérico.Mostrará el título y autor por
        //pantalla.No es posible llamar dentro del método Mostrar a Console.WriteLine.

        public static void Mostrar<T>(this IEnumerable<T> col, Action<T> action)
        {
            foreach(T item in col)
            {
                action(item);
            }
        }
    }
}
