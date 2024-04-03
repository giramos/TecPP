using System;
using System.Globalization;
using System.IO;

namespace ObligatoriaSesion10
{
    class Program
    {
        /*
         * This program processes Bitcoin value information obtained from the
         * url https://api.kraken.com/0/public/OHLC?pair=xbteur&interval=5.
         */
        static void Main(string[] args)
        {
            //var data = Utils.GetBitcoinData();

            //Console.WriteLine(CultureInfo.CurrentCulture);
            //foreach (var d in data)
            //    Console.WriteLine(d);

            //Medimos el tiempo con cada numero de hilos
            const int maximoHilos = 50;
            MostrarLinea(Console.Out, "Num Hilos", "Ticks", "Resultado");
            for (int numeroHilos = 1; numeroHilos <= maximoHilos; numeroHilos++)
            {
                Master master = new Master(Utils.GetBitcoinData(), numeroHilos, 7000);
                DateTime antes = DateTime.Now;
                double resultado = master.Contar();
                DateTime despues = DateTime.Now;
                MostrarLinea(Console.Out, numeroHilos, (despues - antes).Ticks, resultado);

                //Entre ejecuciones, limpiamos y esperamos.
                GC.Collect();
                GC.WaitForFullGCComplete();
            }
            Console.ReadLine();
        }

        static void MostrarLinea(TextWriter stream, string numHilosCabecera, string ticksCabecera, string resultadoCabecera)
        {
            stream.WriteLine("{0};{1};{2}", numHilosCabecera, ticksCabecera, resultadoCabecera);
        }

        static void MostrarLinea(TextWriter stream, int numHilos, long ticks, double resultado)
        {
            stream.WriteLine("{0};{1:N0};{2:N2}", numHilos, ticks, resultado);
        }
    }
}
