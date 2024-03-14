using System;
using System.Collections.Generic;
using System.Linq;
using Modelo;

namespace Linq
{
    class Program
    {

        private static Model modelo = new Model();

        static void Main()
        {
            

            Consulta1();
            Consulta2();
            Consulta3();
            Consulta4();
            Consulta5();
            Consulta6();
            Consulta7();


        }


        private static void Consulta1()
        {
            // Modificar la consulta para mostrar los empleados cuyo nombre empieza por F.

            //El resultado esperado es: Felipe
        }

        private static void Consulta2()
        {

            //Mostrar Nombre y fecha de nacimiento de los empleados de Cantabria con el formato:
            // Nombre=<Nombre>,Fecha=<Fecha>

            /*El resultado esperado es:
              Alvaro 19/10/1945 0:00:00
              Dario 16/12/1973 0:00:0066
            */
        }



        // A partir de aquí, necesitaréis métodos presentes en: https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable?view=net-5.0

        private static void Consulta3()
        {

            //Mostrar los nombres de los departamentos que tengan más de un empleado mayor de edad.




            /*El resultado esperado es:
                Computer Science
                Medicine
            */


            //Posteriormente, modifica la consulta para que:
            //Muestre los nombres de los departamentos que tengan más de un empleado mayor de edad
            //y
            //que el despacho (Office.Number) COMIENCE por "2.1"
            
            
            //El resultado esperado es: Medicine

        }

        private static void Consulta4()
        {

            //El nombre de los departamentos donde ningún empleado tenga despacho en el Building "Faculty of Science".
  
            //Resultado esperado: [Department: Mathematics]
        }

        private static void Consulta5()
        {


            // Mostrar las llamadas de teléfono de más de 5 segundos de duración para cada empleado que tenga más de 50 años
            //Cada línea debería mostrar el nombre del empleado y la duración de la llamada en segundos.
            //El resultado debe estar ordenado por duración de las llamadas (de más a menos).

            

            /*
                { Nombre = Eduardo, Duracion = 23 }
                { Nombre = Eduardo, Duracion = 22 }
                { Nombre = Alvaro, Duracion = 15 }
                { Nombre = Alvaro, Duracion = 10 }
                { Nombre = Felipe, Duracion = 7 }
            */

        }

        private static void Consulta6()
        {
            //Mostrar la llamada realizada más larga para cada profesor, mostrando por pantalla: Nombre_profesor : duracion_llamada_mas_larga

           


            /* ¡OJO NO ESTÁ APLICADO EL FORMATO 
                { Nombre = Alvaro, Maxima = 15 }
                { Nombre = Bernardo, Maxima = 63 }
                { Nombre = Eduardo, Maxima = 23 }
                { Nombre = Felipe, Maxima = 7 }
             */
        }


        private static void Consulta7()
        {
            // Mostrar, agrupados por provincia, el nombre de los empleados
            //Tanto la provincia como los empleados deben estar ordenados alfabéticamente

           

            /*Resultado esperado:
                Alicante : Carlos
                Asturias : Bernardo Felipe
                Cantabria : Alvaro Dario               
                Granada : Eduardo

            */
        }
        //Mostrar, ordenados por edad, los nombres de los empleados pertenecientes al
        //departamento de “Computer Science” que tienen un despacho en la “Faculty of Science” y
        //que han hecho llamadas con duración superior a 1 minuto.
        private static void ConsultaK()
        {
            

        }

        //Mostrar la suma en segundos de las llamadas hechas por los empleados del departamento
        //de “Computer Science”
        private static void ConsultaL()
        {
            

        }

        // Mostrar las llamadas de teléfono hechas por cada departamento, ordenadas por el
        // nombre del departamento.Cada línea debe tener la siguiente estructura:
        // “Departamento=<Nombre>, Duración=<Segundos>”
        private static void ConsultaM()
        {
            
        }

        // Mostrar el departamento que tenga la mayor duración de llamadas telefónicas (en
        // segundos), sumando la duración de las llamadas de todos los empleados que
        // pertenecen al mismo. Mostrar también el nombre de dicho departamento(puede
        // suponerse que solo hay un departamento que cumplirá esta condición.
        private static void ConsultaN()
        {
            
        }
        //•	Para cada edificio, obtener la edad máxima, la edad mínima
        //y el numero de empleados que tienen despacho en ese edificio.
        private static void ConsultaÑ()
        {
            
        }
        
        // Mostrar las llamadas realizadas desde el edificio "Faculty of Science" al edificio "Polytechnical".
        // Debe mostrarse por pantalla el número de origen, el número de destino y la duración de la llamada.
        private static void ConsultaO()
        {
            
        }

        //lista de empleados que no realizan ninguna llamada saliente.
        private static void ConsultaP()
        {
            
        }
        // Mostrar la edad media de los empleados. Por cada departamento
        private static void ConsultaQ()
        {

        }
        //Mostrar los departamentos con el empleado más joven, además del nombre dicho
        //empleado más joven y su edad.Tened en cuenta que puede existir más de un empleado más
        //joven.
        private static void ConsultaR()
        {

        }

        // Numero de llamadas por cada provincia
        private static void Show<T>(IEnumerable<T> colección)
        {
            foreach (var item in colección)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Elementos en la colección: {0}.", colección.Count());
        }
        private static void Limpiar()
        {
            Console.WriteLine("Pulse una tecla para continuar la ejecución...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}