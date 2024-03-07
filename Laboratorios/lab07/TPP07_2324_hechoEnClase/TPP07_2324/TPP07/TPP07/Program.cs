using System;
using Modelo;

using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace TPP07
{
    public static class Program
    {
        static void Main()
        {

            IEnumerable<int> valores = Enumerable.Range(1, 10);

            Console.WriteLine("Colecciones de enteros.");

            //Uso de métodos extensores
            //Map transforma una secuencia de <T>s en una secuencia de <Q>s.
            valores.Map(n => n * n).ForEach(Console.WriteLine);
            Console.WriteLine();

            valores.Map(n => n * n).Map(n => n / 2.0).ForEach(Console.WriteLine);
            Console.WriteLine();

            valores.Map(n => new Angulo(n)) // ESTEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE es muy importante porquw te estan mostranfdo como crear un angulo, vale tambien para perosnas, etc
                .Map(a => a.Grados)
                .ForEach(a => Console.WriteLine(a)); // .ForEach(Console.WriteLine) <- ¿Sabes el motivo?

            Console.WriteLine();

            Console.WriteLine("\nColecciones de Personas.");

            var personas = Factoria.CrearPersonas();

            var nombres = personas.Map(p => p.Nombre);
            nombres.ForEach(Console.WriteLine);
            Console.WriteLine();

            var iniciales = personas.Map(p => p.Nombre).Map(cadena => cadena[0]);
            iniciales.ForEach(Console.WriteLine);
            Console.WriteLine();
            personas.Map(p => p.Nif + "\t" + p.Nombre + "\t" + p.Apellido).ForEach(Console.WriteLine);


            //¿Qué estamos haciendo aquí?
            var info = personas.Map(p => new
            {
                Nombre = p.Nombre,
                Dni = p.Nif
            }
            );

            info.ForEach(Console.WriteLine);

            /* EJERCICIO:
            * - Añade el método Map a tu lista genérica.
            * - A partir de una lista de enteros: Calcula la suma de los cuadrados de la colección.
            * - A partir de una lista de Personas: Calcula la longitud media de los nombres de la colección.
            */
            var sumaCuadrados = valores.Map(s => Math.Pow(s, 2)).Map(x => x + x);

            Console.WriteLine("\n Suma de los cuadrados de una coleccion: ");
            sumaCuadrados.ForEach(Console.WriteLine);

            var longitudMedia = personas.Map(n => n.Nombre).Aggregate(0, (a, b) =>
            {
                return a += b.Length;
            });
            var r = longitudMedia / personas.Count();
            Console.WriteLine($"\nLongitud media de los nombres de las personas ... {r}\n");


            //Método ZIP de Linq: Combina dos secuencias:

            var combinación = valores.Zip(personas.Map(p => p.Nombre));
            combinación.Map(c => c.First + " : " + c.Second).ForEach(Console.WriteLine);

            /* EJERCICIO: Implementa el método Zip de LINQ:
             * - Colecciones potecialmente infinitas.
             * - Trabajará con tuplas .
             * */

            Console.WriteLine();
            Console.WriteLine("\nMetodo zip");

            var res = valores.MetodoZip(personas.Map(p => p.Nombre));
            res.Map(c => c.Item1 + " : " + c.Item2).ForEach(Console.WriteLine);


            Console.WriteLine();
            Console.WriteLine("Metodo zip lazy");

            var col = valores.MetodoZipLazy(personas.Map(p => p.Nombre));
            col.Map(c => c.Item1 + " : " + c.Item2).ForEach(Console.WriteLine);

        }
        public static IEnumerable<(T, Q)> MetodoZip<T, Q>(this IEnumerable<T> col1, IEnumerable<Q> col2)
        {
            IList<(T, Q)> lista = new List<(T, Q)>();
            IEnumerator<T> enum1 = col1.GetEnumerator();
            IEnumerator<Q> enum2 = col2.GetEnumerator();
            while (enum1.MoveNext() && enum2.MoveNext())
            {
                lista.Add((enum1.Current, enum2.Current));
            }
            return lista;

        }

        public static IEnumerable<(T, Q)> MetodoZipLazy<T, Q>(this IEnumerable<T> col1, IEnumerable<Q> col2, T uno = default(T), Q dos = default(Q))
        {
            IList<(T, Q)> lista = new List<(T, Q)>();
            IEnumerator<T> enum1 = col1.GetEnumerator();
            IEnumerator<Q> enum2 = col2.GetEnumerator();
            while (enum1.MoveNext() && enum2.MoveNext())
            {
                yield return (enum1.Current , enum2.Current);
            }

        }

        public static IEnumerable<(T, Q)> ZipLongest<T, Q>(this IEnumerable<T> seqLeft, IEnumerable<Q> seqRight, T defLeft = default, Q defRight = default)
        {
            using (var enumeratorLeft = seqLeft.GetEnumerator())
            using (var enumeratorRight = seqRight.GetEnumerator())
            {
                bool leftHasNext = true;
                bool rightHasNext = true;

                while (leftHasNext || rightHasNext)
                {
                    leftHasNext = enumeratorLeft.MoveNext();
                    rightHasNext = enumeratorRight.MoveNext();

                    yield return (
                        leftHasNext ? enumeratorLeft.Current : defLeft,
                        rightHasNext ? enumeratorRight.Current : defRight
                    );
                }
            }
        }

        public static IEnumerable<(T, Q)> ZipLongest<T, Q>(IEnumerable<T> seqLeft, IEnumerable<Q> seqRight, T defLeft = default, Q defRight = default)
        {
            var result = new List<(T, Q)>();

            using (var enumeratorLeft = seqLeft.GetEnumerator())
            using (var enumeratorRight = seqRight.GetEnumerator())
            {
                bool leftHasNext = true;
                bool rightHasNext = true;

                while (leftHasNext || rightHasNext)
                {
                    leftHasNext = enumeratorLeft.MoveNext();
                    rightHasNext = enumeratorRight.MoveNext();

                    result.Add((
                        leftHasNext ? enumeratorLeft.Current : defLeft,
                        rightHasNext ? enumeratorRight.Current : defRight
                    ));
                }
            }

            return result;
        }


    }


}
