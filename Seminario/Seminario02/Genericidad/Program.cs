using System;

namespace Genericidad
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sort");
            Console.WriteLine("A ver...");

            // Crear un array de objetos que contienen instancias de Persona y Angulo
            ICompara<object>[] mis = new ICompara<object>[] {
                new Persona("Ger"),
                new Persona("Bea"),
                new Persona("Herni"),
                new Angulo(),
                new Persona("Toni"),
                new Persona("Ger", "Igl", 12, "11111111111"),
                new Persona("Ger", "Rams", 33, "3333333"),
                new Persona("bea"),
                new Persona("Bea"),
                new Persona("Bea", "Igl", 22, "22222"),
                new Angulo(),
                new Angulo(8.8, "Bea"),
                new Angulo(12.0, "angulo1"),
                new Angulo(0.3, "Angulo 3"),
                new Angulo(13.2, "P"),
                new Angulo(0.75, "akhgaf"),
                new Angulo(1, "kkkkkk"),
                new Angulo(),
                new Angulo(12.75, "A")
            };

            Console.WriteLine("array de miscelanea: ");
            foreach (var item in mis) { Console.WriteLine(item); }
            Sort<object[]>(mis);
            Console.WriteLine("Sort del array ...");
            foreach (var item in mis) { Console.WriteLine(item); }
        }

        private static void Sort<T>(ICompara<object>[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
                for (int j = array.Length - 1; j > i; j--)
                    if (array[j].Compara(array[i]) < 0) // orden ascendente. si quieres al reves '>'
                    {
                        ICompara<object> item = array[i];
                        array[i] = array[j];
                        array[j] = item;
                    }
        }

        static void Sort<T>(T[] array) where T : ICompara<T>
        {
            for (int i = 0; i < array.Length - 1; i++)
                for (int j = array.Length - 1; j > i; j--)
                    if (array[j].Compara(array[i]) < 0) // orden ascendente. si quieres al reves '>'
                    {
                        T item = array[i];
                        array[i] = array[j];
                        array[j] = item;
                    }
        }
    }

    class Angulo : ICompara<object>
    {
        double _radianes;
        string Nombre { get; set; }

        public double Radianes { get { return _radianes; } set { _radianes = value; } }

        public Angulo(double radianes, string name)
        {
            Nombre = name;
            Radianes = radianes;
        }

        public Angulo()
        {
            Radianes = 0.0;
            Nombre = "Angulo Anonimo";
        }

        public override string ToString() => $"Angulo => Nombre: {Nombre} - radianes: {Radianes}";

        public int Compara(object aa)
        {
            Angulo a = aa as Angulo;
            if(a is Angulo)
            {
                if (this.Nombre.CompareTo(a.Nombre) > 0) { return 1; }
                else if (this.Nombre.CompareTo(a.Nombre) < 0) { return -1; }
                else
                {
                    return 0;
                }
            }
            return 0;
            
        }
    }

    class Persona : ICompara<object>
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public double Años { get; set; }
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

        public int Compara(object otroo)
        {
            Persona otro = otroo as Persona;
            if(otro is Persona)
            {
                if (this.Nombre.CompareTo(otro.Nombre) > 0) { return 1; }
                else if (this.Nombre.CompareTo(otro.Nombre) < 0) { return -1; }
                else { return 0; }
            }
            return 0;

        }

        public override string ToString()
        {
            return $"Persona => Nombre:{Nombre} - Apellidos:{Apellidos} - Años:{Años} - NIF:{NIF}";
        }
    }

    interface ICompara<T>
    {
        int Compara(T otro);
    }
}
