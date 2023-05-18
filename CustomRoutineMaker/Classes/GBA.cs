using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.LinkLabel;

namespace CustomRoutineMaker.Classes
{
    internal class GBA
    {
        public static string Initialize(uint addr, uint routine)
        {
            StringBuilder sb = new();

            //sb.AppendLine(@".create ""out.bin"", 0x" + addr.ToString("X8").PadLeft(8, '0'));
            sb.AppendLine(@".gba");
            sb.AppendLine(@".create ""out.bin"", 0x00000000");

            sb.AppendLine("\n");

            sb.AppendLine($".org\t0x08000000");
            sb.AppendLine($"ldr\tr0,=0x{addr:X8}\r\nmov\tpc,r0\r\n.pool\r\n");

            sb.AppendLine($".org\t0x{addr:X8}\r\n");

            sb.AppendLine($"push\t{{lr}}\r\n");

            sb.AppendLine($"pop\t{{pc}}\r\n");

            sb.AppendLine(".pool\r\n.close");

            return sb.ToString();
        }

        public static List<string> Run(byte[] data, uint addr, string asm)
        {
            AR34 AR34 = new();
            List<string> list = new();
            StringBuilder sb = new();
            int id = 0xc;
            bool is_arm = asm.IndexOf(".arm") > -1 ? true : false;

            for (int i = (int)addr; i < data.Length; i += 4)
            {
                int[] value = new int[1];
                Buffer.BlockCopy(data, i, value, 0, 4);

                if (value[0] != 0)
                {
                    string lines = $"{addr:X8} {value[0]:X8}";

                    int codetype = Convert.ToInt32(lines.Substring(0, 8), 16) >> 24;
                    string[] words = GetWords(lines);

                    List<string> res = AR34.Encrypt(lines, words, ref id);
                    foreach (string st in res)
                        list.Add(st);
                }

                addr += 4;
            }

            return list;
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
                    list.Add($"{s} - code too short");
                    return list;
                }

                string[] words = GetWords(s);

                List<string> res = AR34.Encrypt(s, words, ref id);
                foreach (string st in res)
                    list.Add(st);
            }

            return list;
        }

        private static string[] GetWords(string s)
        {
            string[] words;
            uint addr = Convert.ToUInt32(s.Substring(0, 8), 16);
            var v = s.Substring(8);
            if (v.Length == 4)
                v = v.PadLeft(8, '0');
            uint value = Convert.ToUInt32(v, 16);
            int codetype = (int)(addr >> 24);

            if (codetype == 8)
            {
                words = new string[8];
                string line1 = $"{addr + 0:X8} {value & 0xffff:X4}";
                string line2 = $"{addr + 2:X8} {(value & 0xffff0000) >> 16:X4}";
                words[0] = "00000000";
                words[1] = line1.Substring(0, 8);
                words[2] = line1.Substring(9, 4).PadLeft(8, '0');
                words[3] = "00000000";
                words[4] = "00000000";
                words[5] = line2.Substring(0, 8);
                words[6] = line2.Substring(9, 4).PadLeft(8, '0');
                words[7] = "00000000";
            }
            else if (codetype == 2) { }
            {
                words = new string[2];
                words[0] = s.Substring(0, 8);
                words[1] = v.Substring(0, v.Length);
            }

            return words;
        }
    }
}