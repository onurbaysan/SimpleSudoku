using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sud0ku
{
    class DifficultyLevel
    {
        public enum levels { easy, normal, harder, veteran };
        private static levels level;

        public DifficultyLevel(levels lev)
        {
            level = lev;
        }

        public static levels getLevel() { return level; }
        
    }
}
