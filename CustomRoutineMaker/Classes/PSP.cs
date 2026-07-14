using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace CustomRoutineMaker.Classes;

internal class PSP
{
    public static string Initialize()
    {
        return Common.InitiatizeMips(".psp", "0x08800000", "0x08800000", "0x08801000").ToString();
    }

    public static List<string> Run(byte[] data, uint addr, string asm)
    {
        List<string> list = [];
        StringBuilder[] sb = [new(), new()];
        int linesnum = 0;

        for (int i = 0; i < data.Length / 4; i++)
        {
            int[] value = new int[1];
            Buffer.BlockCopy(data, i * 4, value, 0, 4);

            if (value[0] != 0 && (value[0] & 0xffff0000) != 0xe0000000)
            {
                sb[0].Append($"_L 0x{addr + 0x20000000:X8} 0x{value[0]:X8}\r\n");
                sb[1].Append($"0x{addr + 0x20000000:X8} 0x{value[0]:X8}\r\n");
                linesnum++;
            }
            addr += 4;
        }

        (uint, uint) conditional = Common.GetConditional(asm);
        if (conditional != (0, 0))
        {
            sb[0].Insert(0, $"_L {conditional.Item1:X8} 0x{conditional.Item2:X8}\r\n");
            sb[1].Insert(0, $"{conditional.Item1:X8} {conditional.Item2:X8}\r\n");
        }

        list.AddRange(sb[0].ToString().Split(Environment.NewLine));
        list.AddRange(sb[1].ToString().Split(Environment.NewLine));

        return list;
    }

    public static List<string> ConvertToGHFormat(string[] lines)
    {
        List<string> list = [];
        List<string> pspaddrs = [];

        if (lines.Length == 0 || lines[0] == "" || lines == null)
            return list;

        bool cwcheat = lines[0].Contains("_L");

        for (int i = 0; i < lines.Length; i++)
        {
            string ns = lines[i].ReplaceLineEndings().Replace(" ", "");
            ns = new string([.. ns.Where(c => char.IsLetterOrDigit(c))]).Replace("0x", "");

            if (!ns.StartsWith('C') && !ns.StartsWith('L') && !ns.All(char.IsAsciiHexDigit))
            {
                list.Add($"_C0 {lines[i].ReplaceLineEndings("")}");
                pspaddrs.Add($"{lines[i].ReplaceLineEndings("")}");
                continue;
            }

            if (lines[i].Length < 16 || lines[i].StartsWith('\r') || lines[i].StartsWith("\r\n"))
            {
                list.Add("");
                pspaddrs.Add("");
                continue;
            }

            if (lines[i][..2] == "_C")
            {
                list.Add(lines[i][3..].TrimStart().Replace("\r", ""));
                pspaddrs.Add(lines[i][3..].TrimStart().Replace("\r", ""));
            }
            else
            {
                lines[i] = lines[i].ToUpper().Replace("X", "x").Replace("\r", "").Replace("_L ", "");
                var a = lines[i].TrimStart().Split(" ");
                if (a[0].Length != 8)
                    continue;

                var v = Convert.ToInt32(a[0], 16);
                var type = (v >> 24) & 0xff;
                if (type >= 8)
                {
                    lines[i] = $"_L 0x{(type >= 8 && type < 13 ? v - 0x08800000 | 0x20000000 : v):X8} ";
                    if (a[1].Contains("0x"))
                        lines[i] += $"{a[1]}";
                    else
                        lines[i] += $"0x{a[1]}";
                }
                else if (lines[i][..2] == "_L")
                    lines[i] = $"_L 0x{lines[i][..8]} 0x{lines[i][9..]}";

                pspaddrs.Add($"{lines[i].Replace("_L ", "")}");

                if (cwcheat)
                    list.Add($"{lines[i]}");
                else
                    list.Add($"{lines[i]}");
            }
        }

        list.Add("\n");
        list.AddRange(pspaddrs);

        return list;
    }
}
