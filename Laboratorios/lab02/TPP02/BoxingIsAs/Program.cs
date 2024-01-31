using System;
using System.Collections;
using Microsoft.VisualBasic;
using Modelo;
namespace BoxingIsAs
{
    class Program
    {
        static void Main(string[] args)
        {
            //Conversión automática de tipos integrados a objetos (Boxing) y viceversa (Unboxing):
            int a = 5;
            //Boxing
            Int32 b = a;
            Object c = b;
            Console.WriteLine("Boxing {0}", c);

            //Unboxing
            a = b;
            Console.WriteLine("Unboxing {0}", a);
            a = (int)c;
            Console.WriteLine("Unboxing {0}", a);

            //En métodos:
            Int32 numero = 20;
            int resultado = AutoBoxing(numero);
            Console.WriteLine("Resultado de Autoboxing: {0}", resultado);



            Console.WriteLine("\nUso del is:");
            //Uso del is
            Persona p1 = new Persona("Pepa", "Suárez", "1111B");
            Persona p2 = new Persona("Pepe", "Pérez", "0000A");

            //Devolverá true o false.
            if (p2 is Persona)
                Console.WriteLine("p2 es Persona.");

            Object p3 = p1;
            if (p3 is Persona)
                Console.WriteLine("p3 es Persona.");


            Console.WriteLine("\nUso del as:");

            //Uso del as -> Si no se puede realizar la conversión, devuelve null.
            //Por tanto, es aplicable siempre y cuando el tipo objetivo pueda ser null.

            Persona p4 = p3 as Persona;
            if (p4 != null)
                Console.WriteLine("p3 es Persona.");
            //Recordemos que c era 5.
            Persona p5 = c as Persona;
            if (p5 != null)
                Console.WriteLine("p5 es Persona.");
            else
                Console.WriteLine("p5 no es Persona (null).");

            // ¿Qué ocurre si en lugar de usar as utilizamos un cast directamente?
            //el programa podría lanzar una excepción si la conversión no es posible. 
            //    una excepción de tipo InvalidCastException
            // ¿Realmente es buen ejemplo el propuesto para el operador as?
            //Sí, el ejemplo proporcionado para el operador as es adecuado.Muestra que el operador as es útil cuando se intenta convertir un objeto a un tipo específico y,
            //si la conversión no es posible, en lugar de lanzar una excepción, devuelve null

            object[] arr = new object[] { new Persona("a", "b", "c"), new Persona("d", "e", "f"), new PuntoDeInteres(1.0, 2.0, "a") };
            var res = Metodo(arr);
            foreach(var i in res)
            {
                Console.WriteLine("array res: " + i);
            }
        }


        // Ejercicio para casa
        //EJERCICIO: Crear un método privado y estático que reciba un array de object con objetos Persona y PuntoDeInteres
        //Devolverá un array de Persona con los objetos que sean de tipo persona.


        private static Persona[] Metodo(object[] array)
        {
            Persona[] arrayreturn = new Persona[array.Length];
            int cont = 0;
            foreach (var a in array)
            {
                if (a is Persona)
                {
                    arrayreturn[cont] = (Persona)a;
                    cont++; 
                }
                //Persona p = a as Persona;
                //if (p != null)
                //{
                //    arrayreturn[cont] = p;
                //    cont++;
                //}
                //else
                //{
                //    cont++;
                //}
            }
            return arrayreturn;
        }

        /// <summary>
        /// Box y Unbox.
        /// </summary>
        /// <param name="objeto">Recibe un objeto Int32.
        /// Si se recibe un int como argumento, se convierte (Boxing) automáticamente a Int32.</param>
        /// <returns>Devuelve un int convirtiendo automáticamente y forma transparente
        /// el objeto Int32 a int (Unboxing)</returns>
        private static int AutoBoxing(Int32 objeto)
        {
            return objeto;
        }
    }
}
