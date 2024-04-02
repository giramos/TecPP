namespace Modelo
{
    public class Libro
    {
        public string Titulo { get; }
        public string Autor { get; }

        public int Año { get; }

        public Libro(string titulo, string autor, int año)
        {
            this.Titulo = titulo;
            this.Autor = autor;
            this.Año = año;
        }

        public override string ToString()
        {
            return $"Libro: \"{Titulo}\" por {Autor} ({Año})";
        }
    }
}
