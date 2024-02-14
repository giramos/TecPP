using System;

namespace Modelo
{
    /*
       La genericidad acotada nos permite establecer condicionantes para el uso de la genericidad:
       Siempre y cuando el tipo Valor implemente IComparable de ese tipo (el que sustituya a Valor).          
       Más info en:
       https://docs.microsoft.com/es-es/dotnet/csharp/programming-guide/generics/constraints-on-type-parameters
      */
    public class ContenedorAcotado<Clave, Valor> where Valor : IComparable<Valor>
    {
        private Valor _value;
        public Valor Value { get; }

        private Clave _key;
        public Clave Key { get; }

        public ContenedorAcotado(Clave key, Valor value)
        {
            Key = key;
            Value = value;
        }


        public bool Sustituir(Clave newKey, Valor newValue)
        {
            //Podemos hacer uso del CompareTo<Valor> gracias
            //al uso de la genericidad acotada -> tengo acceso a los métodos de la interfaz
            if (Value.CompareTo(newValue) < 0)
            {
                _value = newValue;
                _key = newKey;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return String.Format("Clave: [{0}] Valor: [{1}]", Key, Value);
        }
    }
}
