using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    [Serializable]
    class RandomHelper
    {
        public static Int32 Next(Int32 max)
        {
            if (r == null)
            {
                r = new Random();
            }
            return r.Next(max);
        }
        [NonSerialized]
        private static Random r = new Random();
    }
}
