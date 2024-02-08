using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoEnClase
{
    internal class Estancia : Evento
    {
        enum Color { Rojo, Azul, Verde}
        public int NumParticipantes {  get; set; }
        public bool IncluyeComida { get; set; }

        public Estancia(int num, bool comida)
        {
            NumParticipantes = num;
            IncluyeComida = comida;
        }

        
    }
}
