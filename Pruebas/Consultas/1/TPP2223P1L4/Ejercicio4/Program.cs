using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Modelo;

namespace Ejercicio4
{
    internal class Program
    {
        private static Model modelo = new Model();
        static void Main()
        {
            //ConsultaA();
            //ConsultaB();
            //ConsultaC();
            //ConsultaD();
            ConsultaE();
        }
        //        Mostrar, ordenados por edad, los nombres de los empleados pertenecientes al
        //departamento de “Computer Science” que tienen un despacho en la “Faculty of
        //Science” y que han hecho llamadas con duración superior a 1 minuto
        private static void ConsultaA()
        {

            var res = modelo.Employees.Join(modelo.PhoneCalls,
                e => e.TelephoneNumber,
                p => p.SourceNumber,
                (em, ph) => new
                {
                    Empleado = em,
                    Llamda = ph
                }
                ).Where(e => e.Empleado.Department.Name.ToLower().Equals("computer science")
                && e.Empleado.Office.Building.ToLower().Equals("faculty of science")
                && e.Llamda.Seconds > 60).OrderBy(o => o.Empleado.Age).Select(e => $"{e.Empleado.Name}").Distinct();
            Show(res);
        }

        //        Mostrar la suma en segundos de las llamadas hechas por los empleados del
        //departamento de “Computer Science”

        private static void ConsultaB()
        {
            var res = modelo.Employees.Join(modelo.PhoneCalls,
                e => e.TelephoneNumber,
                p => p.SourceNumber,
                (em, ph) => new
                {
                    Empleado = em,
                    Llamada = ph
                }).Where(e => e.Empleado.Department.Name.ToLower().Equals("computer science")).
                Aggregate(0, (acc, b) => acc + b.Llamada.Seconds);
            //Show(res);
            Console.WriteLine("Suma " + res);
        }

        // Mostrar la edad media de los empleados. Por cada departamento
        private static void ConsultaC()
        {
            var res = modelo.Employees.Select(edad => edad.Age).Aggregate((acc, emp) =>

                acc + emp) / modelo.Employees.Count();
            Console.WriteLine("Edad media: " + res);

            var r = modelo.Employees.GroupBy(x => x.Department.Name, (key, emp) => new
            {
                Departamento = key,
                Empleado = emp.Select(edad => edad.Age)
                .Aggregate((acc, emp) => acc + emp) / emp.Count()
            });
            Show(r);
        }
        //Mostrar las llamadas de teléfono de más de 5 segundos de duración para cada empleado que tenga más de 50 años
        //Cada línea debería mostrar el nombre del empleado y la duración de la llamada en segundos.
        //El resultado debe estar ordenado por duración de las llamadas (de más a menos).
        private static void ConsultaD()
        {
            var llamadas = modelo.PhoneCalls.Where(seg => seg.Seconds > 5);
            var empleados = modelo.Employees.Where(edad => edad.Age > 50);
            var res = llamadas.Join(empleados,
                l => l.SourceNumber,
                e => e.TelephoneNumber,
                (llam, emp) => new
                {
                    Llamada = llam,
                    Empleado = emp
                }).GroupBy(em => em.Empleado, (key, lla) => new
                {
                    Nombre = key.Name,
                    Apellido = key.Surname,
                    Segundos = lla.Select(a=>a.Llamada.Seconds).Aggregate("",(acc, b) => acc + " " + b)
                }).OrderByDescending(a=> a.Segundos);
            Show(res);
        }
        //Mostrar los departamentos con el empleado más joven, además del nombre dicho
        //empleado más joven y su edad.Tened en cuenta que puede existir más de un empleado más
        //joven.
        private static void ConsultaEE()
        {
            var res = modelo.Employees.GroupBy(dep => dep.Department.Name, (key, emp) => new
            {
                Departamento = key,
                Empleado = emp.Select(e=>e.Age).Aggregate((acc,b)=>acc<b ? acc : b)
            }).Aggregate((acc,b)=>acc.Empleado<=b.Empleado?acc:b);
            Console.WriteLine(res);
            //Show(res);
        }

        private static void ConsultaE()
        {
            var empleadosMasJovenesPorDepartamento = modelo.Employees
                .GroupBy(emp => emp.Department, (dep, empGroup) => new
                {
                    Departamento = dep.Name,
                    EdadMasJoven = empGroup.Min(emp => emp.Age)
                });

            var edadMasJovenGlobal = empleadosMasJovenesPorDepartamento.Min(dep => dep.EdadMasJoven);

            var departamentosConEmpleadosMasJovenes = empleadosMasJovenesPorDepartamento
                .Where(dep => dep.EdadMasJoven == edadMasJovenGlobal);

            foreach (var departamento in departamentosConEmpleadosMasJovenes)
            {
                Console.WriteLine($"Departamento: {departamento.Departamento}");
                var empleadosMasJovenes = modelo.Employees
                    .Where(emp => emp.Department.Name == departamento.Departamento && emp.Age == departamento.EdadMasJoven);
                foreach (var empleado in empleadosMasJovenes)
                {
                    Console.WriteLine($"- Empleado más joven: {empleado.Name}, Edad: {empleado.Age}");
                }
            }
        }



        private static void Show<T>(IEnumerable<T> colección)
        {
            foreach (var item in colección)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Elementos en la colección: {0}.", colección.Count());
        }
    }


}
