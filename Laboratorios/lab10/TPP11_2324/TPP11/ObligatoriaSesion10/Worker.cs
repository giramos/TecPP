using System.Collections.Generic;

namespace ObligatoriaSesion10
{
    internal class Worker
    {
        /// <summary>
        /// Vector del que vamos a obtener el módulo
        /// </summary>
        private BitcoinValueData[] vector;
        private int valor;
        private int indiceDesde, indiceHasta;
        private List<double> valores;

        private static readonly object obj = new();

        /// <summary>
        /// Resultado del cálculo
        /// </summary>
        private int resultado;

        internal int Resultado
        {
            get { return this.resultado; }
        }

        internal Worker(BitcoinValueData[] vector, int valor, List<double> valores, int indiceDesde, int indiceHasta)
        {
            this.vector = vector;
            this.valor = valor;
            this.indiceDesde = indiceDesde;
            this.indiceHasta = indiceHasta;
            this.valores = valores;
        }

        /// <summary>
        /// Método que realiza el cálculo
        /// </summary>
        internal void Calcular()
        {
            this.resultado = 0;
            for (int i = this.indiceDesde; i <= this.indiceHasta; i++)
                if (vector[i].Value >= valor)
                {
                    lock(obj)
                    {
                        if (!valores.Contains(vector[i].Value))
                        {
                            valores.Add(vector[i].Value);
                            this.resultado += 1;
                        }
                    }
                }

        }
    }
}