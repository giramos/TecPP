using System;

namespace Modelo
{
    /// <summary>
    /// Implementación de la versión genérica de la interfaz IComparable
    /// En este caso Album referencia al tipo Album
    /// </summary>
    public class Album : IComparable<Album>
    {
        public string Titulo { get; }
        public string Autor { get; }
        public int AñoPublicacion { get; }

        public Album(string titulo, string autor, int añoPublicacion)
        {
            this.Titulo = titulo;
            this.Autor = autor;
            this.AñoPublicacion = añoPublicacion;
        }

        /// <summary>
        /// En base al año de publicación
        /// </summary>
        /// <param name="other"></param>
        /// <returns>0: mismo año; 1: Actual es más reciente; -1 parámetro es más reciente</returns>
        public int CompareTo(Album other)
        {
            if (this.AñoPublicacion == other.AñoPublicacion)
                return 0;
            return this.AñoPublicacion > other.AñoPublicacion ? 1 : -1;
        }

        public override string ToString()
        {
            return Titulo + " - " + Autor + "(" + AñoPublicacion + ")";
        }
    }
}
