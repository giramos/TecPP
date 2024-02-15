
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratorio04
{
    public static class Algorithms
    {

        static public void Sort<T>(T[] vector) where T : IComparable<T>
        {
            for (int i = 0; i < vector.Length; i++)
                for (int j = vector.Length - 1; j > i; j--)
                    if (vector[i].CompareTo(vector[j]) > 0)
                    {
                        T aux = vector[i];
                        vector[i] = vector[j];
                        vector[j] = aux;
                    }
        }

        static public T Max<T>(T[] array) where T : IComparable<T>
        {
            T grande = array[0]; // al principio decidimos que el grande sea la pos 0
            for(int i = 1; i < array.Length; i++) // Empezamos en la pos 1 
            {
                if (array[i].CompareTo(grande) > 0)
                {
                    grande = array[i];
                }
            }
            return grande;
        }

    }
}
