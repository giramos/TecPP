using Microsoft.VisualStudio.TestTools.UnitTesting;
using ListaEnlazada;
using System;

namespace TestListas
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestAñadirFinal()
        {
            Lista l = new Lista();
            l.AñadirFinal(1);
            l.AñadirFinal(13);
            l.AñadirFinal(133);
            Assert.IsFalse(l.ListaVacia());
            Assert.AreEqual(3, l.NumElementos);
            Assert.AreEqual("Lista Enlazada: 1 13 133", l.ToString());
        }

        [TestMethod]
        public void TestAñadirInicio()
        {
            Lista l = new Lista();
            l.AñadirInicio(1);
            l.AñadirInicio(13);
            l.AñadirInicio(133);
            Assert.IsFalse(l.ListaVacia());
            Assert.AreEqual(3, l.NumElementos);
            Assert.AreEqual("Lista Enlazada: 133 13 1", l.ToString());
        }

        [TestMethod]
        public void TestAñadirPosicion()
        {
            Lista l = new Lista();
            l.AñadirInicio(1);
            l.AñadirInicio(13);
            l.AñadirInicio(133);
            Assert.IsFalse(l.ListaVacia());
            Assert.AreEqual(3, l.NumElementos);
            l.Añadir(24, 2);
            Assert.AreEqual("Lista Enlazada: 133 13 24 1", l.ToString());
            Assert.AreEqual(4, l.NumElementos);
            l.Añadir(24, 0);
            Assert.AreEqual("Lista Enlazada: 24 133 13 24 1", l.ToString());
            Assert.AreEqual(5, l.NumElementos);
            l.Añadir(24, 4);
            Assert.AreEqual("Lista Enlazada: 24 133 13 24 24 1", l.ToString());
            Assert.AreEqual(6, l.NumElementos);

            Lista l1 = new Lista();
            l1.Añadir(1, 0);
            Assert.IsTrue(!l1.ListaVacia(), "La lista no esta vacia");
            Assert.AreEqual("Lista Enlazada: 1", l1.ToString());
            l1.AñadirFinal(3);
            l1.Añadir(0, 0);
            Assert.AreEqual("Lista Enlazada: 0 1 3", l1.ToString());

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestArgumentException()
        {
            Lista l2 = new Lista();
            l2.AñadirInicio(3);
            l2.AñadirInicio(2);
            l2.AñadirInicio(1);
            l2.AñadirInicio(0);

            l2.Añadir(4, 4);
            //Assert.AreEqual("Lista Enlazada: 0 1 2 3 4", l2.ToString());

        }
    }
}
