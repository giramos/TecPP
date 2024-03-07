using System;

namespace Clausuras
{
    public static class ClausuraContador
    {
        /// <summary>
        /// Esta función devuelve una clausura
        /// Almacena el estado de la variable contador.
        /// </summary>
        /// <returns>Clausura contador</returns>
        public static Func<int> CrearContador()
        {
            //Se define el estado
            int contador = 0;

            //Se devuelve la clausura

            return () => ++contador;

        }
    }
}
