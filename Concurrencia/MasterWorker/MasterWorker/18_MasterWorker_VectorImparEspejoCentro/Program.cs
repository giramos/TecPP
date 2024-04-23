using System;
using System.Diagnostics;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace _18_MasterWorker_VectorImparEspejoCentro
{
    //    Dado un vector de números enteros de longitud impar calcular de forma paralela el vector espejo del mismo tomando como centro el elemento medio del vector.Es decir se deben intercambiar  entre si todos los elementos simétricos respecto al centro.Cada hilo deberá intercambiar n elementos con sus simétricos respectivos. Veamos un ejemplo:
    //[Vector original]
    //1 2 3 4 5 6 7 8 9 10 11 12 13 14 15
    //[Salida esperada]
    //13 12 11 10 9 8 7 6 5 4 3 2 1
    //[Algoritmo]
    //Si por ejemplo n fuera 3 cada hilo tomará  3 elementos de cada lado y los intercainbiaría con sus siinétricos. En este caso habría 3 hilos:
    //Hilol que intercambiaría 1 2 3 con 13 14 15
    //Hilo2 que intercambiaría 4 5 6 con  10 11 12
    //Hilo3 que intercambiaría 7 con 9.

    class Program
    {
        static void Main(string[] args)
        {
            int[] vectorOriginal = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            int[] vectorEspejo = new int[vectorOriginal.Length]; // Creamos un nuevo vector para almacenar el resultado

            const int numHilos = 2; // Número de hilos para procesar el vector

            MostrarVector("Vector original:", vectorOriginal);

            // Creamos la instancia de la clase Master
            Master master = new Master(vectorOriginal, vectorEspejo, numHilos);

            // Calculamos el vector espejo de forma paralela
            master.CalcularEspejoParalelo();

            // Mostramos el resultado
            MostrarVector("Vector espejo:", vectorEspejo);
        }

        static void MostrarVector(string mensaje, int[] vector)
        {
            Console.WriteLine(mensaje);
            foreach (var elemento in vector)
            {
                Console.Write(elemento + " ");
            }
            Console.WriteLine();
        }
    }

    public class Master
    {
        private int[] vectorOriginal;
        private int[] vectorEspejo;
        private int numeroHilos;

        public Master(int[] vectorOriginal, int[] vectorEspejo, int numeroHilos)
        {
            if (vectorOriginal.Length % 2 == 0)
            {
                throw new ArgumentException("El vector original debe tener una longitud impar.");
            }

            this.vectorOriginal = vectorOriginal;
            this.vectorEspejo = vectorEspejo;
            this.numeroHilos = numeroHilos;
        }

        public void CalcularEspejoParalelo()
        {
            int longitud = vectorOriginal.Length;
            int elementosPorHilo = longitud / numeroHilos;
            int elementosExtras = longitud % numeroHilos;

            Worker[] workers = new Worker[numeroHilos];

            // Particionamos los datos y creamos los trabajadores
            for (int i = 0; i < numeroHilos; i++)
            {
                int indiceInicio = i * elementosPorHilo;
                int indiceFin = (i + 1) * elementosPorHilo - 1;

                if (i == numeroHilos - 1)
                {
                    indiceFin += elementosExtras;
                }

                workers[i] = new Worker(vectorOriginal, vectorEspejo, indiceInicio, indiceFin);
            }

            // Iniciamos los hilos
            Thread[] hilos = new Thread[numeroHilos];
            for (int i = 0; i < numeroHilos; i++)
            {
                hilos[i] = new Thread(workers[i].Calcular);
                hilos[i].Name = "Hilo " + (i + 1);
                hilos[i].Start();
            }

            // Esperamos a que todos los hilos terminen
            foreach (Thread thread in hilos)
            {
                thread.Join();
            }
        }
    }

    public class Worker
    {
        private int[] vectorOriginal;
        private int[] vectorEspejo;
        private int indiceInicio;
        private int indiceFin;

        public Worker(int[] vectorOriginal, int[] vectorEspejo, int indiceInicio, int indiceFin)
        {
            this.vectorOriginal = vectorOriginal;
            this.vectorEspejo = vectorEspejo;
            this.indiceInicio = indiceInicio;
            this.indiceFin = indiceFin;
        }

        public void Calcular()
        {
            int longitud = vectorOriginal.Length;

            // Intercambiamos los elementos simétricos respecto al centro del vector
            for (int i = indiceInicio; i <= indiceFin; i++)
            {
                vectorEspejo[i] = vectorOriginal[longitud - 1 - i];
                vectorEspejo[longitud - 1 - i] = vectorOriginal[i];
            }

            // Si la longitud es impar, corregimos el elemento central que no fue intercambiado
            if (longitud % 2 != 0 && indiceInicio <= longitud / 2 && indiceFin >= longitud / 2)
            {
                vectorEspejo[longitud / 2] = vectorOriginal[longitud / 2];
            }
        }
    }
}
