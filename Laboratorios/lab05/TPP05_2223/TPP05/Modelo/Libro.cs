using System;

namespace Modelo
{
    public class Libro
    {
        public string Titulo { get; private set; }

        public string Autor { get; private set; }

        public int NumeroPaginas { get; private set; }

        public int Año { get; private set; }
        public string Genero { get; private set; }

        public override string ToString()
        {
            return String.Format("Titulo del Libro: {0} Autor: {1} con {2} número de paginas del año {3} y del genero: {4}.", Titulo, Autor, NumeroPaginas, Año, Genero);
        }

        public Libro(string titulo, string autor, int numeroP, int año, string genero)
        {
            this.Titulo = titulo;
            this.Autor = autor;
            this.NumeroPaginas = numeroP;
            this.Año = año;
            this.Genero = genero;
        }

    }
}
