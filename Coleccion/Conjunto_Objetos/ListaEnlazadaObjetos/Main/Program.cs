using System;
using ListaEnlazadaObjetos;
namespace Main
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Lista listaChar = new Lista();
            //Lista listaGrande = new Lista();
            //char[] caracteres = new[] { 'A', 'B', 'C', 'D' };
            //for (int i = 0; i < caracteres.Length; i++)
            //{
            //    Console.WriteLine("i = " + i);
            //    Console.WriteLine("Lista antes de añadir : " + listaChar.ToString());

            //    listaChar.Añadir(caracteres[i], i);

            //    Console.WriteLine("Lista despues de añadir : " + listaChar.ToString());


            //}
            //listaChar.AñadirInicio('a');
            //Console.WriteLine("Lista despues de añadir al inicio 'a': " + listaChar.ToString());

            //Console.WriteLine("Uso del getElemento(posicion 0): " + listaChar.GetElemento(0));
            //Console.WriteLine("Uso del getPoscion(elemento 'a'): " + listaChar.GetPosicion('a'));


            //// Lista grande enteros
            //for (int i = 0; i < 50; i++)
            //{
            //    listaGrande.Añadir(i);
            //}
            //listaGrande.AñadirFinal(1);
            //Console.WriteLine("Uso del getElemento(posicion 50): " + listaGrande.GetElemento(50));
            //Console.WriteLine("Uso del getPoscion(elemento 1): " + listaGrande.GetPosicion(1));

            //Lista listaMiscelanea = new Lista();
            //listaMiscelanea.Añadir(1);
            //listaMiscelanea.Añadir('a');
            //listaMiscelanea.Añadir(1.5);
            //listaMiscelanea.Añadir("german");
            //Console.WriteLine("Lista miscelanea :" + listaMiscelanea.ToString());
            //Console.WriteLine($"¿Contiene la lista el 1? {listaMiscelanea.Contiene(1)}");
            //Console.WriteLine($"¿Contiene la lista el 1.5? {listaMiscelanea.Contiene(1.5)}");
            //Console.WriteLine($"¿Contiene la lista el 'a'? {listaMiscelanea.Contiene('a')}");
            //Console.WriteLine($"¿Contiene la lista el german? {listaMiscelanea.Contiene("german")}");

            Lista listaInt = new();
            listaInt.Añadir(1);
            listaInt.Añadir('a');
            listaInt.Añadir(1.1);
            listaInt.Añadir("german");
            listaInt.Añadir(null);
            listaInt.Añadir(2);
            listaInt.Añadir(3);
            Console.WriteLine(listaInt.ToString());
            Console.WriteLine();
            Console.WriteLine("[Contains]");
            Console.WriteLine($"¿Contiene 1 la lista? {listaInt.Contiene(1)}");
            Console.WriteLine($"¿Contiene 3 la lista? {listaInt.Contiene(3)}");
            Console.WriteLine($"¿Contiene null la lista? {listaInt.Contiene(null)}");
            //listaInt.Borrar(null);
            //Console.WriteLine(listaInt.ToString());
            //Console.WriteLine();
            Console.WriteLine($"¿Contiene null la lista? {listaInt.Contiene(null)}");
            Console.WriteLine($"¿Contiene 4 la lista? {listaInt.Contiene(4)}");
            Console.WriteLine("[GetElemento]");
            listaInt.Añadir(null);
            Console.WriteLine(listaInt.ToString());
            Console.WriteLine();
            Console.WriteLine($"¿Que hay en la pos 0? {listaInt.GetElemento(0)}");
            Console.WriteLine($"¿Que hay en la pos 6? {listaInt.GetElemento(6)}");
            Console.WriteLine($"¿Que hay en la pos 4? {listaInt.GetElemento(4)}");
            Console.WriteLine($"¿Que hay en la pos 1? {listaInt.GetElemento(1)}");
            Console.WriteLine("[GetPosicion]");
            Console.WriteLine($"¿En que posicion esta el 1.1? {listaInt.    GetPosicion(1.1)}");
            Console.WriteLine($"¿En que posicion esta el null? {listaInt.GetPosicion(null)}");
            Console.WriteLine($"¿En que posicion esta german? {listaInt.GetPosicion("german")}");
            Console.WriteLine($"¿En que posicion esta el 3? {listaInt.GetPosicion(3)}");
            Console.WriteLine(listaInt.ToString());
            Console.WriteLine();
            Console.WriteLine("[Borrar null]");
            listaInt.Borrar(null);
            Console.WriteLine(listaInt.ToString());
            listaInt.AñadirFinal(null);
            Console.WriteLine(listaInt.ToString());
            listaInt.AñadirInicio(null);
            Console.WriteLine(listaInt.ToString());
            listaInt.Añadir(null,3);
            Console.WriteLine(listaInt.ToString());
            listaInt.BorrarFinal(null);
            Console.WriteLine(listaInt.ToString());
            listaInt.AñadirFinal(9);
            Console.WriteLine(listaInt.ToString());
            listaInt.AñadirFinal(9);
            Console.WriteLine(listaInt.ToString());
            listaInt.BorrarFinal(9);
            Console.WriteLine(listaInt.ToString());
            listaInt.AñadirInicio(null);
            Console.WriteLine(listaInt.ToString());
            listaInt.BorrarInicio(null);
            Console.WriteLine(listaInt.ToString());

        }
    }
}
