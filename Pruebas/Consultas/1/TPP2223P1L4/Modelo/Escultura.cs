namespace Modelo
{
    public class Escultura
    {
        public string Titulo { get; }
        public string Autor { get; }

        public int Año { get; }

        public Escultura(string titulo, int año, string autor)
        {
            this.Titulo = titulo;
            this.Autor = autor;
            this.Año = año;
        }


        public override string ToString()
        {
            return $"Escultura: \"{Titulo}\" por {Autor} ({Año})";
        }
    }
}
