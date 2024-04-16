using ColaConcurrente;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Concurrent;
using System.Linq;
using System.Xml.Schema;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        Cola<object> micola = new();
        ConcurrentQueue<object> queue = new();
        [TestInitialize]
        public void TestInit()
        {
            micola.Añadir("hola");
            micola.Añadir(1);
            micola.Añadir('a');

            queue.Enqueue("hola");
            queue.Enqueue(1);
            queue.Enqueue('a');
        }

        [TestMethod]
        public void TestExtraer()
        {
            object hola = "hola";
            object uno = 1;
            object a = 'a';
            
            Assert.AreEqual(micola.Extraer(),"hola");
            Assert.AreEqual(queue.TryDequeue(out hola),true);
            Assert.AreEqual(micola.Count(), queue.Count());
            
            Assert.AreEqual(micola.Extraer(),1);
            Assert.AreEqual(queue.TryDequeue(out uno),true);
            Assert.AreEqual(micola.Count(), queue.Count()); 
            
            Assert.AreEqual(micola.Extraer(),'a');
            Assert.AreEqual(queue.TryDequeue(out a),true);
            Assert.AreEqual(micola.Count(), queue.Count());
        }

        [TestMethod]
        public void TestAñadir()
        {
            micola.Añadir(2);
            Assert.AreEqual(micola.Count(), 4);
            Assert.AreEqual(micola.EstaVacia, false);
            Assert.AreEqual(micola.PrimerElemento(), "hola");
            
            queue.Enqueue(2);
            Assert.AreEqual(queue.Count(), 4);
            Assert.AreEqual(queue.IsEmpty, false);
            Assert.AreEqual(queue.First(), "hola");

        }
    }
}
