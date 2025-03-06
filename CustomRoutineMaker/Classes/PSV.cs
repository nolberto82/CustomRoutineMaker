using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomRoutineMaker.Classes;

internal class PSV
{
    public static string Initialize(uint addr, uint routine, string arch)
    {
        StringBuilder sb = new();

        sb.AppendLine($"//.psv");
        sb.AppendLine($".thumb");

        sb.AppendLine("");

        sb.AppendLine($".org\t0x00000000");
        sb.AppendLine($"b\tmain");
        sb.AppendLine($"return:");
        sb.AppendLine();

        sb.AppendLine("");

        sb.AppendLine($"//ecode:");
        sb.AppendLine($"//.dw\t 0xe0000000");
        sb.AppendLine($"//evalue:");
        sb.AppendLine($"//.dw\t 0x00000000\r\n");

        sb.AppendLine($".org\t0x00000000");
        sb.AppendLine($"main:");

        sb.AppendLine("\n");
        sb.AppendLine("\n");
        sb.AppendLine("\n");

        sb.AppendLine($"b\treturn\n");

        return sb.ToString();
    }

    public static List<string> Run(byte[] data, uint addr, string asm, bool is64)
    {
        List<string> list = new();
        StringBuilder sb = new();

        int size = 4;
        for (int i = 0; i < data.Length;)
        {
            if (i + 4 > data.Length)
                break;

            int[] lo = new int[1];

            Buffer.BlockCopy(data, i, lo, 0, size);

            if (lo[0] != 0)
            {
                var v = lo[0] >> 16 | lo[0] << 16;

                if (v < 0x10000)
                {
                    addr += 2;
                    i += 2;
                    sb.Append($"$A200 {0x80000000 | addr & 0xfffffff:X8} " +
                        $"{v:X8}\n");
                    continue;
                }
                else
                {
                    i += size;
                    addr += (uint)size;
                    sb.Append($"$A200 {0x80000000 | addr & 0xfffffff:X8} " +
                        $"{v:X8}\n");
                    continue;
                }
            }

            i += size;
            addr += (uint)size;
        }

        bool isthumb = asm.IndexOf(".thumb") > -1 ? true : false;
        var temp = sb.ToString().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        List<string> bl = new();
        for (int k = temp.Count - 1; k >= 0; k--)
        {
            var bra = Convert.ToUInt32(temp[k].Substring(15, 8), 16) >> 24;
            if ((!is64 && (bra == 0xeb) || (bra == 0x14 || bra == 0x15 || bra == 0x94 || (bra == 0x95))))
            {
                bl.Add(temp[k]);
                temp.RemoveAt(k);
            }
        }

        temp.Add("");

        List<string> lines = new();
        List<string> values = new();
        List<string> bytes = new();

        for (int k = 0; k < bl.Count; k++)
        {
            bl[k] = bl[k].Replace(" ", "");
            lines.Add($"04000000 {bl[k].Substring(8, 8)} {bl[k].Substring(16, 8)}");
        }

        for (int i = 0; i < temp.Count; i++)
        {
            if (temp[i] == "")
                continue;

            var v1 = temp[i + 0].Replace(" ", "");

            lines.Add($"{v1.Substring(0, 5)} {v1.Substring(5, 8)} {v1.Substring(13, 8)}");
        }
        return lines;
    }
}
