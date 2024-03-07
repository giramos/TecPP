using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        IEnumerable<Persona> personas = Factoria.CrearPersonas();
        IEnumerable<Angulo> angulos = Factoria.CrearAngulos();

        [TestMethod]
        public void TestBuscar()
        {
            var marias = personas.FirstOrDefault(p => p.Nombre.Equals("María"));
            Assert.IsNotNull(marias);
            Console.Write(marias.ToString());

            var dni = personas.FirstOrDefault(d => d.Nif.EndsWith('T'));
            Assert.IsNotNull(dni);
            Console.WriteLine(dni.ToString());
            
        }

        [TestMethod]
        public void TestFiltrar()
        {
            var maria = personas.Select(p => p.Nombre.Equals("María"));
            foreach(var i in maria)
            {
                Assert.IsNotNull(i);
                Console.WriteLine(i.ToString());
            }  
            
            var nif = personas.Select(p => p.Nif.EndsWith("T"));
            foreach(var i in nif)
            {
                Assert.IsNotNull(i);
                Console.WriteLine(i.ToString());
            }
        }
    }
}
