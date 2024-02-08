
namespace Modelo
{
    /// <summary>
    /// Pueden ser public o internal.
    /// Por convenio, comienzan con la letra "I".
    /// Podemos establecer propiedades y métodos (opciones ampliadas en C# 8.0)
    /// Todos los mensajes son abstractos.
    ///     NOTA: En C# 8.0 puede establecerse una implementación predeterminada. No lo usaremos.
    /// 
    /// </summary>
    public interface IComparable
    {
        // <summary>
        /// Método para comparar
        /// </summary>
        /// <param name="c">El objeto a comparar</param>
        /// <returns>Negativo si éste es inferior a c;
        /// cero si son iguales;
        /// Positivo si éste es mayor que c;
        /// </returns>
        int Compare(IComparable c);
    }
}
