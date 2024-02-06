using Microsoft.VisualStudio.TestTools.UnitTesting;
using ListaEnlazadaObjetos;
using System;

/// <summary>
/// Germán Iglesias Ramos
/// UO202549
/// Lista enlazada objetos
/// TPP2024
/// </summary>

namespace Tests
{
    [TestClass]
    public class Prueba
    {
        Lista listaPequeña = new Lista();
        Lista listaGrande = new Lista();
        Lista listaChar = new Lista();
        Lista listaDouble = new Lista();
        Lista listaString = new Lista();
        Lista listaMiscelanea = new Lista();

        [TestInitialize]
        public void Inicio()
        {
            // Lista enteros pequeña
            for (int i = 0; i < 5; i++)
            {
                listaPequeña.Añadir(i);
            }

            // Lista grande enteros
            for (int i = 0; i < 50; i++)
            {
                listaGrande.Añadir(i);
            }

            // Lista de caracteres
            char[] caracteres = new[] { 'A', 'B', 'C', 'D' };
            for (int i = 0; i < caracteres.Length; i++)
            {
                listaChar.Añadir(caracteres[i], i);
            }

            // Lista de dobles
            for (double i = 0.0; i < 5.0; i = i + 0.5)
            {
                listaDouble.Añadir(i);
            }

            // Lista de string
            string[] cadenas = new[] { "hola", "adios", "bien", "mal" };
            for (int i = 0; i < cadenas.Length; i++)
            {
                listaString.Añadir(cadenas[i], i);
            }

            // Lista miscelánea (MIXTA)
            listaMiscelanea.Añadir(1);
            listaMiscelanea.Añadir('a');
            listaMiscelanea.Añadir(1.5);
            listaMiscelanea.Añadir("german");


        }

        [TestMethod]
        public void TestAñadirInicio()
        {
            // Añadimos al inicio 
            listaPequeña.AñadirInicio(1);
            Assert.IsTrue(listaPequeña.NumElementos.Equals(6));
            Assert.IsFalse(listaPequeña.ListaVacia());
            Assert.AreEqual("Lista Enlazada: 1 0 1 2 3 4", listaPequeña.ToString());

            // Añadimos al inicio de la lista Grande
            listaGrande.AñadirInicio(1);
            Assert.IsTrue(listaGrande.NumElementos.Equals(51));
            Assert.IsFalse(listaGrande.ListaVacia());
            Assert.AreEqual(listaGrande.GetElemento(0), 1); // comprobamos que el elemento inicial es el uno            
            Assert.AreEqual(listaGrande.GetPosicion(1), 0); // comprobamos que esta en la posicion inicial

            // Añadimos al inicio de la lista char
            listaChar.AñadirInicio('a');
            Assert.IsTrue(listaChar.NumElementos.Equals(5));
            Assert.IsFalse(listaChar.ListaVacia());
            Assert.AreEqual("Lista Enlazada: a A B C D", listaChar.ToString());
            Assert.AreEqual(listaChar.GetElemento(0), 'a'); // comprobamos que el elemento inicial es el 'a'
            Assert.AreEqual(listaChar.GetPosicion('a'), 0); // comprobamos que esta en la posicion inicial

            // Añadimos al inicio de la lista double
            listaDouble.AñadirInicio(123.333);
            Assert.IsTrue(listaDouble.NumElementos.Equals(11));
            Assert.IsFalse(listaDouble.ListaVacia());
            Assert.AreEqual(listaDouble.GetElemento(0), 123.333); // comprobamos que el elemento inicial es el 'a'
            Assert.AreEqual(listaDouble.GetPosicion(123.333), 0); // comprobamos que esta en la posicion inicial

            // Añadimos al inicio de la lista string
            listaString.AñadirInicio("German");
            Assert.IsTrue(listaString.NumElementos.Equals(5));
            Assert.IsFalse(listaString.ListaVacia());
            Assert.AreEqual("Lista Enlazada: German hola adios bien mal", listaString.ToString());
            Assert.AreEqual(listaString.GetElemento(0), "German"); // comprobamos que el elemento inicial es el 'a'
            Assert.AreEqual(listaString.GetPosicion("German"), 0); // comprobamos que esta en la posicion inicial
        }

        [TestMethod]
        public void TestAñadirFinal()
        {
            // Añadimos al final 
            listaPequeña.AñadirFinal(1);
            Assert.IsTrue(listaPequeña.NumElementos.Equals(6));
            Assert.IsFalse(listaPequeña.ListaVacia());
            Assert.AreEqual("Lista Enlazada: 0 1 2 3 4 1", listaPequeña.ToString());

            // Añadimos al final de la lista Grande
            listaGrande.AñadirFinal(2342342);
            Assert.IsTrue(listaGrande.NumElementos.Equals(51));
            Assert.IsFalse(listaGrande.ListaVacia());
            Assert.AreEqual(listaGrande.GetElemento(50), 2342342); // comprobamos que el elemento final es el uno            
            Assert.AreEqual(listaGrande.GetPosicion(2342342), 50); // comprobamos que esta en la posicion final

            // Añadimos al final de la lista char
            listaChar.AñadirFinal('a');
            Assert.IsTrue(listaChar.NumElementos.Equals(5));
            Assert.IsFalse(listaChar.ListaVacia());
            Assert.AreEqual("Lista Enlazada: A B C D a", listaChar.ToString());
            Assert.AreEqual(listaChar.GetElemento(4), 'a'); // comprobamos que el elemento final es el 'a'
            Assert.AreEqual(listaChar.GetPosicion('a'), 4); // comprobamos que esta en la posicion final

            // Añadimos al final de la lista double
            listaDouble.AñadirFinal(123.333);
            Assert.IsTrue(listaDouble.NumElementos.Equals(11));
            Assert.IsFalse(listaDouble.ListaVacia());
            Assert.AreEqual(listaDouble.GetElemento(10), 123.333); // comprobamos que el elemento final es el 'a'
            Assert.AreEqual(listaDouble.GetPosicion(123.333), 10); // comprobamos que esta en la final inicial

            // Añadimos al final de la lista string
            listaString.AñadirFinal("German");
            Assert.IsTrue(listaString.NumElementos.Equals(5));
            Assert.IsFalse(listaString.ListaVacia());
            Assert.AreEqual("Lista Enlazada: hola adios bien mal German", listaString.ToString());
            Assert.AreEqual(listaString.GetElemento(4), "German"); // comprobamos que el elemento final es el 'a'
            Assert.AreEqual(listaString.GetPosicion("German"), 4); // comprobamos que esta en la posicion final
        }

        [TestMethod]
        public void TestAñadirPosicion()
        {
            // Añadimos al pos 2 
            listaPequeña.Añadir(1, 2);
            Assert.IsTrue(listaPequeña.NumElementos.Equals(6));
            Assert.IsFalse(listaPequeña.ListaVacia());
            Assert.AreEqual("Lista Enlazada: 0 1 1 2 3 4", listaPequeña.ToString());

            // Añadimos al pos 3 de la lista Grande
            listaGrande.Añadir(2342342, 3);
            Assert.IsTrue(listaGrande.NumElementos.Equals(51));
            Assert.IsFalse(listaGrande.ListaVacia());
            Assert.AreEqual(listaGrande.GetElemento(3), 2342342); // comprobamos que el elemento 3 es el 2342342            
            Assert.AreEqual(listaGrande.GetPosicion(2342342), 3); // comprobamos que esta en la posicion 3

            // Añadimos al pos 3 de la lista char
            listaChar.Añadir('a', 3);
            Assert.IsTrue(listaChar.NumElementos.Equals(5));
            Assert.IsFalse(listaChar.ListaVacia());
            Assert.AreEqual("Lista Enlazada: A B C a D", listaChar.ToString());
            Assert.AreEqual(listaChar.GetElemento(3), 'a'); // comprobamos que el elemento 3 es el 'a'
            Assert.AreEqual(listaChar.GetPosicion('a'), 3); // comprobamos que esta en la posicion 3

            // Añadimos al pos 3 de la lista double
            listaDouble.Añadir(123.333, 3);
            Assert.IsTrue(listaDouble.NumElementos.Equals(11));
            Assert.IsFalse(listaDouble.ListaVacia());
            Assert.AreEqual(listaDouble.GetElemento(3), 123.333); // comprobamos que el elemento 3 es el 123.333
            Assert.AreEqual(listaDouble.GetPosicion(123.333), 3); // comprobamos que esta en la posicion 3

            // Añadimos al pos 3 de la lista string
            listaString.Añadir("German", 3);
            Assert.IsTrue(listaString.NumElementos.Equals(5));
            Assert.IsFalse(listaString.ListaVacia());
            Assert.AreEqual("Lista Enlazada: hola adios bien German mal", listaString.ToString());
            Assert.AreEqual(listaString.GetElemento(3), "German"); // comprobamos que el elemento 3 es el 'German'
            Assert.AreEqual(listaString.GetPosicion("German"), 3); // comprobamos que German esta en la posicion 3
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestArgumentExceptionAñadir()
        {
            // Añadir inicio

            // No se puede añadir null a la lista de enteros
            listaPequeña.AñadirInicio(null);

            // No se puede añadir null a la lista de enteros
            listaGrande.AñadirInicio(null);

            // No sepuede añadir null a la lista de char
            listaChar.AñadirInicio(null);

            // No se puede añadir null a la lista de double
            listaDouble.AñadirInicio(null);

            // No se puede añadir null a la lista de string
            listaString.AñadirInicio(null);

            // Añadir final

            // No se puede añadir null a la lista de enteros
            listaPequeña.AñadirFinal(null);

            // No se puede añadir null a la lista de enteros
            listaGrande.AñadirFinal(null);

            // No sepuede añadir null a la lista de char
            listaChar.AñadirFinal(null);

            // No se puede añadir null a la lista de double
            listaDouble.AñadirFinal(null);

            // No se puede añadir null a la lista de string
            listaString.AñadirFinal(null);

            // Añadir por posicion (Comprobar null y posicion)

            // No se puede añadir null a la lista de enteros
            listaPequeña.Añadir(null, 2);

            // No se puede añadir null a la lista de enteros
            listaGrande.Añadir(null, 2);

            // No sepuede añadir null a la lista de char
            listaChar.Añadir(null, 2);

            // No se puede añadir null a la lista de double
            listaDouble.Añadir(null, 2);

            // No se puede añadir null a la lista de string
            listaString.Añadir(null, 2);

            // No se puede añadir en una posicion menor que 0

            // lista de enteros
            listaPequeña.Añadir(12, -2);

            // lista de enteros
            listaGrande.Añadir(12, -2);

            // lista de char
            listaChar.Añadir('b', -2);

            // lista de double
            listaDouble.Añadir(3.75, -2);

            // lista de string
            listaString.Añadir("null", -2);

            // No se puede añadir en una posicion mayor que el numero de elementos

            // lista de enteros. Num elemementos = 5
            listaPequeña.Añadir(12, 5);

            // lista de enteros. Num elementos = 50
            listaGrande.Añadir(12, 50);

            // lista de char. Num elementos = 4
            listaChar.Añadir('b', 4);

            // lista de double. Num elementos = 10
            listaDouble.Añadir(3.75, 10);

            // lista de string. Num elementos = 4
            listaString.Añadir("null", 4);

        }

        [TestMethod]
        public void TestBorrarElemento()
        {
            // Borramos el 2 
            listaPequeña.Borrar(2);
            Assert.IsTrue(listaPequeña.NumElementos.Equals(4));
            Assert.IsFalse(listaPequeña.ListaVacia());
            Assert.AreEqual("Lista Enlazada: 0 1 3 4", listaPequeña.ToString());

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

        [TestMethod]
        public void TestBorrarInicio()
        {
            // Borramos el 0 al inicio 
            listaPequeña.BorrarInicio(0);
            Assert.IsTrue(listaPequeña.NumElementos.Equals(4));
            Assert.IsFalse(listaPequeña.ListaVacia());
            Assert.AreEqual("Lista Enlazada: 1 2 3 4", listaPequeña.ToString());

            // Borramos el 0 al inicio
            listaGrande.BorrarInicio(0);
            Assert.IsTrue(listaGrande.NumElementos.Equals(49));
            Assert.IsFalse(listaGrande.ListaVacia());

            // Borramos el 'A'
            listaChar.BorrarInicio('A');
            Assert.IsTrue(listaChar.NumElementos.Equals(3));
            Assert.IsFalse(listaChar.ListaVacia());
            Assert.AreEqual("Lista Enlazada: B C D", listaChar.ToString());

            // Borramos el 0
            listaDouble.BorrarInicio(0.0);
            Assert.IsTrue(listaDouble.NumElementos.Equals(9));
            Assert.IsFalse(listaDouble.ListaVacia());

            // Borramos el hola
            listaString.BorrarInicio("hola");
            Assert.IsTrue(listaString.NumElementos.Equals(3));
            Assert.IsFalse(listaString.ListaVacia());
            Assert.AreEqual("Lista Enlazada: adios bien mal", listaString.ToString());
        }

        [TestMethod]
        public void TestBorrarFinal()
        {
            // Borramos el 4 al inicio 
            listaPequeña.BorrarFinal(4);
            Assert.IsTrue(listaPequeña.NumElementos.Equals(4));
            Assert.IsFalse(listaPequeña.ListaVacia());
            Assert.AreEqual("Lista Enlazada: 0 1 2 3", listaPequeña.ToString());

            // Borramos el 49 al inicio
            listaGrande.BorrarFinal(49);
            Assert.IsTrue(listaGrande.NumElementos.Equals(49));
            Assert.IsFalse(listaGrande.ListaVacia());

            // Borramos el 'D'
            listaChar.BorrarFinal('D');
            Assert.IsTrue(listaChar.NumElementos.Equals(3));
            Assert.IsFalse(listaChar.ListaVacia());
            Assert.AreEqual("Lista Enlazada: A B C", listaChar.ToString());

            // Borramos el 4.5
            listaDouble.BorrarFinal(4.5);
            Assert.IsTrue(listaDouble.NumElementos.Equals(9));
            Assert.IsFalse(listaDouble.ListaVacia());

            // Borramos el mal
            listaString.BorrarFinal("mal");
            Assert.IsTrue(listaString.NumElementos.Equals(3));
            Assert.IsFalse(listaString.ListaVacia());
            Assert.AreEqual("Lista Enlazada: hola adios bien", listaString.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestArgumentExceptionBorrar()
        {
            // Borrar inicio

            // No se puede borrar null a la lista de enteros
            listaPequeña.BorrarInicio(null);

            // No se puede borrar null a la lista de enteros
            listaGrande.BorrarInicio(null);

            // No se puede borrar null a la lista de char
            listaChar.BorrarInicio(null);

            // No se puede borrar null a la lista de double
            listaDouble.BorrarInicio(null);

            // No se puede borrar null a la lista de string
            listaString.BorrarInicio(null);

            // Borrar final

            // No se puede borrar null a la lista de enteros
            listaPequeña.BorrarFinal(null);

            // No se puede borrar null a la lista de enteros
            listaGrande.BorrarFinal(null);

            // No sepuede borrar null a la lista de char
            listaChar.BorrarFinal(null);

            // No se puede borrar null a la lista de double
            listaDouble.BorrarFinal(null);

            // No se puede borrar null a la lista de string
            listaString.BorrarFinal(null);

            // Borrar (Comprobar null)

            // No se puede añadir null a la lista de enteros
            listaPequeña.Borrar(null);

            // No se puede añadir null a la lista de enteros
            listaGrande.Borrar(null);

            // No sepuede añadir null a la lista de char
            listaChar.Borrar(null);

            // No se puede añadir null a la lista de double
            listaDouble.Borrar(null);

            // No se puede añadir null a la lista de string
            listaString.Borrar(null);

            // Comprobar si la lista esta vacia 

            // Comprobamos que el valor a eliminar realmente se corresponde con el que hay en la lista
            listaMiscelanea.Borrar('b'); // No tenemos ningun elemento que se corresponda con 'b'
            listaMiscelanea.Borrar("german"); // No tenemos ningun elemento que se corresponda con "german"
            listaMiscelanea.Borrar(2); // No tenemos ningun elemento que se corresponda con 2
            listaMiscelanea.Borrar(0.75); // No tenemos ningun elemento que se corresponda con 0.75

            // Comprobamos que el valor a eliminar realmente se corresponde con el que hay en la lista
            listaMiscelanea.BorrarInicio('b'); // No tenemos ningun elemento que se corresponda con 'b'
            listaMiscelanea.BorrarInicio("german"); // No tenemos ningun elemento que se corresponda con "german"
            listaMiscelanea.BorrarInicio(2); // No tenemos ningun elemento que se corresponda con 2
            listaMiscelanea.BorrarInicio(0.75); // No tenemos ningun elemento que se corresponda con 0.75

            // Comprobamos que el valor a eliminar realmente se corresponde con el que hay en la lista
            listaMiscelanea.BorrarFinal('b'); // No tenemos ningun elemento que se corresponda con 'b'
            listaMiscelanea.BorrarFinal("german"); // No tenemos ningun elemento que se corresponda con "german"
            listaMiscelanea.BorrarFinal(2); // No tenemos ningun elemento que se corresponda con 2
            listaMiscelanea.BorrarFinal(0.75); // No tenemos ningun elemento que se corresponda con 0.75

            // Vaciamos la lista
            listaMiscelanea.VaciarLista();
            listaMiscelanea.BorrarInicio(1); // No se puede borrar ya que la lista esta vacia
            listaMiscelanea.BorrarFinal(1); // No se puede borrar ya que la lista esta vacia
            listaMiscelanea.Borrar(1); // No se puede borrar ya que la lista esta vacia

        }

        [TestMethod]
        public void TestVaciarLista()
        {
            // La lista tinene 4 elementos ...
            Assert.IsTrue(listaMiscelanea.NumElementos.Equals(4));
            // Vaciamos la lista ...
            listaMiscelanea.VaciarLista();
            // Comprobamos que la lista ya NO tiene ningun elemento
            Assert.IsTrue(listaMiscelanea.NumElementos.Equals(0));
            // Comprobamos que verdaderamente no imprime nada
            Assert.AreEqual("Lista Enlazada:", listaMiscelanea.ToString());
        }

        [TestMethod]
        public void TestGetPosicion()
        {
            Assert.AreEqual(0, listaMiscelanea.GetPosicion(1)); // Comprobamos que el 1 esta en la pos 0
            Assert.AreEqual(3, listaMiscelanea.GetPosicion("german")); // Comprobamos que "german" esta en la pos 3
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestArgumentExceptionGetPosicion()
        {
            // Comprobamos que "TPP" no se encuentra en nuestra lista
            listaMiscelanea.GetPosicion("TPP");
            // Vaciamos la lista
            listaMiscelanea.VaciarLista();
            // Comprobamos que el 1 no esta en nuestra lista ya que esta vacia
            listaMiscelanea.GetPosicion(1);

        }

        [TestMethod]
        public void TestGetElemento()
        {
            // Comprobamos que el 1.5 esta en nuestra lista en la pos 2
            Assert.AreEqual(1.5, listaMiscelanea.GetElemento(2));
            // COmprobamos que el 1 esta en nuestra lista en la pos 0
            Assert.AreEqual(1, listaMiscelanea.GetElemento(0));

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestArgumentExceptionGetElemento()
        {
            // Comprobamos que el 1.5 no esta en la posicion 1
            Assert.IsFalse(listaMiscelanea.GetElemento(1).Equals(1.5));
            // Comprobamos que el 1 no esta en la pos 1
            Assert.IsFalse(listaMiscelanea.GetElemento(1).Equals(1));
            // Comprobamos que la posicion pasada se execede de los limites de nuestra lista
            listaMiscelanea.GetElemento(4); // La lista tiene 4 elementos por lo que la pos 4 no EXISTe
        }

        [TestMethod]
        public void TestContiene()
        {
            // Comprobamos que nuestra lista contiene un entero 1
            Assert.IsTrue(listaMiscelanea.Contiene(1));
            // Comprobamos que nuestra lista contiene un double 1.5
            Assert.IsTrue(listaMiscelanea.Contiene(1.5));
            // Comprobamos que nuestra lista contiene un string "german"
            Assert.IsTrue(listaMiscelanea.Contiene("german"));
            // Comprobamos que nuestra lista NO contiene un char 'X'
            Assert.IsFalse(listaMiscelanea.Contiene('X'));
        }
    }
}
