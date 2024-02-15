using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio04
{
    public class Pair<T> : IComparable<Pair<T>> where T : IComparable<T>
    {
        T compA;
        T compB;

        public Pair()
        {
            compA = default(T);
            compB = default(T);
        }

        public Pair(T cA, T cB)
        {
            compA = cA;
            compB = cB;
        }

        public int CompareTo(Pair<T> other)
        {
            if (this.compA.CompareTo(other.compA) < 0)
                return -1;
            else if (this.compA.CompareTo(other.compA) > 0)
                return 1;
            else if (this.compA.CompareTo(other.compA) == 0)
            {
                if (this.compB.CompareTo(other.compB) < 0)
                    return -1;
                else if (this.compB.CompareTo(other.compB) > 0)
                    return 1;
                else
                    return 0;
            }
            else
                return 0;
        }

        public override string ToString()
        {
            return $"Par => [{compA.ToString()},{compB.ToString()}]";
        }
    }
}
