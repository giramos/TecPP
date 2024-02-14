using Microsoft.VisualStudio.TestTools.UnitTesting;
using ListaEnlazadaObjetos;
using System;
using ConjuntoObjetos;
using System.Runtime.InteropServices;
using PilaPolimorfica;

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
        Lista listaMiscelanea = new Lista();
        Lista listaNull = new Lista();
        Pila pila = new Pila(5);

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

            // Lista de string
            string[] cadenas = new[] { "hola", "adios", "bien", "mal" };
            for (int i = 0; i < cadenas.Length; i++)
            {
                listaString.A�adir(cadenas[i], i);
            }

            // Lista miscel�nea (MIXTA)
            listaMiscelanea.A�adir(1);
            listaMiscelanea.A�adir('a');
            listaMiscelanea.A�adir(1.5);
            listaMiscelanea.A�adir("german");

            // Lista con null
            for (int i = 0; i < 5; i++) { listaNull.A�adir(null); }

            // Pila
            pila.Push('a');
            pila.Push(1);
            pila.Push("dos");
            pila.Push(3.0);


        }

        [TestMethod]
        public void TestA�adirInicio()
        {
            // A�adimos al inicio 
            listaPeque�a.A�adirInicio(1);
            Assert.IsTrue(listaPeque�a.NumElementos.Equals(6));
            Assert.IsFalse(listaPeque�a.ListaVacia());
            Assert.AreEqual("[1 - 0 - 1 - 2 - 3 - 4 - ]", listaPeque�a.ToString());

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
            Assert.AreEqual("[a - A - B - C - D - ]", listaChar.ToString());
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
            Assert.AreEqual("[German - hola - adios - bien - mal - ]", listaString.ToString());
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
            Assert.AreEqual("[0 - 1 - 2 - 3 - 4 - 1 - ]", listaPeque�a.ToString());

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
            Assert.AreEqual("[A - B - C - D - a - ]", listaChar.ToString());
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
            Assert.AreEqual("[hola - adios - bien - mal - German - ]", listaString.ToString());
            Assert.AreEqual(listaString.GetElemento(4), "German"); // comprobamos que el elemento final es el 'a'
            Assert.AreEqual(listaString.GetPosicion("German"), 4); // comprobamos que esta en la posicion final
        }

        [TestMethod]
        public void TestA�adirPosicion()
        {
            // A�adimos al pos 2 
            listaPeque�a.A�adir(1, 2);
            Assert.IsTrue(listaPeque�a.NumElementos.Equals(6));
            Assert.IsFalse(listaPeque�a.ListaVacia());
            Assert.AreEqual("[0 - 1 - 1 - 2 - 3 - 4 - ]", listaPeque�a.ToString());

            // A�adimos al pos 3 de la lista Grande
            listaGrande.A�adir(2342342, 3);
            Assert.IsTrue(listaGrande.NumElementos.Equals(51));
            Assert.IsFalse(listaGrande.ListaVacia());
            Assert.AreEqual(listaGrande.GetElemento(3), 2342342); // comprobamos que el elemento 3 es el 2342342            
            Assert.AreEqual(listaGrande.GetPosicion(2342342), 3); // comprobamos que esta en la posicion 3

            // A�adimos al pos 3 de la lista char
            listaChar.A�adir('a', 3);
            Assert.IsTrue(listaChar.NumElementos.Equals(5));
            Assert.IsFalse(listaChar.ListaVacia());
            Assert.AreEqual("[A - B - C - a - D - ]", listaChar.ToString());
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
            Assert.AreEqual("[hola - adios - bien - German - mal - ]", listaString.ToString());
            Assert.AreEqual(listaString.GetElemento(3), "German"); // comprobamos que el elemento 3 es el 'German'
            Assert.AreEqual(listaString.GetPosicion("German"), 3); // comprobamos que German esta en la posicion 3
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestArgumentExceptionA�adir()
        {
            // No se puede a�adir en una posicion menor que 0

            // lista de enteros
            listaPeque�a.A�adir(12, -2);

            // lista de enteros
            listaGrande.A�adir(12, -2);

            // lista de char
            listaChar.A�adir('b', -2);

            // lista de double
            listaDouble.A�adir(3.75, -2);

            // lista de string
            listaString.A�adir("null", -2);

            // No se puede a�adir en una posicion mayor que el numero de elementos

            // lista de enteros. Num elemementos = 5
            listaPeque�a.A�adir(12, 5);

            // lista de enteros. Num elementos = 50
            listaGrande.A�adir(12, 50);

            // lista de char. Num elementos = 4
            listaChar.A�adir('b', 4);

            // lista de double. Num elementos = 10
            listaDouble.A�adir(3.75, 10);

            // lista de string. Num elementos = 4
            listaString.A�adir("null", 4);

        }

        [TestMethod]
        public void TestBorrarElemento()
        {
            // Borramos el 2 
            listaPeque�a.Borrar(2);
            Assert.IsTrue(listaPeque�a.NumElementos.Equals(4));
            Assert.IsFalse(listaPeque�a.ListaVacia());
            Assert.AreEqual("[0 - 1 - 3 - 4 - ]", listaPeque�a.ToString());

            // Borramos el 3
            listaGrande.Borrar(3);
            Assert.IsTrue(listaGrande.NumElementos.Equals(49));
            Assert.IsFalse(listaGrande.ListaVacia());

            // Borramos el 'C'
            listaChar.Borrar('C');
            Assert.IsTrue(listaChar.NumElementos.Equals(3));
            Assert.IsFalse(listaChar.ListaVacia());
            Assert.AreEqual("[A - B - D - ]", listaChar.ToString());

            // Borramos el 2.5
            listaDouble.Borrar(2.5);
            Assert.IsTrue(listaDouble.NumElementos.Equals(9));
            Assert.IsFalse(listaDouble.ListaVacia());

            // Borramos el hola
            listaString.Borrar("hola");
            Assert.IsTrue(listaString.NumElementos.Equals(3));
            Assert.IsFalse(listaString.ListaVacia());
            Assert.AreEqual("[adios - bien - mal - ]", listaString.ToString());
        }

        [TestMethod]
        public void TestBorrarInicio()
        {
            // Borramos el 0 al inicio 
            listaPeque�a.BorrarInicio(0);
            Assert.IsTrue(listaPeque�a.NumElementos.Equals(4));
            Assert.IsFalse(listaPeque�a.ListaVacia());
            Assert.AreEqual("[1 - 2 - 3 - 4 - ]", listaPeque�a.ToString());

            // Borramos el 0 al inicio
            listaGrande.BorrarInicio(0);
            Assert.IsTrue(listaGrande.NumElementos.Equals(49));
            Assert.IsFalse(listaGrande.ListaVacia());

            // Borramos el 'A'
            listaChar.BorrarInicio('A');
            Assert.IsTrue(listaChar.NumElementos.Equals(3));
            Assert.IsFalse(listaChar.ListaVacia());
            Assert.AreEqual("[B - C - D - ]", listaChar.ToString());

            // Borramos el 0
            listaDouble.BorrarInicio(0.0);
            Assert.IsTrue(listaDouble.NumElementos.Equals(9));
            Assert.IsFalse(listaDouble.ListaVacia());

            // Borramos el hola
            listaString.BorrarInicio("hola");
            Assert.IsTrue(listaString.NumElementos.Equals(3));
            Assert.IsFalse(listaString.ListaVacia());
            Assert.AreEqual("[adios - bien - mal - ]", listaString.ToString());
        }

        [TestMethod]
        public void TestBorrarFinal()
        {
            // Borramos el 4 al inicio 
            listaPeque�a.BorrarFinal(4);
            Assert.IsTrue(listaPeque�a.NumElementos.Equals(4));
            Assert.IsFalse(listaPeque�a.ListaVacia());
            Assert.AreEqual("[0 - 1 - 2 - 3 - ]", listaPeque�a.ToString());

            // Borramos el 49 al inicio
            listaGrande.BorrarFinal(49);
            Assert.IsTrue(listaGrande.NumElementos.Equals(49));
            Assert.IsFalse(listaGrande.ListaVacia());

            // Borramos el 'D'
            listaChar.BorrarFinal('D');
            Assert.IsTrue(listaChar.NumElementos.Equals(3));
            Assert.IsFalse(listaChar.ListaVacia());
            Assert.AreEqual("[A - B - C - ]", listaChar.ToString());

            // Borramos el 4.5
            listaDouble.BorrarFinal(4.5);
            Assert.IsTrue(listaDouble.NumElementos.Equals(9));
            Assert.IsFalse(listaDouble.ListaVacia());

            // Borramos el mal
            listaString.BorrarFinal("mal");
            Assert.IsTrue(listaString.NumElementos.Equals(3));
            Assert.IsFalse(listaString.ListaVacia());
            Assert.AreEqual("[hola - adios - bien - ]", listaString.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestArgumentExceptionBorrar()
        {
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
            Assert.AreEqual("[]", listaMiscelanea.ToString());
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

        [TestMethod]
        public void TestConjunto()
        {
            Conjunto set = new();
            set.A�adir(1);
            set.A�adir('a');
            set.A�adir("hola");

            // Comprobamos que funciona el metodo a�adir
            Assert.IsTrue(set.NumElementos.Equals(3));
            Assert.IsTrue(set.Contiene(1));
            Assert.IsTrue(set.Contiene('a'));
            Assert.IsTrue(set.Contiene("hola"));

            // Operadores + y -
            set += 'o';
            Assert.AreEqual("[1 - a - hola - o - ]", set.ToString());
            set -= 'a';
            Assert.AreEqual("[1 - hola - o - ]", set.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestArgumentExceptionConjunto()
        {
            // Comprobamos que nuestro conjunto no admite valores repetidos
            Conjunto set = new();
            set.A�adir(1);
            set.A�adir('a');
            set.A�adir("hola");
            Assert.IsTrue(set.NumElementos.Equals(3));
            Assert.IsTrue(set.Contiene(1));
            Assert.IsTrue(set.Contiene('a'));
            Assert.IsTrue(set.Contiene("hola"));

            // Al a�adir un elemento repetido debe de lanzar una excepcion
            set.A�adir('a');
        }

        [TestMethod]
        public void TestNull()
        {
            // Comprobar que nuestra lista null tiene 5 elementos
            Assert.IsTrue(listaNull.NumElementos == 5);
            // Comprobar exactamente que nuestro toString muestra los null
            Assert.AreEqual("[null - null - null - null - null - ]", listaNull.ToString());
            // A�adimos un caracter
            listaNull.A�adirFinal('a');
            // A�adimos otro caracter al inicio
            listaNull.A�adirInicio('a');
            // A�adimos un numero en la pos 2
            listaNull.A�adir(1, 2);
            // Comprobar exactamente que nuestro toString muestra los null
            Assert.AreEqual("[a - null - 1 - null - null - null - null - a - ]", listaNull.ToString());
            // Borramos un null (va a ser el primero)
            listaNull.Borrar(null);
            // Comprobar exactamente que nuestro toString muestra los null
            Assert.AreEqual("[a - 1 - null - null - null - null - a - ]", listaNull.ToString());
            // Borramos un null (va a ser el primero)
            listaNull.Borrar(null);
            // Comprobar exactamente que nuestro toString muestra los null
            Assert.AreEqual("[a - 1 - null - null - null - a - ]", listaNull.ToString());
            // Borramos el ultimo caracter a
            listaNull.BorrarFinal('a');
            // Comprobar exactamente que nuestro toString muestra los null
            Assert.AreEqual("[a - 1 - null - null - null - ]", listaNull.ToString());
            // A�adimos al inicio un null
            listaNull.A�adirInicio(null);
            // Comprobar exactamente que nuestro toString muestra los null
            Assert.AreEqual("[null - a - 1 - null - null - null - ]", listaNull.ToString());
            // A�adimos el null de la posicion 2
            listaNull.A�adir(null, 2);
            // Comprobar exactamente que nuestro toString muestra los null
            Assert.AreEqual("[null - a - null - 1 - null - null - null - ]", listaNull.ToString());
            //// Borramos el ultimo null
            //listaNull.BorrarFinal(null);
            // Comprobar exactamente que nuestro toString muestra los null
            Assert.AreEqual("[null - a - null - 1 - null - null - null - ]", listaNull.ToString());
            // A�adimos un null 
            listaNull.A�adir(null);
            // Comprobar exactamente que nuestro toString muestra los null
            Assert.AreEqual("[null - a - null - 1 - null - null - null - null - ]", listaNull.ToString());
            // A�adimos al final un null
            listaNull.BorrarFinal(null);
            // Comprobar exactamente que nuestro toString muestra los null
            Assert.AreEqual("[null - a - null - 1 - null - null - null - ]", listaNull.ToString());
            // Comprobamos que el elemento de la posicion 2 es un null
            Assert.IsTrue(listaNull.GetElemento(2) == null);
            // Comprobamos que el primer elemento es un null
            Assert.IsTrue(listaNull.GetPosicion(null) == 0);



            /// PILA null ///
            /// 
            pila.Push(null);

            Assert.IsTrue(!pila.EstaVacia, "La pila esta vacia"); // Compruebo que la pila no esta vacia
            Assert.AreEqual(pila.Count(), 5); // Compruebo que el n� de elementos de la pila es 5

            Assert.IsTrue(pila.Peek() == null); // miro que el ultimo elemento sea null
            Assert.IsTrue(pila.Pop() == null); // borro el ultimo elemnto y veo que me devuelve null

            Assert.IsTrue(!pila.EstaVacia, "La pila esta vacia"); // Compruebo que la pila no esta vacia
            Assert.AreEqual(pila.Count(), 4); // Compruebo que el n� de elementos de la pila es 5

            //Vaciamos la pila
            pila.Clear();
            Assert.IsTrue(pila.EstaVacia, "La pila NO esta vacia"); // Compruebo que la pila SI esta vacia
            Assert.AreEqual(pila.Count(), 0); // Compruebo que el n� de elementos de la pila es 0
            //A�ado todo nullss
            pila.Push(null);
            pila.Push(null);
            pila.Push(null);
            pila.Push(null);
            pila.Push(null);
            Assert.IsTrue(pila.EstaLlena, "La NO esta llena"); // Compruebo que la pila esta llena
            Assert.AreEqual(pila.Count(), 5); // Compruebo que el n� de elementos de la pila es 5
        }

        ///////////////////////// Pruebas Pila //////////////////////////////
        ///

        [TestMethod]
        public void TestPush()
        {
            Assert.IsTrue(!pila.EstaVacia, "La pila esta vacia"); // Compruebo que la pila no esta vacia
            Assert.AreEqual(pila.Count(), 4); // Compruebo que el n� de elementos de la pila es 4
            pila.Push("German"); // A�adimos otro elemento
            Assert.IsTrue(!pila.EstaVacia, "La pila esta vacia"); // Compruebo que la pila no esta vacia
            Assert.AreEqual(pila.Count(), 5); // Compruebo que el n� de elementos de la pila es 5
            pila.Clear(); // Vaciamos la lista
            Assert.IsTrue(pila.EstaVacia, "La pila NO esta vacia"); // Compruebo que la pila SI esta vacia
            Assert.AreEqual(pila.Count(), 0); // Compruebo que el n� de elementos de la pila es 0
            pila.Push(1);
            pila.Push(1);
            Assert.IsTrue(!pila.EstaVacia, "La pila esta vacia"); // Compruebo que la pila no esta vacia
            Assert.AreEqual(pila.Count(), 2); // Compruebo que el n� de elementos de la pila es 2
            pila.Push("Hola");
            Assert.AreEqual("Hola", pila.Peek()); // Compruebo que el ultimo elemento de la pila es "hola"
            Assert.IsTrue(!pila.EstaVacia, "La pila esta vacia"); // Compruebo que la pila no esta vacia
            Assert.AreEqual(pila.Count(), 3); // Compruebo que el n� de elementos de la pila es 3
        }

        [TestMethod]
        public void TestPop()
        {
            Assert.IsTrue(!pila.EstaVacia, "La pila esta vacia"); // Compruebo que la pila no esta vacia
            Assert.AreEqual(pila.Count(), 4); // Compruebo que el n� de elementos de la pila es 4
            pila.Pop(); // Borramos el ultimo
            Assert.IsTrue(!pila.EstaVacia, "La pila esta vacia"); // Compruebo que la pila no esta vacia
            Assert.AreEqual(pila.Count(), 3); // Compruebo que el n� de elementos de la pila es 3
            Assert.AreEqual(pila.Peek(), "dos"); // Compruebo que el ultimo elemento es el "dos"
            pila.Pop(); // Borramos el ultimo
            pila.Pop(); // Borramos el ultimo
            Assert.IsTrue(!pila.EstaVacia, "La pila esta vacia"); // Compruebo que la pila no esta vacia
            Assert.AreEqual(pila.Count(), 1); // Compruebo que el n� de elementos de la pila es 1
            Assert.AreEqual(pila.Peek(), 'a'); // Compruebo que el ultimo elemento es el '0'
            pila.Pop(); // Borramos el ultimo
            Assert.IsTrue(pila.EstaVacia, "La pila NO esta vacia"); // Compruebo que la pila SI esta vacia
            Assert.AreEqual(pila.Count(), 0); // Compruebo que el n� de elementos de la pila es 0

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestArgumentExceptionPila()
        {
            pila.Push(3);
            // La pila ya esta llena y al a�adir un nuevo elemento debera dar una excepcion
            pila.Push(3);
            pila.Clear(); // Vacio la pila 
            // Al boorar de una pila vacia me debe dar una excepcion
            pila.Pop();

        }
    }
}
