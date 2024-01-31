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
            // IMPORTANTE ESTO PARA AHORRAR TIEMPO EN EL EXAMEN
            foreach (char letra in texto) // para recorrer un string
                if ("aeiouAEIOU".Contains(letra)) // ACORDARSE DE ESTO PA CONTAR
                    resultado++;
            return resultado;
        }

        // Método extensor (hecho por mi) que cuenta las repeticiones de una letra en un texto
        public static int ContarRepeticionesLetra(this string texto) {

            int contador = 0;
            foreach (char letra in texto) {
                if ("aA".Contains(letra))
                    contador++;
            }

            return contador;
        }


        // Método extensor (hecho por mi) que me dice en qué estación estamos
        public static Estaciones CalcularEstacion(this DateTime date) {
            int month = date.Month;
            if ((month >= 2) && (month <= 4))
            {
                return (Estaciones)1;
            }

            if ((month >= 5) && (month <= 7))
            {
                return (Estaciones)2;
            }

            if ((month >= 8) && (month <= 10))
            {
                return (Estaciones)3;
            }

            else
            {
                return (Estaciones)0;
            }
        }


        // Método extensor (hecho por mi) que me escribe en qué estacion estamos
        public static string Texto(this Estaciones date) {
            int mes = DateTime.Now.Month; // Sacamos el mes

            if ((mes >= 2) && (mes <= 4))
            {
                return "Primavera";
            }

            if ((mes >= 5) && (mes <= 7))
            {
                return "Verano";
            }

            if ((mes >= 8) && (mes <= 10))
            {
                return "Otoño";
            }

            else
            {
                return "Invierno";
            }
        }

        //Crear un método extensor de string que, a partir de un texto, cuente las repeticiones de una letra(debe recibir la letra, obviamente).
        public static int ContarRepeticonesLetra(this string texto, char letra)
        {
            int resultado = 0;
            foreach (char c in texto)
            {
                if (c == letra)
                {
                    resultado++;
                }

            }
            return resultado;
        }

        //Crear un método extensor de vuestra clase Lista denominado Sum() que devuelva la suma de todos los elementos de la lista.
        public static int Sum(this Lista list)
        {
            int suma = 0;
            for (int i = 0; i < list.NumElementos; i++)
            {
                suma += list.GetElemento(i);
            }
            return suma;
        }
    }
}
