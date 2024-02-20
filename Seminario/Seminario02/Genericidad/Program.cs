using System;

namespace Genericidad
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Comparacion con genericidad acotada");
            Console.WriteLine("A ver...");
            Angulo[] arrayAngulo = { new(),  new Angulo(),
                new Angulo(8.8, "Bea"),
                new Angulo(12.0, "angulo1"),
                new Angulo(0.3, "Angulo 3"),
                new Angulo(13.2, "P"),
                new Angulo(0.75, "akhgaf"),
                new Angulo(1, "kkkkkk"),
                new Angulo(),
                new Angulo(12.75, "A") };
            // Crear un array de objetos que contienen instancias de Persona y Angulo
            Persona[] arrayPersona =  {
                new Persona("Ger"),
                new Persona("Bea"),
                new Persona("Herni"),
                new Persona("Toni"),
                new Persona("Ger", "Igl", 12, "11111111111"),
                new Persona("Ger", "Rams", 33, "3333333"),
                new Persona("bea"),
                new Persona("Bea"),
                new Persona("Bea", "Igl", 22, "22222")
            };

            Console.WriteLine("array de angulos: ");
            foreach (var item in arrayAngulo) { Console.WriteLine(item); }
            Sort(arrayAngulo);
            Console.WriteLine("Sort del array angulos...");
            foreach (var item in arrayAngulo) { Console.WriteLine(item); }

            Console.WriteLine("");

            Console.WriteLine("array de personas: ");
            foreach (var item in arrayPersona) { Console.WriteLine(item); }
            Sort(arrayPersona);
            Console.WriteLine("Sort del array personas...");
            foreach (var item in arrayPersona) { Console.WriteLine(item); }

            Console.WriteLine("");

            Console.WriteLine("Comparacion");
            Console.WriteLine("Array miscelaneo");
            // Crear un nuevo arreglo de ICompara<T>
            INombrable[] vector = {
                new Persona("Ger"),
                new Persona("Bea"),
                new Persona("Herni"),
                new Angulo(),
                new Angulo(8.8, "Bea"),
                new Angulo(12.0, "angulo1"),
                new Angulo(0.3, "Angulo 3"),
                new Angulo(13.2, "P"),
                new Persona("Toni"),
                new Persona("Ger", "Igl", 12, "11111111111"),
                new Persona("Ger", "Rams", 33, "3333333"),
                new Persona("bea"),
                new Persona("Bea"),
                new Persona("Bea", "Igl", 22, "22222"),
                new Angulo(0.75, "akhgaf"),
                new Angulo(1, "kkkkkk"),
                new Angulo(),
                new Angulo(12.75, "A")
            };

            foreach (var item in vector)
            {
                Console.WriteLine(item);
            }
            Sort<INombrable[]>(vector);
            Console.WriteLine("Sort array miscelaneo");
            foreach (var item in vector)
            {
                Console.WriteLine(item);
            }
        }
        /// <summary>
        /// Ordena
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        private static void Sort<T>(ICompara<T>[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
                for (int j = array.Length - 1; j > i; j--)
                    if (array[j].Compara((T)array[i]) < 0) // orden ascendente. si quieres al reves '>'
                    {
                        ICompara<T> item = array[i];
                        array[i] = array[j];
                        array[j] = item;
                    }
        }
        /// <summary>
        /// Ordenar con genericidad acotada. Es mas especifico solamente permite comparar elementos del mismo tipo
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
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

        private static void Sort<T>(INombrable[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
                for (int j = array.Length - 1; j > i; j--)
                    if (array[j].Nombrame((Persona)array[i], (Angulo)array[i]) < 0) // orden ascendente. Si quieres al revés, cambia '<' a '>'
                    {
                        INombrable item = array[i];
                        array[i] = array[j];
                        array[j] = item;
                    }
        }
    }

    class Angulo : ICompara<Angulo>, INombrable
    {
        double _radianes;
        public string Nombre { get; set; }

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

        public int Compara(Angulo a)
        {
            if (this.Radianes.CompareTo(a.Radianes) > 0) { return 1; }
            else if (this.Radianes.CompareTo(a.Radianes) < 0) { return -1; }
            else { return 0; }
        }

        public int Nombrame(Persona p, Angulo a)
        {
            if (p.Nombre.CompareTo(a.Nombre) > 0) { return 1; }
            else if (p.Nombre.CompareTo(a.Nombre) < 0) { return -1; }
            else { return 0; }
        }
    }
    interface INombrable
    {
        int Nombrame(Persona p, Angulo a);
    }
    class Persona : ICompara<Persona>, INombrable
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

        public int Compara(Persona otro)
        {
            if (this.Nombre.CompareTo(otro.Nombre) > 0) { return 1; }
            else if (this.Nombre.CompareTo(otro.Nombre) < 0) { return -1; }
            else
            {
                if (this.Apellidos.CompareTo(otro.Apellidos) > 0) { return 1; }
                else if (this.Apellidos.CompareTo(otro.Apellidos) < 0) { return -1; }
                else
                {
                    return 0;
                }
            }
        }

        public override string ToString()
        {
            return $"Persona => Nombre:{Nombre} - Apellidos:{Apellidos} - Años:{Años} - NIF:{NIF}";
        }

        public int Nombrame(Persona p, Angulo a)
        {
            if (p.Nombre.CompareTo(a.Nombre) > 0) { return 1; }
            else if(p.Nombre.CompareTo(a.Nombre) < 0) { return -1; }
            else { return 0; }
        }
    }

    interface ICompara<T>
    {
        int Compara(T otro);
    }
}
