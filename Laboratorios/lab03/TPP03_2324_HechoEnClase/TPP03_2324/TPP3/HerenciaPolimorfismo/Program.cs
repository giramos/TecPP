using System;
using Modelo;
using TrabajoEnClase;

namespace HerenciaPolimorfismo
{
    class Program
    {
        static void Main()
        {
            //Herencia
            Console.WriteLine("Creamos a Cuack");
            Animal pato = new Pato("Cuack",20);

            Console.WriteLine("\nEl pato va a saludar:");
            pato.Saludar();
            
            Console.WriteLine("\n¿El pato se mueve?");
            pato.Mover();
            //¿Cuál es el problema? 

            Limpiar();

            //Equals y HashCode

            Album album = new Album(autor: "Audioslave", título: "Out of Exile", añoPublicacion: 2005);
            Album referencia = album;
            Album album2 = new Album(autor: "Audioslave", título: "Out of Exile", añoPublicacion: 2005);

            //Comparaciones de Identidad
            if (album == referencia)
                Console.WriteLine("album y referencia tienen la misma identidad. Son el mismo objeto y mismo estado.");
            if (album != album2)
                Console.WriteLine("album y album2 NO tienen la misma identidad. NO sabemos si tienen el mismo estado.");

            //Comparaciones de estado.
            if (album.Equals(album2))
                Console.WriteLine("album y album2 tienen el mismo estado.");

            Album album3 = new Album(autor: "Public Enemy", título: "How You Sell Soul to a Soulless People Who Sold Their Soul?", añoPublicacion: 2007);

            if (album.Equals(album3))
                Console.WriteLine("album y album3 tienen el mismo estado.");
            else
                Console.WriteLine("album y album3 NO tienen el mismo estado.");



            Limpiar();

            //Interfaces.

            album = new Album("Pearl Jam", "Vitalogy", 1994);
            Console.WriteLine(album);

            album2 = new Album("The Smashing Pumpkins", "Siamese Dream", 1993, 8.0);
            Console.WriteLine(album2);

            //Operador ??. Si no tiene valor (es decir, es null) asigna el especificado.
            double puntuación = album.Puntuación ?? 10.00;
            album.Puntuación = puntuación;
            Console.WriteLine($"Nueva puntuación: {album}");

            //Uso de Album como IComparable
            Console.WriteLine("El Máximo es: {0}", Max(album, album2));

            //Ejercicio: Modificar la implementación en Album para que sea en base a la puntuación.

            Camiseta camiseta = new ("Lost", "manga larga", 10, true, "XS", Camiseta.Color.Azul);
            Camiseta camiseta1 = new ("Nike", "manga corta", 10, true, "S", Camiseta.Color.Azul);
            Camiseta camiseta2 = new ("Puma", "manga larga", 10, true, "XL", (Camiseta.Color)3);
            Camiseta camiseta3 = new ("Abanderado", "manga corta", 10, true, "XS", (Camiseta.Color)2);

            Console.WriteLine(camiseta.GetBase(0.21));
            Console.WriteLine(camiseta.GetTipoIva(0.10));
            Console.WriteLine(camiseta.GetConcepto("Para trabajar"));
            Console.WriteLine(camiseta.GetIVA());
            Console.WriteLine(camiseta.GetPrecio(0.21));
            Console.WriteLine(camiseta.ToTicket());

        }

        /// <summary>
        /// Método que devuelve valor máximo de cualquier par de objetos IComparable.
        /// En caso de ser iguales, devuelve el objeto "a".
        /// En caso contrario, devuelve el máximo(el objeto "a" o el objeto "b").
        /// ¿Por qué utilizamos Modelo.IComparable en lugar de IComparable directamente?
        /// </summary>
        /// <returns>El objeto con valor máximo.</returns>
        public static Modelo.IComparable Max(Modelo.IComparable a, Modelo.IComparable b)
        {

            return a.Compare(b) >= 0 ? a : b;
        }

        /// <summary>
        /// Limpieza de la consola.
        /// </summary>
        public static void Limpiar()
        {
            Console.WriteLine("\nPulsa una tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
