using ListaGenerica;
using System;
using System.Collections.Generic;
using System.Xml.Linq;


namespace Ejercicio1
{

    public class Program
    {
        static void Main(string[] args)
        {
            Predicate<int>[] arr = {
                a => a%2==0,
                a => a.ToString().Length < 2
            };
            Cola<int> cola = new (arr);
            Console.WriteLine(cola.Encolar(2));//true
            Console.WriteLine(cola.Encolar(1));//false
            Console.WriteLine(cola.Encolar(10));//false
            Console.WriteLine(cola.Encolar(8));//true
            Console.WriteLine(cola.Encolar(7));//false
            Console.WriteLine(cola.Encolar(33));//false
            Console.WriteLine(cola.EstaVacia);//false
            
        }
    }

    //public class Cola<T>: Lista<T>
    //{

    //    public bool EstaVacia { get
    //        {
    //            return NumElementos == 0 ? true : false;
    //        } }
    //    private readonly IEnumerable<Predicate<T>> predicates;

    //    public Cola(IEnumerable<Predicate<T>> predicates1)
    //    {
    //        predicates = predicates1;
    //    }

    //    public bool Encolar(T elemento)
    //    {
    //        bool exito = false;
    //        foreach (var predicate in predicates)
    //        {
    //            if (!predicate(elemento))
    //            {
    //                return false;
    //            }
    //        }
    //        Añadir(elemento);
    //        return true;
    //    }

    //    public void Desencolar()
    //    {
    //        var antiguo = GetElemento(0);
    //        Borrar(antiguo);
    //    }

    //}

    public class Cola<T> 
    {
        private Lista<T> lista;
        public bool EstaVacia
        {
            get
            {
                return lista.NumElementos == 0 ? true : false;
            }
        }
        IEnumerable<Predicate<T>> Condiciones { get; }

        public Cola(IEnumerable<Predicate<T>> predicates1)
        {
            lista = new Lista<T>();
            Condiciones = predicates1;
        }

        public bool Encolar(T elemento)
        {
            bool exito = true; // Suponemos éxito hasta que se demuestre lo contrario
            foreach (var predicate in Condiciones)
            {
                if (!predicate(elemento))
                {
                    exito = false; // Si el elemento no cumple con un predicado, no es un éxito
                    break; // No hay necesidad de verificar más predicados si uno falla
                }
            }
            if (exito)
            {
                lista.Añadir(elemento); // Añadimos el elemento si todos los predicados se cumplen
            }
            return exito;
        }

        public void Desencolar()
        {
            var antiguo = lista.GetElemento(0);
            lista.Borrar(antiguo);
        }

    }


}
