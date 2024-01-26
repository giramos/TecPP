using System;

/// <summary>
/// Germán Iglesias Ramos
/// UO202549
/// Lista enlazada enteros
/// TPP2024
/// </summary>

namespace ListaEnlazada
{
    /// <summary>
    /// Clase lista
    /// </summary>
    public class Lista
    {
        int _numElmentos; // Numero de elementos que contine la lista
        Nodo _inicial; // Nodo correspondiente al primero, al inicial de la lista

        public int NumElementos { get; private set; }
        Nodo Inicial { get; set; }

        /// <summary>
        /// Constructor para el origen de una lista. 
        /// El nº de elementos sera de 0 al crearse y el nodo inicial no Existira
        /// </summary>
        public Lista()
        {
            NumElementos = 0;
            Inicial = null;
        }

        /// <summary>
        /// Metodo que nos dice si la lista esta ocupada o no
        /// </summary>
        /// <returns>true: si esta vacia  || false: si tiene algun elementos</returns>
        public bool ListaVacia()
        {
            return NumElementos.Equals(0) || Inicial.Equals(null) ? true : false;
        }

        /// <summary>
        /// Metodo que añade un valor a la lista. El valor es añadido bien al nodo final 
        /// si la lista contiene mas de un elemento o al inicio si la lista esta vacia
        /// </summary>
        /// <param name="valor">valor entero que se desea aladir</param>
        public void AñadirFinal(int valor)
        {
            bool esEntero = valor % 1 == 0 ? true : throw new ArgumentException("El valor añadido no es un entero");
            if (esEntero)
            {
                if (ListaVacia())
                {
                    Inicial = new Nodo(valor);
                }
                else
                {
                    //Nodo añadir = new Nodo(valor);
                    Nodo nuevo = Inicial; // reserva de memoria
                    while (nuevo.Siguiente != null)
                    {
                        nuevo = nuevo.Siguiente;
                    }
                    nuevo.Siguiente = new Nodo(valor, nuevo.Siguiente);
                }
                NumElementos++;
            }

        }

        public void AñadirInicio(int valor)
        {
            //bool esEntero = valor % 1 == 0 ? true : false;
            bool esEntero = valor % 1 == 0 ? true : throw new ArgumentException("El valor añadido no es un entero");
            if (esEntero)
                Inicial = new Nodo(valor, Inicial);
            NumElementos++;

        }


        public void Añadir(int valor, int pos)
        {
            bool esEntero = valor % 1 == 0 ? true : throw new ArgumentException("El valor añadido no es un entero");
            bool posValida = pos >= 0 ? true : throw new ArgumentException("La posicion ha de ser mayor o igual a 0");
            if (esEntero)
            {
                if (posValida)
                {
                    if (ListaVacia())
                    {
                        Inicial = new Nodo(valor);
                    }
                    else
                    {
                        if (pos == 0)
                            Inicial = new Nodo(valor, Inicial);
                        else if (pos > 0 && pos < NumElementos)
                        {
                            //Nodo añadir = new Nodo(valor);
                            Nodo nuevo = Inicial; // reserva de memoria
                            for (int i = 0; i < pos - 1; i++)
                            {
                                nuevo = nuevo.Siguiente;

                            }
                            nuevo.Siguiente = new Nodo(valor, nuevo.Siguiente);
                        }
                        else
                            throw new ArgumentException("La posicion no es valida");
                    }
                    NumElementos++;
                }
            }
        }


        public override string ToString()
        {
            string str = "Lista Enlazada:";
            Nodo nuevo = Inicial;
            while (nuevo != null)
            {
                str += " " + nuevo.Valor.ToString();
                nuevo = nuevo.Siguiente;
            }
            return str;
        }
    }
}
