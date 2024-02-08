using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoEnClase
{
    internal class Evento : IFacturable
    {
        public string Nombre {  get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public Evento()
        {
            Nombre = "DESCONOCIDO";
            Descripcion = "VACIO";
            FechaInicio = DateTime.Now;
            FechaFin = DateTime.Now.AddDays(1);
        }
        public Evento(string name, string des, DateTime inicio, DateTime fin)
        {
            Nombre = name;
            Descripcion = des;
            FechaInicio = inicio;
            FechaFin = fin;
        }

        public virtual double GetBase(double @double)
        {
            throw new NotImplementedException();
        }

        public virtual double GetTipoIva(double @double)
        {
            throw new NotImplementedException();
        }

        public virtual double GetIVA()
        {
            throw new NotImplementedException();
        }

        public virtual double GetPrecio(double @double)
        {
            throw new NotImplementedException();
        }

        public virtual string GetConcepto(string concepto)
        {
            throw new NotImplementedException();
        }

        public virtual string ToTicket()
        {
            throw new NotImplementedException();
        }
    }
}
