using Modelo;
using System;
using System.Collections.Generic;

namespace MetodosExtensores
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("");
            var personas = Factoria.CrearPersonas();
            var angulos = Factoria.CrearAngulos();
            foreach (var i in personas) { Console.Write(i.ToString() + " "); }
            //foreach (var i in angulos) { Console.WriteLine(i.Grados.ToString() + " " + i.Radianes.ToString() + ""); }

            Console.WriteLine("Buscar en la lista de personas alguien que se llame maria");
            var res = personas.Buscar(x => x.Nombre.Equals("María"));
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(res.ToString());

            Console.WriteLine("");
            Console.WriteLine("");
            
            Console.WriteLine("Buscar en la lista de personas alguien que se llame maria y su dni termine en D");
            res = personas.Buscar(x => x.Nombre.Equals("María") && x.Nif.EndsWith('D'));
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(res?.ToString() ?? "El resultado es null");

            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("Buscar en la lista de angulos rectos y en el primer cuadrante");
            var res1 = angulos.Buscar(x => x.Grados.Equals(90) && x.Radianes > 1);
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine(res1.Grados.ToString() + " " + res1.Radianes.ToString());

            Console.WriteLine("Devolver una lista de personas alguien que se llame maria");
            var res2 = personas.Filtrar(x => x.Nombre.Equals("María"));
            Console.WriteLine("");
            Console.WriteLine("");
            foreach(var i in res2) {  Console.WriteLine(i.ToString()); }

            Console.WriteLine("");
            Console.WriteLine("");

            //Console.WriteLine("Devolver una lista de personas alguien que se llame maria y su dni termine en D");
            //res2 = personas.Filtrar(x => x.Nombre.Equals("María") && x.Nif.EndsWith('D'));
            //Console.WriteLine("");
            //Console.WriteLine("");
            //foreach (var i in res2) { Console.WriteLine(i.ToString()); }

            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("Devolver una lista de angulos rectos y en el primer cuadrante");
            var res3 = angulos.Filtrar(x => x.Grados.Equals(90) && x.Radianes > 1);
            Console.WriteLine("");
            Console.WriteLine("");
            foreach (var i in res3) { Console.WriteLine(i.Grados.ToString() + " " + i.Radianes.ToString()); }

            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("Suma de los años de la coleccion de Marias");
            var res4 = res2.Reduce<Persona,double>((a, b) => b.Edad + a);
            foreach(var i in res2) { Console.WriteLine(i.Edad); }
            Console.WriteLine(res4);

            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("Suma de los grados de la coleccion de angulos");
            res4 = angulos.Reduce<Angulo, double>((a, b) => b.Grados + a);
            foreach (var i in angulos) { Console.WriteLine(i.Grados); }
            Console.WriteLine(res4);

            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("Seno maximo de los grados de la coleccion de angulos");
            res4 = angulos.Reduce<Angulo, double>((a, b) => b.Seno() > a ? b.Seno() : a);
            foreach (var i in angulos) { Console.WriteLine(i.Seno()); }
            Console.WriteLine(res4);

            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("Diccionario 'nombre':veces");
            var res5 = personas.Reduce(new Dictionary<string, int>(), (dicc, persona) =>
            {
                if (!dicc.ContainsKey(persona.Nombre))
                {
                    dicc[persona.Nombre]= 1;

                }
                else
                {
                    
                    dicc[persona.Nombre]++;
                }
                return dicc;
            });
            foreach(var i in res5)
            {
                Console.WriteLine($"Clave:{i.Key} - Valor:{i.Value}");
            }

        }
    }

    public static class Extensores
    {
        /// <summary>
        /// Buscar: A partir de una colección de elementos, nos devuelve el primero que cumpla
        /// un criterio dado o su valor por defecto en el caso de no existir ninguno.
        /// </summary>
        public static T Buscar<T>(this IEnumerable<T> col, Predicate<T> con)
        {
            foreach (T t in col)
            {
                if (con(t)) 
                {
                    return t;
                }
            }
            return default(T);
        }

        /// <summary>
        /// Filtrar: A partir de una colección de elementos, nos devuelve todos aquellos que
        /// cumplan un criterio dado(siendo éste parametrizable)
        /// </summary>
        public static IEnumerable<T> Filtrar<T>(this IEnumerable<T> col, Predicate<T> cond)
        {
            IList<T> list = new List<T>();
            foreach (T t in col)
            {
                if (cond(t))
                {
                    list.Add(t);
                }
            }
            return list;
        }

        /// <summary>
        /// Reduce:  Esta función recibe una colección de elementos y una función que recibe un
        /// primer parámetro del tipo que queremos obtener y un segundo parámetro del tipo
        /// que queremos obtener; su tipo devuelto es el propio del que queremos obtener.
        /// </summary>
        /// 
        public static TAccumulate Reduce<TSource, TAccumulate>(this IEnumerable<TSource> col, Func<TAccumulate, TSource, TAccumulate> func)
        {
            TAccumulate seed = default(TAccumulate);
            foreach(TSource t in col)
            {
                seed = func(seed,t);
            }
            return seed;
        }

        public static TSource Reduce<TSource>(this IEnumerable<TSource> col, Func<TSource, TSource, TSource> func)
        {
            TSource seed = default(TSource);
            foreach (TSource t in col)
            {
                seed = func(seed, t);
            }
            return seed;
        }

        public static TAccumulate Reduce<TSource, TAccumulate>(this IEnumerable<TSource> col,TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
        {
            //seed = default(TAccumulate); // si lo descomentas dara error porque se reiniciara el acumulador en cada iteracion que hagas
            foreach (TSource t in col)
            {
                seed = func(seed, t);
            }
            return seed;
        }

        public static TResult Reduce<TSource, TAccumulate, TResult>(this IEnumerable<TSource> col, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, TResult> resultSelector)
        {
            //seed = default(TAccumulate); // si lo descomentas dara error porque se reiniciara el acumulador en cada iteracion que hagas
            foreach (TSource t in col)
            {
                seed = func(seed, t);
            }
            return resultSelector(seed);
        }
    }
}
