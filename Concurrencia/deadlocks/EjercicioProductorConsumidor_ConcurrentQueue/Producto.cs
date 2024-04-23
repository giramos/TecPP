using System;
using System.Collections.Generic;
using System.Text;

namespace EjercicioProductorConsumidor
{
    class Producto
    {
        public int ProductoID { get; private set; }

        public Producto(int productoID)
        {
            this.ProductoID = productoID;
        }

        public override string ToString()
        {
            return String.Format("Producto {0}", this.ProductoID);
        }
    }
}
