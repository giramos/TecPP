using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoEnClase
{
    public interface IFacturable
    {
        /// <summary>
        /// Indica el precio base
        /// </summary>
        /// <param name="double"></param>
        /// <returns></returns>
        double GetBase(double numero);
        /// <summary>
        /// Indica el tipo de IVA: 0.21, 0.10 ...
        /// </summary>
        /// <param name="double"></param>
        /// <returns></returns>
        double GetTipoIva(double numero);
        /// <summary>
        /// Calculara el IVA en funcion del tipo de IVA y el precio base
        /// </summary>
        /// <returns></returns>
        double GetIVA();
        /// <summary>
        ///  Devolverá el precio (Base + aplicación de IVA al base).
        /// </summary>
        /// <param name="double"></param>
        /// <returns></returns>
        double GetPrecio(double numerp);
        /// <summary>
        /// Devolverá un texto descriptivo.
        /// </summary>
        /// <param name="concepto"></param>
        /// <returns></returns>
        string GetConcepto(string concepto);
        /// <summary>
        ///  Devolverá una cadena con los valores anteriores.
        /// </summary>
        /// <returns></returns>
        string ToTicket();



    }
}
