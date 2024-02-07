using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Germán Iglesias Ramos
/// UO202549
/// Lista enlazada objetos
/// TPP2024
/// </summary>

namespace ListaEnlazadaObjetos
{
    /// <summary>
    /// Clase Nodo
    /// </summary>
    public class Nodo
    {
        object _valor; // es el valor o el dato que tiene el Nodo
        Nodo _siguiente; // referencia o apunta al nodo siguiente

        public object Valor { get; set; }
        public Nodo Siguiente { get; set; }

        /// <summary>;
        /// Constructor para crear un nodo con un objeto por valor y apuntando a un nodo inexistente
        /// Bien puede ser para un nodo final o un nodo inicial sin ningun elemento (nodo) en la lista
        /// </summary>
        /// <param name="valor">valor entero que se quiere almacenar en el nodo</param>
        public Nodo(object objeto)
        {
            Valor = objeto;
            Siguiente = null;

        }

        /// <summary>
        /// Consructor para crear un nodo con un objeto por valor y apuntando a un nodo que le pasamos por parametro
        /// </summary>
        /// <param name="objeto">valor objeto que se quiere almacenar en el nodo</param>
        /// <param name="nodo">nodo al que se pretenede apuntar o referenciar</param>
        public Nodo(object objeto, Nodo nodo)
        {
            Valor = objeto;
            Siguiente = nodo;
        }
    }
}
