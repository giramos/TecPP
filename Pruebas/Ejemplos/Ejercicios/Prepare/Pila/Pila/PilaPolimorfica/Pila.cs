using ListaEnlazadaObjetos;
using System;
using System.Diagnostics;

namespace PilaPolimorfica
{
    public class Pila
    {
        Lista lista;
        uint _numeroMaxElementos;

        public bool EstaVacia { get { return lista.NumElementos == 0 ? true : false; } }
        public bool EstaLlena { get { return lista.NumElementos == _numeroMaxElementos ? true : false; } }

        public Pila(uint numeroMaxElementos)
        {
            if (numeroMaxElementos == 0)
                throw new ArgumentException("¿Para qué creas una pila con un máximo de 0 elementos?");
            _numeroMaxElementos = numeroMaxElementos;
            lista = new Lista();
#if DEBUG
            Invariante();
# endif
        }

        public void Push(object obj)
        {
#if DEBUG
            Invariante();
#endif
            if (EstaLlena)
                throw new InvalidOperationException("La pila esta llena por lo que es imposible añadir mas elementos");
            var numeroAnterior = lista.NumElementos;
            lista.Añadir(obj);
            Debug.Assert(!EstaVacia, "La pila una vez añadido un elemento debe de dejar de estar vacia");
            Debug.Assert(lista.NumElementos == numeroAnterior + 1, "El numero de elementos de la pila ha aumentado al añadir un elemento");
#if DEBUG
            Invariante();
#endif
        }

        public object Pop()
        {
#if DEBUG
            Invariante();
#endif
            if (EstaVacia)
                throw new InvalidOperationException("La pila esta vacia por lo que no es posible sacar, desapilar un elemento");
            var numeroAnterior = lista.NumElementos;
            var elementoUltimo = lista.GetElemento(lista.NumElementos - 1);
            lista.Borrar(elementoUltimo);
            Debug.Assert(!EstaLlena, "La pila una vez desapilado un elemento no puede estar llena");
            Debug.Assert(lista.NumElementos == numeroAnterior - 1, "El numero de elementos de la pila ha disminuido al desapilar un elemnto");
#if DEBUG
            Invariante();
#endif
            return elementoUltimo;
        }

        public object Peek()
        {
#if DEBUG
            Invariante();
#endif
            if (EstaVacia)
                throw new InvalidOperationException("La pila esta vacia por lo que no es posible sacar el elemento final");
#if DEBUG
            Invariante();
#endif
            return lista.GetElemento(lista.NumElementos - 1);
        }

        public void Clear()
        {
#if DEBUG
            Invariante();
#endif
            if (EstaVacia)
                throw new InvalidOperationException("La pila ya esta vacia, nun vien'a cuento realizar ninguna limpieza de pila");
            lista.VaciarLista();
#if DEBUG
            Invariante();
#endif

        }

        public int Count()
        {
#if DEBUG
            Invariante();
#endif
            return lista.NumElementos;
        }

        public override string ToString()
        {
#if DEBUG
            Invariante();
#endif
            string str = "";
            for (int i = 0; i < lista.NumElementos; i++)
            {
                str += " " + lista.GetElemento(i).ToString() + "\n";
            }

#if DEBUG
            Invariante();
#endif
            return str;
        }

        void Invariante()
        {
            Debug.Assert(!(EstaLlena && EstaVacia), "La pila no puede estar al mismo tiempo vacia y llena");
            //Debug.Assert(!EstaLlena && EstaVacia, "La pila no puede estar al mismo tiempo vacia y llena");
            //Debug.Assert(!EstaLlena && !EstaVacia, "La pila no puede estar al mismo tiempo vacia y llena");
            //Debug.Assert(EstaLlena && lista.NumElementos == _numeroMaxElementos, "La pila si esta llena tiene que tener el mismo numero de elementos la lista que el numero maximo de elementos de la pila");
            //Debug.Assert(EstaVacia && lista.NumElementos == 0, "La pila si esta vacia debe de tener el numero de elementos de la pila un valor de 0");
            Debug.Assert(lista.NumElementos <= _numeroMaxElementos, "La pila no puede tener mas elementos que la lista");
        }
    }
}
