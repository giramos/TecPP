using System;

namespace TrabajoEnClase
{
    public class Producto : IFacturable
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public bool Disponibilidad { get; set; }

        public Producto()
        {
            Nombre = "Anonimo";
            Descripcion = "VACIO";
            Stock = 0;
            Disponibilidad = false;
        }
        public Producto(string name)
        {
            Nombre = name;
            Descripcion = "VACIO";
            Stock = 0;
            Disponibilidad = false;
        }
        public Producto(string name, string des, int sto, bool dis)
        {
            Nombre = name;
            Descripcion = des;
            Stock = sto;
            Disponibilidad = dis;
        }

        public void Reponer(int unidades)
        {
            Stock += unidades;
        }
        public void Eliminar(int unidades)
        {
            Stock -= unidades;
        }

        public virtual double GetBase(double @double)
        {
            throw new NotImplementedException();
        }

        public double GetTipoIva(double @double)
        {
            if (@double.Equals(0.21))
            {
                return 0.21;
            }
            else if (@double.Equals(0.18))
            {
                return 0.18;
            }
            else if (@double.Equals(0.10))
            {
                return 0.10;
            }
            else
                return 0.03;
        }

        public virtual double GetIVA()
        {
            throw new NotImplementedException();
        }

        public double GetPrecio(double @double)
        {
            return GetBase(@double) + GetIVA();
        }

        public virtual string GetConcepto(string concepto)
        {
            concepto = "En concepto de: ";
            return concepto;
        }

        public virtual string ToTicket()
        {
            throw new NotImplementedException();
        }
    }
}
