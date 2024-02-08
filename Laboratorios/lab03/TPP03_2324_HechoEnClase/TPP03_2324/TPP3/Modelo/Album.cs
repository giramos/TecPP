using System;

namespace Modelo
{
    public class Album : IComparable
    {
        
        public string Autor { get;  }

        public string Título { get; }

        public int AñoPublicación { get; }

        /// <summary>
        /// Tipos anulables: System.Nullable<T>
        /// Nos permite asignar null a tipos de valor.
        /// ¿En qué contextos es útil?
        /// </summary>
        public double? Puntuación { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="autor"></param>
        /// <param name="título"></param>
        /// <param name="añoPublicacion"></param>
        /// <param name="puntuación">Opcional. null por defecto</param>
        public Album(string autor, string título, int añoPublicacion, double? puntuación = null)
        {
            Autor = autor;
            Título = título;
            AñoPublicación = añoPublicacion;
            Puntuación = puntuación;
        }

        public override string ToString()
        {
            //HasValue de los tipos anuables nos devuelve si la variable tiene valor o no (null)
            if (Puntuación.HasValue)
                return $"{Autor} publicó en {AñoPublicación}: '{Título}'. Puntuación {Puntuación}.";
            else
                return $"{Autor} publicó en {AñoPublicación}: '{Título}'. No ha recibido puntuación.";
        }

        /// <summary>
        /// ¿Para qué sirve el método Equals? 
        /// Invalidamos el Equals de Object
        /// ¿Qué ocurre si no lo hacemos?
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            Album otro = obj as Album;
            if (otro == null)
                return false;
            return Autor.Equals(otro.Autor) && Título.Equals(otro.Título) && AñoPublicación.Equals(otro.AñoPublicación);
            
        }

        /// <summary>
        /// Dos objetos iguales deben devolver el mismo HashCode.
        /// Sin embargo:
        /// Que dos objetos devuelvan el mismo HashCode, no implica que sean iguales.
        /// ¿Dónde y para qué se utiliza?
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            /* 
             * Usando tuplas:
             *  return (Autor, Título, AñoPublicación).GetHashCode();
            */
            return System.HashCode.Combine(Autor, Título, AñoPublicación);
            
        }

        // <summary>
        /// Implementación del método CompareTo de IComparable
        /// <param name="c">El objeto a comparar</param>
        /// <returns>Negativo si éste es inferior a c;
        /// cero si son iguales;
        /// Positivo si éste es mayor que c;
        /// </returns>
        public int Compare(IComparable c)
        {
            Album otro = c as Album;
            if (otro == null)
                throw new ArgumentException("El parámetro no es un Album");
            return this.AñoPublicación - otro.AñoPublicación;

        }
    }
}
