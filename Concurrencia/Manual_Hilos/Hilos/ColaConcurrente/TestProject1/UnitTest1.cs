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

        [TestInitialize]
        public void TestInit()
        {
            micola.A�adir("hola");
            micola.A�adir(1);
            micola.A�adir('a');

            queue.Enqueue("hola");
            queue.Enqueue(1);
            queue.Enqueue('a');

            numbers = new Cola<int>();
            words = new Cola<string>();

            numbers.A�adir(777);
            numbers.A�adir(2077);

            words.A�adir("Vegetta");
            words.A�adir("CiberPunk");
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
        }

        [TestMethod]
        public void TestA�adir()
        {
            micola.A�adir(2);
            Assert.AreEqual(micola.Count(), 4);
            Assert.AreEqual(micola.EstaVacia(), false);
            Assert.AreEqual(micola.PrimerElemento(), "hola");

            queue.Enqueue(2);
            Assert.AreEqual(queue.Count(), 4);
            Assert.AreEqual(queue.IsEmpty, false);
            Assert.AreEqual(queue.First(), "hola");

        }

        [TestMethod]
        public void Test()
        {
            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < 5; i++)
            {
                threads.Add(new Thread(() =>
                {
                    numbers.A�adir(i);
                    words.A�adir("WOLOLO");
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
                    numbers.A�adir(i);
                    numbers.Extraer();

                    words.A�adir("WOLOLO");
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
                c.A�adir(i); // a�adimos
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
        }

        [TestMethod]
        public void IsEmptyTest()
        {
            Cola<int> queue = new Cola<int>();
            Thread[] threads = new Thread[10];
            //Enqueue
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(() => queue.A�adir(5));
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
    }
}
