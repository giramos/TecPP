using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
