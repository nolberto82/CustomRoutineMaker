using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace CustomRoutineMaker.Classes
{
    internal class PSP
    {
        public static string Initialize(uint addr, uint routine)
        {
            StringBuilder sb = new();

            //sb.AppendLine(@".create ""out.bin"", 0x" + addr.ToString("X8").PadLeft(8, '0'));
            sb.AppendLine(@".psp");
            sb.AppendLine(@".create ""out.bin"", 0x00000000");
            sb.AppendLine(@".definelabel hook, 0x08800000");
            sb.AppendLine(@".definelabel function, 0x08801000");

            sb.AppendLine("");

            sb.AppendLine($".org\thook");
            sb.AppendLine($"j\tfunction");

            sb.AppendLine("");

            sb.AppendLine($"//ecode:");
            sb.AppendLine($"//.dw\t 0xe0000000");
            sb.AppendLine($"//evalue:");
            sb.AppendLine($"//.dw\t0x00000000\r\n");

            sb.AppendLine($".org\tfunction");

            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");

            sb.AppendLine($"j\thook+8");

            sb.AppendLine(".close");

            return sb.ToString();
        }

        public static List<string> Run(byte[] data, uint addr, string asm)
        {
            List<string> list = new();
            StringBuilder[] sb = new StringBuilder[2];
            sb[0] = new();
            sb[1] = new();
            int linesnum = 0;

            for (int i = 0; i < data.Length / 4; i++)
            {
                int[] value = new int[1];
                Buffer.BlockCopy(data, i * 4, value, 0, 4);

                if (value[0] != 0 && (value[0] & 0xffff0000) != 0xe0000000)
                {
                    sb[0].Append($"_L 0x{addr - 0x08800000 + 0x20000000:X8} 0x{value[0]:X8}\r\n");
                    sb[1].Append($"0x{addr - 0x08800000 + 0x20000000:X8} 0x{value[0]:X8}\r\n");
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

                string evaluestr = asm.Substring(index + 1, 8);
                if (evaluestr.Length < 8)
                {
                    list.Add("evalue less than 8 chars");
                    return list;
                }

                uint evalue = System.Convert.ToUInt32(evaluestr, 16);
                if (index > -1 && evalue > 0)
                {
                    eaddr = eaddr.Remove(2, 2).Insert(2, $"{linesnum:X2}");
                    sb[0].Insert(0, $"_L 0x{eaddr.ToUpper()} " +
                        $"0x{evalue:X8}\r\n");
                    sb[1].Insert(0, $"0x{eaddr.ToUpper()} " +
                        $"0x{evalue:X8}\r\n");
                }
            }

            list.AddRange(sb[0].ToString().Split(Environment.NewLine));
            list.AddRange(sb[1].ToString().Split(Environment.NewLine));

            return list;
        }

        public static List<string> ConvertToGHFormat(string[] lines)
        {
            List<string> list = new();
            List<string> pspaddrs = new();


            if (lines.Length == 0 || lines[0] == "" || lines == null)
                return list;

            bool cwcheat = lines[0].Contains("_L");

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith('\r') || lines[i].StartsWith("\r\n"))
                {
                    list.Add(" ");
                    continue;
                }

                if (lines[i].Substring(0, 2) == "_C")
                    list.Add(lines[i].Substring(3, lines[i].Length - 3).TrimStart().Replace("\r", ""));
                else
                {
                    lines[i] = lines[i].ToUpper().Replace("X", "x").Replace("\r", "").Replace("_L ", "");
                    var a = lines[i].TrimStart().Split(" ");
                    var v = Convert.ToInt32(a[0], 16);
                    if (((v >> 24) & 0xf) >= 8)
                    {
                        lines[i] = $"_L 0x{v - 0x08800000 | 0x20000000:X8} ";
                        lines[i] += $"0x{a[1]}";
                    }
                    else if (lines[i].Substring(0, 2) == "_L")
                        lines[i] = $"_L 0x{lines[i][..8]} 0x{lines[i][9..]}";

                    pspaddrs.Add($"0x{v - 0x08800000 | 0x20000000:X8} 0x{a[1]}");

                    if (cwcheat)
                        list.Add($"_L {lines[i]}");
                    else
                        list.Add($"{lines[i]}");
                }
            }

            list.Add("");
            list.AddRange(pspaddrs);

            return list;
        }
    }
}
