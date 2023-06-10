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
            systems.Add(new SystemType("Nintendo Switch", "nds", 0x00000000, 0x00000000));
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
            sfd.InitialDirectory = Environment.CurrentDirectory + "\\ASM";

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
                        uint addr = 0;

                        int pos = textAsm.Text.LastIndexOf(".definelabel");

                        if (pos > -1)
                        {
                            while (textAsm.Text[pos] != '0')
                                pos++;

                            var str = textAsm.Text.Substring(pos).Trim().Split("\r");
                            if (str.Length > 0)
                                addr = Convert.ToUInt32(str[0], 16);
                        }

                        if (system == "psx")
                        {
                        }
                        else if (system == "n64")
                        {
                        }
                        else if (system == "nds")
                            textGS.Text = string.Join(Environment.NewLine, NDS.Run(data, addr, textAsm.Text));
                        else if (system == "nds" && name == "Nintendo Switch")
                            textGS.Text = string.Join(Environment.NewLine, SWI.Run(data, addr, textAsm.Text));
                        else if (system == "psp")
                            textGS.Text = string.Join(Environment.NewLine, PSP.Run(data, addr, textAsm.Text));
                        else if (system == "ps2")
                            textGS.Text = string.Join(Environment.NewLine, PS2.Run(data, addr, textAsm.Text));
                        else if (system == "gba")
                        {
                            (List<string> ARcodes, List<string> Rawcodes, List<string> Bytes) = GBA.Run(data, addr, textAsm.Text);
                            textGS.Text = string.Join(Environment.NewLine, ARcodes);
                            textGS.Text += "\r\n\r\n";
                            int c = 0;
                            foreach (var s in Rawcodes)
                            {
                                c++;
                                if (c == Rawcodes.Count)
                                    textGS.Text += $"{s}";
                                else
                                    textGS.Text += $"{s}+\r\n";
                            }

                            textGS.Text += "\r\n\r\n";
                            foreach (var s in Bytes)
                                textGS.Text += s;

                        }
                        else if (system == "nds")
                            textGS.Text = string.Join(Environment.NewLine, NDS.Run(data, addr, textAsm.Text));


                        //else if (system == "ps2")
                        //{
                        //textGS.Text = string.Join(Environment.NewLine, PS2.Run(data, addr, textAsm.Text));
                        //sb2.AppendLine("patch=1,EE," + (addr + 0).ToString("X4") + ",extended," + (number[0]).ToString("X8"));
                        //sb.AppendLine((addr + 0).ToString("X4") + " " + (number[0]).ToString("X8"));
                        //}

                        //addr += 4;
                        //}
                        //if (system == "gba")
                        //textGS.Text += CreateGBACodes(data);
                        //else if (system == "psp" || system == "ps2")
                        //{
                        //textGS.Text += sb2.ToString();
                        //textGS.Text += "\r\n";
                        //textGS.Text += sb.ToString();
                        // }
                        //else
                        //    textGS.Text = sb.ToString();
                    }
                }

                if (File.Exists("out.bin"))
                    File.Delete("out.bin");
            }

            else
                MessageBox.Show("No ASM File loaded", "Error");
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
            else if (system == "ps2")
                textAsm.Text = PS2.Initialize(addr, routine);
            else if (system == "gba")
                textAsm.Text = GBA.Initialize(addr, routine);
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
            textPnach.Text = "";

            if (system == "gba")
            {
                var codes = GBA.ConvertToARFormat(textPS2.Text.Split(new[] { '\n' }));
                if (codes.Count == 0)
                {
                    codes = GBA.ConvertToRawFormat(textPS2.Text.Split(new[] { '\n' }));

                    for (int i = 0; i < codes.Count(); i++)
                    {
                        var s = codes[i].Replace(" ", "");
                        int c = Convert.ToInt32(s.Substring(0, 8), 16) >> 24;
                        int addr = 0;
                        int val = 0;
                        if (c == 0)
                        {
                            addr = (Convert.ToInt32(s.Substring(8, 8), 16) & 0xffffff) << 1 | 0x08000000;
                            i++;
                            s = codes[i].Replace(" ", "");
                            val = Convert.ToInt32(s.Substring(0, 8), 16);
                            textPnach.Text += $"{addr:X8} {val:X8}\r\n";
                        }
                        else
                        {
                            var a = Convert.ToInt32(s.Substring(0, 8), 16);
                            addr = (a & 0xf00000) << 4 | (a & 0xfffff);
                            val = Convert.ToInt32(s.Substring(8, 8), 16);
                            textPnach.Text += $"{addr:X8} {val:X8}\r\n";
                        }
                    }
                }
                else
                    textPnach.Text = string.Join(Environment.NewLine, codes);
            }
            else if (system == "ps2")
                textPnach.Text = string.Join(Environment.NewLine,
                    PS2.ConvertToPnachFormat(textPS2.Text.Split(new[] { '\n' },
                    StringSplitOptions.RemoveEmptyEntries).ToArray()));
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
