using System;

namespace ProgramacionPorContratos
{
    /// <summary>
    /// Excepciones en C#
    /// </summary>
    class Program
    {
        public static AlmacenPares almacen = null;
        static void Main()
        {
            
            Console.WriteLine("Creando almacen....");
            while (almacen == null)
            {
                //Excepciones
                try
                {
                    Console.WriteLine("Escriba la capacidad del almacen (>=1):");
                    int capacidad = Int32.Parse(Console.ReadLine());
                    almacen = new AlmacenPares(capacidad);
                }
                catch(ArgumentException e)
                { 
                    Console.Error.WriteLine("Error: " + e.Message);
                }
                catch (Exception e)//Resto de excepciones aquí
                { 
                    Console.Error.WriteLine("Error: "+ e.Message);
                }
                //¿Cómo y cuándo se utiliza finally { ... }?
                int[] numeros = { 1, 2, 4 };

                foreach (var numero in numeros)
                {
                    Console.WriteLine($"Insertando en almacen el valor: {numero}");
                    InsertarEnAlmacen(numero);
                }
                Console.WriteLine("\n"+almacen);

            }


        }

        public static void InsertarEnAlmacen(int valor)
        {
            try
            {
                almacen.Insertar(valor);
            }
            catch(ArgumentException e)
            {
                Console.Error.WriteLine($"Error: No se ha podido insertar: {valor}. Motivo: {e.Message}");
            }
            catch (InvalidOperationException e)
            {
                Console.Error.WriteLine($"Error: No se ha podido insertar: {valor}. Motivo: { e.Message}");
            }

        }
    }
}
