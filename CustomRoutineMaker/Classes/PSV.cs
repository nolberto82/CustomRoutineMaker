using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CustomRoutineMaker.Classes;

internal class PSV
{
    public static string Initialize(uint addr, uint routine, string arch)
    {
        StringBuilder sb = new();

        sb.AppendLine($"//.psv");
        sb.AppendLine($".syntax unified");
        sb.AppendLine($".thumb");
        sb.AppendLine($".align 4");
        sb.AppendLine($".set patch,0");

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

    public static (List<string>, string) Run(byte[] data, uint addr, string asm, bool is64)
    {
        List<string> lines = [];
        List<byte> bytes = [];
        StringBuilder sb = new();
        int size = 2;
        uint baseaddr = asm.IndexOf(".aslr") == -1 ? 0x80000000 : 0;

        for (int i = 0; i < data.Length;)
        {
            if (i + size >= data.Length)
                break;

            int[] lo = new int[1];
            Buffer.BlockCopy(data, i, lo, 0, size);

            if (lo[0] != 0)
            {
                uint b = (uint)(addr < 0x1000000 ? baseaddr + 0x1000000 : baseaddr);
                if ((lo[0] & 0x1800) > 0)
                {
                    int[] hi = new int[1];
                    Buffer.BlockCopy(data, i + 2, hi, 0, size);
                    var v = hi[0] << 16 | lo[0];
                    sb.Append($"$A200 {b | addr & 0xfffffff:X8} " +
                        $"{v:X8}\n");
                    i += 2;
                    addr += 2;
                    bytes.Add((byte)lo[0]);
                    bytes.Add((byte)(lo[0] >> 8));
                    bytes.Add((byte)hi[0]);
                    bytes.Add((byte)(hi[0] >> 8));
                }
                else
                {
                    sb.Append($"$A100 {b | addr & 0xfffffff:X8} " +
                        $"{lo[0]:X8}\n");
                    bytes.Add((byte)lo[0]);
                    bytes.Add((byte)(lo[0] >> 8));
                }
            }

            i += size;
            addr += (uint)size;
        }


        var temp = sb.ToString().Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        temp.Add("");

        for (int i = 0; i < temp.Count; i++)
        {
            if (temp[i] == "" || temp[i].Length != 23)
                continue;

            var v1 = temp[i + 0].Replace(" ", "");
            lines.Add($"{v1[..5]} {v1.Substring(5, 8)} {v1.Substring(13, 8)}");
        }

        string strbytes = "";
        foreach (var b in bytes)
            strbytes += $"{b:X2} ";

        return (lines, strbytes);
    }

    public static string ConvertSegAddress(string addr)
    {
        string res = string.Empty;

        if (!string.IsNullOrEmpty(addr))
        {
            if (addr.All(char.IsDigit) || addr.ToUpper().Any("0123456789ABCDEF".Contains))
            {
                var s = addr.Split(["+", "-","\r\n"], StringSplitOptions.RemoveEmptyEntries);
                foreach (var x in s)
                {
                    var h = int.TryParse(x, System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var n);
                    if (h)
                        addr = addr.Replace(x, $"{n}");
                }
                DataTable dt = new();
                var c = addr.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
                foreach (var a in c)
                {
                    if (int.TryParse($"{dt.Compute(a, "")}", out var v))
                        res += $"{v:X}\r\n";
                }
            }
        }
        return res;
    }
}
