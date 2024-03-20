using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListaGenerica
{
    /// <summary>
    /// Clase Nodo
    /// </summary>
    public class Nodo<T>
    {
        T _valor; // es el valor o el dato que tiene el Nodo
        Nodo<T> _siguiente; // referencia o apunta al nodo siguiente

        public T Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }
        public Nodo<T> Siguiente { get; set; }

        /// <summary>;
        /// Constructor para crear un nodo con un objeto por valor y apuntando a un nodo inexistente
        /// Bien puede ser para un nodo final o un nodo inicial sin ningun elemento (nodo) en la lista
        /// </summary>
        /// <param name="valor">valor entero que se quiere almacenar en el nodo</param>
        public Nodo(T objeto)
        {
            Valor = objeto;
            Siguiente = null;

        }

        /// <summary>
        /// Consructor para crear un nodo con un objeto por valor y apuntando a un nodo que le pasamos por parametro
        /// </summary>
        /// <param name="objeto">valor objeto que se quiere almacenar en el nodo</param>
        /// <param name="nodo">nodo al que se pretenede apuntar o referenciar</param>
        public Nodo(T objeto, Nodo<T> nodo)
        {
            Valor = objeto;
            Siguiente = nodo;
        }
    }
}
