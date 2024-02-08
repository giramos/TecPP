using System;
using System.Collections;
using System.Text;

#if DEBUG
using System.Diagnostics;
#endif


namespace ProgramacionPorContratos
{
    /// <summary>
    /// Una clase que permite almacenar una cantidad variable de números pares.
    /// Programación por contratos.
    /// </summary>
    public class AlmacenPares
    {
        private IList _pares;

        private int _capacidad;

        public int Capacidad 
        {
            get { return _capacidad; }

            set
            {
                //Precondiciones, siempre en tiempo de ejecución. Se controlan mediante EXCEPCIONES.

                if (value < 1)
                    throw new ArgumentException("La capacidad mínima del almacen es 1.");
                if (value < _pares.Count)
                    throw new InvalidOperationException("La nueva capacidad del almacen no puede ser menor que la cantidad de almacenados.");

                _capacidad = value;
            }
        }

        public override string ToString()
        {
            StringBuilder cadena = new StringBuilder();
            cadena.AppendLine($"Almacenados: {_pares.Count} elementos. Capacidad Máxima: {Capacidad}");
            foreach (var elemento in _pares)
                cadena.Append(elemento + " ");
            return cadena.ToString();
        }

        public AlmacenPares(int capacidad)
        {
            _pares = new ArrayList();
            Capacidad = capacidad;
            /*
             * Comprobamos que el estado del objeto sea consistente.
             * En el constructor, solamente al final.
             */
#if DEBUG
            Invariante();
#endif
        }

        public void Insertar(int valor)
        {

#if DEBUG
            //Estado consistente al comienzo del método.
            Invariante();

            //Para comprobar la postcondición.
            int cantidadPrevia = _pares.Count;
#endif

            // Precondiciones del método.
            if (valor % 2 != 0)
                throw new ArgumentException("Debe ser un número par.");
            if (_pares.Count == Capacidad)
                throw new InvalidOperationException("El almacen está lleno.");


#if DEBUG
            /*
             * Las postcondiciones son propias de cada método.
             * Comprueban que la ejecución del método ha sido correcta.
             * Sirven para detectar defectos durante el desarrollo del código.
             * En la versión Release no se contemplan, por eso utilizamos compilación condicional.
            */

            Debug.Assert(_pares.Count == cantidadPrevia + 1,"Fallo en postcondición de AlmacenPares.Insertar");

            //LA invariante comprueba que el estado del objeto es consistente tras finalizar el método.
            Invariante();
#endif
        }

        /// <summary>
        /// Completar con lo mostrado en esta clase.
        /// </summary>
        /// <param name="valor">valor a eliminar</param>
        public void Borrar(int valor)
        {

        }


#if DEBUG
        private void Invariante()
        {
            /* En ningún caso _pares debe ser null
            * y
            * tamaño de _pares siempre tiene que ser <= que Capacidad.
            */
            Debug.Assert(_pares != null && _pares.Count <= Capacidad, "Error en la invariante.");
        }
#endif

    }
}
