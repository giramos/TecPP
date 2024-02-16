using System;
using System.Reflection.Metadata.Ecma335;

namespace Polimorfismo
{
//    ¿Cómo podría implementarse un algoritmo de ordenación válido para
//ambos tipos de objetos?
//• Deberá poder utilizarse para nuevos objetos en el futuro
//• Diséñelo e impleméntelo en C#, ordenando ángulo por Radians y
//persona por Surname1, Surname2 y Name
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sort");
            Console.WriteLine("A ver...");
            var arr = new[] {new Angulo(), new Angulo(8.8), new Angulo(12.0), new Angulo(0.3),
            new Angulo(13.2), new Angulo(0.75), new Angulo(1), new Angulo(), new Angulo(12.75)};
            Console.WriteLine("array de angulos: ");
            foreach (var item in arr) { Console.WriteLine(item); }
            Sort(arr); Console.WriteLine("Sort del array ...");
            foreach (var item in arr) { Console.WriteLine(item); }

            Console.WriteLine("");

            Console.WriteLine("Sort");
            Console.WriteLine("A ver...");
            Persona[] vec = new Persona[] { new("Ger"), new("Bea"), new("Herni"), new(), new("Toni"), new("Ger", "Igl", 12, "11111111111"), new("Ger", "Rams", 33, "3333333"), new("bea"), new("Bea"), new("Bea", "Igl", 22, "22222") };

            Console.WriteLine("array de personas: ");
            foreach (var item in vec) { Console.WriteLine(item); }
            Sort(vec); Console.WriteLine("Sort del array ...");
            foreach (var item in vec) { Console.WriteLine(item); }


        }

        static void Sort(ICompara[] array)
        {
            for(int i=0; i<array.Length-1; i++)
                for(int j=array.Length-1; j>i; j--)
                    if (array[j].Compara(array[i]) < 0) // orden ascendente. si quieres al reves '>'
                    {
                        ICompara item = array[i];
                        array[i] = array[j];
                        array[j] = item;
                    }
        }
    }

    class Angulo : ICompara
    {
        double _radianes;

        public double Radianes { get { return _radianes; } set { _radianes = value; } }

        public Angulo(double radianes)
        {
            Radianes = radianes;
        }

        public Angulo()
        {
            Radianes = 0.0;
        }

        public override string ToString() => $"Angulo => radianes: {Radianes}";

        public int Compara(object otro)
        {
            Angulo a = otro as Angulo;
            if(a != null)
                if(this.Radianes > a.Radianes) { return 1; }
                else if (this.Radianes < a.Radianes) { return -1; }
                else
                {
                    return 0;
                }
            throw new ArgumentNullException("");
        }

    }

    class Persona : ICompara
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int Años { get; set; }
        public string NIF { get; set; }

        public Persona()
        {
            Nombre = "Anonimo";
            Apellidos = "Apellido";
            Años = 0;
            NIF = "unknow";
        }

        public Persona(string name)
        {
            Nombre = name;
            Apellidos = "Apellido";
            Años = 0;
            NIF = "unknow";
        }

        public Persona(string nombre, string apellidos, int años, string nIF) : this(nombre)
        {
            Apellidos = apellidos;
            Años = años;
            NIF = nIF;
        }

        public int Compara(object otro)
        {
            Persona p = otro as Persona;
            if (p != null)
                if (this.Nombre.CompareTo(p.Nombre) > 0) { return 1; }
                else if (this.Nombre.CompareTo(p.Nombre) < 0) { return -1; }
                else
                {
                    if (this.Apellidos.CompareTo(p.Apellidos) > 0) { return 1; }
                    else if (this.Apellidos.CompareTo(p.Apellidos) < 0) { return -1; }
                    else { return 0; }
                }
            throw new ArgumentException("Error");
        }

        public override string ToString()
        {
            return $"Persona => Nombre:{Nombre} - Apellidos:{Apellidos} - Años:{Años} - NIF:{NIF}";
        }
    }

    interface ICompara
    {
        int Compara(object otro);
    }
}
