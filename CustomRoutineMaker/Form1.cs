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
        AR34 ar34;
        public Form1()
        {
            InitializeComponent();

            UpdateButtonState();

            UpdateStatusBar();

            ar34 = new AR34();

            btnAsm.Enabled = false;
            comboBox1.SelectedIndex = 0;
            textAddress.CharacterCasing = CharacterCasing.Upper;
        }

        private void UpdateButtonState()
        {
            if (textAddress.Text.Length == 0)
            {
                btnNew.Enabled = false;
            }
            else
            {
                btnNew.Enabled = true;
            }
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
                int index = textAsm.Text.IndexOf("out.bin") + 12;
                textAddress.Text = textAsm.Text.Substring(index, 8);
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
            UpdateButtonState();
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
                        uint addr = Convert.ToUInt32(textAddress.Text, 16);

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

                                            List<string> res = ar34.Encrypt(lines, words, addrt);
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

            switch (comboBox1.Text)
            {
                case "PS1":
                    sb.AppendLine(".psx");
                    break;
                case "PS2":
                    sb.AppendLine(".ps2");
                    break;
                case "PSP":
                    sb.AppendLine(".psp");
                    break;
                case "GBA":
                    sb.AppendLine(".gba");
                    sb.AppendLine(".thumb");
                    break;
            }

            sb.AppendLine(@".create ""out.bin"", 0x" + textAddress.Text);

            sb.AppendLine("\n");

            sb.AppendLine(@".org" + "\t" + "0x");

            if (comboBox1.Text == "GBA")
            {
                sb.AppendLine(@"ldr" + "\t" + "r0,=0x" + textAddress.Text);
                sb.AppendLine(@"mov" + "\t" + "pc,r0");
                sb.AppendLine(@".pool");
            }
            else
                sb.AppendLine(@"j" + "\t" + "0x" + textAddress.Text);

            sb.AppendLine("\n");

            sb.AppendLine(@".org" + "\t" + "0x" + textAddress.Text);

            sb.AppendLine("\n");
            sb.AppendLine("\n");
            sb.AppendLine("\n");

            if (comboBox1.Text == "GBA")
                sb.AppendLine(@"mov" + "\t" + "pc,r7");
            else
                sb.AppendLine(@"j" + "\t" + "0x");

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

        //private void textAddress_TextChanged(object sender, EventArgs e)
        //{
        //    if (textAddress.Text.Length == 8)
        //    {
        //        switch (textAddress.Text.Substring(0, 3))
        //        {
        //            case "80":
        //                comboBox1.Text = "PS1";
        //                break;
        //            case "20":
        //                comboBox1.Text = "PS2";
        //                break;
        //            case "088":
        //                comboBox1.Text = "PSP";
        //                break;
        //            case "08":
        //                comboBox1.Text = "GBA";
        //                break;
        //        }
        //    }

        //    UpdateButtonState();
        //}

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            AR34 ar = new AR34();
            string[] lines = textPS2.Lines;

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

                if (comboBox1.Text == "GBA")
                {
                    string[] words = new string[4];
                    words[0] = "00000000";
                    words[1] = textPS2.Text.Substring(0, 8);
                    words[2] = textPS2.Text.Substring(9, 4).PadLeft(8, '0');
                    words[3] = "00000000";
                    List<string> res = ar.Encrypt(s, words, 8);
                    foreach (string st in res)
                        textPnach.Text += st + Environment.NewLine;
                }
                else
                {
                    textPnach.Text += $"patch=1,EE,{split[0]:X4},extended,{split[1]:X8}\n";
                }

            }
        }

        private void textAddress_TextChanged(object sender, EventArgs e)
        {
            UpdateButtonState();
        }
    }
}
