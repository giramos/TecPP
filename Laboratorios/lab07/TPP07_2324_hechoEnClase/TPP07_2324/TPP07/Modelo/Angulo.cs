using System;

namespace Modelo
{
    public class Angulo
    {
        public double Radianes { get; }

        public float Grados
        {
            get { return (float)(this.Radianes / Math.PI * 180); }
        }

        public Angulo(double radianes)
        {
            this.Radianes = radianes;
        }

        public Angulo(float grados)
        {
            this.Radianes = grados / 180.0 * Math.PI;
        }

        public double Seno()
        {
            return Math.Sin(this.Radianes);
        }

        public double Coseno()
        {
            return Math.Cos(this.Radianes);
        }

        public double Tangente()
        {
            return Seno() / Coseno();
        }
    }
}
