using System;
using ListaEnlazadaObjetos;
namespace Main
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Lista listaChar = new Lista();
            Lista listaGrande = new Lista();
            char[] caracteres = new[] { 'A', 'B', 'C', 'D' };
            for (int i = 0; i < caracteres.Length; i++)
            {
                Console.WriteLine("i = " + i);
                Console.WriteLine("Lista antes de añadir : " + listaChar.ToString());
                
                listaChar.Añadir(caracteres[i], i);

                Console.WriteLine("Lista despues de añadir : " + listaChar.ToString());

                
            }
            listaChar.AñadirInicio('a');
            Console.WriteLine("Lista despues de añadir al inicio 'a': " + listaChar.ToString());

            Console.WriteLine("Uso del getElemento(posicion 0): " + listaChar.GetElemento(0));
            Console.WriteLine("Uso del getPoscion(elemento 'a'): " + listaChar.GetPosicion('a'));


            // Lista grande enteros
            for (int i = 0; i < 50; i++)
            {
                listaGrande.Añadir(i);
            }
            listaGrande.AñadirFinal(1);
            Console.WriteLine("Uso del getElemento(posicion 50): " + listaGrande.GetElemento(50));
            Console.WriteLine("Uso del getPoscion(elemento 1): " + listaGrande.GetPosicion(1));
        }
    }
}
