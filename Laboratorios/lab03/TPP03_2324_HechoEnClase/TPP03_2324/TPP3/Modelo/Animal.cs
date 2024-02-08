using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    /// <summary>
    /// Clase abstracta. -> que tiene al menos un metodo abstracta. Las clases abstractas no se pueden implantar (instaciar). Seran sus clases derivadas
    /// ¿Para qué se utilizan?
    /// </summary>
    public abstract class Animal
    {
        public string Nombre { get; set; }
        public Animal(string nombre)
        {
            this.Nombre = nombre;
        }

        //¿Podemos crear métodos abstractos? ¿Y propiedades?
        //¿Qué implicaciones tiene en las clases derivadas?

        /// <summary>
        /// Habilitamos Enlace Dinámico, utilizamos virtual.
        /// </summary>
        public virtual void Saludar()
        {
            Console.WriteLine($"[ANIMAL] Mi nombre es {Nombre}.");
        }

        /// <summary>
        /// Método Mover.
        /// </summary>
        public void Mover()
        {
            Console.WriteLine($"[ANIMAL] {Nombre} se mueve.");
        }



    }
}
