using System;
using System.Collections.Generic;
using System.Text;

namespace CustomRoutineMaker.Classes;

internal class Common
{
    public static string InitiatizeMips(string name, string create, string hook, string function)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($@"{name}");
        sb.AppendLine($@".create ""out.bin"", {create}");
        sb.AppendLine($@".definelabel hook, {hook}");
        sb.AppendLine($@".definelabel function, {function}");

        sb.AppendLine("");

        sb.AppendLine($".org\thook");
        sb.AppendLine($"j\tfunction\r\nreturn:");

        sb.AppendLine("");

        sb.AppendLine($"//ecode: d0000000 00000000");
        sb.AppendLine("");

        sb.AppendLine($".org\tfunction");
        sb.AppendLine($"j\treturn+4");

        sb.AppendLine(".close");
        return sb.ToString();
    }

    public static (uint,uint) GetConditional(string asm)
    {
        string[] lines = asm.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < lines.Length; i++)
        {
            string[] line = lines[i].Trim().Split([":", " "], StringSplitOptions.RemoveEmptyEntries);
            if (line.Length == 0) continue;
            if (line[0].StartsWith("//ecode"))
            {
                uint eaddr = Convert.ToUInt32(line[1], 16);
                uint evalue = Convert.ToUInt32(line[2], 16);

                if ((eaddr & 0xfffffff) != 0)
                {
                    return (eaddr, evalue);
                }
            }
        }
        return (0,0);
    }
}
