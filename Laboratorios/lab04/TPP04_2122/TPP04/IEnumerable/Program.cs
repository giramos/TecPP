using System;
//Colecciones Genéricas
using System.Collections.Generic;

//Colecciones NO genéricas
//using System.Collections;

namespace IEnumerable
{
    class Program
    {
        static void Main(string[] args)
        {
            string palabra = "palabra";
            Console.WriteLine("Recorriendo palabra con foreach - IEnumerable");
            //Podemos usar el foreach porque String implementa IEnumerable
            foreach (var letra in palabra)
            {
                Console.Write(letra + " ");
            }

            Console.WriteLine("\nRecorriendo palabra con IEnumerator");
            //Realmente lo que se está haciendo es lo siguiente
            //IEnumerable tiene un único método y lo que hace éste es
            //Es devolver un IEnumerator (iterador)

            IEnumerator<char> iterador = palabra.GetEnumerator();
            while (iterador.MoveNext())
                Console.Write(iterador.Current + " ");


            //Nota: La tabla de multiplicar empieza multiplicando por 1.

            TablaMultiplicar tablaDel7 = new TablaMultiplicar(7, 10);
            Console.WriteLine("\nImplementación propia de IEnumerable e IEnumerator en una clase:");
            foreach (int elemento in tablaDel7)
                Console.WriteLine(elemento);

            // prueba del método MetodoExamen con string
            string palabra1 = "patata";
            string palabra2 = "patasa";
            IEnumerable<char> retornoPata = MetodoExamen<char>(palabra1, palabra2); // tipo char porque es el tipo de los elementos de dentro de la colección
            foreach (var letra in retornoPata) {
                Console.Write(letra);
            }


                      
        }

        /*
         * Crear un método que reciba dos colecciones que implementen IEnumerable<T> -> solo hay un tipo
         * Y devuelva como resultado un IEnumerable<T> con los valores que coincidan en la misma posición.
         * Resolver el ejercicio utilizando ITERADORES (IEnumerator).
         * Probar enviando:
         * Un array de caracteres y un string.
         * Una lista de caracteres y un string.
         * Vuestra lista con caracteres y otra lista vuestra con caracteres.
         * El resultado se recorre en un foreach y se imprime elemento a elemento.
        */
        public static IEnumerable<T> MetodoExamen<T>(IEnumerable<T> coleccion1, IEnumerable<T> coleccion2) {
            IList<T> resultado = new List<T>(); // porque List implementa IEnumerable

            // crear los iteradores
            IEnumerator<T> iteradorColeccion1 = coleccion1.GetEnumerator();
            IEnumerator<T> iteradorColeccion2 = coleccion2.GetEnumerator();

            while (iteradorColeccion1.MoveNext() && iteradorColeccion2.MoveNext()) {
                var current1 = iteradorColeccion1.Current;
                var current2 = iteradorColeccion2.Current;

                // si coinciden -> añadir
                if (current2.Equals(current1)) {
                    resultado.Add(current2);
                }  
            }

            IEnumerable<T> resultadoFinal = resultado;
            return resultadoFinal;

        }

        public IEnumerable<T> Metodo<T>(IEnumerable<T> a, IEnumerable<T> b)
        {
            IList<T> list = new List<T>();
            IEnumerator<T> col1 = a.GetEnumerator();
            IEnumerator<T> col2 = b.GetEnumerator();
            while (col1.MoveNext() && col2.MoveNext())
            {
                if (col1.Current.Equals(col2.Current))
                {
                    list.Add(col1.Current);
                }
            }
            return list;
        }
    }
}
