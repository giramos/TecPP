using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MaxValueApp
{
    internal class MaxValue
    {
        //the attribute
        int value;
        //a random number generator to complicate things
        Random r = new Random();
        public MaxValue(int value)
        {
            this.value = value;
        }
        public int Value
        {
            get
            {
                return value;
            }
            set
            {                
                if (value > this.value)
                {
                    //just to complicate things
                    Thread.Sleep(r.Next(50));
                    this.value = value;
                }
            }
        }
    }
}
