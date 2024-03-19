using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Germán Iglesias Ramos
/// UO202549
/// Lista enlazada generica
/// TPP2024
/// </summary>

namespace ListaGenerica
{   /// <summary>
    /// Clase lista
    /// </summary>
    public class Lista<T> : IEnumerable<T>
    {
        int _numElmentos; // Numero de elementos que contine la lista
        Nodo<T> _inicial; // Nodo correspondiente al primero, al inicial de la lista

        public int NumElementos { get { return _numElmentos; } }


        /// <summary>
        /// Constructor para el origen de una lista. 
        /// El nº de elementos sera de 0 al crearse y el nodo inicial no Existira
        /// </summary>
        public Lista()
        {
            _numElmentos = 0;
            _inicial = null;
        }

        /// <summary>
        /// Metodo que nos dice si la lista esta ocupada o no
        /// </summary>
        /// <returns>true: si esta vacia  || false: si tiene algun elementos</returns>
        public bool ListaVacia()
        {
            return NumElementos.Equals(0) || _inicial.Equals(null) ? true : false;
        }

        /// <summary>
        /// Metodo que añade un valor a la lista. El valor es añadido bien al nodo final 
        /// si la lista contiene mas de un elemento o al inicio si la lista esta vacia
        /// </summary>
        /// <param name="valor">valor objeto que se desea aladir</param>
        public void AñadirFinal(T valor)
        {
            if (ListaVacia())
            {
                _inicial = new Nodo<T>(valor);
            }
            else
            {
                Nodo<T> nuevo = _inicial;
                while (nuevo.Siguiente != null)
                {
                    nuevo = nuevo.Siguiente;
                }
                nuevo.Siguiente = new Nodo<T>(valor, nuevo.Siguiente);
            }
            _numElmentos++;
        }

        /// <summary>
        /// Metodo añadir al inicion de la lista
        /// </summary>
        /// <param name="valor">Valor que deseamos añadir</param>
        public void AñadirInicio(T valor)
        {
            _inicial = new Nodo<T>(valor, _inicial);
            _numElmentos++;

        }

        /// <summary>
        /// Metodo añadir por posicion y por valor
        /// </summary>
        /// <param name="valor">Valor que deseamos añadir</param>
        /// <param name="pos">Psocion en la que deseamos añadir el valor</param>
        public void Añadir(T valor, int pos)
        {
            bool posValida = pos >= 0 ? true : throw new ArgumentException("La posicion ha de ser mayor o igual a 0");
            if (posValida)
            {
                if (ListaVacia())
                {
                    _inicial = new Nodo<T>(valor);
                }
                else
                {
                    if (pos == 0)
                        _inicial = new Nodo<T>(valor, _inicial);
                    else if (pos > 0 || pos < NumElementos)
                    {
                        Nodo<T> nuevo = _inicial;
                        for (int i = 0; i < pos - 1; i++)
                        {
                            nuevo = nuevo.Siguiente;
                        }
                        nuevo.Siguiente = new Nodo<T>(valor, nuevo.Siguiente);
                    }
                    else
                        throw new ArgumentException("La posicion no es valida");
                }
                _numElmentos++;
            }
        }

        /// <summary>
        /// Metodo que añade un valor a la lista. Se va añadiendo los elementos en orden ascendente (0-1-...)
        /// </summary>
        /// <param name="valor">Valor que deseamos añadir a la lista</param>
        public void Añadir(T valor)
        {
            if (ListaVacia())
            {
                _inicial = new Nodo<T>(valor);
            }
            else
            {
                Nodo<T> nuevo = _inicial;
                while (nuevo.Siguiente != null)
                {
                    nuevo = nuevo.Siguiente;
                }
                nuevo.Siguiente = new Nodo<T>(valor, null);
            }
            _numElmentos++;
        }

        /// <summary>
        /// Metodo que borra el primer nodo de la lista siempre y cuando coincida con el valor pasado por parametro
        /// </summary>
        /// <param name="valor">valor que deseamos borrar del inicio</param>
        public void BorrarInicio(T valor)
        {
            if (ListaVacia())
                throw new Exception("La lista está vacía, no hay nada que borrar");
            else if (_inicial.Valor == null && valor == null)
                _inicial = _inicial.Siguiente;
            else if (_inicial.Valor != null && _inicial.Valor.Equals(valor))
                _inicial = _inicial.Siguiente;
            else
                throw new ArgumentException("El valor debe ser igual al nodo inicial");
            _numElmentos--;
        }

        /// <summary>
        /// Metodo que borra el nodo final de la lista
        /// </summary>
        /// <param name="valor">Valor que debe de tener el nodo final</param>
        /// <exception cref="Exception">Si la lista esta vacia, no hay nada que borrar</exception>
        /// <exception cref="ArgumentException">Si no existe un nodo final con el valor pasado por parametro</exception>
        public void BorrarFinal(T valor)
        {
            if (ListaVacia())
                throw new Exception("La lista está vacía, no hay nada que borrar");
            else
            {
                //Nodo añadir = new Nodo(valor);
                Nodo<T> nuevo = _inicial;
                Nodo<T> nodoABorrar = null;

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
        }

        /// <summary>
        /// Metodo que borra un elemento de la lista que coincida con el valor pasado por parametro.
        /// Si dos elementos tienen el mismo valor se borrara siempre el primero que se encuentre en la lista
        /// </summary>
        /// <param name="valor">Valor que deseamos borrar de la lista</param>
        /// <exception cref="ArgumentException">Si el valor pasado no se corresponde con un entero</exception>
        /// <exception cref="Exception">si la lista esta vacia, no habra elemento a borrar</exception>
        public void Borrar(T valor)
        {
            if (!ListaVacia())
            {
                if (_inicial.Valor.Equals(valor))
                {
                    _inicial = _inicial.Siguiente;
                    _numElmentos--;
                }
                else
                {
                    Nodo<T> nuevo = _inicial;
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
        }

        /// <summary>
        /// Metodo que nos devueleve el elemento en la posicion que indicamos por parametro
        /// </summary>
        /// <param name="pos">posicion de la que queremos saber el valor del elemento </param>
        /// <returns>devuleve el valor del elemento</returns>
        /// <exception cref="ArgumentException"></exception>
        public T GetElemento(int pos)
        {
            bool posValida = pos >= 0 && pos < NumElementos ? true : throw new ArgumentException("La posicion ha de ser mayor o igual a 0 y menor al numero de elementos");
            Nodo<T> nuevo = _inicial;
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
        public int GetPosicion(T valor)
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
                    if (pos == -1)
                    {
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
        public bool Contiene(T valor)
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
                    if (GetElemento(i) == null)
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
                _inicial = null;
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
            Nodo<T> nuevo = _inicial;
            sb.Append("[");
            while (nuevo != null)
            {
                sb.Append($"{nuevo.Valor} - ");
                nuevo = nuevo.Siguiente;
            }
            sb.Append("]");
            return sb.ToString();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new MiListaTeEnumero<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class MiListaTeEnumero<T> : IEnumerator<T>
    {
        Lista<T> _lista;
        int _currentPos;

        public MiListaTeEnumero(Lista<T> list)
        {
            _lista = list;
            _currentPos = -1;
        }
        public T Current { get { return _lista.GetElemento(_currentPos); } }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            //Cuando acaba el enumerable…
            //throw new NotImplementedException();
            //Console.WriteLine("Ejecutando el dispose ...");
        }

        public bool MoveNext()
        {
            _currentPos++;
            return _currentPos < _lista.NumElementos;
        }

        public void Reset()
        {
            _currentPos = -1;
        }
    }

    public static class Extensores
    {
        /// <summary>
        /// Buscar: A partir de una colección de elementos, nos devuelve el primero que cumpla
        /// un criterio dado o su valor por defecto en el caso de no existir ninguno.
        /// </summary>
        public static T Buscar<T>(this IEnumerable<T> col, Predicate<T> con)
        {
            if (con == null) throw new ArgumentException("La condicion no puede ser null");
            foreach (T t in col)
            {
                if (con(t))
                {
                    return t;
                }
            }
            return default(T);
        }

        /// <summary>
        /// Filtrar: A partir de una colección de elementos, nos devuelve todos aquellos que
        /// cumplan un criterio dado(siendo éste parametrizable)
        /// </summary>
        public static IEnumerable<T> Filtrar<T>(this IEnumerable<T> col, Predicate<T> cond)
        {
            if (cond == null) throw new ArgumentException("La condicion no puede ser null");

            IList<T> list = new List<T>();
            foreach (T t in col)
            {
                if (cond(t))
                {
                    list.Add(t);
                }
            }
            return list;
        }

        /// <summary>
        /// Reduce:  Esta función recibe una colección de elementos y una función que recibe un
        /// primer parámetro del tipo que queremos obtener y un segundo parámetro del tipo
        /// que queremos obtener; su tipo devuelto es el propio del que queremos obtener.
        /// </summary>
        /// 
        public static TAccumulate Reduce<TSource, TAccumulate>(this IEnumerable<TSource> col, Func<TAccumulate, TSource, TAccumulate> func)
        {
            TAccumulate seed = default(TAccumulate);
            foreach (TSource t in col)
            {
                seed = func(seed, t);
            }
            return seed;
        }

        public static TSource Reduce<TSource>(this IEnumerable<TSource> col, Func<TSource, TSource, TSource> func)
        {
            if (func == null) throw new ArgumentException("La funcion no puede ser null");
            TSource seed = default(TSource);
            foreach (TSource t in col)
            {
                seed = func(seed, t);
            }
            return seed;
        }

        public static TAccumulate Reduce<TSource, TAccumulate>(this IEnumerable<TSource> col, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
        {
            //seed = default(TAccumulate); // si lo descomentas dara error porque se reiniciara el acumulador en cada iteracion que hagas
            if (func == null) throw new ArgumentException("La funcion no puede ser null");

            foreach (TSource t in col)
            {
                seed = func(seed, t);
            }
            return seed;
        }

        public static TResult Reduce<TSource, TAccumulate, TResult>(this IEnumerable<TSource> col, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, TResult> resultSelector)
        {
            //seed = default(TAccumulate); // si lo descomentas dara error porque se reiniciara el acumulador en cada iteracion que hagas
            if (func == null) throw new ArgumentException("La funcion no puede ser null");
            foreach (TSource t in col)
            {
                seed = func(seed, t);
            }
            return resultSelector(seed);
        }

        public static IEnumerable<T> Invertir<T>(this IEnumerable<T> coleccion)
        {
            Lista<T> col = new Lista<T>();
            foreach (var i in coleccion)
            {
                col.Añadir(i, 0);
            }
            return col;
        }

        public static IEnumerable<Q> Map<T, Q>(this IEnumerable<T> enumerable, Func<T, Q> func)
        {
            IList<Q> lista = new List<Q>();
            foreach (T actual in enumerable)
                lista.Add(func(actual));
            return lista;

        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action(item);
            }
        }

    }
}

