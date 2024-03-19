using ListaGenerica;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        Persona[] personas; Angulo[] angulos;
        Lista<Persona> p; Lista<Angulo> a;

        [TestInitialize]
        public void Init()
        {
            personas = Factoria.CrearPersonas();
            angulos = Factoria.CrearAngulos();
            p = new Lista<Persona>();
            a = new Lista<Angulo>();
            foreach (Persona persona in personas)
            {
                p.Añadir(persona);
            }
            foreach(Angulo angulo in angulos)
            {
                a.Añadir(angulo);
            }
        }

        [TestMethod]
        public void TestBuscar()
        {
            var res = p.Buscar(p => p.Nombre.ToLower().Equals("juan"));
            
            Assert.AreEqual(res, personas[1]);

             res = p.Buscar(p => p.Nombre.ToLower().Equals("juan") && p.Nif.EndsWith('A'));

            Assert.AreEqual(res, personas[12]);
        }

        [TestMethod]
        public void TestFiltrar()
        {
            var res = p.Filtrar(p => p.Nombre.ToLower().Equals("juan"));
            var personas1 = res.ToArray();
            Assert.AreEqual(personas1[1], personas[12]);

            res = p.Filtrar(p => p.Nombre.ToLower().Equals("juan") && p.Nif.EndsWith('F'));
            personas1 = res.ToArray();
            Assert.AreEqual(personas1[0], personas[1]);

        }

        [TestMethod]
        public void TestReduce()
        {
            var res = p.Reduce(new Dictionary<string, int>(), (dicc, p) =>
            {
                if (dicc.ContainsKey(p.Nombre))
                {
                    dicc[p.Nombre]++;
                }
                else
                {
                    dicc[p.Nombre] = 1;
                }
                return dicc;
            });
            int valor = 2;
            Assert.AreEqual(res.TryGetValue("María", out valor), true );
        }

        [TestMethod]
        public void TestSelect()
        {
            var res = p.Map(e => $"{e.Nombre} : {e.Apellido}");
            res.ForEach(i => Console.WriteLine(i));
        }

        [TestMethod]
        public void TestInvertir()
        {
            var num = Enumerable.Range(0, 11);
            num.ForEach(n => Console.WriteLine(n));
            var inver = num.Invertir();
            inver.ForEach(n => Console.WriteLine(n));
        }
    }
}
