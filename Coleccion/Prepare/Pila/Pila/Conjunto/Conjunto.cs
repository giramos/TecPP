using ListaEnlazadaObjetos;
using System;
using System.Net;

/// <summary>
/// Germán Iglesias Ramos
/// UO202549
/// Lista enlazada objetos
/// TPP2024
/// </summary>

namespace ConjuntoObjetos
{
    public class Conjunto : Lista
    {
        public Conjunto() : base() { }

        public void AñadirConjunto(object valor)
        {
            if (base.Contiene(valor))
                throw new InvalidOperationException("No se pueden añadir en el conjunto elementos repetidos");
            else
                base.Añadir(valor);
        }

        public static Conjunto operator +(Conjunto set, object valor)
        {
            set.AñadirConjunto(valor); return set;
        }

        public static Conjunto operator -(Conjunto set, object valor)
        {
            set.Borrar(valor); return set;
        }


    }
}
