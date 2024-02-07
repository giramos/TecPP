using System;
using Modelo;
namespace HerenciaPolimorfismo
{
    class Program
    {
        static void Main(string[] args)
        {
            //IFacturable cami = new Camiseta();
            //Console.WriteLine(cami.ToTicket());

            //Herencia
            Console.WriteLine("Creamos a Cuack");
            Animal pato = new Pato("Cuack",20); // lo puedo hacer porque pato hereda de animal

            Console.WriteLine("\nEl pato va a saludar:");
            pato.Saludar(); // es un animal que lo pasa a pato en tiempo de ejecución -> llama al de pato porque está redefinido el método en pato
            
            Console.WriteLine("\n¿El pato se mueve?");
            pato.Mover();  // aquí va a llamar al de animal -> porque el de pato no está redefiniendo al de animal
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

            Album album3 = new Album(autor: "Public Enemy", título: " How You Sell Soul to a Soulless People Who Sold Their Soul?", añoPublicacion: 2007);

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

        }

        /// <summary>
        /// Método polimórfico que el valor máximo de cualquier par de objetos IComparable
        /// ¿Por qué utilizamos Modelo.IComparable en lugar de IComparable directamente?
        /// </summary>
        /// <returns>El objeto con valor máximo.</returns>
        public static Modelo.IComparable Max(Modelo.IComparable a, Modelo.IComparable b)
        {

            return a.Compare(b) > 0 ? a : b;
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
