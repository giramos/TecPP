using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Colecciones
{
    [TestClass]
    public class UnitTest1
    {
        IList<object> lista = new List<object>();
        IDictionary<int, string> dic1 = new Dictionary<int, string>();
        IDictionary<char, List<int>> dic2 = new Dictionary<char, List<int>>();

        [TestInitialize]
        public void TestInicial()
        {
            lista.Add(1);
            lista.Add(2.0);
            lista.Add('3');
            lista.Add("cuatro");

            dic1.Add(0, "cero");
            dic1.Add(1, "uno");
            dic1.Add(2, "dos");
            dic1.Add(3, "tres");

            dic2.Add('a', new List<int>());
            dic2.Add('b', new List<int>());
            dic2.Add('c', new List<int>());
            dic2.Add('d', new List<int>());
        }
        [TestMethod]
        public void TestMethodList()
        {
            lista.Add(5);
            lista.Add(lista.Count);// Añadiremos el 5 otra vez = n2 elementos actual
            Assert.IsTrue(lista.Count.Equals(6));
            Assert.IsTrue(lista[5].Equals(5));
            Assert.IsTrue(lista[4].Equals(5));
            Assert.IsTrue(lista[3].Equals("cuatro"));
            Assert.IsTrue(lista[2].Equals('3'));
            Assert.IsTrue(lista[1].Equals(2.0));
            Assert.IsTrue(lista[0].Equals(1));
            // Conocemos el nº de elementos tras añadir tres elementos == 9 elementos
            lista.Add("german");
            lista.Add("iglesias");
            lista.Add("ramos");
            Assert.IsTrue(lista.Count.Equals(9));
            lista[0] = 0; // Reescribimos el elementos de la pos 0
            var obetener = lista.IndexOf(0);
            Assert.IsTrue(obetener == 0); // Comprobamos que el elemento obetener esta en el indice 0
            Assert.IsFalse(lista.Contains(1)); // Comprobamos que ya no existe el uno en nuestra lista

            // Conocer el primer indice
            lista.Remove(lista[5]);
            Assert.IsTrue(lista.Contains(5));
            lista.Remove(5);
            Assert.IsFalse(lista.Contains(5));
            // Recorremos con un foreach
            foreach (var i in lista)
            {
                Console.Write(i + " ");
            }

            IList<int> vector = new List<int> { 1, 2, 3, 2 };
            vector.Remove(2);
            Assert.AreEqual(3, vector.Count);
            Assert.IsTrue(vector.Contains(2));
        }

        [TestMethod]
        public void TestMethodDicc()
        {
            dic1.Add(4,"cero");
            foreach(var i in dic1)
            {
                Console.WriteLine("Clave: "+i.Key + " Valor: " + i.Value);
            }
            // No se puede añadir con una misma clave
            //dic1.Add(0, "cinco");

            // Podemos modificar la clave valor 
            dic1[0] = "uno";
            Assert.AreEqual(dic1[0], "uno");

            // Cuantos nº pares del diccionario hay
            var cuantos = dic1.Count;
            Assert.AreEqual(cuantos, 5);

            dic1[cuantos - 1] = "doce";
            foreach (var i in dic1)
            {
                Console.WriteLine("Clave: " + i.Key + " Valor: " + i.Value);
            }

            foreach(var i in dic1)
            {
                if(i.Key > 0 && i.Key < 3)
                {
                    Console.WriteLine("Valores 1ª hornada: " + i.Value);
                }
                else
                {
                    Console.WriteLine("Valores 2ª hornada: " + i.Value);
                }
            }

            Assert.IsTrue(dic1.ContainsKey(4));
            Assert.IsTrue(dic1.ContainsKey(3));
            Assert.IsTrue(dic1.ContainsKey(2));
            Assert.IsTrue(dic1.ContainsKey(1));
            Assert.IsTrue(dic1.ContainsKey(0));
            Assert.IsFalse(dic1.ContainsKey(5));

        }
    }
}
