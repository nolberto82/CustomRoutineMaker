using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PS1AsmToGameshark
{
    public partial class Form1 : Form
    {
        string asm_filename;

        public Form1()
        {
            InitializeComponent();

            UpdateButtonState();

            UpdateStatusBar();

            btnAsm.Enabled = false;
            comboBox1.SelectedIndex = 0;
            textAddress.CharacterCasing = CharacterCasing.Upper;
        }

        private void UpdateButtonState()
        {
            if (textAddress.Text.Length < 8)
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
                                if (comboBox1.Text == "PS1")
                                {
                                    sb.AppendLine((addr + 0).ToString("X4") + " " + (number[0] & 0xffff).ToString("X4"));
                                    sb.AppendLine((addr + 2).ToString("X4") + " " + ((number[0] & 0xffff0000) >> 16).ToString("X4"));
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

                        textGS.Text = sb.ToString();

                        if (comboBox1.Text == "PSP" || comboBox1.Text == "PS2")
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
            }

            sb.AppendLine(@".create ""out.bin"", 0x" + textAddress.Text);

            sb.AppendLine("\n");

            sb.AppendLine(@".org" + "\t" + "0x");
            sb.AppendLine(@"j" + "\t" + "0x" + textAddress.Text);

            sb.AppendLine("\n");

            sb.AppendLine(@".org" + "\t" + "0x" + textAddress.Text);

            sb.AppendLine("\n");
            sb.AppendLine("\n");
            sb.AppendLine("\n");

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

        private void textAddress_TextChanged(object sender, EventArgs e)
        {
            if (textAddress.Text.Length == 8)
            {
                switch (textAddress.Text.Substring(0, 2))
                {
                    case "80":
                        comboBox1.Text = "PS1";
                        break;
                    case "20":
                        comboBox1.Text = "PS2";
                        break;
                    case "08":
                        comboBox1.Text = "PSP";
                        break;
                }
            }

            UpdateButtonState();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
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

                textPnach.Text += $"patch=1,EE,{split[0]:X4},extended,{split[1]:X8}\n";
            }
        }
    }
}
