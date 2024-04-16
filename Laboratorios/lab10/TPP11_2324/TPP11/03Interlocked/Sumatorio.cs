using System.Threading;

namespace _03Interlocked
{
    public class Sumatorio
    {
        protected long valor;
        private int numHilos;

        internal Sumatorio(long valor, int numHilos)
        {
            this.valor = valor;
            this.numHilos = numHilos;
        }

        internal long Valor
        {
            get { return this.valor; }
        }

        protected virtual void DecrementarValor()
        {   
            this.valor = this.valor - 1;
        }

        internal void Calcular() // Master - Worker // Action 
        {
            int iteraciones = (int)valor / numHilos; //Las necesarias para dejar el recuento a 0.
            Thread[] hilos = new Thread[numHilos];
            for (int i = 0; i < numHilos; i++)
                hilos[i] = new Thread(
                    () =>
                        {
                            for (int j = 0; j < iteraciones; j++)
                            {
                                this.DecrementarValor();
                            }
                        }
                     );

            //Iniciamos hilos y hacemos Join
            foreach (Thread hilo in hilos)
                hilo.Start();
            foreach (Thread hilo in hilos)
                hilo.Join();
        }
    }
}
