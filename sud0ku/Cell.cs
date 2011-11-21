using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sud0ku
{
    class Cell
    {
        public int row { get; set; }
        public int column { get; set; }
        public int value { get; set; }
        public HashSet<int> possibilities = new HashSet<int>();
        public int recursiveDepth { get; set; }
    }
}
