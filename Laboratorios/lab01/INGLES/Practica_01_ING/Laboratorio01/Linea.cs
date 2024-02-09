using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio01
{
    public class Linea
    {
        double _m, _n;
        public double M { get { return _m; } set { _m = value; } }
        public double N { get { return _n; } set { _n = value; } }

        public Linea(double m, double n)
        {
            M = m;
            N = n;
        }

        public Linea()
        {
            M = 0;
            N = 0;
        }

        ~Linea() { Console.WriteLine("Ejecutando el destructor ... m= " + M + "y n= " + N); }

        public Linea Perpendicular(Point2d punto)
        {
            double m = (-1/this.M);
            double n = (punto.Y + ((1 / this.M) * punto.X));
            return new Linea(m, n);
        }

        public Point2d Interseccion(Linea linea)
        {
            // y0=(n1/ m1−n2/ m2)/ (1 / m1−1 / m2)
            double coordenadaY = ((this.N/this.M) - (linea.N/linea.M))/((1/this.M) - (1/linea.M));
            // x0=( y0−n2)/ m2
            double coordenadaX = (((coordenadaY-linea.N)/linea.M));
            return new Point2d(coordenadaX, coordenadaY);
        }

        public double DistanciaLineaPunto(Point2d punto)
        {
            var per = Perpendicular(punto);
            var inter = Interseccion(per);
            return punto.DistanciaEuclidea(inter);
        }

        public override string ToString()
        {
            return $"y={M}x + {N}";
        }
    }
}
