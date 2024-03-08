using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Number_guessing_game
{
    public class Base
    {
        public int AreEqual { get; internal set; }
    }

    public class Derived : Base
    {
        public void TestInt()
        {
            int i = base.AreEqual; // CS0117
        }
    }
}
