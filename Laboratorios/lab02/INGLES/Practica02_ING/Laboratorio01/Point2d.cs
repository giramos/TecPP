using System;

namespace Laboratorio01
{
    public enum Color { Transparente=1, Negro=2, Rojo=3};
    public class Point2d
    {
        double _x, _y;
        Color _color;

        public double X { get { return _x; } set {  _x = value; } }
        public double Y { get { return _y; } set {  _y = value; } }
        public Color Color { get { return _color; } set { _color = value; } }

        public Point2d(double x, double y, Color color)
        {
            X = x;
            Y = y;
            Color = color;
        }
        public Point2d(double x, double y)
        {
            X = x;
            Y = y;
            Color = (Color)1; // transparente
        }

        public Point2d()
        {
            X = 0; Y = 0;
            Color = Color.Transparente;
        }

        ~Point2d() { Console.WriteLine($"Ejecutando el destructor... color:{Color}-x:{X}-y:{Y}"); }

        /// <summary>
        /// {\displaystyle d(p,q)={\sqrt {(q_{1}-p_{1})^{2}+(q_{2}-p_{2})^{2}}}}.
        /// </summary>
        /// <param name="puntoA"></param>
        /// <param name="puntoB"></param>
        /// <returns></returns>
        public double DistanciaEuclidea(Point2d a)
        {
            return Math.Sqrt((this.X - a.X) * (this.X - a.X) + (this.Y - a.Y) * (this.Y - a.Y));
        }

        public override string ToString()
        {
            return $"({X},{Y}):{Color}";
        }


    }
}
