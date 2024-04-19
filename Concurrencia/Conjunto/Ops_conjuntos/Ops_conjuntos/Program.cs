using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
            var res = Union(setA,setB);
            res.ToList().ForEach(x => Console.Write(x + " "));

            Console.WriteLine("\nEjemplo Interseccion");
            res = Interseccion(setA,setB);
            res.ToList().ForEach(x => Console.Write(x + " "));

            Console.WriteLine("\nEjemplo Diferencia");
            res = Diferencia(setA,setB);
            res.ToList().ForEach(x => Console.Write(x + " "));

            Console.WriteLine("\nEjemplo Diferencia simetrica");
            res = DiferenciaSimetrica(setA,setB);
            res.ToList().ForEach(x => Console.Write(x + " "));
        }

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
