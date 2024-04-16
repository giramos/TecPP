﻿using ListaGenerica;
using System;
using System.Diagnostics;

namespace ColaConcurrente
{
    public class Cola<T>
    {
        Lista<T> lista;
        static readonly object obj = new();

        public bool EstaVacia => lista.NumElementos == 0 ? true : false;

        public int NumerElementos => lista.NumElementos;

        public Cola()
        {
            lista = new Lista<T>();
        }

        public void Añadir(T obj)
        {
            lock(obj) 
                lista.Añadir(obj);
        }

        public T Extraer()
        {
            if (EstaVacia)
                throw new InvalidOperationException("La cola esta vacia por lo que no es posible sacar el primer elemento");

            var elementoPrimero = default(T);
            lock (obj)
            {
                elementoPrimero = lista.GetElemento(0);
                lista.BorrarInicio(elementoPrimero);
            }
            return elementoPrimero;
        }

        public T PrimerElemento()
        {
            if (EstaVacia)
                throw new InvalidOperationException("La cola esta vacia por lo que no es posible sacar el primer elemento");
            var elementoPrimero = default(T);
            lock (obj)
            {
                elementoPrimero = lista.GetElemento(0);
            }
            return elementoPrimero;
        }

        public void Clear()
        {
            if (EstaVacia)
                throw new InvalidOperationException("La cola ya esta vacia, nun vien'a cuento realizar ninguna limpieza de la cola");
            lock(obj)
                lista.VaciarLista();

        }

        public int Count()
        {
            return lista.NumElementos;
        }

        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < lista.NumElementos; i++)
            {
                str += " " + lista.GetElemento(i).ToString() + "\n";
            }

            return str;
        }


    }
}
