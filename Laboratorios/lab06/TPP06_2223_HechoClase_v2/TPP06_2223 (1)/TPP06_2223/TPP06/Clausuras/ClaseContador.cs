
namespace Clausuras
{
    public class ClaseContador
    {
        private int _numero;

        public int Valor
        {
            get { return _numero; }
        }

        public ClaseContador()
        {
            _numero = 0;
        }

        public void Incrementar()
        {
            _numero++;
        }
    }
}
