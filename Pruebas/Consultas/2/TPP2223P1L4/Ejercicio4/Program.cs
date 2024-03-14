﻿using System;
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


            //Consulta1();
            //Consulta2();
            //Consulta3();
            //Consulta4();
            //Consulta5();
            //Consulta6();
            Consulta7();
            ConsultaK();
            ConsultaL();
            ConsultaM();
            ConsultaÑ();
            ConsultaO();
            ConsultaP();
            ConsultaQ();
            ConsultaR();
            ConsultaS();


        }


        private static void Consulta1()
        {
            // Modificar la consulta para mostrar los empleados cuyo nombre empieza por F.
            var res = modelo.Employees.Where(e => e.Name.StartsWith('F')).Select(e => e.Name);
            Show(res);

            //El resultado esperado es: Felipe
        }

        private static void Consulta2()
        {

            //Mostrar Nombre y fecha de nacimiento de los empleados de Cantabria con el formato:
            // Nombre=<Nombre>,Fecha=<Fecha>
            Console.WriteLine("Consulta2");
            var res = modelo.Employees.Where(e => e.Province.Equals("Cantabria")).Select(e => $"{e.Name} {e.DateOfBirth}");
            Show(res);

            /*El resultado esperado es:
              Alvaro 19/10/1945 0:00:00
              Dario 16/12/1973 0:00:0066
            */
        }



        // A partir de aquí, necesitaréis métodos presentes en: https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable?view=net-5.0

        private static void Consulta3()
        {

            //Mostrar los nombres de los departamentos que tengan más de un empleado mayor de edad.

            var res = modelo.Departments.Where(e => e.Employees.Where(e => e.Age >= 18).Count() > 1).Select(d => d.Name);
            Console.WriteLine("Consulta 3:");
            Show(res);

            /*El resultado esperado es:
                Computer Science
                Medicine
            */


            //Posteriormente, modifica la consulta para que:
            //Muestre los nombres de los departamentos que tengan más de un empleado mayor de edad
            //y
            //que el despacho (Office.Number) COMIENCE por "2.1"
            res = modelo.Departments.Where(e => e.Employees.Where(e => e.Age >= 18 && e.Office.Number.StartsWith("2.1")).Count() > 1).Select(d => d.Name);
            Console.WriteLine("Consulta 3 bis:");
            Show(res);

            //El resultado esperado es: Medicine

        }

        private static void Consulta4()
        {

            //El nombre de los departamentos donde ningún empleado tenga despacho
            //en el Building "Faculty of Science".

            var res = modelo.Departments.Where(e => e.Employees.Where(e => e.Office.Building.ToLower().Equals("faculty of science")).Count() < 1);
            Console.WriteLine("Consulta 4:");
            Show(res);
            //Resultado esperado: [Department: Mathematics]
        }

        private static void Consulta5()
        {


            // Mostrar las llamadas de teléfono de más de 5 segundos de duración para cada empleado
            // que tenga más de 50 años
            //Cada línea debería mostrar el nombre del empleado y la duración de la llamada en segundos.
            //El resultado debe estar ordenado por duración de las llamadas (de más a menos).
            
            var res = modelo.Employees.Join(modelo.PhoneCalls,
                e => e.TelephoneNumber,
                p => p.SourceNumber,
                (em, ph) => new
                {
                    Empleado = em,
                    Llamada = ph.Seconds
                }).Where(e=> e.Empleado.Age > 50 && e.Llamada > 5 );
            Show(res);


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

            var res = modelo.Employees.Join(modelo.PhoneCalls,
                e => e.TelephoneNumber,
                p => p.SourceNumber,
                (em, ph) => new
                {
                    Empleado = em,
                    Llamada = ph
                }).GroupBy(e => e.Empleado, (key, llam) => new
                {
                    Nombre = key.Name,
                    Maxima = llam.Select(l => l.Llamada.Seconds).Aggregate(0, (acc, b) => acc > b ? acc : b)
                }).Select(e => new
                {
                    e.Nombre,
                    e.Maxima
                });
            Show(res);


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

            var res = modelo.Employees.GroupBy(e => e.Province, (key, em) => new
            {
                Provincia = key,
                Nombre = em.Select(e=>e.Name).Aggregate("", (acc, b) => acc + " " + b)
            }).OrderBy(a=>a.Provincia).Select(e => $"{e.Provincia} : {e.Nombre}");
            Show(res);

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
            var emp = modelo.Employees.Where(e => e.Department.Name.Equals("Computer Science") && e.Office.Building.Equals("Faculty of Science"));
            var llam = modelo.PhoneCalls.Where(p => p.Seconds > 60);
            var res = emp.Join(llam,
                e => e.TelephoneNumber,
                l => l.SourceNumber,
                (em, ll) => new
                {
                    Nombre = em.Name,
                    Llamada = ll.Seconds,
                    Edad = em.Age
                }).OrderBy(e => e.Edad).Select(e => new
                {
                    e.Nombre,
                    e.Llamada,
                    e.Edad
                });
            Show(res);

        }

        //Mostrar la suma en segundos de las llamadas hechas por los empleados del departamento
        //de “Computer Science”
        private static void ConsultaL()
        {
            var em = modelo.Employees.Where(e => e.Department.Name.Equals("Computer Science"));
            var res = em.Join(modelo.PhoneCalls,
                e => e.TelephoneNumber,
                p => p.SourceNumber,
                (em, ph) => new
                {
                    Empleado = em,
                    Llamada = ph
                }).GroupBy(e => e.Empleado.Department.Name, (key, lla) => new
                {
                    Departamento = key,
                    Suma = lla.Select(l => l.Llamada.Seconds).Sum()
                });
            Show(res);

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
        private static void ConsultaS()
        {

        }
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