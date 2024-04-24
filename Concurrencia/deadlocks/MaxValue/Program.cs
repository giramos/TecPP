using System;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace MaxValueApp
{
    internal class Program
    {
        public static int[] CreateRandomVector(int numberOfElements, int lowest, int greatest)
        {
            int[] vector = new int[numberOfElements];
            Random random = new Random();
            for (int i = 0; i < numberOfElements; i++)
                vector[i] = random.Next(lowest, greatest + 1);
            return vector;
        }
        static void Main(string[] args)
        {
            int m = 100000, times=10;
            int[] a= CreateRandomVector(m, 0, m);
            Random random= new Random();
            MaxValue x = new MaxValue(0);            
            for (int i = 0; i < times; i++)
            {
                Parallel.For(0, m,
                    (i) =>
                    {
                        //x.Value = a[i];
                        lock (x) // Agregar un bloqueo para evitar condiciones de carrera
                        {
                            x.Value = a[i];
                        }
                    });
                Console.WriteLine($"{a.Max()}, {x.Value}");
            }
        }
    }
}
