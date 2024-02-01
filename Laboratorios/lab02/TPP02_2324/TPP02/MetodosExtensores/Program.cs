using ListaEnlazada;
using System;

namespace MetodosExtensores
{
    class Program
    {
        static void Main(string[] args)
        {
            //Demostración de métodos extensores.
            string prueba = "murciélago";
            //¡OJO! Fíjate que, en este caso, no pasamos ningún parámetro.
            int vocalesSinTilde = prueba.ContarVocalesSinTilde();
            Console.WriteLine("El texto {0} contiene {1} vocales sin tilde", prueba, vocalesSinTilde);


            //Ejercicios
            //Crear un método extensor de string que, a partir de un texto, cuente las repeticiones de una letra (debe recibir la letra, obviamente).
            string prueba1 = "kjdfhwieuhwecnoijoaaaoiwjeoiewjgjnfosiipodfmcbbviuuuuveosdpe";
            var repesLetra = prueba1.RepeticionesLetra('a');
            Console.WriteLine($"Las veces que se repite la letra 'a' en la palabra {prueba1} es {repesLetra}");

            //Crear un método extensor de DateTime Estacion() que devuelva la estación (string). No es necesario utilizar nada de la práctica anterior.
            DateTime time = DateTime.Now;
            var estacion = time.MetodoEstacion();
            Console.WriteLine($"Estamos en la estacion del año {estacion}");

            //Crear un método extensor de vuestra clase Lista denominado Sum() que devuelva la suma de todos los elementos de la lista.            
            Lista l = new Lista();
            l.Añadir(133);
            l.Añadir(132);
            l.Añadir(133);
            l.Añadir(134);
            var suma = l.Sum();
            Console.WriteLine($"La suma de la lista es {suma}");


        }
    }
}
