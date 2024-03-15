using System;
using System.Collections.Generic;

namespace Modelo
{
    public class Department
    {

        public Department(string name, IEnumerable<Employee> employees)
        {
            Name = name;
            Employees = employees;
        }

        public readonly string Name;

        public readonly IEnumerable<Employee> Employees;

        public override string ToString()
        {
            return String.Format("[Department: {0}]", Name);
        }
    }
}
