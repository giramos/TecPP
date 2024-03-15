using System;

namespace Modelo
{
    public class Office
    {
        public Office(string number, string building)
        {
            Number = number;
            Building = building;
        }

        public readonly string Number;
        public readonly string Building;

        public override string ToString()
        {
            return String.Format("[Office: {0}\\{1}]", Number, Building);
        }
    }
}
