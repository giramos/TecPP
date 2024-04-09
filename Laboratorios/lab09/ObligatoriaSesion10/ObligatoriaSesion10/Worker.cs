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

        /// <summary>
        /// Resultado del cálculo
        /// </summary>
        private int resultado;

        internal int Resultado
        {
            get { return this.resultado; }
        }

        internal Worker(BitcoinValueData[] vector, int valor, int indiceDesde, int indiceHasta)
        {
            this.vector = vector;
            this.valor = valor;
            this.indiceDesde = indiceDesde;
            this.indiceHasta = indiceHasta;
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
                    this.resultado += 1;
                }
                    
        }
    }
}