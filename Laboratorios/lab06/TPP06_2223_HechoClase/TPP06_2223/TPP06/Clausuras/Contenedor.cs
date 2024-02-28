
namespace Clausuras
{
    public class Contenedor<T>
    {
        private T _valor;

        public Contenedor(T valor)
        {
            _valor = valor;
        }

        public T GetValor()
        {
            return _valor;
        }

        public void SetValor(T valor)
        {
            _valor = valor;
        }
    }
}
