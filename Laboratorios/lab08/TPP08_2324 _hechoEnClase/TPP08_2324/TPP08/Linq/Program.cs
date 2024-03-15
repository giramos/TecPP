using System;
using System.Collections;

// Colecciones genéricas.
using System.Collections.Generic;
// LINQ
using System.Linq;

using Modelo;

namespace Linq
{
    class Program
    {
        
        private static Model modelo = new Model();

        static void Main()
        {
             //SintaxisLinq();
             //EjemploJoin();
             //EjemploGroupBy();

            
            Consulta1();
            Consulta2();
            Consulta3();
            Consulta4();
            Consulta5();
            Consulta6();
            Consulta7();
            Consulta8();
            Consulta9();
            Consulta10();
            Consulta11();
            Consulta12();


        }

        private static void SintaxisLinq()
        {
            //Obtener las llamadas de más de 15 segundos de duración


            //Sintaxis de consulta
            var c1 =
                from pc in modelo.PhoneCalls
                where pc.Seconds > 15
                select pc;
            Show(c1);

            Console.WriteLine();
            
            //Equivalente con sintaxis de métodos.
            //¡OJO! SE DEFINE LA CONSULTA. NO SE EJECUTA. ¿POR QUÉ?
            var c1_m = modelo.PhoneCalls.Where(ll => ll.Seconds > 15);
            //¿Qué ocurre si la consulta anterior la finalizamos con un .ToList()?

            Show(c1_m);
            Limpiar();
        }

        private static void EjemploJoin()
        {
            //Mostrar las llamadas de cada empleado con el formato: "<Nombre>;<Duración de la llamada>"

            //El método Join, une dos colecciones a partir de un atributo común:
            //Lo utilizamos sobre un IEnumerable (modelo.PhoneCalls)
            var result = modelo.PhoneCalls.Join(

                modelo.Employees, //para unir sus elementos con los de un segundo IEnumerable (modelo.Employees)

                llamada => llamada.SourceNumber, //Atributo clave del primer IEnumerable (PhoneCalls)

                emp => emp.TelephoneNumber, //Atributo clave del 2º IEnumerable (Employees)

                (llamada, emp) => $"{emp.Name};{llamada.Seconds}" // Función que recibe y trata cada par de llamada-empleado de claves coincidentes.
            );

            Show(result);
            Limpiar();
        }


        private static void EjemploGroupBy()
        {
            //GroupBy: Vamos a mostrar la duración de las llamadas agrupadas por número de teléfono (origen)

            var llamadas = modelo.PhoneCalls;
            var resultado = llamadas.GroupBy(ll => ll.SourceNumber);

            //resultado ahora mismo es un  IEnumerable<IGrouping>
            Console.WriteLine("Imprimiendo directamente:");
            Show(resultado);


            Console.WriteLine("\nImprimiendo mediante recorrido:");
            foreach (var grupo in resultado)
            {
                //Cada IGrouping tiene una Key:
                Console.Write("\nClave [" + grupo.Key + "] : ");
                //Y tenemos un listado. En este caso, de llamadas:
                foreach (var llamada in grupo)
                {
                    Console.Write(llamada.Seconds + " ");
                }
            }

            //Sin embargo GroupBy nos presenta otras opciones, vamos a combinar éstas
            //con los objetos anónimos:

            var opcion2 = llamadas.GroupBy(
                ll => ll.SourceNumber, //Agrupamos por número de origen

                //el primer parámetro es el número de origen (clave)
                //el segundo parámetro es un IEnumerable<PhoneCall> asociados a esa clave.
                (numero, llamadasEncontradas) =>
                new //Vamos emplear una función que cree objetos anónimos con la info que necesitamos
                {
                    Key = numero,
                    //Duraciones sigue siendo un IEnumerable.
                    Duraciones = llamadasEncontradas.Select(ll => ll.Seconds)
                }
                );

            Console.WriteLine("\n\nImprimiendo directamente:");
            Show(opcion2);
            Console.WriteLine("\nImprimiendo con el Aggregate:");
            var conAggregate = opcion2.Select( a => $"{a.Key} : { a.Duraciones.Aggregate("", (acumulado, actual) => $"{acumulado} {actual}") }" );
            //¿Podríamos hacer el Aggregate directamente en el objeto anónimo?
            Show(conAggregate);
            Limpiar();
        }

        private static void Consulta1()
        {
            // Modificar la consulta para mostrar los empleados cuyo nombre empieza por F.
            var resultado = modelo.Employees.Where(e => e.Name.StartsWith('F')).Select(e=>e.Name);
            
            Show(resultado);
            //El resultado esperado es: Felipe
        }

        private static void Consulta2()
        {

            //Mostrar Nombre y fecha de nacimiento de los empleados de Cantabria con el formato:
            // Nombre=<Nombre>,Fecha=<Fecha>
            var res = modelo.Employees.Where(e => e.Province.ToLower().Equals("cantabria")).Select(e=>$"{e.Name} {e.DateOfBirth}");

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
            var res = modelo.Departments.Where(e => e.Employees.Where(e => e.Age >= 18).Count() > 1).Select(d => $"{d.Name}");
            Show(res);

            /*El resultado esperado es:
                Computer Science
                Medicine
            */


            //Posteriormente, cree una nueva versión de la consulta para que:
            //Muestre los nombres de los departamentos que tengan más de un empleado mayor de edad
            //y
            //que el despacho (Office.Number) COMIENCE por "2.1"
            res = modelo.Departments.Where(e => e.Employees.Where(e => e.Age >= 18 && e.Office.Number.StartsWith("2.1")).Count() > 1).Select(d => $"{d.Name}");
            Show(res);
            //El resultado esperado es: Medicine
        }

        private static void Consulta4()
        {

            //El nombre de los departamentos donde ningún empleado tenga despacho en el Building "Faculty of Science".
            var res = modelo.Departments.Where(d => d.Employees.Where(e => e.Office.Building.ToLower().Equals("faculty of science")).Count() < 1).Select(d => $"Department: {d.Name}");
            Show(res);
            //Resultado esperado: [Department: Mathematics]
        }

        private static void Consulta5()
        {


            // Mostrar las llamadas de teléfono de más de 5 segundos de duración para cada empleado que tenga más de 50 años
            //Cada línea debería mostrar el nombre del empleado y la duración de la llamada en segundos.
            //El resultado debe estar ordenado por duración de las llamadas (de más a menos).
            var emp = modelo.Employees.Where(e => e.Age > 50);
            var llam = modelo.PhoneCalls.Where(p => p.Seconds > 5);
            var res = emp.Join(llam,
                e => e.TelephoneNumber,
                l => l.SourceNumber,
                (em, ll) => new
                {
                    Empleado = em.Name,
                    Llamada = ll.Seconds
                }).OrderByDescending(a => a.Llamada).Select(a => $"Nombre = {a.Empleado}, Duracion = {a.Llamada}");
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
            //Mostrar la llamada realizada más larga para cada empleado, mostrando por pantalla: Nombre_empleado : duracion_llamada_mas_larga
            var res = modelo.Employees.Join(modelo.PhoneCalls,
                emp => emp.TelephoneNumber,
                llam => llam.SourceNumber,
                (e, l) => new
                {
                    Empleado = e.Name,
                    Llamada = l.Seconds
                }).GroupBy(e => e.Empleado,
                (e, l) => new
                {
                    key = e,
                    duracion = l.Max(e => e.Llamada)
                }).Select(e => $"Nombre = {e.key}, Maxima = {e.duracion}");
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
            //Tanto la provincia como los empleados de cada provicia seguirán un orden alfabético.
            var res = modelo.Employees.GroupBy(e => e.Province,
                (e, n) => new
                {
                    key = e,
                    Nombre = n.Aggregate("",(acc, b) => acc + " " + b.Name)
                }).OrderBy(a => a.key).Select(a => $"{a.key} : {a.Nombre}");
            Show(res);


            /*Resultado esperado:
                Alicante : Carlos
                Asturias : Bernardo Felipe
                Cantabria : Alvaro Dario               
                Granada : Eduardo

            */
        }

        //        1. Los nombres de los empleados que pertenecen al departamento de “Computer
        //Science”, tienen un despacho en la “Faculty of Science” y han realizado al menos una
        //llamada con duración superior a 1 minuto.
        private static void Consulta8()
        {
            var emp = modelo.Employees.Where(e => e.Department.Name.ToLower().Equals("computer science"));
            var ofi = emp.Where(e => e.Office.Building.ToLower().Equals("faculty of science"));
            var res = ofi.Join(modelo.PhoneCalls,
                emp => emp.TelephoneNumber,
                llam => llam.SourceNumber,
                (e, l) => new
                {
                    Empleado = e.Name,
                    Llamada = l.Seconds
                });
            var r = res.Where(e => e.Llamada > 60);
            Show(r);
        }

        //        La suma en segundos de las llamadas cuyo número de origen es el de un empleado del
        //departamento “Computer Science”
        private static void Consulta9()
        {
            var emp = modelo.Employees.Where(e => e.Department.Name.ToLower().Equals("computer science"));
            var res = emp.Join(modelo.PhoneCalls,
                emp => emp.TelephoneNumber,
                llam => llam.SourceNumber,
                (e, l) => new
                {
                    Empleado = e.Name,
                    Llamada = l.Seconds
                }).GroupBy(e => e.Empleado, (e, l) => new
                {
                    Nombre = e,
                    Duracion = l.Aggregate(0, (acc, b) => acc + b.Llamada)
                });
            Show(res);
        }

            //Las llamadas de teléfono realizadas por cada departamento, ordenadas por nombre de departamento.
            //Cada línea debe tener el formato: 
            //Departamento= Nombre; Duración=Segundos

        private static void Consulta10()
        {
            var r = modelo.Employees.Join(modelo.PhoneCalls,
                emp => emp.TelephoneNumber,
                llam => llam.SourceNumber,
                (e, l) => new
                {
                    Departamento = e.Department.Name,
                    Duracion = l.Seconds
                }).OrderBy(d => d.Departamento);
            Show(r);
        }

        //        4. El nombre del departamento con el empleado más joven, junto con el nombre de éste y
        //su edad.Tened en cuenta que puede existir más de un resultado.

        private static void Consulta11()
        {
            var r = modelo.Employees.GroupBy(e => e.Department.Name, (dep, emp) => new
            {
                NombreDepartamento = dep,
                Empleado = emp.Select(e=> e.Name).Aggregate((acc,b) => acc + " " + b),
                Edad = emp.Select(e=> e.Age).Aggregate((acc,b) => acc <= b ? acc : b)

            }).Select(e => $"Departamento = {e.NombreDepartamento} - Empleado = {e.Empleado} - Edad = {e.Edad}");
            Show(r);
        }

        //        5. El nombre del departamento que tenga la mayor duración de llamadas telefónicas,
        //sumando la duración de las llamadas de todos los empleados que pertenecen al mismo.
        //Puede asumirse que solamente un departamento cumplirá esta condición.

        private static void Consulta12()
        {
            var res = modelo.Employees.Join(modelo.PhoneCalls,
                emp => emp.TelephoneNumber,
                llam => llam.SourceNumber,
                (e, l) => new
                {
                    Empleado = e.Name,
                    Departamento = e.Department.Name,
                    Duracion = l.Seconds
                }).GroupBy(d => d.Departamento, (clave, sum) => new
                {
                    key = clave,
                    Duracion = sum.Aggregate(0,(acc,b) => acc + b.Duracion)
                });
            Show(res);
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
