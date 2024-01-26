using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Lista enlazada
/// </summary>
namespace ListaEnlazada
{
    /// <summary>
    /// Clase Nodo
    /// </summary>
    public class Nodo
    {
        int _valor; // es el valor o el dato que tiene el Nodo
        Nodo _siguiente; // referencia o apunta al nodo siguiente

        public int Valor { get; set; }
        public Nodo Siguiente { get; set; }

        /// <summary>
        /// Constructor para crear un nodo con un entero por valor y apuntando a un nodo inexistente
        /// Bien puede ser para un nodo final o un nodo inicial sin ningun elemento (nodo) en la lista
        /// </summary>
        /// <param name="entero">valor entero que se quiere almacenar en el nodo</param>
        public Nodo(int entero)
        {
            Valor = entero;
            Siguiente = null;

        }

        /// <summary>
        /// Consructor para crear un nodo con un entero por valor y apuntando a un nodo que le pasamos por parametro
        /// </summary>
        /// <param name="entero">valor entero que se quiere almacenar en el nodo</param>
        /// <param name="nodo">nodo al que se pretenede apuntar o referenciar</param>
        public Nodo(int entero, Nodo nodo)
        {
            Valor = entero;
            Siguiente = nodo;
        }
    }
}
