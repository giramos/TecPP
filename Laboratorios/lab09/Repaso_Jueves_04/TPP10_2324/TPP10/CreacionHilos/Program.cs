using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CreacionHilos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Ejercicio:  crear dos hilosmanualmente que trabajen con el ienumerable de palabras
            // proporcionado. Ambos hilos haran uso de la misma variable compartida que hará de contador.
            // El primer hilo recorrera el ienumerable y restara 1 al contador por cada palabra de tamaño par.
            // El segundo hilo hara una operación similar, pero sumará 1 por cada palabra de tamaño impar que encuentre.
            // Imprimir mediante este procedimiento la diferencia entre el nº de palabras para y el impar
            var palabras = new string[] { "Hola", "adios", "BUENAS", "ok", "OKI", "Doki", "PEPE", "el", "hijo" };
            int cont = 0;
            Thread hilo1 = new Thread(()=>Metodo(palabras, ref cont, true));
            Thread hilo2 = new Thread(()=>Metodo(palabras, ref cont, false));
            hilo1.Start();
            hilo2.Start();
            hilo1.Join();
            hilo2.Join();
            Console.WriteLine(cont);


            // Ejercicio: //Empleando el ienumerable de palabras clasifica, mediante la creacion manual de hilos, 
            //las palabras en tres colecciones distintas: 
            //    1)Palabras con todas sus letras mayusculas 
            //    2)PAlabras con todas sus letras minusculas 
            //    3)Palabras con letras tanto mayusculas como minusculas.
            //Finalmente comprueba mediante PLinq que cada solucion posee las palabras adecuadas
            List<string> may = new List<string>();
            List<string> min = new List<string>();
            List<string> mix = new List<string>();
            Thread hilomin = new Thread(() => Clasifica(palabras, min, Tipo.Minusculas));
            Thread hilomay = new Thread(() => Clasifica(palabras, may, Tipo.Mayusculas));
            Thread hilomix = new Thread(() => Clasifica(palabras, mix, Tipo.Mixto));
            hilomin.Start();
            hilomay.Start();
            hilomix.Start();
            hilomin.Join();
            hilomay.Join();
            hilomix.Join();
            Console.WriteLine($"Minusculas:");
            min.ForEach(Console.WriteLine);
            Console.WriteLine($"Mayusculas:");
            may.ForEach(Console.WriteLine);
            Console.WriteLine($"Mixto:");
            mix.ForEach(Console.WriteLine);

        }

        private static void Clasifica(string[] palabras, List<string> col, Tipo tipo)
        {
            foreach(var i in palabras)
            {
                if (i.All(Char.IsLower) && tipo.Equals(Tipo.Minusculas))
                {
                    col.Add(i);
                }
                else if (i.All(Char.IsUpper) && tipo.Equals(Tipo.Mayusculas))
                {
                    col.Add(i);
                }
                else if (!i.All(Char.IsLower) && !i.All(Char.IsUpper) && tipo.Equals(Tipo.Mixto))
                {
                    col.Add(i);
                }
            }
        }

        public enum Tipo { Mayusculas, Minusculas, Mixto }

        private static void Metodo(string[] palabras, ref int cont, bool v)
        {
            foreach(string s in palabras)
            {
                if(s.Length % 2 == 0 && v)
                {
                    cont--;
                }
                else if(s.Length% 2 != 0 && !v)
                {
                    cont++;
                }
            }
        }
    }
}
