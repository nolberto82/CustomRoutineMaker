using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomRoutineMaker.Classes
{
    internal class SWI
    {
        public static string Initialize(uint addr, uint routine)
        {
            StringBuilder sb = new();

            //sb.AppendLine(@".create ""out.bin"", 0x" + addr.ToString("X8").PadLeft(8, '0'));
            sb.AppendLine(@".nds");
            sb.AppendLine(@".create ""out.bin"", 0x00000000");

            sb.AppendLine("\n");

            sb.AppendLine($".org\t0x00000000");
            sb.AppendLine($"bl\t0x00000000");
            sb.AppendLine();

            //    sb.AppendLine(@"bl" + "\t" + "0x" + routine.ToString("X8").PadLeft(8, '0'));

            sb.AppendLine("");

            sb.AppendLine($"//ecode:");
            sb.AppendLine($"//.dw\t 0xe0000000");
            sb.AppendLine($"//evalue:");
            sb.AppendLine($"//.dw\t 0x00000000\r\n");

            sb.AppendLine($".org\t0x{addr:X8}");

            sb.AppendLine("\n");
            sb.AppendLine("\n");
            sb.AppendLine("\n");

            sb.AppendLine($"bx\tlr");

            sb.AppendLine(".close");

            return sb.ToString();
        }

        public static List<string> Run(byte[] data, uint addr, string asm)
        {
            List<string> list = new();
            StringBuilder sb = new();

            for (int i = 0; i < data.Length / 4; i++)
            {
                int[] value = new int[1];
                Buffer.BlockCopy(data, i * 4, value, 0, 4);

                if (value[0] != 0)
                    sb.Append($"04000000 {addr & 0xffffff:X8} {value[0]:X8}\n");

                addr += 4;
            }

            bool isthumb = asm.IndexOf(".thumb") > -1 ? true : false;
            var lines = sb.ToString().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var bl = lines[lines.Count - 1];
            var bra = Convert.ToUInt32(lines[0].Substring(18, 8), 16);
            if ((bra & 0xff00) >= 0xf000 || (bra >> 24) == 0xeb)
            {
                bl = lines[0];
                lines.RemoveAt(0);
            }
            else
                lines.RemoveAt(lines.Count - 1);

            //lines = lines.Select(l => l.Substring(11, 10)).ToList();

            //if ((lines.Count % 2) > 0)
            //    lines.Add("0x00000000");

            //for (int i = 0; i < lines.Count; i += 2)
            //    list.Add($"{lines[i]} {lines[i + 1]}\r\n");

            lines.Add(bl);

            return lines;
        }
    }
}
