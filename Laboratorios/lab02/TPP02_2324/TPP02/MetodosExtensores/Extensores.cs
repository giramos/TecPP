using ListaEnlazada;
using System;

namespace MetodosExtensores
{
    public enum Estaciones { Invierno = 0, Primavera = 1, Verano = 2, Otoño = 3 };

    /// <summary>
    /// Los métodos extensores están contenidos en clases estáticas.
    /// </summary>
    public static class Extensores
    {
        /// <summary>
        /// Los métodos extensores son métodos estáticos.
        /// Afectan a la clase que se establece después del this
        /// En este caso, extendemos string con el método ContarVocalesSinTilde
        /// </summary>
        /// <param name="texto"></param>
        /// <returns>Número de vocales sin tilde</returns>
        public static int ContarVocalesSinTilde(this string texto)
        {
            int resultado = 0;
            foreach (char letra in texto)
                if ("aeiouAEIOU".Contains(letra))
                    resultado++;
            return resultado;

        }

        //Crear un método extensor de string que, a partir de un texto, cuente las repeticiones de una letra (debe recibir la letra, obviamente).
        public static int RepeticionesLetra(this string texto, char letra)
        {
            int res = 0;
            foreach(var i in texto)
                if (i.Equals(letra))
                    res++;
            return res;    
        }

        //Crear un método extensor de DateTime Estacion() que devuelva la estación (string).
        public static Estaciones MetodoEstacion(this DateTime dateTime)
        {
            int mes = dateTime.Month;
            if (mes >= 1 || mes <= 3)
                return Estaciones.Invierno;
            else if (mes >= 4 || mes <= 6)
                return Estaciones.Primavera;
            else if (mes >= 7 || mes <= 9)
                return Estaciones.Verano;
            else
                return Estaciones.Otoño;
        }

        //Crear un método extensor de vuestra clase Lista denominado Sum() que devuelva la suma de todos los elementos de la lista.        
        public static int Sum(this Lista lista)
        {
            int suma = 0;
            for(int i=0; i<lista.NumElementos; i++)
            {
                suma = suma + lista.GetElemento(i);
            }
            return suma;    
        }
    }
}
