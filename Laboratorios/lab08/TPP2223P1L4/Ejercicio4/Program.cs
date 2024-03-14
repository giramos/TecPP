using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
            //ConsultaE();
            //ConsultaF();
            //ConsultaG();
            //ConsultaH();
            //ConsultaI();
            //ConsultaJ();
            //ConsultaK();
            //ConsultaL();
            //ConsultaM();
            //ConsultaN();
            //ConsultaÑ();
            //ConsultaOO();
            //ConsultaOOO();
            //ConsultaO();
            ConsultaP();
        }

        //Obtener las llamadas de más de 15 segundos de duración
        private static void ConsultaA()
        {
            var llamadas = modelo.PhoneCalls.Where(d => d.Seconds > 15);
            Show(llamadas);
        }
        //Mostrar las llamadas de cada empleado con el formato: "<Nombre>;<Duración de la llamada>"
        private static void ConsultaB()
        {
            var llamadas = modelo.Employees.Join(modelo.PhoneCalls,
                telefono => telefono.TelephoneNumber,
                origen => origen.SourceNumber,
                (emp, llam) => $"{emp.Name};{llam.Seconds}");

            Show(llamadas);

        }
        // Vamos a mostrar la duración de las llamadas agrupadas por número de teléfono (origen
        private static void ConsultaC()
        {
            var llamada = modelo.PhoneCalls.GroupBy(tel => tel.SourceNumber, (num, sec) => new
            {
                key = num,
                segundos = sec.Select(a => a.Seconds).Aggregate("",(ac, b) => $"{ac} {b}"),
            });
            Show(llamada);
        }
        // Modificar la consulta para mostrar los empleados cuyo nombre empieza por F.
        private static void ConsultaD()
        {
            var empleados = modelo.Employees.Where(n => n.Name.StartsWith("F"));
            Show(empleados);

        }
        //Mostrar Nombre y fecha de nacimiento de los empleados de Cantabria con el formato:
        // Nombre=<Nombre>,Fecha=<Fecha>
        private static void ConsultaE()
        {
            var emp = modelo.Employees.Where(pro => pro.Province.ToLower().Equals("cantabria")).Select((a) => 
            $"{a.Name} {a.DateOfBirth}");
            Show(emp);
        }
        //Mostrar los nombres de los departamentos que tengan más de un empleado mayor de edad.
        private static void ConsultaF()
        {
            var empleados = modelo.Employees.Where(edad => edad.Age >= 18);
            var depar = modelo.Departments.Where(a => a.Employees.Where(edad => edad.Age >= 18).Count()>1).Select(a => a.Name);            
            
            Show(depar);

            //Posteriormente, modifica la consulta para que:
            //Muestre los nombres de los departamentos que tengan más de un empleado mayor de edad
            //y
            //que el despacho (Office.Number) COMIENCE por "2.1"

            var depar1 = modelo.Departments.Where(a => a.Employees.Where(edad => edad.Age >= 18 && edad.Office.Number.StartsWith("2.1")).Count() > 1);
            Show(depar1);

        }

        //El nombre de los departamentos donde ningún empleado tenga despacho en el Building "Faculty of Science".
        //Resultado esperado: [Department: Mathematics]
        private static void ConsultaG()
        {
            var depar1 = modelo.Departments.Where(d => d.Employees.Where(e => e.Office.Building.ToLower().Equals("faculty of science")).Count()<1);
            Show(depar1);
        }
        // Mostrar las llamadas de teléfono de más de 5 segundos de duración para cada empleado que tenga más de 50 años
        //Cada línea debería mostrar el nombre del empleado y la duración de la llamada en segundos.
        //El resultado debe estar ordenado por duración de las llamadas (de más a menos).
        private static void ConsultaH()
        {
            var llamadas = modelo.PhoneCalls.Where(p => p.Seconds > 5);
            var empleado = modelo.Employees.Where(e => e.Age > 50);
            var res = llamadas.Join(empleado,
                llama => llama.SourceNumber,
                tele => tele.TelephoneNumber,
                (llamada, empleado) => new
                {
                    Llamada = llamada.Seconds,
                    Empleado = empleado.Name
                }).OrderBy(l => l.Llamada);
            Show(res);
        }

        //Mostrar la llamada más larga para cada profesor, mostrando por pantalla: Nombre_profesor : duracion_llamada_mas_larga
        private static void ConsultaI()
        {
            var res = modelo.Employees.Join(modelo.PhoneCalls,
                emp => emp.TelephoneNumber,
                llam => llam.SourceNumber,
                (e, l) => new
                {
                    Empleado = e.Name,
                    Duracion = l.Seconds
                });
            var res1 = res.GroupBy(e => e.Empleado, (e, l) => new
            {
                Key = e,
                Duracion = l.Max(e => e.Duracion)
            }) ;
            var res2 = res.GroupBy(e => e.Empleado, (e, l) => new
            {
                Key = e,
                Duracion = l.Aggregate(0, (a, b) => a > b.Duracion ? a : b.Duracion)
            }); ;
            Show(res2);
        }

        // Mostrar, agrupados por provincia, el nombre de los empleados
        //Tanto la provincia como los empleados deben estar ordenados alfabéticamente
        private static void ConsultaJ()
        {
            var emp = modelo.Employees.GroupBy(e => e.Province, (pro,nom) => new
            {
                key = pro,
                nombre = nom.Select(a => a.Name).Aggregate("",(a,b)=> a + " " + b)
            }).OrderBy(a => a.key);
            Show(emp);
        }

        //Mostrar, ordenados por edad, los nombres de los empleados pertenecientes al
        //departamento de “Computer Science” que tienen un despacho en la “Faculty of Science” y
        //que han hecho llamadas con duración superior a 1 minuto.
        private static void ConsultaK()
        {
            var emp = modelo.Employees;
            var dep = emp.Where(e => e.Department.Name.ToLower().Equals("computer science"));
            var ofi = dep.Where(d => d.Office.Building.ToLower().Equals("faculty of science"));
            var res = emp.Join(modelo.PhoneCalls,
                emp => emp.TelephoneNumber,
                llam => llam.SourceNumber,
                (emp, llam) => new
                {
                    Empleado = emp,
                    Duracion = llam.Seconds
                }).OrderBy(a => a.Empleado.Age);
            var res1 = res.Where(a => a.Duracion>60).Select(a=>a.Empleado.Name).Distinct();
            Show(res1);

        }

        //Mostrar la suma en segundos de las llamadas hechas por los empleados del departamento
        //de “Computer Science”
        private static void ConsultaL()
        {
            var emp = modelo.Employees;
            var dep = emp.Where(e => e.Department.Name.ToLower().Equals("computer science"));
            var res = dep.Join(modelo.PhoneCalls,
                emp => emp.TelephoneNumber,
                llam => llam.SourceNumber,
                (emp, llam) => new
                {
                    Empleados = emp,
                    Duracion = llam.Seconds

                });
            var res1 = res.Aggregate(0,(acc,b)=> acc + b.Duracion );
            //Show(res1);
            Console.WriteLine("Suma: " + res1);

        }

        // Mostrar las llamadas de teléfono hechas por cada departamento, ordenadas por el
        // nombre del departamento.Cada línea debe tener la siguiente estructura:
        // “Departamento=<Nombre>, Duración=<Segundos>”
        private static void ConsultaM()
        {
            var emp = modelo.Employees;
            var llam = modelo.PhoneCalls;
            var res = emp.Join(llam,
                emp => emp.TelephoneNumber,
                llam => llam.SourceNumber,
                (emp, llam) => new
                {
                    Departamento = emp.Department.Name,
                    Duracion = llam.Seconds
                }).OrderBy(a => a.Departamento);
            Show(res);
        }

        // Mostrar el departamento que tenga la mayor duración de llamadas telefónicas (en
        // segundos), sumando la duración de las llamadas de todos los empleados que
        // pertenecen al mismo. Mostrar también el nombre de dicho departamento(puede
        // suponerse que solo hay un departamento que cumplirá esta condición.
        private static void ConsultaN()
        {
            var emp = modelo.Employees;
            var llam = modelo.PhoneCalls;
            var res = emp.Join(llam,
                emp => emp.TelephoneNumber,
                llam => llam.SourceNumber,
                (emp, llam) => new
                {
                    Departamento = emp.Department.Name,
                    Duracion = llam.Seconds
                }).GroupBy(a => a.Departamento, (nom, dur) => new
                {
                    key = nom,
                    dur = dur.Aggregate(0, (acc, b) => acc + b.Duracion)
                }).Max(a=>a.dur);
            Console.WriteLine();
        }
        //•	Para cada edificio, obtener la edad máxima, la edad mínima
        //y el numero de empleados que tienen despacho en ese edificio.
        private static void ConsultaÑ()
        {
            var res = modelo.Employees.GroupBy(e => e.Office.Building, (key, emp) => new
            {
                Edificio = key,
                Numero = emp.Select(x => x.Name).Count(),
                Minima = emp.Select(x => x.Age).Aggregate((acc, b) => acc < b ? acc : b),
                Maxima = emp.Select(x => x.Age).Aggregate((acc, b) => acc > b ? acc : b)
            });
            Show(res);
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
