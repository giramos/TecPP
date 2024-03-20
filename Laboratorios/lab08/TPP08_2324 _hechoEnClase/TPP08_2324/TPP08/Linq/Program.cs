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
            Consulta13();
            Consulta14();
            Consulta15();
            Consulta16();
            Consulta17();


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
            var conAggregate = opcion2.Select(a => $"{a.Key} : {a.Duraciones.Aggregate("", (acumulado, actual) => $"{acumulado} {actual}")}");
            //¿Podríamos hacer el Aggregate directamente en el objeto anónimo?
            Show(conAggregate);
            Limpiar();
        }

        private static void Consulta1()
        {
            // Modificar la consulta para mostrar los empleados cuyo nombre empieza por F.
            var resultado = modelo.Employees.Where(e => e.Name.StartsWith('F')).Select(e => e.Name);

            Show(resultado);
            //El resultado esperado es: Felipe
        }

        //Mostrar Nombre y fecha de nacimiento de los empleados de Cantabria con el formato:
        // Nombre=<Nombre>,Fecha=<Fecha>
        private static void Consulta2()
        {
            var res = modelo.Employees.Where(e => e.Province.ToLower().Equals("cantabria")).Select(e => $"{e.Name} {e.DateOfBirth}");

            Show(res);
            /*El resultado esperado es:
              Alvaro 19/10/1945 0:00:00
              Dario 16/12/1973 0:00:0066
            */
        }



        // A partir de aquí, necesitaréis métodos presentes en: https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable?view=net-5.0

        //Mostrar los nombres de los departamentos que tengan más de un empleado mayor de edad.

        private static void Consulta3()
        {

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

        //El nombre de los departamentos donde ningún empleado tenga despacho en el Building "Faculty of Science".
        private static void Consulta4()
        {
            var res = modelo.Departments.Where(d => d.Employees.Where(e => e.Office.Building.ToLower().Equals("faculty of science")).Count() < 1).Select(d => $"Department: {d.Name}");
            Show(res);
            //Resultado esperado: [Department: Mathematics]
        }

        // Mostrar las llamadas de teléfono de más de 5 segundos de duración para cada empleado que tenga más de 50 años
        //Cada línea debería mostrar el nombre del empleado y la duración de la llamada en segundos.
        //El resultado debe estar ordenado por duración de las llamadas (de más a menos).
        private static void Consulta5()
        {
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

        //Mostrar la llamada realizada más larga para cada empleado,
        //mostrando por pantalla: Nombre_empleado : duracion_llamada_mas_larga
        private static void Consulta6()
        {
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

        // Mostrar, agrupados por provincia, el nombre de los empleados
        //Tanto la provincia como los empleados de cada provicia seguirán un orden alfabético.
        private static void Consulta7()
        {

            var res = modelo.Employees.GroupBy(e => e.Province,
                (e, n) => new
                {
                    key = e,
                    Nombre = n.Aggregate("", (acc, b) => acc + " " + b.Name)
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
            var res = modelo.Employees.Join(modelo.PhoneCalls,
               e => e.TelephoneNumber,
               p => p.SourceNumber,
               (em, ph) => new
               {
                   Empleado = em,
                   Llamada = ph
               }).GroupBy(d => d.Empleado.Department, (key, llam) => new
               {
                   Departamento = key.Name,
                   Duracion = llam.Select(l => l.Llamada.Seconds).Aggregate("", (acc, b) => acc + " " + b)
               }).OrderBy(d => d.Departamento);
            Show(res);

            //Salida:
            //{ Departamento = Medicine, Duracion = 2 15 5 10 }
            //{ Departamento = Computer Science, Duracion = 63 7 32 }
            //{ Departamento = Mathematics, Duracion = 23 22 3 }
            //{ Departamento = Physics, Duracion = 7 }
            //Elementos en la colección: 4.
        }

        //        4. El nombre del departamento con el empleado más joven, junto con el nombre de éste y
        //su edad.Tened en cuenta que puede existir más de un resultado.
        private static void Consulta11()
        {
            var res = modelo.Employees.GroupBy(e => e.Department.Name, (key, emp) => new
            {
                Departamento = key,
                Joven = emp.Select(e => e.Age).Min(),
                Empleados = emp.Aggregate("", (acc, b) => acc + " " + b.Name),

            }).Aggregate((acc, b) => acc.Joven < b.Joven ? acc : b);

            Console.WriteLine(res);

            //salida:
            //{ Departamento = Computer Science, Joven = 50, Empleados = Bernardo Carlos }

            var empleadosMasJovenesPorDepartamento = modelo.Employees
                .GroupBy(emp => emp.Department, (dep, empGroup) => new
                {
                    Departamento = dep.Name,
                    EdadMasJoven = empGroup.Min(emp => emp.Age)
                });

            var edadMasJovenGlobal = empleadosMasJovenesPorDepartamento.Min(dep => dep.EdadMasJoven);

            var departamentosConEmpleadosMasJovenes = empleadosMasJovenesPorDepartamento
                .Where(dep => dep.EdadMasJoven == edadMasJovenGlobal);

            Show(departamentosConEmpleadosMasJovenes);

            //Salida:
            //{ Departamento = Medicine, EdadMasJoven = 50 }
            //{ Departamento = Computer Science, EdadMasJoven = 50 }
            //Elementos en la colección: 2.
        }

        //        5. El nombre del departamento que tenga la mayor duración de llamadas telefónicas,
        //sumando la duración de las llamadas de todos los empleados que pertenecen al mismo.
        //Puede asumirse que solamente un departamento cumplirá esta condición.
        private static void Consulta12()
        {
            var res = modelo.Employees.Join(modelo.PhoneCalls,
                 e => e.TelephoneNumber,
                 p => p.SourceNumber,
                 (em, ph) => new
                 {
                     Empleado = em,
                     Llamada = ph
                 }).GroupBy(d => d.Empleado.Department, (key, llam) => new
                 {
                     Departamento = key.Name,
                     Duracion = llam.Select(l => l.Llamada.Seconds).Sum()
                 }).Min(e => $"{e.Departamento} = {e.Duracion} segundos totales");
            Console.WriteLine(res);

            //Salida:
            //Computer Science = 102 segundos totales

            var res1 = modelo.Employees
        .Join(modelo.PhoneCalls,
            e => e.TelephoneNumber,
            p => p.SourceNumber,
            (em, ph) => new
            {
                Empleado = em,
                Llamada = ph
            })
        .GroupBy(d => d.Empleado.Department, (key, llam) => new
        {
            Departamento = key.Name,
            Duracion = llam.Select(l => l.Llamada.Seconds).Sum()
        })
        .OrderByDescending(e => e.Duracion) // Ordenar por duración en orden descendente
        .First(); // Tomar el primero, que será el que tenga la mayor duración

            Console.WriteLine($"{res1.Departamento} = {res1.Duracion} segundos totales");


            //Salida:
            //Computer Science = 102 segundos totales
        }

        // Numero de llamadas por cada provincia
        private static void Consulta13()
        {
            var res = modelo.Employees.Join(modelo.PhoneCalls, e => e.TelephoneNumber, p => p.SourceNumber,
                (em, ph) => new
                {
                    Empleado = em,
                    Llamada = ph
                }).GroupBy(e => e.Empleado.Province, (key, llama) => new
                {
                    Provincia = key,
                    Numero = llama.Select(e => e.Llamada).Count()

                });
            Show(res);

            //Salida: 
            //{ Provincia = Cantabria, Numero = 4 }
            //{ Provincia = Asturias, Numero = 4 }
            //{ Provincia = Granada, Numero = 3 }
            //Elementos en la colección: 3.

        }

        // Suma de las edaddes pertenecierntes a Comuter Science y que sean de Asturias
        private static void Consulta14()
        {
            //Resultado esperado: 50

            var emp = modelo.Employees.Where(e => e.Department.Name.ToLower().Equals("computer science") && e.Province.ToLower().Equals("asturias"));
            var suma = emp.Aggregate(0, (acc, edad) => acc + edad.Age);
            Show(emp);
            var res = emp.Select(e => new
            {
                Suma = e.Age
            }).Aggregate(0, (acc, b) => acc + b.Suma);
            Console.WriteLine("Suma de las edades: " + res);
        }

        // Mostrar la edad media de los empleados. Por cada departamento
        private static void Consulta15()
        {
            var res = modelo.Employees.GroupBy(d => d.Department.Name, (key, emp) => new
            {
                Departamento = key,
                Media = emp.Select(e => e.Age).Average()
            });
            Show(res);

            //salida:
            //{ Departamento = Medicine, Media = 64 }
            //{ Departamento = Computer Science, Media = 52 }
            //{ Departamento = Mathematics, Media = 79 }
            //{ Departamento = Physics, Media = 74 }

            var res1 = modelo.Employees.GroupBy(d => d.Department.Name, (key, emp) => new
            {
                Departamento = key,
                Media = emp.Select(e => e.Age).Aggregate((acc, b) => acc + b) / emp.Count()
            });
            Show(res1);

            //salida:
            //{ Departamento = Medicine, Media = 64 }
            //{ Departamento = Computer Science, Media = 52 }
            //{ Departamento = Mathematics, Media = 79 }
            //{ Departamento = Physics, Media = 74 }
        }

        //lista de empleados que no realizan ninguna llamada saliente.
        private static void Consulta16()
        {
            var emp = modelo.Employees.Join(modelo.PhoneCalls, e => e.TelephoneNumber, p => p.SourceNumber,
            (emp, ph) => new
            {
                Empleado = emp,
                Llamada = ph.SourceNumber
            }).Select(e => e.Empleado).Distinct();
            var res = modelo.Employees.Except(emp);

            Show(res);

            //Salida:
            //[Employee: Carlos]
            //[Employee: Dario]

            // Obtener una lista de todos los números de teléfono usados como origen en las llamadas salientes
            var numerosLlamadasSalientes = modelo.PhoneCalls.Select(call => call.SourceNumber).Distinct();

            // Seleccionar los empleados cuyo número de teléfono no está en la lista de números de llamadas salientes
            var empleadosSinLlamadasSalientes = modelo.Employees
                .Where(emp => !numerosLlamadasSalientes.Contains(emp.TelephoneNumber));

            Show(empleadosSinLlamadasSalientes);

            //Salida:
            //[Employee: Carlos]
            //[Employee: Dario]
        }

        //•	Para cada edificio, obtener la edad máxima, la edad mínima
        //y el numero de empleados que tienen despacho en ese edificio.
        private static void Consulta17()
        {
            var res = modelo.Employees.GroupBy(e => e.Office.Building, (key, emp) => new
            {
                Edificio = key,
                EdadMaxima = emp.Select(e => e.Age).Max(),
                EdadMinima = emp.Select(e => e.Age).Min(),
                Numero = emp.Count()
            });
            Show(res);

            //Salida:
            //{ Edificio = Faculty of Science, EdadMaxima = 78, EdadMinima = 50, Numero = 3 }
            //{ Edificio = Headquarters, EdadMaxima = 54, EdadMinima = 54, Numero = 1 }
            //{ Edificio = Polytechnical, EdadMaxima = 79, EdadMinima = 50, Numero = 2 }
            //Elementos en la colección: 3.
        }

        //•	Mostrar las llamadas realizadas desde el edificio "Faculty of Science" al edificio "Polytechnical".
        //Debe mostrarse por pantalla el número de origen, el número de destino y la duración de la llamada.
        private static void ConsultaO()
        {
            var res = modelo.Employees.Join(modelo.PhoneCalls,
                e => e.TelephoneNumber,
                p => p.SourceNumber,
                (em, ph) => new
                {
                    Empleado = em,
                    Llamada = ph
                })
                .Join(modelo.Offices,
                emp => emp.Empleado.Office.Number,
                ofi => ofi.Number,
                (empl, ofic) => new
                {
                    Origen = empl.Llamada.SourceNumber,
                    Destino = empl.Llamada.DestinationNumber,
                    Duracion = empl.Llamada.Seconds,
                    Oficina = ofic.Building
                })
                ;
            foreach (var llamada in res)
            {
                Console.WriteLine($"Origen: {llamada.Origen}, Destino: {llamada.Destino}, Duración: {llamada.Duracion} segundos");
            }
        }

        private static void ConsultaOOO()
        {
            var res = modelo.Employees
                .Join(modelo.PhoneCalls,
                    e => e.TelephoneNumber,
                    p => p.SourceNumber,
                    (em, ph) => new
                    {
                        Empleado = em,
                        Llamada = ph
                    })
                .Join(modelo.Offices,
                    emp => emp.Empleado.Office.Number,
                    ofi => ofi.Number,
                    (empl, ofic) => new
                    {
                        Origen = empl.Llamada.SourceNumber,
                        Destino = empl.Llamada.DestinationNumber,
                        Duracion = empl.Llamada.Seconds,
                        EdificioOrigen = modelo.Offices.FirstOrDefault(o => o.Number == empl.Empleado.Office.Number)?.Building,
                        EdificioDestino = modelo.Offices.FirstOrDefault(o => o.Number == ofic.Number)?.Building
                    })
                .Where(llamada => llamada.EdificioOrigen == "Faculty of Science" && llamada.EdificioDestino == "Polytechnical");

            foreach (var llamada in res)
            {
                Console.WriteLine($"Edificio de origen: {llamada.EdificioOrigen}, Número de origen: {llamada.Origen}, " +
                                  $"Edificio de destino: {llamada.EdificioDestino}, Número de destino: {llamada.Destino}, " +
                                  $"Duración: {llamada.Duracion} segundos");
            }
        }



        // Mostrar las llamadas realizadas desde el edificio "Faculty of Science" al edificio "Polytechnical".
        // Debe mostrarse por pantalla el número de origen, el número de destino y la duración de la llamada.
        private static void ConsultaOO()
        {
            var llamadas = modelo.PhoneCalls
                .Join(modelo.Employees, // Unir con la información de los empleados
                    call => call.SourceNumber, // Clave de la llamada
                    emp => emp.TelephoneNumber, // Clave del empleado
                    (call, emp) => new // Seleccionar la llamada junto con la información del empleado
                    {
                        Llamada = call,
                        Empleado = emp
                    })
                .Join(modelo.Offices, // Unir con la información de los edificios
                    combined => combined.Empleado.Office.Number, // Clave del empleado combinada con el edificio
                    office => office.Number, // Clave del edificio
                    (combined, office) => new // Seleccionar la llamada junto con la información del edificio
                    {
                        combined.Llamada.SourceNumber, // Número de origen de la llamada
                        combined.Llamada.DestinationNumber, // Número de destino de la llamada
                        combined.Llamada.Seconds // Duración de la llamada
                    })
                .Where(call => call.SourceNumber.StartsWith("985") && call.DestinationNumber.StartsWith("2.1")); // Filtrar las llamadas del edificio "Faculty of Science" al "Polytechnical"

            Show(llamadas);
        }

        //lista de empleados que no realizan ninguna llamada saliente.
        private static void ConsultaP()
        {
            var numerosDeTelefonoSalientes = modelo.PhoneCalls.Select(call => call.SourceNumber).Distinct();

            var empleadosSinLlamadasSalientes = modelo.Employees
                .Where(emp => !numerosDeTelefonoSalientes.Contains(emp.TelephoneNumber))
                .Select(emp => emp.Name);

            Show(empleadosSinLlamadasSalientes);
        }
		
		 private void Query6() 
        {
            var i = 0;
            // Muestrame las llamadas ordenadas por duracion (mas a menos) con un ranking (por numeros)
            ///var llamadas = model.PhoneCalls
            /// .OrderByDescending(p => p.Seconds)
            ///.Select(p => $"Rank: {i++}  Duration: {p.Seconds}");

            var llamadas = model.PhoneCalls
                .OrderByDescending(p => p.Seconds)
                .Zip(Enumerable.Range(1, model.PhoneCalls.Count() + 1), 
                    (c,i) => $"Rank: {i++}  p.Seconds)");
            Console.WriteLine("Llamadas: ");
            Show(llamadas);
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
