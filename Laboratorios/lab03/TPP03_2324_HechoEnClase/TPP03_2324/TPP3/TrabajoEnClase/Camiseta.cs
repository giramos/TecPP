using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TrabajoEnClase
{
    public class Camiseta : Producto
    {
       
        public string Talla { get; set; }
        public string Color { get ; set; }

        public Camiseta(string name, string des, int sto, bool dis, string tal, string col) : base(name, des, sto, dis)
        {
            Talla = tal;
            Color = col;
        }

        public double ColorNumero(string color)
        {
            if(color == "negro")
            {
                return 1.0;
            }
            return
        }

        public override double GetBase(double @double)
        {
            // Supongamos un precio base de 10 dólares por camiseta, con un ajuste según el color
            @double = (double)Color;
            double precioBase = 10.0;
            if (Color == "Rojo")
                precioBase += 2.0; // Ejemplo de ajuste por color
            else if (Color == "Azul")
                precioBase += 1.0; // Ejemplo de ajuste por color

            return precioBase;
        }
        public override double GetIVA()
        {
            return GetBase(0.21) * GetTipoIva(0.21);
        }

        public override string GetConcepto(string concepto)
        {
            return base.GetConcepto(concepto) + $"{concepto}";
        }
        public override string ToTicket()
        {
            return $"Ticket: [Base: {GetBase(0.21)} - TipoIVA: {GetTipoIva(0.21)} - Precio: {GetPrecio(0.21)} - IVA: {GetIVA()}]";
        }
    }
}
