using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomRoutineMaker
{
    internal class CodeLine
    {
        public int addr;
        public int value;

        public CodeLine(int addr, int value)
        {
            this.addr = addr;
            this.value = value;
        }
    }
}
