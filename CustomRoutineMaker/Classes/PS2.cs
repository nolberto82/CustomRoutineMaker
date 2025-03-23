using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CustomRoutineMaker.Classes;

internal class PS2
{
    public static string Initialize(uint addr, uint routine)
    {
        StringBuilder sb = new();

        //sb.AppendLine(@".create ""out.bin"", 0x" + addr.ToString("X8").PadLeft(8, '0'));
        sb.AppendLine(@".ps2");
        sb.AppendLine(@".create ""out.bin"", 0x20000000");

        sb.AppendLine("\n");

        sb.AppendLine($".org\t0x20000000");
        sb.AppendLine($"j\t0x200A0000");

        sb.AppendLine("");

        sb.AppendLine($"//ecode:");
        sb.AppendLine($"//.dw\t0xe0000000");
        sb.AppendLine($"//evalue:");
        sb.AppendLine($"//.dw\t0x00000000\r\n");

        sb.AppendLine($".org\t0x200A0000");

        sb.AppendLine("\n");
        sb.AppendLine("\n");
        sb.AppendLine("\n");

        sb.AppendLine($"j\t0x200A0008");

        sb.AppendLine(".close");

        return sb.ToString();
    }

    public static List<string> Run(byte[] data, uint addr, string asm)
    {
        List<string> list = [];
        StringBuilder[] sb = new StringBuilder[2];
        const int entry = 0x20000000;
        sb[0] = new();
        sb[1] = new();
        int linesnum = 0;

        sb[0].Append("//PCSX2\r\n");
        sb[1].Append("//PS2\r\n");

        for (int i = 0; i < data.Length / 4; i++)
        {
            int[] value = new int[1];
            Buffer.BlockCopy(data, i * 4, value, 0, 4);

            if (value[0] != 0 && (value[0] & 0xffff0000) != 0xe0000000)
            {
                sb[0].Append($"patch=1,EE,{entry | addr:X8},extended,{value[0]:X8}\r\n");
                sb[1].Append($"{entry | addr:X8} {value[0]:X8}\r\n");
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

            uint evalue = System.Convert.ToUInt32(asm.Substring(index + 1, 8), 16);

            if (index > -1 && evalue > 0)
            {
                eaddr = eaddr.Remove(2, 2).Insert(2, $"{linesnum:X2}");
                sb[0].Insert(0, $"patch=1,EE,{eaddr.ToUpper()} " +
                    $"{evalue:X8}\r\n");
                sb[1].Insert(0, $"{eaddr.ToUpper()} " +
                    $"{evalue:X8}\r\n");
            }
        }

        list.AddRange(sb[0].ToString().Split(Environment.NewLine));
        list.AddRange(sb[1].ToString().Split(Environment.NewLine));

        return list;
    }

    public static List<string> ConvertToPnachFormat(string[] lines)
    {
        List<string> list = [];

        if (lines.Length == 0 || lines[0] == "" || lines == null)
            return list;

        for (int i = 0; i < lines.Length; i++)
        {
            string ns = lines[i].Trim().Replace(" ", "");
            if (lines[i].StartsWith('\r') || lines[i].StartsWith("\r\n"))
            {
                list.Add(" ");
                continue;
            }
            else if (ns.Length < 16)
            {
                list.Add(" ");
                continue;
            }
            if (!ns.All("0123456789abcdefABCDEF".Contains))
            {
                list.Add($"//{lines[i].Replace("\r", "")}");
                continue;
            }
            list.Add($"patch=1,EE,{ns.Substring(0, 8):X4},extended,{ns.Substring(8, 8):X8}");
        }

        return list;
    }
}
