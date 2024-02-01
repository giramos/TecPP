using Microsoft.VisualStudio.TestTools.UnitTesting;
using ListaEnlazadaObjetos;
using System;

/// <summary>
/// Germ�n Iglesias Ramos
/// UO202549
/// Lista enlazada objetos
/// TPP2024
/// </summary>

namespace Tests
{
    [TestClass]
    public class Prueba
    {
        Lista listaPeque�a = new Lista();
        Lista listaGrande = new Lista();
        Lista listaChar = new Lista();
        Lista listaDouble = new Lista();
        Lista listaString = new Lista();

        [TestInitialize]
        public void Inicio()
        {
            // Lista enteros peque�a
            for (int i = 0; i < 5; i++)
            {
                listaPeque�a.A�adir(i);
            }

            // Lista grande enteros
            for (int i = 0; i < 50; i++)
            {
                listaGrande.A�adir(i);
            }

            // Lista de caracteres
            char[] caracteres = new[] { 'A', 'B', 'C', 'D' };
            for (int i = 0; i < caracteres.Length; i++)
            {
                listaChar.A�adir(caracteres[i], i);
            }

            // Lista de dobles
            for (double i = 0.0; i < 5.0; i = i + 0.5)
            {
                listaDouble.A�adir(i);
            }

            //Lista de string
            string[] cadenas = new[] { "hola", "adios", "bien", "mal" };
            for(int i = 0; i<cadenas.Length; i++)
            {
                listaString.A�adir(cadenas[i], i);
            }

        }

        [TestMethod]
        public void TestA�adirInicio()
        {
            // A�adimos al inicio 
            listaPeque�a.A�adirInicio(1);
            Assert.IsTrue(listaPeque�a.NumElementos.Equals(6));
            Assert.IsFalse(listaPeque�a.ListaVacia());
            Assert.AreEqual("Lista Enlazada: 1 0 1 2 3 4", listaPeque�a.ToString());
            
            // A�adimos al inicio de la lista Grande
            listaGrande.A�adirInicio(1); 
            Assert.IsTrue(listaGrande.NumElementos.Equals(51));
            Assert.IsFalse(listaGrande.ListaVacia());
            Assert.AreEqual(listaGrande.GetElemento(0), 1); // comprobamos que el elemento inicial es el uno            
            Assert.AreEqual(listaGrande.GetPosicion(1), 0); // comprobamos que esta en la posicion inicial

            // A�adimos al inicio de la lista char
            listaChar.A�adirInicio('a');
            Assert.IsTrue(listaChar.NumElementos.Equals(5));
            Assert.IsFalse(listaChar.ListaVacia());
            Assert.AreEqual("Lista Enlazada: a A B C D", listaChar.ToString());
            Assert.AreEqual(listaChar.GetElemento(0), 'a'); // comprobamos que el elemento inicial es el 'a'
            Assert.AreEqual(listaChar.GetPosicion('a'), 0); // comprobamos que esta en la posicion inicial

            // A�adimos al inicio de la lista double
            listaDouble.A�adirInicio(123.333);
            Assert.IsTrue(listaDouble.NumElementos.Equals(11));
            Assert.IsFalse(listaDouble.ListaVacia());
            Assert.AreEqual(listaDouble.GetElemento(0), 123.333); // comprobamos que el elemento inicial es el 'a'
            Assert.AreEqual(listaDouble.GetPosicion(123.333), 0); // comprobamos que esta en la posicion inicial

            // A�adimos al inicio de la lista string
            listaString.A�adirInicio("German");
            Assert.IsTrue(listaString.NumElementos.Equals(5));
            Assert.IsFalse(listaString.ListaVacia());
            Assert.AreEqual("Lista Enlazada: German hola adios bien mal", listaString.ToString());
            Assert.AreEqual(listaString.GetElemento(0), "German"); // comprobamos que el elemento inicial es el 'a'
            Assert.AreEqual(listaString.GetPosicion("German"), 0); // comprobamos que esta en la posicion inicial
        }

        [TestMethod]
        public void TestA�adirFinal()
        {
            // A�adimos al final 
            listaPeque�a.A�adirFinal(1);
            Assert.IsTrue(listaPeque�a.NumElementos.Equals(6));
            Assert.IsFalse(listaPeque�a.ListaVacia());
            Assert.AreEqual("Lista Enlazada: 0 1 2 3 4 1", listaPeque�a.ToString());

            // A�adimos al final de la lista Grande
            listaGrande.A�adirFinal(2342342);
            Assert.IsTrue(listaGrande.NumElementos.Equals(51));
            Assert.IsFalse(listaGrande.ListaVacia());
            Assert.AreEqual(listaGrande.GetElemento(50), 2342342); // comprobamos que el elemento final es el uno            
            Assert.AreEqual(listaGrande.GetPosicion(2342342), 50); // comprobamos que esta en la posicion final

            // A�adimos al final de la lista char
            listaChar.A�adirFinal('a');
            Assert.IsTrue(listaChar.NumElementos.Equals(5));
            Assert.IsFalse(listaChar.ListaVacia());
            Assert.AreEqual("Lista Enlazada: A B C D a", listaChar.ToString());
            Assert.AreEqual(listaChar.GetElemento(4), 'a'); // comprobamos que el elemento final es el 'a'
            Assert.AreEqual(listaChar.GetPosicion('a'), 4); // comprobamos que esta en la posicion final

            // A�adimos al final de la lista double
            listaDouble.A�adirFinal(123.333);
            Assert.IsTrue(listaDouble.NumElementos.Equals(11));
            Assert.IsFalse(listaDouble.ListaVacia());
            Assert.AreEqual(listaDouble.GetElemento(10), 123.333); // comprobamos que el elemento final es el 'a'
            Assert.AreEqual(listaDouble.GetPosicion(123.333), 10); // comprobamos que esta en la final inicial

            // A�adimos al final de la lista string
            listaString.A�adirFinal("German");
            Assert.IsTrue(listaString.NumElementos.Equals(5));
            Assert.IsFalse(listaString.ListaVacia());
            Assert.AreEqual("Lista Enlazada: hola adios bien mal German", listaString.ToString());
            Assert.AreEqual(listaString.GetElemento(4), "German"); // comprobamos que el elemento final es el 'a'
            Assert.AreEqual(listaString.GetPosicion("German"), 4); // comprobamos que esta en la posicion final
        }

        [TestMethod]
        public void TestA�adirPosicion()
        {
            // A�adimos al pos 2 
            listaPeque�a.A�adir(1,2);
            Assert.IsTrue(listaPeque�a.NumElementos.Equals(6));
            Assert.IsFalse(listaPeque�a.ListaVacia());
            Assert.AreEqual("Lista Enlazada: 0 1 1 2 3 4", listaPeque�a.ToString());

            // A�adimos al pos 3 de la lista Grande
            listaGrande.A�adir(2342342,3);
            Assert.IsTrue(listaGrande.NumElementos.Equals(51));
            Assert.IsFalse(listaGrande.ListaVacia());
            Assert.AreEqual(listaGrande.GetElemento(3), 2342342); // comprobamos que el elemento 3 es el 2342342            
            Assert.AreEqual(listaGrande.GetPosicion(2342342), 3); // comprobamos que esta en la posicion 3

            // A�adimos al pos 3 de la lista char
            listaChar.A�adir('a', 3);
            Assert.IsTrue(listaChar.NumElementos.Equals(5));
            Assert.IsFalse(listaChar.ListaVacia());
            Assert.AreEqual("Lista Enlazada: A B C a D", listaChar.ToString());
            Assert.AreEqual(listaChar.GetElemento(3), 'a'); // comprobamos que el elemento 3 es el 'a'
            Assert.AreEqual(listaChar.GetPosicion('a'), 3); // comprobamos que esta en la posicion 3

            // A�adimos al pos 3 de la lista double
            listaDouble.A�adir(123.333, 3);
            Assert.IsTrue(listaDouble.NumElementos.Equals(11));
            Assert.IsFalse(listaDouble.ListaVacia());
            Assert.AreEqual(listaDouble.GetElemento(3), 123.333); // comprobamos que el elemento 3 es el 123.333
            Assert.AreEqual(listaDouble.GetPosicion(123.333), 3); // comprobamos que esta en la posicion 3

            // A�adimos al pos 3 de la lista string
            listaString.A�adir("German", 3);
            Assert.IsTrue(listaString.NumElementos.Equals(5));
            Assert.IsFalse(listaString.ListaVacia());
            Assert.AreEqual("Lista Enlazada: hola adios bien German mal", listaString.ToString());
            Assert.AreEqual(listaString.GetElemento(3), "German"); // comprobamos que el elemento 3 es el 'German'
            Assert.AreEqual(listaString.GetPosicion("German"), 3); // comprobamos que German esta en la posicion 3
        }

        [TestMethod]
        public void TestBorrarElemento()
        {
            // Borramos el 2 
            listaPeque�a.Borrar(2);
            Assert.IsTrue(listaPeque�a.NumElementos.Equals(4));
            Assert.IsFalse(listaPeque�a.ListaVacia());
            Assert.AreEqual("Lista Enlazada: 0 1 3 4", listaPeque�a.ToString());

            // Borramos el 3
            listaGrande.Borrar(3);
            Assert.IsTrue(listaGrande.NumElementos.Equals(49));
            Assert.IsFalse(listaGrande.ListaVacia());

            // Borramos el 'C'
            listaChar.Borrar('C');
            Assert.IsTrue(listaChar.NumElementos.Equals(3));
            Assert.IsFalse(listaChar.ListaVacia());
            Assert.AreEqual("Lista Enlazada: A B D", listaChar.ToString());

            // Borramos el 2.5
            listaDouble.Borrar(2.5);
            Assert.IsTrue(listaDouble.NumElementos.Equals(9));
            Assert.IsFalse(listaDouble.ListaVacia());

            // Borramos el hola
            listaString.Borrar("hola");
            Assert.IsTrue(listaString.NumElementos.Equals(3));
            Assert.IsFalse(listaString.ListaVacia());
            Assert.AreEqual("Lista Enlazada: adios bien mal", listaString.ToString()); 
        }
    }
}
