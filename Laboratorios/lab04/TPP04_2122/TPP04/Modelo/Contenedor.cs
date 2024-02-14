using System;

namespace Modelo
{
    /// <summary>
    /// Ejemplo de clase genérica.
    /// Fíjate dónde se definen los marcadores de tipo.
    /// </summary>
    /// <typeparam name="TClave">Marcador de tipo</typeparam>
    /// <typeparam name="TValor">Otro marcador de tipo</typeparam>
    public class Contenedor<TClave, TValor> // utiliza dos tipos. Pueden ser el mismo, no tienen por qué ser diferentes
    {

        private TValor _valor;
        public TValor Value { get { return _valor; } }

        private TClave _clave;
        public TClave Key { get { return _clave; } }

        public Contenedor(TClave key, TValor value)
        {
            _clave = key;
            _valor = value;
        }


        /*
        ¡NOTA!
            Fíjate que al estar definidos en la clase tanto TClave como TValor,
            no definimos el método como Sustituir<Clave,Valor>.
            Si hubiera un tercer parámetro genérico (K):

            Sustituir<K>(Clave newKey, Valor newValue, K otro){ ... } -> ya sería método genérico

        // no es un método genérico
        */
        public void Sustituir(TClave newKey, TValor newValue)
        {
            _clave = newKey;
            _valor = newValue;
            //Imaginemos que queremos hacer el cambio en función
            //del método CompareTo de la interfaz IComparable<T>

            //Tal y como está en esta clase la genericidad no podemos
            //Los marcadores de tipo (T,U,Clave,Valor...) solamente
            //nos proporcionan acceso a los métodos de Object.
            //Este problema lo resolvemos mediante genericidad acotada

            /* NO COMPILA -> porque solo tengo acceso a los métodos de object -> solución en contenedor acotado (explica la genericidad acotada)

           if (Value.CompareTo(newValue) == -1)
           {
               this.Value = newValue;
               this.Key = newKey;
               return true;
           }
           return false;
           */
        }

        public override string ToString()
        {
            return String.Format("Clave: [{0}] Valor: [{1}]", Key, Value);
        }

    }
}
