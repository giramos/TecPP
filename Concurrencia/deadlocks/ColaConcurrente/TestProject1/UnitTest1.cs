using ColaConcurrente;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Schema;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        Cola<object> micola = new();
        ConcurrentQueue<object> queue = new();

        public Cola<int> numbers;
        public Cola<string> words;

        Cola<int> colita = new();
        Thread[] hilitos = new Thread[5];


        [TestInitialize]
        public void TestInit()
        {
            micola.Añadir("hola");
            micola.Añadir(1);
            micola.Añadir('a');

            queue.Enqueue("hola");
            queue.Enqueue(1);
            queue.Enqueue('a');

            numbers = new Cola<int>();
            words = new Cola<string>();

            numbers.Añadir(777);
            numbers.Añadir(2077);

            words.Añadir("Vegetta");
            words.Añadir("CiberPunk");
        }

        [TestMethod]
        public void TestExtraer()
        {
            object hola = "hola";
            object uno = 1;
            object a = 'a';

            Assert.AreEqual(micola.Extraer(), "hola");
            Assert.AreEqual(queue.TryDequeue(out hola), true);
            Assert.AreEqual(micola.Count(), queue.Count());

            Assert.AreEqual(micola.Extraer(), 1);
            Assert.AreEqual(queue.TryDequeue(out uno), true);
            Assert.AreEqual(micola.Count(), queue.Count());

            Assert.AreEqual(micola.Extraer(), 'a');
            Assert.AreEqual(queue.TryDequeue(out a), true);
            Assert.AreEqual(micola.Count(), queue.Count());

            Console.WriteLine(micola.ToString());
            Console.WriteLine(queue.ToString());
        }

        [TestMethod]
        public void TestAñadir()
        {
            micola.Añadir(2);
            Assert.AreEqual(micola.Count(), 4);
            Assert.AreEqual(micola.EstaVacia(), false);
            Assert.AreEqual(micola.PrimerElemento(), "hola");

            queue.Enqueue(2);
            Assert.AreEqual(queue.Count(), 4);
            Assert.AreEqual(queue.IsEmpty, false);
            Assert.AreEqual(queue.First(), "hola");
            Console.WriteLine(micola.ToString());
            Console.WriteLine(queue.ToString());
        }

        [TestMethod]
        public void Test()
        {
            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < 5; i++)
            {
                threads.Add(new Thread(() =>
                {
                    numbers.Añadir(i);
                    words.Añadir("WOLOLO");
                }
                ));
            }

            foreach (Thread t in threads)
                t.Start();

            foreach (Thread t in threads)
                t.Join();

            Assert.AreEqual(7, numbers.Count());
            Assert.AreEqual(7, words.Count());


            threads.Clear();

            for (int i = 0; i < 5; i++)
            {
                threads.Add(new Thread(() =>
                {
                    numbers.Extraer();
                    words.Extraer();
                }
                ));
            }

            foreach (Thread t in threads)
                t.Start();

            foreach (Thread t in threads)
                t.Join();

            Assert.AreEqual(2, numbers.Count());
            Assert.AreEqual(2, words.Count());

            threads.Clear();

            for (int i = 0; i < 5; i++)
            {
                threads.Add(new Thread(() =>
                {
                    numbers.Añadir(i);
                    numbers.Extraer();

                    words.Añadir("WOLOLO");
                    words.Extraer();
                }
                ));
            }

            foreach (Thread t in threads)
                t.Start();

            foreach (Thread t in threads)
                t.Join();

            Assert.AreEqual(2, numbers.Count());
            Assert.AreEqual(2, words.Count());


            Console.WriteLine(numbers.ToString());
            Console.WriteLine(words.ToString());
        }

        [TestMethod]
        public void TestMethod2()
        {
            Cola<int> c = new Cola<int>();

            int[] posiciones = new int[4];
            Thread[] hilos = new Thread[4];

            for (int i = 0; i < 4; i++)
            {
                posiciones[i] = i; // rellenamos
                c.Añadir(i); // añadimos
            }

            for (int i = 0; i < 4; i++)
            {
                hilos[i] = new Thread(() => c.Extraer()); // la tarea de cada uno
                hilos[i].Start(); // lanzamos los hilos
            }

            foreach (Thread hilo in hilos)
            {
                hilo.Join();
            }

            Assert.AreEqual(true, c.EstaVacia());
            Console.WriteLine(c.ToString());
        }

        [TestMethod]
        public void IsEmptyTest()
        {
            Cola<int> queue = new Cola<int>();
            Thread[] threads = new Thread[10];
            //Enqueue
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(() => queue.Añadir(5));
                threads[i].Start();
            }
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(() => queue.Extraer());
                threads[i].Start();
            }

            foreach (var t in threads)
            {
                t.Join();
            }
            Console.WriteLine(queue.NumeroDeElementos);
            Assert.IsTrue(queue.NumeroDeElementos == 0);
            Assert.IsTrue(queue.EstaVacia());
        }

        [TestMethod]
        public void TestMethod1()
        {
            for (int i = 0; i < hilitos.Length; i++)
            {
                hilitos[i] = new Thread(() => colita.Añadir(i));
                hilitos[i].Start();
            }

            foreach (var i in hilitos)
            {
                i.Join();
            }

            Console.Write(colita.ToString());
            Assert.IsFalse(colita.NumeroDeElementos == 0);
            Assert.IsTrue(colita.NumeroDeElementos == 5);
            Assert.IsTrue(!colita.EstaVacia());
        }

        [TestMethod]
        public void TestEnqueue()
        {
            colita = new Cola<int>();

            Assert.IsTrue(colita.EstaVacia());

            Thread[] threads = new Thread[5];

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(() => colita.Añadir(i));
                threads[i].Start();
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            Assert.AreEqual(5, colita.NumeroDeElementos);

            Console.WriteLine(colita.ToString());
        }

        [TestMethod]
        public void TestDequeue()
        {
            colita = new Cola<int>();

            Thread[] threads = new Thread[5];

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(() => colita.Añadir(i));
                threads[i].Start();
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            Assert.AreEqual(5, colita.NumeroDeElementos);

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(() => colita.Extraer());
                threads[i].Start();
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            Assert.IsTrue(colita.EstaVacia());

            Assert.AreEqual(0, colita.NumeroDeElementos);

            Console.WriteLine(colita.ToString());
        }

        [TestMethod]
        public void TestPeek()
        {
            colita = new Cola<int>();

            Thread[] threads = new Thread[2];

            threads[0] = new Thread(() => colita.Añadir(1));
            threads[1] = new Thread(() => colita.Añadir(2));

            foreach (Thread thread in threads)
            {
                thread.Start();
                thread.Join();
            }

            Assert.AreEqual(1, colita.PrimerElemento());

            Console.WriteLine(colita.ToString());
        }
    }
}
