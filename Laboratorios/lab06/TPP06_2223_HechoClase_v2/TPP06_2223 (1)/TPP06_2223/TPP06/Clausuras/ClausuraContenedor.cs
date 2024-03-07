
using System;

namespace Clausuras
{
    public static class ClausuraContenedor
    {
        public static void CrearContador<T>(T valor, out Action<T> set, out Func<T> get)
        {
            //Se define el estado
            T _valor = valor;

            //Funciones a definir

            set = valor => _valor = valor;

            get = () => _valor;

        }
    }
}
