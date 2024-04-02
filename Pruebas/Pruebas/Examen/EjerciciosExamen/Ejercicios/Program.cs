using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.Intrinsics.X86;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography;

namespace Ejercicios
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Ejercicio 1 => Vocales

            Console.WriteLine("Ejercicio 1 => Vocales");
            Console.WriteLine();

            var res = "germanGerman".Vocales();

            Console.WriteLine("A=>" + res[0]);
            Console.WriteLine("E=>" + res[1]);
            Console.WriteLine("I=>" + res[2]);
            Console.WriteLine("O=>" + res[3]);
            Console.WriteLine("U=>" + res[4]);

            // Ejercicio 2 => Vocales Clausulas

            Console.WriteLine();
            Console.WriteLine("Ejercicio 2 => Vocales Clausulas");
            Console.WriteLine();

            var res1 = ClausulaVocales("germanGerman");
            Console.WriteLine("A=>" + res1());
            Console.WriteLine("E=>" + res1());
            Console.WriteLine("I=>" + res1());
            Console.WriteLine("O=>" + res1());
            Console.WriteLine("U=>" + res1());

            Console.WriteLine();
            Console.WriteLine("Ejercicio 2 => Vocales Clausulas");
            Console.WriteLine();

            string cadena = "Mensajero";

            var funcion = ClausulaVocales(cadena);

            // Invocamos 5 veces a la función devuelta
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(funcion());
            }

            // Ejercicio 3 => Funcion de orden superior

            Console.WriteLine();
            Console.WriteLine("Ejercicio 3 => Funcion de orden superior");
            Console.WriteLine();

            var array = Enumerable.Range(0, 1000);
            var predicados = new Predicate<int>[] {

                x => !x.ToString().EndsWith("0"), x => x% 2 != 0

            };
            var res2 = FuncionOrdenSuperior(array, predicados);
            foreach (var i in res2)
            {
                Console.WriteLine("Nº incumplidos: " + i.Key);
                Console.WriteLine("Numeros que los incumplen");
                foreach (var j in i.Value)
                {
                    Console.WriteLine(j);
                }
                Console.WriteLine();
            }

            // Ejercicio 3

            //Random random = new Random();

            //// Generar un array de 1000 palabras de longitud 2-4 caracteres
            //string[] words = new string[1000];
            //for (int i = 0; i < words.Length; i++)
            //{
            //    int length = random.Next(2, 5); // Longitud aleatoria entre 2 y 4 caracteres
            //    string word = new string(Enumerable.Range(0, length).Select(_ => (char)random.Next('a', 'z' + 1)).ToArray());
            //    words[i] = word;
            //}

            //// Predicados para verificar si la longitud de la palabra es impar y si es igual a 4
            //Predicate<string> oddLengthPredicate = s => s.Length % 2 != 0;
            //Predicate<string> lengthEquals4Predicate = s => s.Length == 4;

            //// Procesar las palabras con los predicados
            //var result = FuncionOrdenSuperior(words,new[] { oddLengthPredicate, lengthEquals4Predicate });

            //// Mostrar el resultado
            //foreach (var kvp in result)
            //{
            //    Console.WriteLine($"Número de predicados incumplidos: {kvp.Key}");
            //    Console.WriteLine("Palabras que incumplen estos predicados:");
            //    foreach (var item in kvp.Value)
            //    {
            //        Console.WriteLine(item);
            //    }
            //    Console.WriteLine();
            //}

            //            Codifica un método genérico(NumberOfPrecededBy) que tome como parámetro tu propia lista 
            //                genérica(o haz que el método sea un miembro o un método de extensión) y un predicado
            //                .Devuelve cuántos de los elementos son precedidos por un elemento que verifica el predicado.
            //                Utiliza solo tus métodos Filter, Map y Reduce(quizás no necesites todos ellos).
            //                Evita la iteración. Pista: el primer elemento es precedido por default(T), 
            //                recuerda actualizar el elemento precedente.
            //                Utiliza una cadena adecuada de métodos o ten en cuenta que la forma corta 
            //                de la lambda Reduce habitual puede expandirse de esta manera:

            //(list of parameters)=>
            //{
            //                if (condition)
            //                    hacer algo
            //                else
            //                    hacer algo más
            //                haz esto siempre
            //    return acumulador
            //}

            //        Ejemplo: si los datos son { 2, 3, 2, 2, 3, 3, 3, 3, 4, 2}
            //            y contamos cuántos de los elementos son precedidos por un número par, el resultado es 5
            //                (por defecto para int es 0, por lo tanto, el primer 2 es precedido por 0).
            //                Utiliza los datos proporcionados con un predicado adecuado en la llamada, muestra el resultado.

            // Ejercicio 4 => Extensor de INT

            Console.WriteLine();
            Console.WriteLine("Ejercicio 4 => Extensor de INT");
            Console.WriteLine();

            var arr = new Func<int, int>[]
            {
                 a => a + 10,
                 b => b + 10,
                 c => c + 10
            };

            var res4 = 10.ExtensorInt(arr, 50);
            Console.Write(res4);

            // Ejercicio 5 => Currifica el metodo anterior

            Console.WriteLine();
            Console.WriteLine("Ejercicio 5 => curry ");
            Console.WriteLine();

            var res5 = ExtensorIntCurry(10);
            Console.WriteLine(res5(arr, 50));
            

        }

        //Ejercicio 2.Sin borrar el código anterior, implemente una funcionalidad similar empleando cláusulas: 
        //public static Func<int> ClausulaVocales(string cadena).
        //Cada vez que se invoque a la función devuelta, irá devolviendo un entero con la distribución de
        //la vocal actual(se irán devolviendo en orden: a, e, i, o, u; y vuelta a empezar). Por tanto, si
        //probamos con ClasulaVocales(“Mensajero”) e invocamos 5 veces a la función devuelta: 
        //obtendremos los valores: 1, 2, 0, 1, 0
        public static Func<int> ClausulaVocales(string cadena)
        {
            IList lista = new ArrayList();
            int a = 0, e = 0, i = 0, o = 0, u = 0;
            int n = 0;
            foreach (var p in cadena)
            {
                if (p is 'a') a++;
                else if (p is 'e') e++;
                else if (p is 'i') i++;
                else if (p is 'o') o++;
                else if (p is 'u') u++;
                else;
            }
            lista.Add(a);
            lista.Add(e);
            lista.Add(i);
            lista.Add(o);
            lista.Add(u);
            return () =>
            {
                int res = (int)lista[n];
                n++;
                return res;
            }
            ;
        }

        public static Func<int> ClausulaVocales1(string cadena)
        {
            IList<int> lista = new List<int>(new int[5]);
            int n = 0;
            foreach (char i in cadena)
            {
                if ("aeiouAEIOU".Contains(i))
                {
                    int indice = "aeiouAEIOU".IndexOf(i);
                    lista[indice]++;
                }
            }
            return () =>
            {
                var res = lista[n];
                n++;
                return res;
            };

        }

        // Ejercicio 3. Impleméntese una función de orden superior genérica que reciba(1,75 puntos) :
        // Un IEnumerable de Predicados
        // Un IEnumerable de elementos
        //Devolverá un IDictionary con la siguiente estructura:
        // Como claves utilizará el número de predicados incumplidos.
        // Para cada entrada tendrá una IList con los elementos que han incumplido ese total de
        //predicados.
        //A través de un proyecto de test, pruébese la funcionalidad del método empleando un array de
        //1000 palabras de longitud 2-4 caracteres y usando de predicados: uno para saber si la longitud
        //de la palabra es impar y otro para saber si la longitud de la palabra es igual que 4 (0,75 puntos).
        //
        public static IDictionary<int, IList<T>> FuncionOrdenSuperior<T>(IEnumerable<T> vector, IEnumerable<Predicate<T>> cond)
        {
            IDictionary<int, IList<T>> dicc = new Dictionary<int, IList<T>>();
            int n = 0;
            foreach (var i in vector)
            {
                foreach (var j in cond)
                {
                    if (j(i) == false)
                    {
                        if (dicc.ContainsKey(n))
                        {
                            dicc[n++].Add(i);

                        }

                        dicc[n] = new List<T>();
                    }
                }
            }
            return dicc;
        }

        public static Func<Func<int, int>[], int, int> ExtensorIntCurry(Int32 entero)
        {
            int res = 0;
            return (array,maximo) => 
            {
                res = entero;
                foreach (var p in array)
                {
                    res += p(res);
                    if (res > maximo) break;
                }
                return res;
            };
        }

    }

    public static class Extensor
    {
        //        Ejercicio 1. Impleméntese un método Vocales() extensor de la clase String que devuelva la
        //distribución de vocales en una lista(empleando la de.NET) (1,00 puntos). Por tanto,
        //“Mensajero”.Vocales() devolverá una lista con 5 entradas(una por vocal) indicando cuántas
        //veces se repite cada vocal.

        public static IList<int> Vocales(this string cadena)
        {
            IList<int> lista = new List<int>(new int[5]);
            foreach (char i in cadena)
            {
                if ("aeiouAEIOU".Contains(i))
                {
                    int indice = "aeiouAEIOU".IndexOf(i);
                    lista[indice]++;
                }
            }
            return lista;
        }

        public static IList Vocales1(this string cadena)
        {
            IList lista = new ArrayList();
            int a = 0, e = 0, i = 0, o = 0, u = 0;
            foreach (var p in cadena)
            {
                if (p is 'a') a++;
                else if (p is 'e') e++;
                else if (p is 'i') i++;
                else if (p is 'o') o++;
                else if (p is 'u') u++;
                else;
            }
            lista.Add(a);
            lista.Add(e);
            lista.Add(i);
            lista.Add(o);
            lista.Add(u);
            return lista;
        }

        //        Ejercicio 1. Implemente un método extensor Aplicacion de la clase Int32 que reciba:
        // Array de funciones con parámetro y retorno de tipo int.
        // Una variable “máximo” de tipo int.
        //La función deberá devolver el resultado de ir aplicando cada una de las funciones al resultado
        //de haber aplicado la anterior(a la primera función se le pasará el propio número). Si durante el
        //proceso se supera el valor de la variable “máximo”, se devolverá el resultado acumulado hasta
        //el momento.Pruébese el funcionamiento del método con 3 funciones más o menos simples
        //(1,75 puntos).
        //Basándose en la misma funcionalidad del anterior y partiendo de un método
        //AplicacionCurrificada que recibirá los 3 parámetros anteriores(contando el propio número)
        //impleméntese la versión currificada y realícense las pruebas que se consideren adecuadas para
        //demostrar la utilidad de la currificación(0,75 puntos).
        public static int ExtensorInt(this Int32 entero, Func<int, int>[] array, int maximo)
        {
            int res = entero;
            foreach (var p in array)
            {
                res += p(res);
                if (res > maximo) break;
            }
            return res;
        }

    }
}
