﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomRoutineMaker.Classes;

internal class SWI
{
    public static string Initialize(uint addr, uint routine, string arch)
    {
        StringBuilder sb = new();

        sb.AppendLine($"//.swi");

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
        List<string> list = [];
        StringBuilder sb = new();

        int size = 4;
        for (int i = 0; i < data.Length; i += size)
        {
            if (i + 4 > data.Length)
                break;

            int[] lo = new int[1];

            Buffer.BlockCopy(data, i, lo, 0, size);

            if (lo[0] != 0)
            {
                sb.Append($"04000000 {addr & 0xfffffff:X8} " +
                    $"{lo[0]:X8}\n");
            }

            addr += (uint)size;
        }

        bool isthumb = asm.IndexOf(".thumb") > -1 ? true : false;
        var temp = sb.ToString().Split(['\n'], StringSplitOptions.RemoveEmptyEntries).ToList();
        List<string> bl = [];
        for (int k = temp.Count - 1; k >= 0; k--)
        {
            var bra = Convert.ToUInt32(temp[k].Substring(18, 8), 16) >> 24;
            if ((!is64 && (bra == 0xeb) || (bra == 0x14 || bra == 0x15 || bra == 0x94 || (bra == 0x95))))
            {
                bl.Add(temp[k]);
                temp.RemoveAt(k);
            }
        }

        temp.Add("");

        List<string> lines = [];
        List<string> values = [];
        List<string> bytes = [];

        for (int k = 0; k < bl.Count; k++)
        {
            bl[k] = bl[k].Replace(" ", "");
            lines.Add($"04000000 {bl[k].Substring(8, 8)} {bl[k].Substring(16, 8)}");
        }

        for (int i = 0; i < temp.Count; i += 2)
        {
            if (temp[i] == "")
                continue;

            var v1 = temp[i + 0].Replace(" ", "");
            var v2 = temp[i + 1].Replace(" ", "");
            if (v2 != string.Empty)
                lines.Add($"08000000 {v1.Substring(8, 8)} {v2.Substring(16, 8)} {v1.Substring(16, 8)}");
            else
                lines.Add($"04000000 {v1.Substring(8, 8)} {v1.Substring(16, 8)}");
        }

        lines.Add("");
        lines.Add("base=0x");

        var idalines = lines.ToArray();
        for (int k = 0; k < bl.Count; k++)
            lines.Add($"patch_dword(base+0x0{bl[k].Substring(9, 7)},0x{bl[k].Substring(16, 8)})");


        for (int i = bl.Count; i < idalines.Length; i++)
        {
            if (idalines[i] == "" || idalines[i] == "base=0x")
                continue;

            var l = idalines[i].Replace(" ", "");

            if (l.Length == 32)
            {
                lines.Add($"patch_dword(base+0x0{l.Substring(9, 7)},0x{l.Substring(24, 8)})");
                var a = Convert.ToInt32(l.Substring(8, 8), 16) + 4;
                lines.Add($"patch_dword(base+0x0{a.ToString("X8").Substring(1, 7)},0x{l.Substring(16, 8)})");
            }
            else
                lines.Add($"patch_dword(base+0x0{l.Substring(9, 7)},0x{l.Substring(16, 8)})");

        }

        lines.Add("");

        var gdblines = lines.ToArray();
        for (int k = 0; k < bl.Count; k++)
            lines.Add($"set *($g+0x{bl[k].Substring(8, 8)})=0x{bl[k].Substring(16, 8)}");


        for (int i = bl.Count; i < gdblines.Length; i++)
        {
            if (gdblines[i] == "" || gdblines[i] == "base=0x")
                continue;

            var l = gdblines[i + 0].Replace(" ", "");

            if (l.Length == 32)
                lines.Add($"set *(char**)($g+0x{l.Substring(8, 8)})=0x{l.Substring(16, 16)}");
            else
                lines.Add($"set *($g+0x{l.Substring(8, 8)})=0x{l.Substring(16, 8)}");

        }
        return lines;
    }
}
