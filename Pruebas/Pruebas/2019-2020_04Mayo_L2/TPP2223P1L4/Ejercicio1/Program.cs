using System;


namespace Ejercicio1
{

    public static class Program
    {
        static void Main(string[] args)
        {

            Func<int, int>[] funciones =
            {
               n => n + 1,
            n => n * 2,
            n => n - 3
            };

            var res = 5.Aplicacion(funciones,20);
            Console.WriteLine(res);

            Console.ReadLine();

            var curry = AplicacionCurry(5);
            Console.WriteLine(curry(funciones)(20));
        }

        public static int Aplicacion(this Int32 numero, Func<int, int>[] arrayFunc, int maximo)
        {
            int acumulado = arrayFunc[0](numero);
            for (int i = 1; i < arrayFunc.Length; i++)
            {
                acumulado = arrayFunc[i](acumulado);
                if (acumulado > maximo)
                {
                    break; // Terminar el bucle si el acumulado supera el máximo
                }
            }
            return acumulado;
        }

        public static Func<Func<int, int>[], Func<int,int>> AplicacionCurry(Int32 numero)
        {
            return arrayFunc => (maximo) => {

                int acumulado = arrayFunc[0](numero);
                for (int i = 1; i < arrayFunc.Length; i++)
                {
                    acumulado = arrayFunc[i](acumulado);
                    if (acumulado > maximo)
                    {
                        break; // Terminar el bucle si el acumulado supera el máximo
                    }
                }
                return acumulado;
            };
        }
    }

    

  
}
