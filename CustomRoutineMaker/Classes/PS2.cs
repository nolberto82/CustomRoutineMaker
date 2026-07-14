using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CustomRoutineMaker.Classes;

internal class PS2
{
    public static string Initialize()
    {
        return Common.InitiatizeMips(".ps2", "0x20000000", "0x20000000", "0x200a0000").ToString();
    }

    public static List<string> Run(byte[] data, uint addr, string asm)
    {
        List<string> list = [];
        StringBuilder[] sb = new StringBuilder[2];
        const int entry = 0x20000000;
        sb[0] = new();
        sb[1] = new();
        int linesnum = 0;

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

        (uint, uint) conditional = Common.GetConditional(asm);
        if (conditional != (0, 0))
        {
            sb[0].Insert(0, $"patch=1,EE,{conditional.Item1:X8},extended,{conditional.Item2:X8}\r\n");
            sb[1].Insert(0, $"{conditional.Item1:X8} {conditional.Item2:X8}\r\n");
        }

        sb[0].Insert(0, "//PCSX2\r\n");
        sb[1].Insert(0, "//PS2\r\n");

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
            string ns = lines[i].ReplaceLineEndings().Replace("patch=1,EE,", "").
               Replace(",extended,", "").Replace(" ", "");
            ns = new string([.. ns.Where(c => char.IsLetterOrDigit(c))]);
            if (ns.StartsWith('\r') || ns.StartsWith("\r\n"))
                list.Add(" ");
            else if (ns.All(char.IsAsciiHexDigit))
            {
                if (ns.Length < 16)
                {
                    list.Add(" ");
                    continue;
                }
                if (lines[i].StartsWith("patch"))
                    list.Add($"{ns[..8]:X4} {ns.Substring(8, 8):X8}");
                else
                    list.Add($"patch=1,EE,{ns[..8]:X4},extended,{ns.Substring(8, 8):X8}");
            }
        }

        return list;
    }

    public static bool IsHex(string s)
    {
        for (int i = 0; i < s.Length; i++)
        {
            char c = s[i];
            if (!char.IsAsciiHexDigitUpper(c))
                return false;
        }

        return true;
    }
}
