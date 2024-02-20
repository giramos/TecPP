using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Angulo
    {
        public double Radianes { get; private set; }

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

        public override string ToString()
        {
            return $"Radianes: {this.Radianes}, Grados: {this.Grados}, Seno: {this.Seno()}, Coseno: {this.Coseno()}, Tangente: {this.Tangente()}, Cuadrante: {this.Cuadrante}";
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

        public int Cuadrante
        {
            get
            {
                if (this.Radianes <= Math.PI / 2)
                    return 1;
                if (this.Radianes <= Math.PI)
                    return 2;
                if (this.Radianes <= 3 * Math.PI / 2)
                    return 3;
                return 4;
            }
        }

    }


}
