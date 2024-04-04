using System;

namespace MasterWorkerClase
{
    internal class Worker
    {
        short[] vector1;
        short[] vector2;
        static readonly object obj = new();
        int desde, hasta;

        int res;

        public int Resultado { get { return res; } }

        
        public Worker(short[] vector1, short[] vector2, int desde, int hasta)
        {
            this.vector1 = vector1;
            this.vector2 = vector2;
            this.desde = desde;
            this.hasta = hasta;
        }

        internal void Calcular()
        {
            res = 0;
            for (int i = desde; i <= hasta; i++)
            {
                bool coincide = true;
                for (int j = 0; j < vector2.Length; j++)
                {
                    if (i+j >= vector1.Length || vector1[i+j] != vector2[j])
                    {
                        coincide = false;
                        break;
                    }
                }
                if (coincide)
                {
                    MetodoBloqueo();
                }
            }
        }

        private void MetodoBloqueo()
        {
            lock(obj)
                res++;
        }
    }
}