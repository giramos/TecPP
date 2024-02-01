using System;
using System.Collections.Generic;
using Modelo;
namespace BoxingIsAs
{
    class Program
    {
        static void Main(string[] args)
        {
            //Conversión automática de tipos integrados a objetos (Boxing) y viceversa (Unboxing):
            int entero = 5;
            //Boxing
            object enteroObjeto = entero;

            //¡Ojo! ¿Qué está ocurriendo?
            entero = 500;
            Console.WriteLine("Boxing entero: {0} . enteroObjeto: {1}", entero, enteroObjeto);

            //La línea inferio no compila, no existe una conversión implícita.
            //entero = enteroObjeto;
            //Unboxing
            entero = (int)enteroObjeto;
            Console.WriteLine("Unboxing {0}", entero);



            Console.WriteLine("\nUso del is:");
            //Uso del is
            Persona p1 = new Persona("Pepa", "Suárez", "1111B");
            Persona p2 = new Persona("Pepe", "Pérez", "0000A");

            //Devolverá true o false.
            if (p2 is Persona)
                Console.WriteLine("p2 es Persona.");

            object p3 = p1;
            if (p3 is Persona)
                Console.WriteLine("p3 es Persona.");


            Console.WriteLine("\nUso del as:");

            //Uso del as -> Si no se puede realizar la conversión, devuelve null.
            //Por tanto, es aplicable siempre y cuando el tipo objetivo pueda ser null.

            Persona p4 = p3 as Persona;
            if (p4 != null)
                Console.WriteLine("p3 es Persona.");
            //Recordemos que c era 5.
            Persona p5 = enteroObjeto as Persona;
            if (p5 != null)
                Console.WriteLine("p5 es Persona.");
            else
                Console.WriteLine("p5 no es Persona (null).");

            // ¿Qué ocurre si en lugar de usar as utilizamos un cast directamente?
            // ¿Realmente es buen ejemplo el propuesto para el operador as?

            //EJERCICIO: Crear un método privado y estático que reciba un array de object con objetos Persona y PuntoDeInteres
            //Devolverá un ArrayList (o List) de Persona con los objetos que sean de tipo persona y otro con los PuntoDeInteres
            object[] arr = new object[] { new Persona("a", "b", "c"), new Persona("d", "e", "f"), new PuntoDeInteres(1, 2, "a"), new PuntoDeInteres(3, 4, "b") };
            var res = Metodo(arr);
            foreach (var i in res.Item1) { Console.WriteLine($"Lista persona {i} "); }
            foreach (var i in res.Item2) { Console.WriteLine($"Lista punto {i} "); }
        }

        private static (List<Persona>, List<PuntoDeInteres>) Metodo(object[] array)
        {
            var listaPersona = new List<Persona>();
            var listaPunto = new List<PuntoDeInteres>();

            foreach (var i in array)
            {
                //Persona p = i as Persona;
                if (i is Persona)
                {
                    listaPersona.Add(i as Persona);
                }
                else
                {
                    listaPunto.Add(i as PuntoDeInteres);
                }
            }
            return (listaPersona, listaPunto);

        }
    }
}
