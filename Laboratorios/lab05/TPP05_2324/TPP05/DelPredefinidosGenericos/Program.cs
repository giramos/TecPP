using System;
using System.Linq;
using System.Reflection.Emit;
using Modelo;

namespace DelPredefinidosGenericos
{
    /// <summary>
    /// C# Contiene una serie de delegados predefinidos genéricos, véase: Func, Predicate y Action.
    /// </summary>
    class Program
    {
        public static string Concatenar(char a, char b)
        {
            return String.Format("{0}{1}", a, b);
        }

        public static bool TieneLongitudUno(string cadena)
        {
            return cadena.Length == 1;
        }

        public static void Almacena(int valor)
        {
            //<-- Código que simula almacenar el valor en algún lugar.
            Console.WriteLine("LOG: El valor {0} ha sido almacenado", valor);
        }

        static void Main(string[] args)
        {
            //Func Puede recibir entre 0 y 16 parámetros y siempre retorna un valor

            Func<char, char, string> funcion = Concatenar;
            Console.WriteLine(funcion('f', 'u'));

            //Predicate Siempre recibe un único parámetro y siempre devuelve bool.
            //¿Qué alternativa tenemos?
            Predicate<string> funcionPredicado = TieneLongitudUno;
            String cadena = "S";

            if (funcionPredicado(cadena)) //Recordad, para invocar tenemos que usar () con los parámetros necesarios.
                Console.WriteLine(cadena + " tiene longitud 1.");
            else
                Console.WriteLine(cadena + " no tiene longitud 1.");

            //Action recibe entre 0 y 16 parámetros y NO DEVUELVE NADA (void)

            Action<int> funcionAction = Almacena;
            funcionAction(2);

            int valor = 2;

            //Pasamos como primer parámetro una función. AplicaciónDoble ¿Es una función de orden superior?
            int resultado = AplicaciónDoble(Suma, valor);
            Console.WriteLine("Resultado de la aplicación doble: {0}", resultado);

            //Ejemplo de orden superior.
            Persona[] personas = Factoria.CrearPersonas();
            Persona[] adultos = Array.FindAll(personas, n => n.Edad >= 18);
            Persona[] adultos1 = Array.FindAll(personas, EsAdulto);

            adultos.ForEach(Console.WriteLine);

            ////////////////////////////////
            /// EJERCICIOS PROPUESTOS LAB 05 //////////////
            /// ///////////////////////////
            /// 

            Libro[] libros = Factoria.CrearLibros();
            //Para obtener cuántos libros tienen menos de N páginas
            var pag = 500;
            var res = libros.Contar(x => x.NumeroPaginas < pag);
            Console.WriteLine("\n Ejemplos con Libros: Contar - nº paginas: " + res);
            Console.WriteLine("");
            //Para obtener cuántos libros se publicaron más tarde del año N.
            res = libros.Contar(x => x.Año > 2000);
            Console.WriteLine("\n Ejemplos con Libros: Contar - mas tarde al año 2000: " + res);
            Console.WriteLine("");
            //Ahora, utiliza los métodos anteriores con strings para:
            //     Contar vocales de un string.
            //     Obtener las consonantes de la un string.
            //     Si contiene alguna vocal con tilde.
            var palabra = "GermánIglesiasRamosBeatrizRamosDíazAntonioJesúsIglesiasPericónHernánIglesiasRamos";
            Console.WriteLine($"Palabra: {palabra}");
            Console.WriteLine("Contar vocales de la palabra.");
            res = palabra.Contar(x => "AEIOUaeiouáéíóú".Contains(x));
            Console.WriteLine(res); Console.WriteLine("");
            Console.WriteLine("Contar consonantes de la palabra.");
            res = palabra.Contar(x => !("AEIOUaeiouáéíóú".Contains(x)));
            Console.WriteLine(res); Console.WriteLine("");
            Console.WriteLine("Contar tildes de la palabra.");
            res = palabra.Contar(x => "áéíóú".Contains(x));
            Console.WriteLine(res); Console.WriteLine("");
            Console.WriteLine("Contiene alguna vocal con tilde????.");
            var res1 = palabra.Count(x => "áéíóú".Contains(x));
            Console.WriteLine($"Posicion:{res}"); Console.WriteLine("");
            Console.WriteLine("Obtener una lista de consonantes de la palabra");
            var res2 = palabra.Filtrar(x => !("AEIOUaeiouáéíóú".Contains(x)));
            res2.Mostrar(x => Console.Write(x)); Console.WriteLine("");
            //            Implementar mediante expresiones lambda: 
            // La multiplicación de dos enteros.
            // Comprobar que una cadena tiene una determinada longitud. 
            // Saber si un entero es divisible por otro entero.
            Func<int,int,int> mult = (a, b) => a * b;
            int variable = 6;
            Predicate<string> longitud = a => a.Length.Equals(variable); 
            Func<int,int,bool> divisible = (a,b) => a % b == 0;

            Console.WriteLine();
            Console.WriteLine($"Multiplicacion de dos enteros (2,3): {mult(2,3)}"); Console.WriteLine();
            Console.WriteLine($"Comprobar si la palabra german tiene una longitud de {variable}: {longitud("german")}");Console.WriteLine();
            Console.WriteLine($"Saber si 13 es divisible entre 2 => {divisible(13,2)}");


        }

        public static int Suma(int a)
        {
            return a + a;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="funcion"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public static int AplicaciónDoble(Func<int, int> funcion, int a)
        {
            // Puedo pasar el resultado de la primera aplicación al mismo método.
            //El Func necesita un int y devuelve un int.
            return funcion(funcion(a));
        }

        private static bool EsAdulto(Persona persona)
        {
            return persona.Edad >= 18;
        }


    }
}
