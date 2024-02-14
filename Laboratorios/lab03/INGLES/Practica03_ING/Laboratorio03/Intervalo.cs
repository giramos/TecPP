using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio03
{
    public class Intervalo : IComparable
    {
        public double? extIzquierdo;
        public double? extDerecho;
            
        public Intervalo()
        {
            extIzquierdo = null;
            extDerecho = null;
            Check();
        }

        public Intervalo(double? a, double? b)
        {
            extIzquierdo = a;
            extDerecho = b;
            Check();
        }

        public double? Tamaño()
        {
            if (extDerecho == null && extIzquierdo == null)
                return null;
            return extDerecho - extIzquierdo;
        }

        public override bool Equals(object obj)
        {
            return obj is Intervalo intervalo &&
                   extIzquierdo == intervalo.extIzquierdo &&
                   extDerecho == intervalo.extDerecho;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(extIzquierdo, extDerecho);
        }

        protected bool Check()
        {
            if (extIzquierdo == null && extDerecho == null)
                return !(extIzquierdo <= extDerecho);
            return (extIzquierdo <= extDerecho) || (extIzquierdo == null && extDerecho != null) ||
                (extIzquierdo != null && extDerecho == null);

        }

        private void Invariante()
        {
            if (extIzquierdo == null && extDerecho == null)
                Debug.Assert(!(extIzquierdo <= extDerecho));
            Debug.Assert(extIzquierdo <= extDerecho);
            Debug.Assert(extIzquierdo == null && extDerecho != null);
            Debug.Assert(extIzquierdo != null && extDerecho == null);
        }

        //Sean A=(-2,4) y B=(2,5) dos intervalos de la recta real, 
        //su unión será A∪B=(-2,5), y su intersección será A∩B=(2,4)
        public static Intervalo operator *(Intervalo a, Intervalo b)
        {
            double? newA = 0;
            if (a.extIzquierdo >= b.extIzquierdo)
                newA = a.extIzquierdo;
            else
                newA = b.extIzquierdo;

            double? newB = 0;
            if (a.extDerecho <= b.extDerecho)
                newB = a.extDerecho;
            else
                newB = b.extDerecho;

            return new Intervalo(newA, newB);
        }

        public override string ToString()
        {
            return $"Intervalo: extremoIzquierda[{extIzquierdo}] extremoDerecha[{extDerecho}]";
        }
    }
}
