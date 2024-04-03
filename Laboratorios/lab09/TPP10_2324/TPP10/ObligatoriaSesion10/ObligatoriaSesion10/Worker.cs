using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObligatoriaSesion10
{
    public class Worker
    {/// <summary>
     /// Vector del que vamos a contar 
     /// </summary>
        private BitcoinValueData[] vectorBitcoin;

        private int limite; //Barrera a parit de la cual se empieza a contar 

        /// <summary>
        /// Índices que indican el rango de elementos del vector 
        /// con el que vamos a trabajar.
        /// En el rango se incluyen ambos índices.
        /// </summary>
        private int fromIndex, toIndex;

        private long result;

        internal long Result    //Propiedad que guarda el resultado 
        {
            get { return this.result; }
        }


        // INicia los datos
        //Constructor al que se le pasa tambien el limite 
        internal Worker(BitcoinValueData[] vector, int fromIndex, int toIndex, int limite)
        {
            this.vectorBitcoin = vector;
            this.fromIndex = fromIndex;
            this.toIndex = toIndex;
            this.limite = limite;
        }

        /// <summary>
        /// Método que realiza el cálculo
        /// </summary>
        internal void Contar()
        {
            this.result = 0;
            for (int i = this.fromIndex; i <= this.toIndex; i++)
                if (vectorBitcoin.ElementAt(i).Value >= limite)
                //if(vectorBitcoin[i].Value >= limite)
                {
                    this.result += 1;
                }
        }

    }
}
