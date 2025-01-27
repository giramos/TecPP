﻿using System;
using System.Collections.Generic;

namespace Modelo
{
    public class Model
    {

        public IList<Department> Departments { get { return departments; } }
        public IList<Employee> Employees { get { return employees; } }
        public IList<Office> Offices { get { return offices; } }
        public IList<PhoneCall> PhoneCalls { get { return phoneCalls; } }


        static Model()
        {
            ResetModel();
        }

        static void ResetModel()
        {

            offices = new List<Office> {
                new Office("2.1", "Faculty of Science"),
                new Office("2.2", "Faculty of Science"),
                new Office("2.1", "Polytechnical"),
                new Office("3.1", "Headquarters"),
                new Office("2.1", "Headquarters")
                };

            employees = new List<Employee> {
                new Employee ("Alvaro", "Alvarez", new DateTime(1945,10,19), "985 001", "alvaro@uniovi.es", "Cantabria", offices[0] ),
                new Employee ("Bernardo", "Bernardez", new DateTime(1973,12,09), "985 002", "bernardo@uniovi.es", "Asturias", offices[1]),
                new Employee ("Carlos", "Carlez", new DateTime(1969,10,03), "985 003", "carlos@uniovi.es", "Alicante", offices[4]),
                new Employee ("Dario", "Dariez", new DateTime(1973,12,16), "100 004", "dario@uniovi.es", "Cantabria", offices[2]),
                new Employee ("Eduardo", "Eduardez", new DateTime(1945,02,10), "985 005", "eduardo@uniovi.es", "Granada", offices[2]),
                new Employee ("Felipe", "Felipez", new DateTime(1950,02,20), "985 006", "felipe@uniovi.es", "Asturias", offices[1]),
                };

            departments = new List<Department> {
                new Department ( "Mathematics", new List<Employee> {employees[4]} ),
                new Department ( "Computer Science",new List<Employee> {employees[1], employees[2]}),
                new Department ( "Medicine",new List<Employee> {employees[0], employees[3]}),
                new Department ( "Physics",new List<Employee> {employees[5]}),
                };

            employees[4].Department = departments[0];
            employees[1].Department = employees[2].Department = departments[1];
            employees[0].Department = employees[3].Department = departments[2];
            employees[5].Department = departments[3];

            phoneCalls = new List<PhoneCall> {
                new PhoneCall { SourceNumber = "985 001", DestinationNumber = "100 000", Seconds = 02, Date = new DateTime(2011,    1,  7,  8,  12, 0)},
                new PhoneCall { SourceNumber = "985 001", DestinationNumber = "200 000", Seconds = 15, Date = new DateTime(2011,    1,  7,  13, 12, 0) },
                new PhoneCall { SourceNumber = "100 000", DestinationNumber = "985 005", Seconds = 15, Date = new DateTime(2011,    2,  7,  9,  23, 0) },
                new PhoneCall { SourceNumber = "200 000", DestinationNumber = "985 002", Seconds = 01, Date = new DateTime(2011,    2,  7,  10, 5,  0) },
                new PhoneCall { SourceNumber = "985 005", DestinationNumber = "100 000", Seconds = 23, Date = new DateTime(2011,    3,  8,  10, 40, 0) },
                new PhoneCall { SourceNumber = "985 002", DestinationNumber = "985 001", Seconds = 63, Date = new DateTime(2011,    3,  8,  14, 0,  0) },
                new PhoneCall { SourceNumber = "985 002", DestinationNumber = "985 005", Seconds = 07, Date = new DateTime(2011,    4,  8,  14, 37, 0) },
                new PhoneCall { SourceNumber = "100 000", DestinationNumber = "985 001", Seconds = 20, Date = new DateTime(2011,    4,  8,  17, 12, 0) },
                new PhoneCall { SourceNumber = "985 001", DestinationNumber = "985 006", Seconds = 05, Date = new DateTime(2011,    5,  12, 8,  12, 0)},
                new PhoneCall { SourceNumber = "985 005", DestinationNumber = "985 001", Seconds = 22, Date = new DateTime(2011,    5,  5,  10, 35, 0) },
                new PhoneCall { SourceNumber = "985 001", DestinationNumber = "985 002", Seconds = 10, Date = new DateTime(2011,    6,  7,  13, 12, 0) },
                new PhoneCall { SourceNumber = "985 006", DestinationNumber = "985 001", Seconds = 07, Date = new DateTime(2011,    6,  7,  20, 34, 0) },
                new PhoneCall { SourceNumber = "985 005", DestinationNumber = "985 005", Seconds = 03, Date = new DateTime(2011,    7,  8,  10, 40, 0) },
                new PhoneCall { SourceNumber = "985 002", DestinationNumber = "985 001", Seconds = 32, Date = new DateTime(2011,    7,  8,  14, 0,  0) },
            };
        }

        private static List<Department> departments;
        private static List<Employee> employees;
        private static List<Office> offices;
        private static List<PhoneCall> phoneCalls;
        public static Random random = new Random();

        public static string[] GenerarPalabras(int cantidad, int longitudMinima, int longitudMaxima)
        {
            const string caracteres = "abcdefghijklmnopqrstuvwxyz";
            string[] palabras = new string[cantidad];

            for (int i = 0; i < cantidad; i++)
            {
                int longitud = random.Next(longitudMinima, longitudMaxima + 1);
                char[] palabra = new char[longitud];
                for (int j = 0; j < longitud; j++)
                {
                    palabra[j] = caracteres[random.Next(caracteres.Length)];
                }
                palabras[i] = new string(palabra);
            }

            return palabras;
        }
    }
}
