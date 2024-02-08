using System;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Xml.Linq;

/// <summary>
/// Germán Iglesias Ramos
/// UO202549
/// Lista enlazada objetos
/// TPP2024
/// </summary>

namespace ListaEnlazadaObjetos
{
    /// <summary>
    /// Clase lista
    /// </summary>
    public class Lista
    {
        int _numElmentos; // Numero de elementos que contine la lista
        Nodo _inicial; // Nodo correspondiente al primero, al inicial de la lista

        public int NumElementos { get { return _numElmentos; } }
        Nodo Inicial { get; set; }

        /// <summary>
        /// Constructor para el origen de una lista. 
        /// El nº de elementos sera de 0 al crearse y el nodo inicial no Existira
        /// </summary>
        public Lista()
        {
            _numElmentos = 0;
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
        /// <param name="valor">valor objeto que se desea aladir</param>
        public void AñadirFinal(object valor)
        {
            //bool esDistintoNull = valor != null ? true : throw new ArgumentException("No está permitido añadir null a nuestra lista");
            //if (esDistintoNull)
            //{
            if (ListaVacia())
            {
                Inicial = new Nodo(valor);
            }
            else
            {
                Nodo nuevo = Inicial;
                while (nuevo.Siguiente != null)
                {
                    nuevo = nuevo.Siguiente;
                }
                nuevo.Siguiente = new Nodo(valor, nuevo.Siguiente);
            }
            _numElmentos++;
            //}
        }

        /// <summary>
        /// Metodo añadir al inicion de la lista
        /// </summary>
        /// <param name="valor">Valor que deseamos añadir</param>
        public void AñadirInicio(object valor)
        {
            //bool esDistintoNull = valor != null ? true : throw new ArgumentException("No está permitido añadir null a nuestra lista");
            //if (esDistintoNull)
            Inicial = new Nodo(valor, Inicial);
            _numElmentos++;

        }

        /// <summary>
        /// Metodo añadir por posicion y por valor
        /// </summary>
        /// <param name="valor">Valor que deseamos añadir</param>
        /// <param name="pos">Psocion en la que deseamos añadir el valor</param>
        public void Añadir(object valor, int pos)
        {
            //bool esDistintoNull = valor != null ? true : throw new ArgumentException("No está permitido añadir null a nuestra lista");
            bool posValida = pos >= 0 ? true : throw new ArgumentException("La posicion ha de ser mayor o igual a 0");
            //if (esDistintoNull)
            //{
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
                    else if (pos > 0 || pos < NumElementos)
                    {
                        Nodo nuevo = Inicial;
                        for (int i = 0; i < pos - 1; i++)
                        {
                            nuevo = nuevo.Siguiente;
                        }
                        nuevo.Siguiente = new Nodo(valor, nuevo.Siguiente);
                    }
                    else
                        throw new ArgumentException("La posicion no es valida");
                }
                _numElmentos++;
            }
            //}
        }

        /// <summary>
        /// Metodo que añade un valor a la lista. Se va añadiendo los elementos en orden ascendente (0-1-...)
        /// </summary>
        /// <param name="valor">Valor que deseamos añadir a la lista</param>
        public virtual void Añadir(object valor)
        {
            //bool esDistintoNull = valor != null ? true : throw new ArgumentException("No está permitido añadir null a nuestra lista");
            //if (esDistintoNull)
            //{
            if (ListaVacia())
            {
                Inicial = new Nodo(valor);
            }
            else
            {
                Nodo nuevo = Inicial;
                while (nuevo.Siguiente != null)
                {
                    nuevo = nuevo.Siguiente;
                }
                nuevo.Siguiente = new Nodo(valor, null);
            }
            _numElmentos++;
            //}
        }

        /// <summary>
        /// Metodo que borra el primer nodo de la lista siempre y cuando coincida con el valor pasado por parametro
        /// </summary>
        /// <param name="valor">valor que deseamos borrar del inicio</param>
        public void BorrarInicio(object valor)
        {
            //bool esDistintoNull = valor != null ? true : throw new ArgumentException("No está permitido usar null en nuestra lista");
            //if (esDistintoNull)
            //{
            if (ListaVacia())
                throw new Exception("La lista está vacía, no hay nada que borrar");
            else if (Inicial.Valor == null && valor == null)
                Inicial = Inicial.Siguiente;
            else if (Inicial.Valor != null && Inicial.Valor.Equals(valor))
                Inicial = Inicial.Siguiente;
            else
                throw new ArgumentException("El valor debe ser igual al nodo inicial");
            _numElmentos--;
            //}
        }

        /// <summary>
        /// Metodo que borra el nodo final de la lista
        /// </summary>
        /// <param name="valor">Valor que debe de tener el nodo final</param>
        /// <exception cref="Exception">Si la lista esta vacia, no hay nada que borrar</exception>
        /// <exception cref="ArgumentException">Si no existe un nodo final con el valor pasado por parametro</exception>
        public void BorrarFinal(object valor)
        {
            //bool esDistintoNull = valor != null ? true : throw new ArgumentException("No está permitido usar null en nuestra lista");
            //if (esDistintoNull)
            //{
            if (ListaVacia())
                throw new Exception("La lista está vacía, no hay nada que borrar");
            else
            {
                //Nodo añadir = new Nodo(valor);
                Nodo nuevo = Inicial;
                Nodo nodoABorrar = null;

                while (nuevo.Siguiente != null)
                {
                    if ((nuevo.Siguiente.Valor == null && valor == null) || (nuevo.Siguiente.Valor != null && nuevo.Siguiente.Valor.Equals(valor)))
                    {
                        nodoABorrar = nuevo;
                    }
                    nuevo = nuevo.Siguiente;
                }
                if (nodoABorrar == null)
                    throw new ArgumentException("No hay un nodo final con ese valor");
                if (nodoABorrar.Siguiente.Siguiente == null)
                {
                    nodoABorrar.Siguiente = null;
                }

                _numElmentos--;
            }
            //}
        }

        /// <summary>
        /// Metodo que borra un elemento de la lista que coincida con el valor pasado por parametro.
        /// Si dos elementos tienen el mismo valor se borrara siempre el primero que se encuentre en la lista
        /// </summary>
        /// <param name="valor">Valor que deseamos borrar de la lista</param>
        /// <exception cref="ArgumentException">Si el valor pasado no se corresponde con un entero</exception>
        /// <exception cref="Exception">si la lista esta vacia, no habra elemento a borrar</exception>
        public void Borrar(object valor)
        {
            //bool esDistintoNull = valor != null ? true : throw new ArgumentException("No está permitido usar null en nuestra lista");
            //if (esDistintoNull)
            //{
            if (!ListaVacia())
            {
                if (Inicial.Valor.Equals(valor))
                {
                    Inicial = Inicial.Siguiente;
                    _numElmentos--;
                }
                else
                {
                    Nodo nuevo = Inicial;
                    while (nuevo.Siguiente != null)
                    {
                        if ((nuevo.Siguiente.Valor == null && valor == null) || (nuevo.Siguiente.Valor != null && nuevo.Siguiente.Valor.Equals(valor)))
                        {
                            nuevo.Siguiente = nuevo.Siguiente.Siguiente;
                            _numElmentos--;
                            return;
                        }
                        nuevo = nuevo.Siguiente;
                    }
                    throw new ArgumentException("El valor pasado no se encuentra en la lista");
                }
            }
            else
                throw new Exception("La lista está vacía, no hay nada que borrar");
            //}
        }

        /// <summary>
        /// Metodo que nos devueleve el elemento en la posicion que indicamos por parametro
        /// </summary>
        /// <param name="pos">posicion de la que queremos saber el valor del elemento </param>
        /// <returns>devuleve el valor del elemento</returns>
        /// <exception cref="ArgumentException"></exception>
        public object GetElemento(int pos)
        {
            bool posValida = pos >= 0 && pos < NumElementos ? true : throw new ArgumentException("La posicion ha de ser mayor o igual a 0 y menor al numero de elementos");
            Nodo nuevo = Inicial;
            if (posValida)
            {
                if (ListaVacia())
                    throw new ArgumentException("La lista esta vacia, no se puede obtener elementos de ella");
                else
                {
                    for (int i = 0; i < pos; i++)
                    {
                        nuevo = nuevo.Siguiente;
                    }
                }
            }
            return nuevo.Valor;
        }

        /// <summary>
        /// Metodo que nos devuelve la posicion de un valor pasado por parametro
        /// </summary>
        /// <param name="valor">Valor del cual queremos saber su posicion</param>
        /// <returns>devuelve la posicion del valor</returns>
        /// <exception cref="ArgumentException"></exception>
        public int GetPosicion(object valor)
        {
            bool esDistintoNull = valor != null ? true : false;
            int pos = -1;
            if (esDistintoNull)
            {
                if (ListaVacia())
                    throw new ArgumentException("La lista esta vacia, no se puede obtener elementos de ella");
                else
                {
                    for (int i = 0; i < NumElementos; i++)
                    {
                        if (GetElemento(i) != null && GetElemento(i).Equals(valor))
                        {
                            pos = i;
                            break;
                        }
                    }
                    if (pos == -1) 
                        throw new ArgumentException("El valor pasado por parámetro no se encuentra en la lista");                     
                }
            }
            else
            {
                if (ListaVacia())
                    throw new ArgumentException("La lista esta vacia, no se puede obtener elementos de ella");
                else
                {
                    for (int i = 0; i < NumElementos; i++)
                    { 
                        if (GetElemento(i) == null)
                        {
                            pos = i;
                            break;
                        }
                    }
                    if (pos == -1) {
                        throw new ArgumentException("El valor pasado por parámetro no se encuentra en la lista");
                    }                        
                }
            }
            return pos;
        }

        /// <summary>
        /// Metodo que nos devolvera (true/false) si coincide el valor pasado por parametro con un elemento de nuestra lista
        /// El orden de comprobacion sera desde el inicio del la lista (pos=0), hasta el final de ella
        /// </summary>
        /// <param name="valor">valor que queremos comprobar si esta presente en la lista</param>
        /// <returns>true-> si esta presente || falsa-> si NO esta presente</returns>
        /// <exception cref="ArgumentException"></exception>
        public bool Contiene(object valor)
        {
            bool esDistintoNull = valor != null ? true : false;
            if (esDistintoNull)
            {
                for (int i = 0; i < NumElementos; i++)
                {
                    if (valor.Equals(GetElemento(i))) // Cuidado con el == -> identidad vs ref
                    {
                        return true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < NumElementos; i++)
                {
                    if (GetElemento(i)==null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void VaciarLista()
        {
            if (!ListaVacia())
            {
                Inicial = null;
                _numElmentos = 0;
            }

        }

        /// <summary>
        /// Metodo redefinido ToString()
        /// </summary>
        /// <returns>Devuelve una lista con el valor de todos los nodos</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Nodo nuevo = Inicial;
            sb.Append("[");
            while (nuevo != null)
            {
                sb.Append($"{nuevo.Valor ?? "null"} - ");
                nuevo = nuevo.Siguiente;
            }
            sb.Append("]");
            return sb.ToString();
        }
    }
}
