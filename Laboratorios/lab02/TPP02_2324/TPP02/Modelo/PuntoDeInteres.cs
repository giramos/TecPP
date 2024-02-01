using System;

namespace Modelo
{
    public class PuntoDeInteres
    {
        public double Latitud { get; }
        public double Longitud { get; }
        public string Nombre { get; }

        public PuntoDeInteres(double latitud, double longitud, string nombre)
        {
            Latitud = latitud;
            Longitud = longitud;
            Nombre = nombre;
        }

        public override string ToString()
        {
            return String.Format("{0} en {1} {2}", Nombre, Latitud, Longitud);
        }
    }
}
