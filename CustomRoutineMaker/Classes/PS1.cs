using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomRoutineMaker.Classes
{
    internal class PS1
    {
        public static string Initialize(uint addr, uint routine)
        {
            StringBuilder sb = new();

            sb.AppendLine(@".psx");
            sb.AppendLine(@".create ""out.bin"", 0x00000000");
            sb.AppendLine(@".definelabel hook, 0x80000000");
            sb.AppendLine(@".definelabel function, 0x80007600");

            sb.AppendLine("\n");

            sb.AppendLine($".org\t hook");
            sb.AppendLine($"j\t function");

            sb.AppendLine("");

            sb.AppendLine($"//ecode:");
            sb.AppendLine($"//.dw\t 0xd0000000");
            sb.AppendLine($"//evalue:");
            sb.AppendLine($"//.dh\t 0x0000\r\n");

            sb.AppendLine($".org\t function");

            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");

            sb.AppendLine($"j\t hook+8");

            sb.AppendLine(".close");

            return sb.ToString();
        }

        public static List<string> Run(byte[] data, uint addr, string asm)
        {
            List<string> list = new();
            StringBuilder sb = new();
            sb = new();
            int linesnum = 0;
            int size = 2;

            for (int i = 0; i < data.Length / 4; i++)
            {
                int[] value = new int[1];
                Buffer.BlockCopy(data, i * 4, value, 0, 4);

                if (value[0] != 0 && (value[0] & 0xffff0000) != 0xe0000000)
                {
                    var a = 0x80000000 | addr;
                    sb.Append($"{a + 0:X8} {value[0] & 0xffff:X4}\r\n");
                    sb.Append($"{a + 2:X8} {(value[0] & 0xffff0000) >> 16:X4}\r\n");
                    linesnum++;
                }
                addr += 4;
            }

            int index = asm.IndexOf("//ecode:");

            if (index > 0)
            {
                while (asm[index] != 'x')
                    index++;

                string eaddr = asm.Substring(index + 1, 8);
                index = asm.IndexOf("//evalue:");

                while (asm[index] != 'x')
                    index++;

                uint evalue = System.Convert.ToUInt32(asm.Substring(index + 1, 4), 16);

                if (index > -1 && evalue > 0)
                {
                    eaddr = eaddr.Remove(2, 2).Insert(2, $"{linesnum:X2}");
                    sb.Insert(0, $"patch=1,EE,{eaddr.ToUpper()} " +
                        $"{evalue:X8}\r\n");
                    sb.Insert(0, $"{eaddr.ToUpper()} " +
                        $"{evalue:X8}\r\n");
                }
            }

            list.AddRange(sb.ToString().Split(Environment.NewLine));

            return list;
        }
    }
}
