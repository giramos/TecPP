using System;

namespace Modelo
{
    public class Persona
    {
        public string Nombre { get; private set; }

        public string Apellido { get; private set; }

        public string Nif { get; private set; }

        public int Edad { get; private set; }

        public override string ToString()
        {
            return String.Format("{0} {1} con NIF {2} tiene {3} años.", Nombre, Apellido, Nif, Edad);
        }

        public Persona(string nombre, string apellido, string nif, int edad)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Nif = nif;
            this.Edad = edad;
        }

        public void Saluda()
        {
            Console.WriteLine("Hola, soy {0}", this.Nombre);
        }

    }
}
