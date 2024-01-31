using System;

namespace MetodosExtensores
{
    class Program
    {
        static void Main(string[] args)
        {
            //Demostración de métodos extensores.
            string prueba = "murciélagoa";
            //¡OJO! Fíjate que, en este caso, no pasamos ningún parámetro.
            int vocalesSinTilde = prueba.ContarVocalesSinTilde();
            Console.WriteLine("El texto {0} contiene {1} vocales sin tilde", prueba, vocalesSinTilde);


            //Ejercicios
            //Crear un método extensor de string que, a partir de un texto, cuente las repeticiones de una letra (que debe recibir).
            int repeticionesLetra = prueba.ContarRepeticionesLetra();
            Console.WriteLine("El texto {0} contiene {1} letras 'a'", prueba, repeticionesLetra);

            /*
             * Crear un método extensor de DateTime Estacion() que devuelva la estación (string).
            */
            DateTime now = DateTime.Now;
            Estaciones EstacionActual = now.CalcularEstacion();
            Console.WriteLine($"Estacion: {EstacionActual}");

            /*
             * Crear un método extensor Texto() de Estaciones que devuelva un string con el nombre de la estación.
             * 
             * 
             * */
            String EstacionTxt = EstacionActual.Texto();
            Console.WriteLine($"Estacion: {EstacionTxt}");

        }
    }
}
