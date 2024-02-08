using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoEnClase
{
    internal class Bono: Producto
    {
        int Minutos { get; set; }

        public Bono()
        {
            Minutos = 0;
        }

        public Bono(int min, string name) : base (name)
        {
            Minutos = min;
        }


    }
}
