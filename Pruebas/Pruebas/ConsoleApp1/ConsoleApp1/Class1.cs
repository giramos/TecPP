using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1
{
    public static class Class1
    {
        public static T Mayor<T>(this T a, T b) where T : IComparable<T>
        {
            if (a.CompareTo(b) > 0)
            {
                return a;
            }
            return b;
        }
    }
    public interface ICompara
    {
        int CompareTo(object obj);
    }
  public class Angulo: ICompara
    {
        double _radianes;

        public double Radianes { get { return _radianes; } set { _radianes = value; } }

        public Angulo()
        {
            _radianes = 0.0;
        }

        public Angulo(double r)
        {
            _radianes = r;
        }

        public int CompareTo(object obj)
        {
            Angulo angulo = obj as Angulo;
            if(angulo != null)
            {
                return this.Radianes.CompareTo(angulo.Radianes);
            }
            return 0;

        }
    }

    public class Persona : ICompara
    {
        string _name;
        string _surname;

        public string Nombre { get { return _name; } set { _name = value; } }
        public string Apellido { get { return _surname; } set { _surname = value; } }
        public Persona()
        {
            _name = "ANINIMO";
            _surname = "APELIIDO";
        }

        public Persona(string name, string surname)
        {
            _name = name;
            _surname = surname;
        }

        public int CompareTo(object obj)
        {
            Persona p = obj as Persona;
            if(p != null)
            {
                if (this.Nombre.CompareTo(p.Nombre) == 0)
                {
                    return this.Apellido.CompareTo(p.Apellido);
                }
                return this.Nombre.CompareTo(p.Nombre);
            }
            return 0;
        }
    }
}
