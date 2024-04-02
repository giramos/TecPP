using ListaGenerica;
using System;
using System.Linq;

namespace ConsultasSinLINQ
{
    class Person
    {
        public String Name { get; set; }
        public int Age { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Lista<Person> people = new();
            people.Añadir(new Person { Name = "John", Age = 18 });
            people.Añadir(new Person { Name = "Mary", Age = 7 });
            people.Añadir(new Person { Name = "Oscar", Age = 57 });
            people.Añadir(new Person { Name = "Laura", Age = 43 });
            people.Añadir(new Person { Name = "James", Age = 23 });
            people.Añadir(new Person { Name = "Lucy", Age = 12 });
            people.Añadir(new Person { Name = "Lucas", Age = 9 });



            // Find the first Person in people that is older than 20.
            var uno = people.Buscar(p => p.Age > 20);
            Console.WriteLine($"Primera persona mayor de 20 años {uno.Name} edad => {uno.Age}");

            // Create a collection with people that are 20 years of age.
            var dos = people.Filtrar(p => p.Age == 20);
            dos.ForEach(e => Console.WriteLine(e));

            // Create a collection that has strings of the form "<person_name> is <person.age> years old." with the elements of people.
            var tres = people.Map(p => $"{p.Name} is {p.Age} years old");
            tres.ForEach(e => Console.WriteLine(e));

            // Calculate the sum of the ages of people.
            var cuatro = people.Map(e => e.Age).Reduce((acc, p) => acc + p);
            Console.WriteLine("Suma de los años de la gente: " + cuatro);

            // Calculate the sum of the ages of people over 20 years of age.
            var cinco = people.Filtrar(e => e.Age < 20).Reduce(0,(acc,p)=> acc + p.Age);
            Console.WriteLine("suma de los años de la gente menor de 20: " + cinco);

            // Find the youngest and the oldest person in people.
            var seis = people.Map(e => e.Age).Reduce((acc,p)=>acc>p?acc:p);
            Console.WriteLine("Edad mas vieja: " + seis);
            var seis1 = people.Where(e => e.Age == seis);
            seis1.ForEach(e=>Console.WriteLine(e.Name));
            var siete = people.Map(e => e.Age).Reduce(seis,(acc,p)=> acc > p?p:acc);
            Console.WriteLine("Edad mas jove: " + siete);
            var ssiete = people.Where(e => e.Age == siete);
            ssiete.ForEach(e => Console.WriteLine(e.Name));


            // Print people ordered by age


        }
    }
}
