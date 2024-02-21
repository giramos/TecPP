using System;
using Modelo;
namespace Delegados
{
    class Program
    {
        //Creamos un tipo (delegate) para cualquier método (de instancia o clase)
        //Que reciba 2 char y devuelva un string.
        public delegate string OperacionConDosChar(char a, char b);

        //Otro
        public delegate void ListaMetodos();


        /// <summary>
        /// Concatena dos char y los devuelve en un string.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string Concatenar(char a, char b)
        {
            return String.Format("{0}{1}", a, b);
        }


        /// <summary>
        /// Genera una cadena de tamaño [2,7] con a y b, aleatoriamente.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string CadenaAleatoria(char a, char b)
        {
            string cadena = "";
            Random random = new Random();
            int alea = random.Next(2, 8);
            for (int i = 0; i < alea; i++)
            {
                int eligeCaracter = random.Next(0, 2);
                cadena += eligeCaracter == 0 ? a : b;
            }
            return cadena;
        }

        public static void Imprime()
        {
            Console.WriteLine("Imprimiendo por pantalla");
        }


        static void Main(string[] args)
        {
            //Referenciamos al método Concatenar porque CUMPLE LAS CONDICIONES del tipo delegado;
            //unaFunción está apuntando al método Concatenar.
            OperacionConDosChar unaFuncion = Concatenar;
            //Invocamos, usando () y los parámetros necesarios, mediante el delegado:
            string resultado = unaFuncion('a', 'b');
            Console.WriteLine(resultado);

            //Cambio de la referencia a otro método que sigue cumpliendo las condiciones.
            unaFuncion = CadenaAleatoria;
            Console.WriteLine(unaFuncion('a', 'b'));

            OperacionConDosChar unaFuncion2 = Concatenar;


            OperacionConDosChar[] arrayFunciones = new OperacionConDosChar[] { unaFuncion, unaFuncion2 };

            //Los delegados pueden utilizarse como parámetros y retorno de funciones.
            // A estas funciones (que reciben y/o devuelven otras funciones, se les denomina 
            //FUNCIONES DE ORDEN SUPERIOR.
            OperacionConDosChar funcionDevuelta = GetDelegadoRandom(arrayFunciones);
            Console.WriteLine("Imprimo el resultado de una función aleatoria: {0}", funcionDevuelta('s', 'i'));


            //Coleccionando funciones. Echad un ojo al ejemplo "Observer"
            Persona persona = new Persona("Pepe", "Suárez", "11111", 32);
            Persona persona2 = new Persona("José", "Suárez", "2222", 14);

            Console.WriteLine("\nLlamada a listaMétodos...");


            ListaMetodos listaMetodos;

            listaMetodos = persona.Saluda; // La asignación implica un reset a la colección
            listaMetodos += persona2.Saluda; // Añadimos.
            listaMetodos += Imprime; //Añadimos.
            listaMetodos(); //Invoca a todas las funciones coleccionadas.

            Console.WriteLine("\nLlamada a listaMétodos eliminando persona2.Saluda()...");

            listaMetodos -= persona2.Saluda; //Eliminamos 
            listaMetodos(); //Volvemos a invocar.
        }

        /// <summary>
        /// FUNCIÓN DE ORDEN SUPERIOR. Devuelve una función aleatoria de un array de funciones. 
        /// </summary>
        /// <param name="arrayFunciones"></param>
        /// <returns></returns>
        public static OperacionConDosChar GetDelegadoRandom(OperacionConDosChar[] arrayFunciones)
        {
            Random random = new Random();
            int alea = random.Next(0, arrayFunciones.Length);
            return arrayFunciones[alea];

        }

    }
}
