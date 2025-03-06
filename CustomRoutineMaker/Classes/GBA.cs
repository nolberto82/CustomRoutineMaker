using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TreeView;

namespace CustomRoutineMaker.Classes;

internal class GBA
{
    public static string Initialize(uint addr, uint routine)
    {
        StringBuilder sb = new();

        //sb.AppendLine(@".create ""out.bin"", 0x" + addr.ToString("X8").PadLeft(8, '0'));
        sb.AppendLine(@".gba");
        sb.AppendLine(@".create ""out.bin"", 0x00000000");
        sb.AppendLine(@".definelabel branch,0x08000000");
        sb.AppendLine(@".definelabel function,0x0203ff00");

        sb.AppendLine("\n");

        sb.AppendLine($".org\tbranch");
        sb.AppendLine($"ldr\tr0,=function+1\r\nbx\tr0\r\n.pool\r\n");

        sb.AppendLine($".org\tfunction\r\n");
        sb.AppendLine($"ldr\tr0,=branch+9");
        sb.AppendLine($"push\t{{r0}}\r\n\r\n\r\n");

        sb.AppendLine($"pop\t{{pc}}\r\n");

        sb.AppendLine(".pool\r\n.close");

        return sb.ToString();
    }

    public static (List<string>, List<string>, List<string>) Run(byte[] data, uint addr, string asm)
    {
        AR34 AR34 = new();
        List<string> list = new();
        List<string> unlist = new();
        List<string> bytes = new();
        StringBuilder sb = new();
        int id = 0xc;
        bool is_arm = asm.IndexOf(".arm") > -1 ? true : false;
        bool branch = false;

        for (int i = (int)addr; i < data.Length;)
        {
            int codetype = (int)(addr >> 24);
            int step = codetype == 2 ? 4 : 2;

            int[] value = new int[1];
            Buffer.BlockCopy(data, i, value, 0, step);

            if (value[0] != 0)
            {
                string lines = $"{addr:X8} {value[0]:X8}";

                string[] words = GetWords(lines);

                if (words == null || words.Length == 0)
                    continue;

                var enc = AR34.Encode(lines, words, ref id);
                var code = AR34.Encrypt(enc.ToArray());
                foreach (string st in code)
                    list.Add(st);

                //for (int j = 0; j < enc.Count; j += 2)
                    unlist.Add($"patch_dword(0x{addr:X8},0x{value[0]:X8})");
                //unlist.Add($"{enc[j]} {enc[j + 1]}");

                if (codetype == 8 && !branch)
                {
                    bytes.Add($"\r\n");
                    branch = true;
                }

                for (int j = 0; j < step; j++)
                    bytes.Add($"{data[i + j]:X2} ");

            }

            addr += (uint)step;
            i += step;
        }

        return (list, unlist, bytes);
    }

    public static List<string> ConvertToARFormat(string[] lines)
    {
        AR34 AR34 = new();
        List<string> list = new();
        int id = 0xc;

        if (lines.Length == 0 || lines[0] == "" || lines == null)
            return list;

        for (int i = 0; i < lines.Length; i++)
        {
            string s = lines[i].Trim().Replace(" ", "");

            if (s.Length < 12)
            {
                //list.Add($"{""}");
                //continue;
                s = s.PadLeft(12, '0');
            }

            string[] words = GetWords(s);

            if (words == null || words.Length == 0)
                continue;

            var enc = AR34.Encode(s, words, ref id);
            var code = AR34.Encrypt(enc.ToArray());
            foreach (string st in code)
                list.Add(st);
        }

        return list;
    }

    public static List<string> ConvertToRawFormat(string[] lines)
    {
        AR34 AR34 = new();
        List<string> list = new();
        int id = 0xc;

        if (lines.Length == 0 || lines[0] == "" || lines == null)
            return list;

        for (int i = 0; i < lines.Length; i++)
        {
            string s = lines[i].Trim();

            if (lines[i].Trim().Replace(" ", "").Length < 16)
            {
                list.Add($"{""}");
                continue;
            }

            string[] words = s.Split(" ");

            var enc = AR34.Decode(s, words, ref id);
            var code = AR34.Decrypt(enc.ToArray());
            foreach (string st in code)
                list.Add(st);
        }

        return list;
    }

    private static string[] GetWords(string s)
    {
        string[] words = null;
        uint addr = Convert.ToUInt32(s.Substring(0, 8), 16);
        var v = s.Substring(8).Trim();
        uint value = Convert.ToUInt32(v, 16);
        int codetype = (int)(addr >> 24);

        if (codetype == 8)
        {
            words = new string[4];
            string line1 = $"{addr + 0:X8} {value & 0xffff:X4}";
            string line2 = $"{addr + 2:X8} {(value & 0xffff0000) >> 16:X4}";
            words[0] = "00000000";
            words[1] = line1.Substring(0, 8);
            words[2] = line1.Substring(9, 4).PadLeft(8, '0');
            words[3] = "00000000";
        }
        else if (codetype == 2)
        {
            words = new string[2];
            words[0] = s.Substring(0, 8);
            words[1] = v.Substring(0, v.Length);
        }

        return words;
    }

    private static string[] GetDecodeWords(string s)
    {
        string[] words = null;
        uint addr = Convert.ToUInt32(s.Substring(0, 8), 16);
        var v = s.Substring(8).Trim();
        uint value = Convert.ToUInt32(v, 16);
        int codetype = (int)(addr >> 28);

        if (codetype == 8)
        {
            words = new string[4];
            string line1 = $"{addr + 0:X8} {value & 0xffff:X4}";
            string line2 = $"{addr + 2:X8} {(value & 0xffff0000) >> 16:X4}";
            words[0] = "00000000";
            words[1] = line1.Substring(0, 8);
            words[2] = line1.Substring(9, 4).PadLeft(8, '0');
            words[3] = "00000000";
        }
        else if (codetype == 2)
        {
            words = new string[2];
            words[0] = s.Substring(0, 8);
            words[1] = v.Substring(0, v.Length);
        }

        return words;
    }
}