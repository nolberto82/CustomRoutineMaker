using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomRoutineMaker
{
    internal class SystemType
    {
        public string name;
        public string shortname;
        public uint routine;
        public uint origaddr;

        public SystemType(string name, string shortname, uint routine, uint origaddr)
        {
            this.name = name;
            this.shortname = shortname;
            this.routine = routine;
            this.origaddr = origaddr;
        }
    }
}
