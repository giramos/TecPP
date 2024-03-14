namespace Modelo
{
    public class Disco
    {
        public string Titulo { get; }
        public string Autor { get; }

        public int Año { get; }

        public Disco(int año, string titulo, string autor)
        {
            this.Titulo = titulo;
            this.Autor = autor;
            this.Año = año;
        }

        public override string ToString()
        {
            return $"Disco: \"{Titulo}\" por {Autor} ({Año})";
        }
    }
}
