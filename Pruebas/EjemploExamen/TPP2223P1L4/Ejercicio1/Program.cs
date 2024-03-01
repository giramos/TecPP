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
            Console.WriteLine(cola.Encolar(2));
            Console.WriteLine(cola.EstaVacia);
        }
    }

    public class Cola<T>: Lista<T>
    {

        public bool EstaVacia { get
            {
                return NumElementos == 0 ? true : false;
            } }
        private readonly IEnumerable<Predicate<T>> predicates;

        public Cola(IEnumerable<Predicate<T>> predicates1)
        {
            predicates = predicates1;
        }

        public bool Encolar(T elemento)
        {
            bool exito = false;
            foreach (var predicate in predicates)
            {
                if (!predicate(elemento))
                {
                    return false;
                }
            }
            Añadir(elemento);
            return true;
        }

        public void Desencolar()
        {
            var antiguo = GetElemento(0);
            Borrar(antiguo);
        }

    }

  
}
