using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio03
{
    public class Barra: Intervalo
    {
        double _altura;

        public Barra()
        {
            _altura = 0;
        }

        public Barra(double alt, double? extI, double? extD) : base(extI,extD) 
        {
            if(alt >= 0) 
                _altura = alt;
            Invariante();
        }

        public override bool Equals(object obj)
        {
            return obj is Barra barra &&
                   base.Equals(obj) &&
                   _altura == barra._altura;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), _altura);
        }

        public bool Invariante()
        {
            return base.Check() || _altura >= 0;
        }

        public double? Size()
        {
            return _altura * base.Tamaño();
        }

        public override string ToString()
        {
            return base.ToString() + $" Barra: altura[{_altura}]";
        }

        public static Barra operator *(Barra a, Barra b)
        {
            double newAlto = 0;
            if (a._altura <= b._altura)
                newAlto = a._altura;
            else
                newAlto = b._altura;

            Intervalo c = new Intervalo(a.extIzquierdo, a.extDerecho) * new Intervalo(b.extIzquierdo, b.extDerecho);
            return new Barra(newAlto, c.extIzquierdo, c.extDerecho);

        }

    }
}
