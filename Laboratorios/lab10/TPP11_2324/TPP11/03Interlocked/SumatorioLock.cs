using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _03Interlocked
{
    internal class SumatorioLock : Sumatorio
    {
        private  readonly object Lock = new object();
        public SumatorioLock(long valor, int numHilos) : base(valor, numHilos)
        {

        }

        protected override void DecrementarValor()
        {
            lock(Lock)
                base.valor = base.valor - 1;
        }
    }
}
