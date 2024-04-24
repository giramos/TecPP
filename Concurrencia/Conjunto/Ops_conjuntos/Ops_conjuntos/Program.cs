using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ops_conjuntos
{
    /// <summary>
    /// Uso de la collecion HashSet => similar al diccionario pero en este caso tan solo tenemos KEYS
    /// Consecuencia: no almacena valores repetidos
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            var setA = new HashSet<string> { "A", "B", "C" };
            var setB = new HashSet<string> { "C", "D", "E", "F" };

            Console.WriteLine("Ejemplo Union");
            var res = Union(setA, setB);
            res.ToList().ForEach(x => Console.Write(x + " "));

            Console.WriteLine("\nEjemplo Interseccion");
            res = Interseccion(setA, setB);
            res.ToList().ForEach(x => Console.Write(x + " "));

            Console.WriteLine("\nEjemplo Diferencia");
            res = Diferencia(setA, setB);
            res.ToList().ForEach(x => Console.Write(x + " "));

            Console.WriteLine("\nEjemplo Diferencia simetrica");
            res = DiferenciaSimetrica(setA, setB);
            res.ToList().ForEach(x => Console.Write(x + " "));

            Console.WriteLine("\n");

            string[] vectorA = { "A", "B", "C" };
            string[] vectorB = { "C", "D", "E", "F" };

            Stopwatch sw = new Stopwatch();
            sw.Start();
            HashSet<string> resultado = CalcularUnion(vectorA, vectorB);
            sw.Stop();
            resultado.ToList().ForEach(x => Console.Write(x + " "));
            Console.WriteLine("El tiempo en la union con foreach es {0} msg", sw.ElapsedMilliseconds);
            Console.WriteLine("\n");

            sw.Restart();
            resultado = CalcularUnionFor(vectorA, vectorB);
            sw.Stop();
            resultado.ToList().ForEach(x => Console.Write(x + " "));
            Console.WriteLine("El tiempo en la union con for es {0} msg", sw.ElapsedMilliseconds);
            Console.WriteLine("\n");

            sw.Restart();
            resultado = CalcularInterseccion(vectorA, vectorB);
            sw.Stop();
            resultado.ToList().ForEach(x => Console.Write(x + " "));
            Console.WriteLine("El tiempo en la interseccion con foreach es {0} msg", sw.ElapsedMilliseconds);
            Console.WriteLine("\n");

            sw.Restart();
            resultado = CalcularInterseccionFor(vectorA, vectorB);
            sw.Stop();
            resultado.ToList().ForEach(x => Console.Write(x + " "));
            Console.WriteLine("El tiempo en la interseccion con for es {0} msg", sw.ElapsedMilliseconds);
            Console.WriteLine("\n");

            sw.Restart();
            resultado = CalcularDiferencia(vectorA, vectorB);
            sw.Stop();
            resultado.ToList().ForEach(x => Console.Write(x + " "));
            Console.WriteLine("El tiempo en la diferencia con foreach es {0} msg", sw.ElapsedMilliseconds);
            Console.WriteLine("\n");

            sw.Restart();
            resultado = CalcularDiferenciaFor(vectorA, vectorB);
            sw.Stop();
            resultado.ToList().ForEach(x => Console.Write(x + " "));
            Console.WriteLine("El tiempo en la diferencia con for es {0} msg", sw.ElapsedMilliseconds);
            Console.WriteLine("\n");

            sw.Restart();
            resultado = CalcularDiferenciaSimetrica(vectorA, vectorB);
            sw.Stop();
            resultado.ToList().ForEach(x => Console.Write(x + " "));
            Console.WriteLine("El tiempo en la diferencia simetrica con foreach es {0} msg", sw.ElapsedMilliseconds);
            Console.WriteLine("\n");

            sw.Restart();
            resultado = CalcularDiferenciaSimetricaFor(vectorA, vectorB);
            sw.Stop();
            resultado.ToList().ForEach(x => Console.Write(x + " "));
            Console.WriteLine("El tiempo en la diferencia simetrica con for es {0} msg", sw.ElapsedMilliseconds);
            Console.WriteLine("\n");

            Console.WriteLine("\n");

            //sw.Restart();
            //var result = CalcularUnionPLINQ(vectorA, vectorB);
            //sw.Stop();
            //result.ToList().ForEach(x => Console.Write(x + " "));
            //Console.WriteLine($"El tiempo en la unión con PLINQ es {sw.ElapsedMilliseconds} ms");

            //sw.Restart();
            //result = CalcularInterseccionPLINQ(vectorA, vectorB);
            //sw.Stop();
            //result.ToList().ForEach(x => Console.Write(x + " "));
            //Console.WriteLine($"El tiempo en la intersección con PLINQ es {sw.ElapsedMilliseconds} ms");

            //sw.Restart();
            //result = CalcularDiferenciaPLINQ(vectorA, vectorB);
            //sw.Stop();
            //result.ToList().ForEach(x => Console.Write(x + " "));
            //Console.WriteLine($"El tiempo en la diferencia con PLINQ es {sw.ElapsedMilliseconds} ms");

            //sw.Restart();
            //result = CalcularDiferenciaSimetricaPLINQ(vectorA, vectorB);
            //sw.Stop();
            //result.ToList().ForEach(x => Console.Write(x + " "));
            //Console.WriteLine($"El tiempo en la diferencia simétrica con PLINQ es {sw.ElapsedMilliseconds} ms");

        }

        // INVOKE => ESQUELETO

        private static void CalcularOperacionesConjuntos(string[] vectorA, string[] vectorB, out HashSet<string> interseccion, out HashSet<string> diferencia, out HashSet<string> union, out HashSet<string> diferenciaSimetrica)
        {
            interseccion = new HashSet<string>();
            diferencia = new HashSet<string>();
            union = new HashSet<string>();
            diferenciaSimetrica = new HashSet<string>();

            Parallel.Invoke(
                () => CalcularInterseccion(vectorA, vectorB, interseccion),
                () => CalcularDiferencia(vectorA, vectorB, diferencia),
                () => CalcularUnion(vectorA, vectorB, union),
                () => CalcularDiferenciaSimetrica(vectorA, vectorB, diferenciaSimetrica)
            );
        }

        private static void CalcularInterseccion(string[] vectorA, string[] vectorB, HashSet<string> interseccion)
        {
            // Lógica para calcular la intersección entre vectorA y vectorB
            // Luego agregar los elementos resultantes a 'interseccion'
        }

        private void CalcularDiferencia(string[] vectorA, string[] vectorB, HashSet<string> diferencia)
        {
            // Lógica para calcular la diferencia entre vectorA y vectorB
            // Luego agregar los elementos resultantes a 'diferencia'
        }

        private void CalcularUnion(string[] vectorA, string[] vectorB, HashSet<string> union)
        {
            // Lógica para calcular la unión entre vectorA y vectorB
            // Luego agregar los elementos resultantes a 'union'
        }

        private void CalcularDiferenciaSimetrica(string[] vectorA, string[] vectorB, HashSet<string> diferenciaSimetrica)
        {
            // Lógica para calcular la diferencia simétrica entre vectorA y vectorB
            // Luego agregar los elementos resultantes a 'diferenciaSimetrica'
        }


        ///        // PLINQ


        private static HashSet<string> CalcularDiferenciaSimetricaPLINQ(string[] vectorA, string[] vectorB)
        {
            var conjuntoA = vectorA.ToHashSet();
            var conjuntoB = vectorB.ToHashSet();

            return conjuntoA.AsParallel().Except(conjuntoB).Concat(conjuntoB.AsParallel().Except(conjuntoA)).ToHashSet();
        }
        [Obsolete]
        private static HashSet<string> CalcularDiferenciaPLINQ(string[] vectorA, string[] vectorB)
        {
            var conjuntoA = vectorA.ToHashSet();
            var conjuntoB = vectorB.ToHashSet();

            return conjuntoA.AsParallel().Except(conjuntoB).ToHashSet();
        }

        [Obsolete]

        private static HashSet<string> CalcularInterseccionPLINQ(string[] vectorA, string[] vectorB)
        {
            var conjuntoA = vectorA.ToHashSet();
            var conjuntoB = vectorB.ToHashSet();

            return new HashSet<string>(conjuntoA.AsParallel().Intersect(conjuntoB));
        }
        [Obsolete]

        private static HashSet<string> CalcularUnionPLINQ(string[] vectorA, string[] vectorB)
        {
            var conjuntoA = vectorA.ToHashSet();
            var conjuntoB = vectorB.ToHashSet();

            return conjuntoA.AsParallel().Union(conjuntoB).ToHashSet();
        }



                            // TPL: FOR or FOREACH


        private static HashSet<string> CalcularDiferenciaSimetrica(string[] vectorA, string[] vectorB)
        {
            var res = new HashSet<string>();

            // Agregar todos los elementos de vectorA que no están en vectorB
            Parallel.ForEach(vectorA, i =>
            {
                if (!vectorB.Contains(i))
                {
                    lock (res)
                    {
                        res.Add(i);
                    }
                }
            });

            // Agregar todos los elementos de vectorB que no están en vectorA
            Parallel.ForEach(vectorB, j =>
            {
                if (!vectorA.Contains(j))
                {
                    lock (res)
                    {
                        res.Add(j);
                    }
                }
            });

            return res;
        }

        private static HashSet<string> CalcularDiferenciaSimetricaFor(string[] vectorA, string[] vectorB)
        {
            var res = new HashSet<string>();

            Parallel.For(0, vectorA.Length, i =>
            {
                var elementoA = vectorA[i];
                if (!vectorB.Contains(elementoA))
                {
                    lock (res)
                    {
                        res.Add(elementoA);
                    }
                }
            });

            Parallel.For(0, vectorB.Length, j =>
            {
                var elementoB = vectorB[j];
                if (!vectorA.Contains(elementoB))
                {
                    lock (res)
                    {
                        res.Add(elementoB);
                    }
                }
            });

            return res;
        }

        private static HashSet<string> CalcularDiferencia(string[] vectorA, string[] vectorB)
        {
            var res = new HashSet<string>();
            Parallel.ForEach(vectorA, i =>
            {
                lock (res)
                {
                    if (!vectorB.Contains(i))
                    {
                        res.Add(i);
                    }
                }
                
            });
            return res;
        }

        private static HashSet<string> CalcularDiferenciaFor(string[] vectorA, string[] vectorB)
        {
            var res = new HashSet<string>();

            Parallel.For(0, vectorA.Length, i =>
            {
                bool encontrado = false;

                // Comprobar si vectorA[i] está en vectorB
                foreach (var j in vectorB)
                {
                    if (j.Equals(vectorA[i]))
                    {
                        encontrado = true;
                        break;
                    }
                }

                // Si vectorA[i] no está en vectorB, agregarlo al resultado
                if (!encontrado)
                {
                    lock (res)
                    {
                        res.Add(vectorA[i]);
                    }
                }
            });

            return res;
        }


        private static HashSet<string> CalcularInterseccion(string[] vectorA, string[] vectorB)
        {
            var res = new HashSet<string>();
            Parallel.ForEach(vectorA, i =>
            {
                lock (res)
                {
                    if (vectorB.Contains(i))
                    {
                        res.Add(i);
                    }
                }
            });
            return res;
        }

        private static HashSet<string> CalcularInterseccionFor(string[] vectorA, string[] vectorB)
        {
            var res = new HashSet<string>();
            Parallel.For(0, vectorA.Length, i =>
            {
                string elementoA = vectorA[i];

                foreach (string elementoB in vectorB)
                {
                    if (elementoA.Equals(elementoB))
                    {
                        lock (res)
                        {
                            res.Add(elementoA);
                        }
                        break; // Una vez que se encuentra la intersección, no es necesario seguir buscando
                    }
                }
            });
            return res;
        }

        private static HashSet<string> CalcularUnion(string[] vectorA, string[] vectorB)
        {
            var res = new HashSet<string>();

            Parallel.ForEach(vectorA, i =>
            {
                lock (res)
                {
                    res.Add(i);
                }
            });

            Parallel.ForEach(vectorB, j =>
            {
                lock (res)
                {
                    res.Add(j);
                }
            });

            return res;
        }

        private static HashSet<string> CalcularUnionFor(string[] vectorA, string[] vectorB)
        {
            var res = new HashSet<string>();

            Parallel.For(0,vectorA.Length, i =>
            {
                lock (res)
                {
                    res.Add(vectorA[i]);
                }
            });

            Parallel.For(0,vectorB.Length, j =>
            {
                lock (res)
                {
                    res.Add(vectorB[j]);
                }
            });

            return res;
        }





                                    // SECUENCIAL


        //La unión de conjuntos A y B produce un tercer conjunto C con todos los elementos de A y B.
        //    Obvio, los elementos repetidos de A y B únicamente van a aparecer una vez.
        static IEnumerable<T> Union<T>(IEnumerable<T> colA, IEnumerable<T> colB)
        {
            return colA.ToHashSet().Union(colB);
        }

        //La intersección de conjuntos A y B produce un tercer conjunto con nada más los elementos 
        //    comunes de A y B.Esta es la operación opuesta a la diferencia simétrica.
        static IEnumerable<T> Interseccion<T>(IEnumerable<T> colA, IEnumerable<T> colB)
        {
            return colA.ToHashSet().Intersect(colB);
        }

        //La diferencia de conjuntos A y B retorna un tercer conjunto con los elementos existentes
        //    en A que no existen en B.
        static IEnumerable<T> Diferencia<T>(IEnumerable<T> colA, IEnumerable<T> colB)
        {
            return colA.ToHashSet().Except(colB);
        }

        //La diferencia simétrica de conjuntos A y B produce un tercer conjunto C con los 
        //    elementos que no son comunes en A y B.La operación opuesta a la intersección.
        static IEnumerable<T> DiferenciaSimetrica<T>(IEnumerable<T> colA, IEnumerable<T> colB)
        {
            var conjunto = colA.ToHashSet();
            conjunto.SymmetricExceptWith(colB);
            return conjunto;
        }
    }
}
