using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoEnClase
{
    internal class Exhibición : Evento
    {
        public int NumAtletas {  get; set; }

        public Exhibición(int num, string name, string des, DateTime inicio, DateTime fin) : base (name, des, inicio, fin)
        {
            NumAtletas = num;
        }  
    }
}
