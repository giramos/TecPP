using System.Collections;
using System.Collections.Generic;


namespace IEnumerable
{
    //IEnumerable<T> nos indica que la clase que implementa la interfaz
    //contiene elementos y podemos recorrerlos
    //NO INDICA CÓMO RECORRERLOS,
    //INDICA QUE SE PUEDE RECORRER.
    //El método GetEnumerator devolverá un OBJETO  con los mensajes necesarios (IEnumerator)
    //Para realizar el recorrido.

    //¡OJO! Si TablaMultiplicar fuera genérica: TablaMultiplicar<T> la interfaz a implementar
    // podría darse el caso que la interfaz a implementar fuera IEnumerable<T>:
    //
    //Coleccionaría T y recorrería T. ¿Sería igual para IEnumerator?
    public class TablaMultiplicar : IEnumerable<int>
    {
        private int _numero;
        private int _nTerminos;

        public TablaMultiplicar(int numero, int nTerminos)
        {
            this._numero = numero;
            this._nTerminos = nTerminos;
        }


        /// <summary>
        /// El Enumerator es el encargado de realizar la cuenta.
        /// Implementación Genérica
        /// </summary>
        /// <returns>Objeto que implementa IEnumerator (Genérico)</returns>
        public IEnumerator<int> GetEnumerator()
        {
            return new GeneradorTabla(this._numero, this._nTerminos); // solo crea y devuelve el iterador
        }

        IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new GeneradorTabla(this._numero, this._nTerminos);
        }



        /// <summary>
        /// Clase que implementa IEnumerator. 
        /// Esta clase es la que gestiona COMO se recorren los elementos.
        /// Normalmente está anidada dentro de la clase a recorrer.
        /// ASÍ EXPONEMOS SOLO MENSAJES PARA LA ITERACIÓN:
        /// Current, MoveNext(), Reset() y Dispose() para la liberación de recursos.
        /// </summary>
        public class GeneradorTabla : IEnumerator<int>
        {
            private int _numero;
            private int _nTerminos;
            private int _contador;

            /// <summary>
            /// Current devuelve el elemento actual. Es una propiedad de solo-lectura
            /// </summary>
            public int Current
            {
                get
                {
                    return _contador * _numero;
                }
            }
            /// <summary>
            /// Implementación polimórfica de Current
            /// </summary>
            object IEnumerator.Current { get { return _contador * _numero; } }


            /// <summary>
            /// Es el iterador
            /// Recibe todos los datos necesarios para hacer la iteración.
            /// Si tuviésemos un objeto, debería recibir el objeto. Véase la lista.
            /// </summary>
            /// <param name="numero"></param>
            /// <param name="maximoElementos"></param>
            public GeneradorTabla(int numero, int nTerminos)
            {
                this._numero = numero;
                this._nTerminos = nTerminos;
                Reset(); // ¡Inicializamos!
            }

            /// <summary>
            /// Reiniciamos el Enumerator a la posición ANTERIOR al primero.
            /// </summary>
            public void Reset()
            {
                this._contador = 0; // ponemos el anterior al primero para luego hacer el moveNext()
            }


            /// <summary>
            /// Apuntamos al siguiente elemento (el siguiente de Current)
            /// </summary>
            /// <returns>Devuelve si aún quedan más elementos</returns>
            public bool MoveNext()
            {
                //Si no entiendes esta línea, conviértela a un ++ y desarrolla el return.
                return _contador++ != _nTerminos;
            }


            /// <summary>
            /// Liberamos recursos
            /// </summary>
            public void Dispose()
            {
                //Si procede, los recursos se liberan aquí.
            }
        }
    }
}
