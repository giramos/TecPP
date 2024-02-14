using System;
using Modelo;

namespace Genericidad
{
    //Genericidad -> abstracion modelo para otras abstracciones
    class Program
    {
        static void EjemploMétodos()
        {
            ImprimirAlerta<string>("Probando cadenas", 2); // es genérico

            //También podemos dejar que el compilador infiera el tipo:
            ImprimirAlerta(1, 2);
            ImprimirAlerta(3.0, 2);
            Album album = new Album(titulo: "Led Zeppelin IV", autor: "Led Zeppelin", añoPublicacion: 1971);
            ImprimirAlerta(album, 2);

            //Tipo por defecto:
            ImprimirAlerta(0, 2);

            //Aunque no siempre puede lograrlo, véase la siguiente línea comentada:
            //ImprimirAlerta(null, 2); //El ejemplo más simple. -> hay que especificar porque null es de muchos tipos y no puede inferirlo
            //¿En cuantas clases es null un valor por defecto? -> muchas

            //¿Qué ventajas nos proporciona las versiones genéricas frente a las polimórficas? -> no ir haciendo casts
            //Mejor rendimiento (mejor implementacion), mayor robustez (deteccion de errores en tiempo de compilacion)
        }



        /// <summary>
        /// Ejemplo de método genérico.
        /// La genericidad nos permite crear plantillas de código.
        /// La T (y demás marcadores) se sustituyen por el tipo determinado.
        /// Indicamos la T solo si la clase NO indica T.
        /// 
        /// T viene de Type (convenio), pero podemos utilizar cualquier palabra.
        /// 
        /// ¿Cuándo?
        /// 
        /// En tiempo de compilación se crea una versión del método para
        /// cada invocación con distinto tipo.
        /// 
        /// En el ejemplo, el compilador crea 3 versiones del método:
        /// int, double y album.
        /// 
        /// </summary>
        /// <typeparam name="T">Tipo, la T por convenio.</typeparam>
        /// <param name="contenido">Valor a imprimir.</param>
        /// <param name="nVeces">Número de veces a imprimir el valor.</param>
        public static void ImprimirAlerta<T>(T contenido, int nVeces)
        {
            //Cuando trabajamos con genericidad, default(T) 
            //Nos devuelve el valor por defecto del tipo (T)

            if (contenido.Equals(default(T)))
            {
                //NOTA: La condición de arriba fallaría en el caso de recibir un valor null.
                //Podéis profundizar más analizando EqualityComparer<T> en System.Collections.Generic
                //
                //La condición debería ser: EqualityComparer<T>.Default.Equals(contenido, default(T)))


                Console.WriteLine("Detectado tipo por defecto: {0}.", contenido);
                return;
            }

            //Mediante el uso de var, el tipo es inferido directamente por el compilador.
            var colorTexto = Console.ForegroundColor;
            var fondoTexto = Console.BackgroundColor;

            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;


            for (int i = 0; i < nVeces; i++)
                Console.WriteLine("¡Alerta! Valor: " + contenido);

            Console.BackgroundColor = fondoTexto;
            Console.ForegroundColor = colorTexto;
        }


        public static void EjemploClases()
        {
            Album albumA = new Album(titulo: "13th Floor Elevators", autor: "The Psychedelic Sounds of the 13th Floor Elevators", añoPublicacion: 1966);
            Album albumB = new Album(titulo: "Led Zeppelin IV", autor: "Led Zeppelin", añoPublicacion: 1971);

            Contenedor<int, string> contenedor = new Contenedor<int, string>(0, "Primera posición");
            Console.WriteLine(contenedor);


            var contenedor2 = new Contenedor<string, Album>("Lez", albumA);
            Console.WriteLine(contenedor2);

            //Error de compilación: ¿Por qué?
            //contenedor.Sustituir(contenedor2.Key, contenedor2.Value);

            Console.WriteLine("\nCambio en contenedor:");
            contenedor.Sustituir(4, "Otro valor");
            Console.WriteLine(contenedor);


            //Uso de clases con genericidad acotada:
            //El compilador no da error porque Album implementa IComparable
            var acotado = new ContenedorAcotado<string, Album>("13th", albumA);
            Console.WriteLine("\n\nEstado inicial de contenedor acotado:");
            Console.WriteLine(acotado);

            //Intentamos sustituir:
            if (acotado.Sustituir("Lez", albumB))
            {
                Console.WriteLine("Cambio en el contenedor acotado:");
                Console.WriteLine(acotado);
            }
            else
                Console.WriteLine("No se ha realizado ningún cambio en el contenedor acotado.");

        }

        static void Main(string[] args)
        {
            EjemploMétodos();
            Console.WriteLine("\nPulse una tecla para continuar con el siguiente ejemplo...");
            Console.ReadKey();
            Console.Clear();
            EjemploClases();
        }

    }
}
