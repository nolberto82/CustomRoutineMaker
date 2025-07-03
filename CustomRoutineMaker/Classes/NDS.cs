using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomRoutineMaker.Classes;

internal class NDS
{
    public static string Initialize(uint addr, uint routine, SystemType sys)
    {
        StringBuilder sb = new();

        //sb.AppendLine(@".create ""out.bin"", 0x" + addr.ToString("X8").PadLeft(8, '0'));
        sb.AppendLine($".{sys.shortname}");
        sb.AppendLine(@".create ""out.bin"", 0x00000000");

        sb.AppendLine("\n");

        sb.AppendLine($".org\t0x{sys.origaddr:X8}");
        sb.AppendLine($"bl\t0x{sys.routine:X8}");
        sb.AppendLine();

        //    sb.AppendLine(@"bl" + "\t" + "0x" + routine.ToString("X8").PadLeft(8, '0'));

        sb.AppendLine("");

        sb.AppendLine($"//ecode:");
        sb.AppendLine($"//.dw\t 0xe0000000");
        sb.AppendLine($"//evalue:");
        sb.AppendLine($"//.dw\t 0x00000000\r\n");

        sb.AppendLine($".org\t0x{sys.routine:X8}");

        sb.AppendLine("\n");
        sb.AppendLine("\n");
        sb.AppendLine("\n");

        sb.AppendLine($"bx\tlr");

        sb.AppendLine(".close");

        return sb.ToString();
    }

    public static List<string> Run(byte[] data, uint addr, string asm)
    {
        List<string> list = [];
        StringBuilder sb = new();
        uint function = addr;

        for (int i = 0; i < data.Length; i += 4)
        {
            int[] value = new int[1];
            Buffer.BlockCopy(data, i, value, 0, 4);

            if (value[0] != 0)
                sb.AppendLine($"{i & 0xfffffff:X8} {value[0]:X8}");

            addr += 4;
        }

        bool isthumb = asm.IndexOf(".thumb") > -1 ? true : false;
        var lines = sb.ToString().Replace(" ", "").Split([Environment.NewLine], StringSplitOptions.RemoveEmptyEntries).ToList();

        List<string> bl = [];
        for (int h = lines.Count - 1; h >= 0; h--)
        {
            if (lines[h].Substring(8, 2) == "EB")
            {
                bl.Add(lines[h].Insert(8, " "));
                lines.RemoveAt(h);
            }
        }

        lines = [.. lines.Select(l => l.Substring(8, 8))];

        if ((lines.Count % 2) > 0)
            lines.Add("00000000");

        for (int i = 0; i < lines.Count; i += 2)
            list.Add($"{lines[i]} {lines[i + 1]}");

        int index = asm.IndexOf("//ecode:");
        int eaddr = 0;
        int evalue = 0;
        if (index > 0)
        {
            while (asm[index] != 'x')
                index++;

            eaddr = Convert.ToInt32(asm.Substring(index + 1, 8), 16);

            index = asm.IndexOf("//evalue:");

            while (asm[index] != 'x')
                index++;

            evalue = Convert.ToInt32(asm.Substring(index + 1, 8), 16);
        }

        list.Add($"5{bl[0].Substring(1,7):X8} {evalue:X8}");
        list.AddRange(bl);
        list.Insert(0, $"{eaddr:X8} {lines.Count * 4:X8}");
        list.Add($"D2000000 00000000");

        return list;
    }
}
