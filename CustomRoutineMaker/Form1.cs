using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace CustomRoutineMaker
{
    public partial class Form1 : Form
    {
        string asm_filename;
        string addrtext;
        AR34 ar34;

        string[] systems_ext = { ".psx", ".ps2", ".psp", ".gba" };
        public Form1()
        {
            InitializeComponent();

            UpdateStatusBar();

            ar34 = new AR34();

            btnAsm.Enabled = false;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            textAsm.CharacterCasing = CharacterCasing.Lower;
            textPS2.CharacterCasing = CharacterCasing.Upper;
            textPnach.CharacterCasing = CharacterCasing.Upper;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "ASM Files (*.asm)|*.asm";
            ofd.InitialDirectory = Environment.CurrentDirectory;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                asm_filename = ofd.FileName;
                textAsm.Text = File.ReadAllText(ofd.FileName);
                string fileout = "out.bin";
                int index = -1;
                if (textAsm.Text.Contains(fileout))
                {
                    index = textAsm.Text.IndexOf(fileout);

                    while (textAsm.Text[index] != 'x')
                        index++;

                    addrtext = textAsm.Text.Substring(index + 1, 8);
                }
                else
                    return;

                foreach (string s in systems_ext)
                {
                    if (textAsm.Text.Contains(s))
                    {
                        index = textAsm.Text.IndexOf(s) + 1;
                        comboBox1.SelectedItem = s.Substring(index, 3).ToUpper();
                    }
                }
            }
        }

        private void SaveFile()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "ASM Files (*.asm)|*.asm";
            sfd.InitialDirectory = Environment.CurrentDirectory;

            if (asm_filename == "" || asm_filename == null)
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    asm_filename = sfd.FileName;
                    File.WriteAllText(sfd.FileName, textAsm.Text);

                }
            }
            else
            {
                File.WriteAllText(asm_filename, textAsm.Text);
            }
        }

        private void textAsm_TextChanged(object sender, EventArgs e)
        {
            UpdateStatusBar();
        }

        private void btnAssemble_Click(object sender, EventArgs e)
        {
            if (textAsm.Text != "")
            {
                SaveFile();

                ProcessStartInfo app = new ProcessStartInfo();
                app.WorkingDirectory = Environment.CurrentDirectory;
                app.FileName = "armips.exe";
                app.Arguments = asm_filename;
                app.UseShellExecute = false;
                app.RedirectStandardOutput = true;

                using (Process process = Process.Start(app))
                {
                    using (StreamReader sr = process.StandardOutput)
                    {
                        textGS.Text = sr.ReadToEnd();
                    }

                    if (textGS.Text == "")
                    {
                        StringBuilder sb = new StringBuilder();
                        StringBuilder sb2 = new StringBuilder();
                        byte[] temp = File.ReadAllBytes("out.bin");
                        uint addr = Convert.ToUInt32(addrtext, 16);
                        int id = 0xc;

                        for (int i = 0; i < temp.Length / 4; i++)
                        {
                            int[] number = new int[1];
                            Buffer.BlockCopy(temp, i * 4, number, 0, 4);
                            if (number[0] != 0)
                            {
                                if (comboBox1.Text == "PS1" || comboBox1.Text == "GBA")
                                {
                                    sb.AppendLine((addr + 0).ToString("X4").PadLeft(8, '0') + " " + (number[0] & 0xffff).ToString("X4"));
                                    sb.AppendLine((addr + 2).ToString("X4").PadLeft(8, '0') + " " + ((number[0] & 0xffff0000) >> 16).ToString("X4"));

                                    if (comboBox1.Text == "GBA")
                                    {
                                        string lines = (addr + 0).ToString("X4").PadLeft(8, '0') + " " + (number[0]).ToString("X8");
                                        if (lines != "")
                                        {
                                            int addrt = Convert.ToInt32(lines.Substring(0, 8), 16) >> 24;
                                            string[] words;

                                            if (addrt == 8)
                                            {
                                                words = new string[8];
                                                string line1 = (addr + 0).ToString("X4").PadLeft(8, '0') + " " + (number[0] & 0xffff).ToString("X4");
                                                string line2 = (addr + 2).ToString("X4").PadLeft(8, '0') + " " + ((number[0] & 0xffff0000) >> 16).ToString("X4");
                                                words[0] = "00000000";
                                                words[1] = line1.Substring(0, 8);
                                                words[2] = line1.Substring(9, 4).PadLeft(8, '0');
                                                words[3] = "00000000";
                                                words[4] = "00000000";
                                                words[5] = line2.Substring(0, 8);
                                                words[6] = line2.Substring(9, 4).PadLeft(8, '0');
                                                words[7] = "00000000";
                                            }
                                            else
                                            {
                                                words = new string[2];
                                                words[0] = lines.Substring(0, 8);
                                                words[1] = lines.Substring(9, 8);
                                            }

                                            List<string> res = ar34.Encrypt(lines, words, addrt, ref id);
                                            foreach (string st in res)
                                                sb2.AppendLine(st);
                                        }
                                    }
                                }
                                else if (comboBox1.Text == "PSP")
                                {
                                    string convertedstr = (addr + 0).ToString("X4").Replace("88", "0x200");
                                    sb.AppendLine(convertedstr + " 0x" + (number[0]).ToString("X8"));
                                    sb2.AppendLine("_L " + convertedstr + " 0x" + (number[0]).ToString("X8"));
                                }
                                else
                                {
                                    sb.AppendLine((addr + 0).ToString("X4") + " " + (number[0]).ToString("X8"));
                                    sb2.AppendLine("patch=1,EE," + (addr + 0).ToString("X4") + ",extended," + (number[0]).ToString("X8"));
                                }

                            }
                            addr += 4;
                        }

                        if (comboBox1.Text == "PS2")
                        {
                            sb.AppendLine("");
                            sb.AppendLine("//PCSX2 pnatch");
                        }

                        if (comboBox1.Text == "PSP")
                        {
                            sb.AppendLine("");
                            sb.AppendLine("//CWCheat version");
                        }

                        if (comboBox1.Text != "GBA")
                            textGS.Text = sb.ToString();

                        if (comboBox1.Text == "PSP" || comboBox1.Text == "PS2" || comboBox1.Text == "GBA")
                            textGS.Text += sb2.ToString();

                    }
                }
            }
            else
            {
                MessageBox.Show("No ASM File loaded", "Error");
            }
        }

        private void textAsm_Click(object sender, EventArgs e)
        {
            UpdateStatusBar();
        }

        private void UpdateStatusBar()
        {
            statusAsm.Text = "Line Number: " + textAsm.GetLineFromCharIndex(textAsm.SelectionStart).ToString();
            statusBar.Invalidate();
            statusBar.Refresh();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            uint addr = 0x00000000;
            uint routine = 0x00000000;

            addrtext = addr.ToString("X1");

            switch (comboBox1.Text)
            {
                case "PS1":
                    sb.AppendLine(".psx");
                    break;
                case "PS2":
                    sb.AppendLine(".ps2");
                    routine = 0x200a0000;
                    break;
                case "PSP":
                    sb.AppendLine(".psp");
                    routine = 0x08801000;
                    break;
                case "GBA":
                    sb.AppendLine(".gba");
                    sb.AppendLine(".thumb");
                    routine = 0x08000000;
                    break;
            }

            sb.AppendLine(@".create ""out.bin"", 0x" + addrtext);
             
            sb.AppendLine("\n");

            sb.AppendLine(@".org" + "\t" + "0x" + routine.ToString("X1").PadLeft(8, '0'));

            if (comboBox1.Text == "GBA")
            {
                sb.AppendLine(@"ldr" + "\t" + "r0,=0x" + addrtext);
                sb.AppendLine(@"mov" + "\t" + "pc,r0");
                sb.AppendLine(@".pool");
            }
            else
                sb.AppendLine(@"j" + "\t" + "0x" + addrtext);

            sb.AppendLine("\n");

            sb.AppendLine(@".org" + "\t" + "0x" + addrtext);

            sb.AppendLine("\n");
            sb.AppendLine("\n");
            sb.AppendLine("\n");

            if (comboBox1.Text == "GBA")
                sb.AppendLine(@"mov" + "\t" + "pc,r7");
            else
                sb.AppendLine(@"j" + "\t" + "0x" + (routine + 8).ToString("X1").PadLeft(8, '0'));

            if (comboBox1.Text == "GBA")
                sb.AppendLine(@".pool");

            sb.AppendLine(".close");
            textAsm.Text = sb.ToString();
            asm_filename = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists("armips.exe"))
            {
                MessageBox.Show("You need the armips assembler", "Error");
                this.Close();
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            string[] lines = textPS2.Lines;
            int id = 0xc;
            textPnach.Text = "";

            foreach (string s in lines)
            {
                if (s == "" || s == null)
                    continue;
                string[] split = s.Split(' ');
                if (split.Length == 1)
                {
                    textPnach.Text += $"patch=1,EE,{split[0]:X4},extended, value missing";
                    break;
                }

                if (comboBox2.Text == "GBA")
                {
                    string[] words = new string[4];
                    words[0] = "00000000";
                    words[1] = s.Substring(0, 8);
                    words[2] = s.Substring(9, 4).PadLeft(8, '0');
                    words[3] = "00000000";

                    List<string> res = ar34.Encrypt(s, words, 8, ref id);
                    foreach (string st in res)
                        textPnach.Text += st + Environment.NewLine;
                }
                else
                {
                    textPnach.Text += $"patch=1,EE,{split[0]:X4},extended,{split[1]:X8}\n";
                }

            }
        }
    }
}
