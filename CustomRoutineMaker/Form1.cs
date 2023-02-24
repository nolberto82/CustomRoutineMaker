using CustomRoutineMaker.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CustomRoutineMaker
{
    public partial class Form1 : Form
    {
        string asm_filename;
        string addrtext;
        AR34 ar34;

        List<SystemType> systems;
        public Form1()
        {
            InitializeComponent();

            UpdateStatusBar();

            ar34 = new AR34();
            systems = new List<SystemType>();
            systems.Add(new SystemType("Playstation", "ps1", 0x80007600, 0x80000000));
            systems.Add(new SystemType("Playstation 2", "ps2", 0x200a0000, 0x20000000));
            systems.Add(new SystemType("Playstation Portable", "psp", 0x08801000, 0x00000000));
            systems.Add(new SystemType("Nintendo 64", "n64", 0x80400000, 0x80000000));
            systems.Add(new SystemType("Gameboy Advance", "gba", 0x0203ff00, 0x0203ff00));
            systems.Add(new SystemType("Nintendo DS", "nds", 0x02000000, 0x02000000));
            systems.Add(new SystemType("Generic", "gen", 0x00000000, 0x00000000));

            foreach (SystemType s in systems)
            {
                comboBox1.Items.Add(s.name);
                if (s.shortname == "gba" || s.shortname == "ps2" || s.shortname == "psp")
                    comboBox2.Items.Add(s.name);
            }

            btnAsm.Enabled = false;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
            textAsm.CharacterCasing = CharacterCasing.Lower;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("ASM"))
                Directory.CreateDirectory("ASM");

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "ASM Files (*.asm)|*.asm";
            ofd.InitialDirectory = Environment.CurrentDirectory + "\\ASM";

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

                foreach (SystemType s in systems)
                {
                    if (textAsm.Text.Contains(s.shortname))
                    {
                        index = textAsm.Text.IndexOf(s.shortname) + 1;
                        comboBox1.SelectedItem = s.name;
                    }
                }
            }
        }

        private void SaveFile()
        {
            if (!Directory.Exists("ASM"))
                Directory.CreateDirectory("ASM");

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "ASM Files (*.asm)|*.asm";
            sfd.InitialDirectory = Environment.CurrentDirectory + "/ASM";

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
                app.CreateNoWindow = true;

                using (Process process = Process.Start(app))
                {
                    using (StreamReader sr = process.StandardOutput)
                        textGS.Text = sr.ReadToEnd();

                    if (textGS.Text == "")
                    {
                        StringBuilder sb = new StringBuilder();
                        StringBuilder sb2 = new StringBuilder();
                        byte[] data = File.ReadAllBytes("out.bin");
                        string system = systems[comboBox1.SelectedIndex].shortname;
                        string name = systems[comboBox1.SelectedIndex].name;
                        uint addr = systems[comboBox1.SelectedIndex].origaddr;

                        //for (int i = 0; i < data.Length / 4; i++)
                        //{

                        if (system == "psx")
                        {
                            //sb.Append($"{addr:X8} {number[0] & 0xffff:X4}\r\n");
                            //sb.Append($"{addr + 2:X8} {number[0] >> 16:X4}\r\n");
                        }
                        if (system == "n64")
                        {

                            //var r = BitConverter.GetBytes(number[0]);
                            //addr |= 0x01000000;
                            //sb.Append($"{addr:X8} {r[0]:X2}{r[1]:X2}\r\n");
                            //sb.Append($"{addr + 2:X8} {r[2]:X2}{r[3]:X2}\r\n");
                        }
                        else if (system == "psp")
                            textGS.Text = string.Join(Environment.NewLine, PSP.Run(data, addr, textAsm.Text));
                        else if (system == "nds")
                        {
                            textGS.Text = string.Join(Environment.NewLine, NDS.Run(data, addr, textAsm.Text));
                        }
                        else if (name == "Generic")
                        {
                            //string convertedstr = "04000000 " + ((addr & 0xffffff)).ToString("X8");
                            //sb.AppendLine(convertedstr + " " + (number[0]).ToString("X8"));
                        }
                        else if (system == "ps2")
                        {
                            //sb2.AppendLine("patch=1,EE," + (addr + 0).ToString("X4") + ",extended," + (number[0]).ToString("X8"));
                            //sb.AppendLine((addr + 0).ToString("X4") + " " + (number[0]).ToString("X8"));
                        }

                        //addr += 4;
                        //}

                        if (name == "Generic")
                        {
                            textGS.Text = sb.ToString();
                        }
                        else
                        {
                            if (system == "gba")
                                textGS.Text += CreateGBACodes(data);
                            else if (system == "psp" || system == "ps2")
                            {
                                if (system == "ps2")
                                    textGS.Text += "//PCSX2\r\n";


                                textGS.Text += sb2.ToString();
                                textGS.Text += "\r\n";
                                textGS.Text += sb.ToString();
                            }
                            else
                                textGS.Text = sb.ToString();
                        }
                    }
                }
                if (File.Exists("out.bin"))
                    File.Delete("out.bin");
            }
            else
            {
                MessageBox.Show("No ASM File loaded", "Error");
            }
        }

        private StringBuilder CreateGBACodes(byte[] data)
        {
            uint addr = 0;//= systems[comboBox1.SelectedIndex].origaddr;
            StringBuilder sb = new StringBuilder();
            int id = 0xc;

            for (int i = 0; i < data.Length / 4; i++)
            {
                int[] number = new int[1];
                Buffer.BlockCopy(data, i * 4, number, 0, 4);
                if (number[0] != 0)
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
                            sb.AppendLine(st);
                    }
                }
                addr += 4;
            }
            return sb;
        }

        private void textAsm_Click(object sender, EventArgs e)
        {
            UpdateStatusBar();
        }

        private void UpdateStatusBar(string selectedlinesnum = "")
        {
            statusAsm.Text = $"Line Number: {textAsm.GetLineFromCharIndex(textAsm.SelectionStart)} {selectedlinesnum}";
            statusBar.Invalidate();
            statusBar.Refresh();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            uint addr = systems[comboBox1.SelectedIndex].origaddr;
            uint routine = systems[comboBox1.SelectedIndex].routine;
            string system = systems[comboBox1.SelectedIndex].shortname;

            if (system == "psp")
                textAsm.Text = PSP.Initialize(addr, routine);
            else if (system == "nds")
                textAsm.Text = NDS.Initialize(addr, routine);
            //addrtext = addr.ToString("X8");
            //sb.AppendLine("." + system);

            ////sb.AppendLine(@".create ""out.bin"", 0x" + addr.ToString("X8").PadLeft(8, '0'));
            //sb.AppendLine(@".create ""out.bin"", 0x00000000");

            //sb.AppendLine("\n");

            //if (system == "gba")
            //{
            //    sb.AppendLine(@".org" + "\t" + "0x" + addrtext.PadLeft(8, '0'));
            //    sb.AppendLine(@"ldr" + "\t" + "r0,=0x" + routine.ToString("X8").PadLeft(8, '0'));
            //    sb.AppendLine(@"bx" + "\t" + "r0");
            //    sb.AppendLine(@".pool");
            //}
            //else if (system == "nds")
            //{
            //    sb.AppendLine(@".org" + "\t" + "0x" + addr.ToString("X8").PadLeft(8, '0'));
            //    sb.AppendLine(@"bl" + "\t" + "0x" + routine.ToString("X8").PadLeft(8, '0'));
            //    //sb.AppendLine(@".pool");
            //}
            //else
            //{
            //    sb.AppendLine(@".org" + "\t" + "0x" + addrtext.PadRight(8, '0'));
            //    sb.AppendLine(@"j" + "\t" + "0x" + routine.ToString("X8").PadLeft(8, '0'));
            //}

            //sb.AppendLine("\n");

            //sb.AppendLine(@".org" + "\t" + "0x" + routine.ToString("X8").PadLeft(8, '0'));

            //sb.AppendLine("\n");
            //sb.AppendLine("\n");
            //sb.AppendLine("\n");

            //if (system == "gba")
            //    sb.AppendLine(@"bx" + "\t" + "r0");
            //else if (system == "nds")
            //    sb.AppendLine(@"bx" + "\t" + "r14");
            //else
            //    sb.AppendLine(@"j" + "\t" + "0x" + (routine + 8).ToString("X8").PadLeft(8, '0'));

            //if (system == "gba" || system == "nds")
            //    sb.AppendLine(@".pool");

            //sb.AppendLine(".close");

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
            string system = systems.Find(s => s.name == (string)comboBox2.SelectedItem).shortname;
            string[] lines = textPS2.Lines;
            int id = 0xc;
            textPnach.Text = "";

            //foreach (string s in lines)
            //{
            //if (s == "" || li != null || s.Length < 16)

            if (system == "gba")
            {
                //string[] words = new string[4] { "", "", "", "" };

                //words[0] = "00000000";
                //words[1] = s.Substring(0, 8);
                //words[2] = s.Substring(9, 4).PadLeft(8, '0');
                //words[3] = "00000000";

                //List<string> res = ar34.Encrypt(s, words, 8, ref id);
                //foreach (string st in res)
                //    textPnach.Text += st + Environment.NewLine;
            }
            else if (system == "ps2")
            {
                //string ns = s;
                //if (s.Length <= 16)
                //    ns = ns.PadRight(17, '0').Replace(" ", "");
                //textPnach.Text += $"patch=1,EE,{ns.Substring(0, 8):X4},extended,{ns.Substring(9, 8):X8}\n";
            }
            else if (system == "psp")
                textPnach.Text = string.Join(Environment.NewLine,
                    PSP.ConvertToGHFormat(textPS2.Text.Split(new[] { '\n' },
                    StringSplitOptions.RemoveEmptyEntries).ToArray()));
        }

        private void textGS_MouseDown(object sender, MouseEventArgs e)
        {
            var count = textGS.SelectedText.Split(Environment.NewLine).Length;

            if (count > 1)
                UpdateStatusBar($"{count:X2}");
        }

        private void textGS_MouseMove(object sender, MouseEventArgs e)
        {
            var count = textGS.SelectedText.Split(Environment.NewLine).Length;

            if (count > 1)
                UpdateStatusBar($"- Selection: {count:X2}");
        }
    }
}
