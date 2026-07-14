using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomRoutineMaker.Classes;

internal class PS1
{
    public static string Initialize()
    {
        return Common.InitiatizeMips(".psx", "0x80000000", "0x80000000", "0x80007600").ToString();
    }

    public static List<string> Run(byte[] data, uint addr, string asm)
    {
        List<string> list = [];
        StringBuilder sb = new();
        int linesnum = 0;

        for (int i = 0; i < data.Length / 4; i++)
        {
            int[] value = new int[1];
            Buffer.BlockCopy(data, i * 4, value, 0, 4);

            if (value[0] != 0 && (value[0] & 0xffff0000) != 0xe0000000)
            {
                var a = 0x80000000 | addr;
                sb.Append($"{a + 0:X8} {value[0] & 0xffff:X4}\r\n");
                sb.Append($"{a + 2:X8} {(value[0] & 0xffff0000) >> 16:X4}\r\n");
                linesnum++;
            }
            addr += 4;
        }

        (uint, uint) conditional = Common.GetConditional(asm);
        if (conditional != (0, 0))
        {
            sb.Insert(0, $"{conditional.Item1:X8} {conditional.Item2:X4}\r\n");
        }

        list.AddRange(sb.ToString().Split(Environment.NewLine));

        return list;
    }
}
