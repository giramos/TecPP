using Microsoft.VisualStudio.TestTools.UnitTesting;
using ListaEnlazada;
using System;

namespace TestListas
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestA�adirFinal()
        {
            Lista l = new Lista();
            l.A�adirFinal(1);
            l.A�adirFinal(13);
            l.A�adirFinal(133);
            Assert.IsFalse(l.ListaVacia());
            Assert.AreEqual(3, l.NumElementos);
            Assert.AreEqual("Lista Enlazada: 1 13 133", l.ToString());
        }

        [TestMethod]
        public void TestA�adirInicio()
        {
            Lista l = new Lista();
            l.A�adirInicio(1);
            l.A�adirInicio(13);
            l.A�adirInicio(133);
            Assert.IsFalse(l.ListaVacia());
            Assert.AreEqual(3, l.NumElementos);
            Assert.AreEqual("Lista Enlazada: 133 13 1", l.ToString());
        }

        [TestMethod]
        public void TestA�adirPosicion()
        {
            Lista l = new Lista();
            l.A�adirInicio(1);
            l.A�adirInicio(13);
            l.A�adirInicio(133);
            Assert.IsFalse(l.ListaVacia());
            Assert.AreEqual(3, l.NumElementos);
            l.A�adir(24, 2);
            Assert.AreEqual("Lista Enlazada: 133 13 24 1", l.ToString());
            Assert.AreEqual(4, l.NumElementos);
            l.A�adir(24, 0);
            Assert.AreEqual("Lista Enlazada: 24 133 13 24 1", l.ToString());
            Assert.AreEqual(5, l.NumElementos);
            l.A�adir(24, 4);
            Assert.AreEqual("Lista Enlazada: 24 133 13 24 24 1", l.ToString());
            Assert.AreEqual(6, l.NumElementos);

            Lista l1 = new Lista();
            l1.A�adir(1, 0);
            Assert.IsTrue(!l1.ListaVacia(), "La lista no esta vacia");
            Assert.AreEqual("Lista Enlazada: 1", l1.ToString());
            l1.A�adirFinal(3);
            l1.A�adir(0, 0);
            Assert.AreEqual("Lista Enlazada: 0 1 3", l1.ToString());

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestArgumentException()
        {
            Lista l2 = new Lista();
            l2.A�adirInicio(3);
            l2.A�adirInicio(2);
            l2.A�adirInicio(1);
            l2.A�adirInicio(0);

            l2.A�adir(4, 4);
            //Assert.AreEqual("Lista Enlazada: 0 1 2 3 4", l2.ToString());

        }
    }
}
