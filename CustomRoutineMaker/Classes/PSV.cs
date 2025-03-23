using System;
using System.Collections.Generic;
using System.Drawing;
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

    public static List<string> ConvertSegAddress(string addr)
    {
        List<string> res = new();
        List<int> numbers = new();
        string math = "";
        if (!string.IsNullOrEmpty(addr))
        {
            var d = addr.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            foreach (var s in d)
            {
                var number = "";
                for (int i = 0; i < s.Length; i++)
                {
                    char v = s[i];
                    switch (v)
                    {
                        case '+' or '-':
                            math = v.ToString();
                            numbers.Add(Convert.ToInt32(number, 16));
                            number = "";
                            break;
                        default:
                            number += v;
                            break;
                    }

                    if (number != "" && i == s.Length - 1)
                        numbers.Add(Convert.ToInt32(number, 16));
                }

                if (numbers.Count > 1)
                {
                    switch (math)
                    {
                        case "+": res.Add($"{numbers[0] + numbers[1] - 0x4000:X}"); break;
                        case "-": res.Add($"{numbers[0] - numbers[1] - 0x4000:X}"); break;
                    }
                }
                else if (numbers.Count == 1)
                {
                    res.Add($"{numbers[0] - 0x4000:X}");
                }
                numbers.Clear();
            }
        }
        return res;
    }
}
