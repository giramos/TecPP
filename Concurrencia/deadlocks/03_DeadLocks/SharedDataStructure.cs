using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_DeadLocks
{
    public class SharedDataStructure
    {
        public List<int> SharedData { get; set; }
        public static readonly object ob = new();
        public SharedDataStructure()
        {
            SharedData = new List<int>();
        }
        public void Add(int n)
        {
            lock (ob)
            {
                SharedData.Add(n);
            }
            //this.SharedData.Add(n);
        }

        public override string ToString()
        {
            return string.Format("SharedDataStructure has {0} items in the list {1}", this.SharedData.Count, this.SharedData);
        }

    }
}
