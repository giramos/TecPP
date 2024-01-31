using Microsoft.VisualStudio.TestTools.UnitTesting;
using ListaEnlazada;
using System;
/// <summary>
/// Germán Iglesias Ramos
/// UO202549
/// Lista enlazada enteros
/// TPP2024
/// </summary>
namespace TestListas
{
    [TestClass]
    public class UnitTest1
    {
        Lista lista = new Lista();
        Lista listaVacia = new Lista();

        [TestInitialize]
        public void Init()
        {

            lista.AñadirInicio(1);
            lista.AñadirFinal(2);
            lista.AñadirFinal(3);
            lista.AñadirFinal(4);
            lista.AñadirFinal(5);
        }

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
        public void TestAñadir()
        {
            listaVacia.Añadir(2);
            listaVacia.Añadir(2);
            listaVacia.Añadir(2);
            listaVacia.Añadir(2);
            Assert.IsFalse(listaVacia.ListaVacia());
            Assert.AreEqual(4, listaVacia.NumElementos);
            Assert.AreEqual("Lista Enlazada: 2 2 2 2", listaVacia.ToString());
            listaVacia.Añadir(3);
            listaVacia.Añadir(4);
            Assert.AreEqual(6, listaVacia.NumElementos);
            Assert.AreEqual("Lista Enlazada: 2 2 2 2 3 4", listaVacia.ToString());
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

            // Caso excepecional: No se podra añadir en la ultima posicion con el metodo Añadir(valor, pos)
            // para ello contamos con el metodo AñadirFinal()
            l2.Añadir(4, 4);
            //Assert.AreEqual("Lista Enlazada: 0 1 2 3 4", l2.ToString());

        }

        [TestMethod]
        public void TestBorrarInicio()
        {
            lista.BorrarInicio(1);
            Assert.AreEqual(lista.NumElementos, 4);
            Assert.AreEqual("Lista Enlazada: 2 3 4 5", lista.ToString());
            lista.BorrarInicio(2);
            Assert.AreEqual(lista.NumElementos, 3);
            Assert.AreEqual("Lista Enlazada: 3 4 5", lista.ToString());
        }

        [TestMethod]
        public void TestBorrarFinal()
        {
            lista.BorrarFinal(5);
            Assert.AreEqual(lista.NumElementos, 4);
            Assert.AreEqual("Lista Enlazada: 1 2 3 4", lista.ToString());
            lista.BorrarFinal(4);
            Assert.AreEqual(lista.NumElementos, 3);
            Assert.AreEqual("Lista Enlazada: 1 2 3", lista.ToString());
        }

        [TestMethod]
        public void TestBorrar()
        {
            lista.Borrar(3);
            Assert.AreEqual(lista.NumElementos, 4);
            Assert.AreEqual("Lista Enlazada: 1 2 4 5", lista.ToString());
            lista.Borrar(1);
            Assert.AreEqual(lista.NumElementos, 3);
            Assert.AreEqual("Lista Enlazada: 2 4 5", lista.ToString());
            lista.Borrar(5);
            Assert.AreEqual(lista.NumElementos, 2);
            Assert.AreEqual("Lista Enlazada: 2 4", lista.ToString());
            lista.Borrar(2);
            Assert.AreEqual(lista.NumElementos, 1);
            Assert.AreEqual("Lista Enlazada: 4", lista.ToString());
            lista.Borrar(4);
            Assert.AreEqual(lista.NumElementos, 0);
            Assert.AreEqual("Lista Enlazada:", lista.ToString());

            // Caso excepcional: dos elmentos iguales [Se debe borrar el 1º]
            lista.AñadirInicio(1);
            lista.AñadirFinal(2);
            lista.AñadirFinal(3);
            lista.AñadirFinal(2);
            lista.AñadirFinal(4);
            Assert.AreEqual(lista.NumElementos, 5);
            Assert.AreEqual("Lista Enlazada: 1 2 3 2 4", lista.ToString());
            lista.Borrar(2);
            Assert.AreEqual(lista.NumElementos, 4);
            Assert.AreEqual("Lista Enlazada: 1 3 2 4", lista.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestArgumentException1()
        {
            lista.BorrarFinal(1); // dara una excepcion porque no coincide el valor final 
            lista.BorrarInicio(3); // dara una excepcion porque no coincide con el valor inicial
            lista.Borrar(23); // dara una excepcion ya que el valor pasado no se encuentra en la lista
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void TestArgumentException2()
        {
            listaVacia.BorrarFinal(1); // dara una excepcion porque lalista esta vacia
            listaVacia.BorrarInicio(3); // dara una excepcion porque lalista esta vacia
            listaVacia.Borrar(23); // dara una excepcion porque lalista esta vacia
        }

        [TestMethod]
        public void TestGetElemento()
        {
            Assert.AreEqual(1, lista.GetElemento(0));
            Assert.AreEqual( 5, lista.GetElemento(4));
        }

        [TestMethod]
        public void TestGetPosicion()
        {
            Assert.AreEqual(0, lista.GetPosicion(1));
            Assert.AreEqual(2, lista.GetPosicion(3));
            Assert.AreEqual(3, lista.GetPosicion(4));
            Assert.AreEqual(4, lista.GetPosicion(5));
        }
    }
}
